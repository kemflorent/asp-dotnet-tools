using System.Collections.Generic; 
using CrudWebApp.Interfaces;
using CrudWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CrudWebApp.Interfaces {

   public interface IUserService {
        ActionResult<IEnumerable<User>> All();
        ActionResult<User?> Find(int id);
        ActionResult<User> Insert(User item);
        ActionResult<User> Update(User item);
        void Delete(int id);
    }
}