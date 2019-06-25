using BoggleREST.API.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoggleREST.Bussiness_Layer.Services
{
    internal partial class FirebaseTokensService : IFirebaseTokensService
    {
        private readonly BoggleContext dbContext;

        public FirebaseTokensService(BoggleContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public bool AddDeviceToken(string token) {
            FirebaseTokens ft = new FirebaseTokens();
            ft.Token = token;
            ft.UserId = dbContext.GetCurrentUserId();
            if (dbContext.FirebaseTokens.Any(x => x.Token == token))
                return false;
            dbContext.FirebaseTokens.Add(ft);
            dbContext.SaveChanges();
            return true;
        }
    }
}
