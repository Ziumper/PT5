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

        public List<UserDto> GetUsers()
        {
            var usersInDb = dbContext.Users.ToList();
            return dtoConverter.CreateUserDto(usersInDb);
        }

        public void CreateUser(UserDto dto)
        {
            if (dto.Login == null || dto.Password == null || dto.Ip == null || dto.Id == null) return;
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

        public int GetLastId()
        {
            var result = dbContext.Users.OrderBy(u => u.Id).LastOrDefault();
            if(result != null)
            {
                return result.Id;
            }

            return 1;
        }

        public bool LoginUser(string loginOfUser)
        {
            if (IsUserExisting(loginOfUser) == false) return false; 
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

            var user = dbContext.Users.FirstOrDefault(us => us.Login == HostUserName && us.IsLogged);

            return user != null;
        }

        private bool IsUserLogedInAsAdministratorInWindows()
        {
            WindowsIdentity windowsIdentity = WindowsIdentity.GetCurrent();
            IPrincipal principal = new WindowsPrincipal(windowsIdentity);

            bool IsAuthenticated = windowsIdentity.IsAuthenticated;
            //bool IsAdmin = principal.IsInRole("BUILTIN\\" + "Administrators");

            return IsAuthenticated;
        }

        public bool IsUserExisting(string loginOfUser)
        {
            if(loginOfUser == null) return false;
            var user = dbContext.Users.Select(u => u.Login == loginOfUser && u.IsActive).FirstOrDefault();
            if (user != false) return true;

            return false;
        }

        public bool IsUserCanBeFound(string loginOfUser)
        {
            if(loginOfUser == null) return false;
            var user = dbContext.Users.Select(u => u.Login == loginOfUser).FirstOrDefault();
            return user;
        }

        public bool UpdateUser(UserDto registerUserDto)
        {
            if(IsUserCanBeFound(registerUserDto.Login) == false) return false;
            if(registerUserDto.Password == null || registerUserDto.Login == null || registerUserDto.Ip == null) return false;
            var user = dbContext.Users.Where(u => u.Login == registerUserDto.Login).Single();
            if (user != null)
            {
                user.Password = registerUserDto.Password;
                user.IsActive = registerUserDto.IsActive;
                user.Login = registerUserDto.Login;
                user.Ip = registerUserDto.Ip;
                dbContext.SaveChanges();
                return true;
            }



            return false;
        }

        public void UpdateOrCreate(UserDto updateOrCreate)
        {
            if(IsUserCanBeFound(updateOrCreate.Login))
                UpdateUser(updateOrCreate);
             else
                CreateUser(updateOrCreate);
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}