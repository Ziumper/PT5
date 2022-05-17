using PTBusinessLogic.Model;
using PTBusinessLogic.Service;
using PTDatabase;
using PTDatabase.Models;
using System.Diagnostics;
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

        public bool FindIfCurrentUserPresent()
        {
            var user = dbContext.Users.Select(us => us.Login == HostUserName).FirstOrDefault();

            if (user == false) return false;

            return true;
        }

        public bool CheckIsUserLogedInCorrect()
        {
            if (!IsUserLogedInAsAdministratorInWindows()) return false;

            var user = dbContext.Users.Select(us => us.Login == HostUserName && us.IsLogged).FirstOrDefault();

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

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}