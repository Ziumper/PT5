using PTBusinessLogic.Models;
using PTDatabase;
using PTDatabase.Models;
using System.Diagnostics;

namespace PTBusinessLogic
{
    public class FileManager
    {
        public string HostUserName => Environment.GetEnvironmentVariable("UserName");

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
    }
}