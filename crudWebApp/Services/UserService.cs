using System.Collections.Generic; 
using CrudWebApp.Interfaces;
using CrudWebApp.Models;
using CrudWebApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CrudWebApp.Services {

    public class UserService : IUserService
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<UserService> _logger;

        public UserService(AppDbContext dbContext, ILogger<UserService> logger) {
            _dbContext = dbContext;
            _logger = logger;
        }

        public ActionResult<IEnumerable<User>> All()
        {
            _logger.LogInformation("Find all users");
            return _dbContext.Users.ToList();       // throw new NotImplementedException();
        }

        public ActionResult<User?> Find(int id)
        {   _logger.LogInformation("Find User by id: {id}", id);
            return _dbContext.Users.Find(id);    // throw new NotImplementedException();
        }

        public ActionResult<User> Insert(User item)
        {
            _logger.LogInformation("Insert User: {user}", item);
            if(item.Id != null) {
                Update(item);
            }
            item.CreatedAt = DateTime.Now;
            _dbContext.Users.Add(item);
            _dbContext.SaveChanges();
            return item; // throw new NotImplementedException();
        }

        public ActionResult<User> Update(User item)
        {
            _logger.LogInformation("Update User: {user}", item);
            if(item.Id == null) {
                Insert(item);
            }
            var user = _dbContext.Users.Find(item.Id);
            if (user != null)
            {   
                user.Username = item.Username;
                user.Email = item.Email;
                user.Password = item.Password;
                _dbContext.SaveChanges();

                return user;
            }

            return null;  // throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            _logger.LogInformation("Delete User by id: {id}", id);
            var user = _dbContext.Users.Find(id);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
            }
            // throw new NotImplementedException();
        }
    }
}