using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTDatabase.Models
{
    public class FilePermission : BaseModel
    {
        public string Name { get; set; }
        public User User { get; set; }
        public File File { get; set; }
    }
}
