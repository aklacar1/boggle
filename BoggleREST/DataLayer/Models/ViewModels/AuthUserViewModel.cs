using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoggleREST.DataLayer.Models.ViewModels
{
    public class AuthUserViewModel
    {
        public string UserName { get; set; }
        public List<string> Roles { get; set; }
        public string UserId { get; set; }
    }
}
