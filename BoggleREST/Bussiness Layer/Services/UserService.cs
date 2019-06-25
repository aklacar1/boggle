using BoggleREST.API.ServiceInterfaces;
using BoggleREST;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoggleREST.BussinessLayer.Services
{
    internal partial class UserService : IUserService
    {
        private readonly BoggleContext dbContext;

        public UserService(BoggleContext dbContext)
        {
            this.dbContext = dbContext;
        }
        #region Insert Person
        public string InsertUser(Users user)
        {
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
            return user.Id;
        }
        #endregion
        #region Update User
        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="user"></param>
        public Users UpdateUser(Users user)
        {
            dbContext.Entry(user).State = EntityState.Modified;
            dbContext.SaveChanges();
            return user;
        }
        #endregion
        #region Load User
        public Users GetUserById(string id)
        {
            return dbContext.Users.SingleOrDefault(u => u.Id == id);
        }
        public Users GetUserByUsername(string Username)
        {
            return dbContext.Users.SingleOrDefault(u => u.UserName == Username);
        }
        #endregion
        #region Delete User
        public Users DeleteUserById(string id)
        {
            Users user = dbContext.Users.Find(id);
            dbContext.Users.Remove(user);
            dbContext.SaveChanges();
            return user;
        }
        public Users DeleteUserByUserName(string userName)
        {
            Users user = dbContext.Users.SingleOrDefault(u => u.UserName == userName);
            dbContext.Users.Remove(user);
            dbContext.SaveChanges();
            return user;
        }
        #endregion
    }
}
