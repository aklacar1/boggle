using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoggleREST.API.ServiceInterfaces
{
    public interface IFirebaseTokensService
    {
        bool AddDeviceToken(string token);
    }
}
