using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication4.Entites;
using WebApplication4.Infrastructure;
using WebApplication4.Repository;

namespace WebApplication4.Controllers
{
    public class UserController : Controller
    {
        private readonly UserRepository uRepository;

        private string login;
        private string password;

        public UserController(IConfiguration configuration)
        {
            login = AuthorizeData.Login;
            password = AuthorizeData.Password;
            uRepository = new UserRepository(configuration, login, password);
        }

        public IActionResult Index()
        {
            return View(uRepository.FindAll());
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public IActionResult Create(Users user)
        {
            if (ModelState.IsValid)
            {
                uRepository.Add(user);
                return RedirectToAction("Index");
            }
            return View(user);

        }

        // GET:/User/Delete/1
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            uRepository.Remove(id.Value);
            return RedirectToAction("Index", "User");
        }
    }
}
