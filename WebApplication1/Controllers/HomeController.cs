using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication1.Models;
using WebApplication1.Models.MainSystem;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        MainSystemDevelopContext MainSystem = new MainSystemDevelopContext();

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return Redirect("/Account/Login");
            }

            return View();
        }
    }
}
