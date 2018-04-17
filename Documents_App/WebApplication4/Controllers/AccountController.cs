using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication4.Infrastructure;
using WebApplication4.ViewModels;

namespace WebApplication4.Controllers
{
    public class AccountController : Controller
    {
        private string connectionString;

        public AccountController(IConfiguration configuration)
        {
            connectionString = configuration.GetValue<string>("DBInfo:ConnectionString");
        }

        public IActionResult Authorization()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Authorization(LoginModel model)
        {

            if (ModelState.IsValid)
            {
                AuthorizeData.Login = model.Login;
                AuthorizeData.Password = model.Password;

                if (model.Login == "postgres")
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    return RedirectToAction("Index", "Documents");
                }
            }
            else
            {
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View();
        }
    }
}
