using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTDatabase.Models
{
    public class User : BaseModel
    {
        public string Login { get; set; }
        public string Password { get; set; }   
        public string Ip { get; set; }  
        public List<FilePermission> FilePermissions { get; set; }
        public bool IsLogged { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
