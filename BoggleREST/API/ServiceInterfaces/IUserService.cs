using BoggleREST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoggleREST.API.ServiceInterfaces
{
    public interface IUserService
    {
        string InsertUser(Users user);
        Users UpdateUser(Users user);
        Users GetUserById(string id);
        Users GetUserByUsername(string Username);
        Users DeleteUserById(string id);
        Users DeleteUserByUserName(string userName);
    }
}
