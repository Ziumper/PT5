using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTDatabase.Models
{
    public class File : BaseModel
    {
        public string Name { get; set; }
        public string Descripition { get; set; }
        public User CreatedBy { get; set; }
        public string Path { get; set; }
        List<FileHistory> History { get; set; }
        List<FilePermission> Permissions { get; set; }
    }
}
