using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BoggleREST.DataLayer.Models.BindingModels
{
    public class inUserModel
    {
        public string UserName { get; set; }
        public string EMail { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
