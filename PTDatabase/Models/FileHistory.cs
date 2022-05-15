using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTDatabase.Models
{
    public class FileHistory : BaseModel
    {
        public User User { get; set; }
        public File File { get; set; }
        public string Message { get; set; }
    }
}
