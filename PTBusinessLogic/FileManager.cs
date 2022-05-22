using PTBusinessLogic.Model;
using PTBusinessLogic.Service;
using PTDatabase;
using PTDatabase.Models;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;

namespace PTBusinessLogic
{
    public class FileManager : IDisposable
    {
        public static string HostUserName => Environment.GetEnvironmentVariable("UserName");
        private SqliteDbContext dbContext;
        private DtoConverter dtoConverter;

        public FileManager()
        {
            dbContext = new SqliteDbContext();
            dbContext.Database.EnsureCreated();
            dtoConverter = new DtoConverter();
        }

        public void CreateUser(RegisterUserDto dto)
        {
            User user = dtoConverter.CreateUserEntity(dto);
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
        }

        public bool IsUserCredentialsCorrect(string loginOfUser, string password)
        {
            var result = dbContext.Users.Select(u => u.Login == loginOfUser && u.Password == password).FirstOrDefault();
            if (result == false) return false;

            return true;
        }

        public bool LoginUser(string loginOfUser)
        {
            var user = dbContext.Users.Where(u => u.Login == loginOfUser).Single();

            //if (user == false) return false;
            user.IsLogged = true;
            user.UpdatedTime = DateTime.Now;
            
            dbContext.Users.Update(user);
            dbContext.SaveChanges(true);

            return true;
        }

        public bool FindIfCurrentUserPresent()
        {
            var user = dbContext.Users.Select(us => us.Login == HostUserName).FirstOrDefault();

            if (user == false) return false;

            return true;
        }

        public bool CheckIsUserLogedInCorrect()
        {
            if (!IsUserLogedInAsAdministratorInWindows()) return false;

            var user = dbContext.Users.Where(us => us.Login == HostUserName && us.IsLogged).Single();

            return user != null;
        }

        private bool IsUserLogedInAsAdministratorInWindows()
        {
            WindowsIdentity windowsIdentity = WindowsIdentity.GetCurrent();
            IPrincipal principal = new WindowsPrincipal(windowsIdentity);

            bool IsAuthenticated = windowsIdentity.IsAuthenticated;
            bool IsAdmin = principal.IsInRole("BUILTIN\\" + "Administrators");

            return IsAdmin && IsAuthenticated;
        }

        public bool IsUserExisting(string loginOfUser)
        {
            var user = dbContext.Users.Where(u => u.Login == loginOfUser).Single();
            if(user != null) return true;

            return false;
        }

        public bool UpdateUser(RegisterUserDto registerUserDto)
        {
            var user = dbContext.Users.Where(u => u.Login == registerUserDto.Login).Single();
            if (user != null)
            {
                user.Password = registerUserDto.Password;
                user.UpdatedTime = DateTime.Now;

                dbContext.SaveChanges();
                return true;
            }



            return false;
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}