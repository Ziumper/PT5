using PTBusinessLogic.Models;
using PTDatabase;
using PTDatabase.Models;
using System.Diagnostics;
using System.Security.Principal;

namespace PTBusinessLogic
{
    public class FileManager
    {
        public static string HostUserName => Environment.GetEnvironmentVariable("UserName");

        public void CreateDatabase()
        {
            if (!System.IO.File.Exists(SqliteDbContext.Path))
            {
                using (SqliteDbContext db = new SqliteDbContext())
                {
                    db.Database.EnsureCreated();
                }
            }
        }

        public void CreateUser(RegisterUserDto dto)
        {
            throw new NotImplementedException();
        }

        public bool CheckIsUserLogedInCorrect()
        {
            WindowsIdentity windowsIdentity = WindowsIdentity.GetCurrent();
            IPrincipal principal = new WindowsPrincipal(windowsIdentity);

            bool IsAuthenticated = windowsIdentity.IsAuthenticated;
            bool IsAdmin = principal.IsInRole("BUILTIN\\" + "Administrators");

            return IsAdmin && IsAuthenticated;
        }
    }
}