using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using WebApplication1.Models.MainSystem;
using WebApplication1.Models.ViewModel;
using WebApplication1.Models.BalimoonBML;

namespace WebApplication1.Controllers
{
    public class UsersController : Controller
    {
        private readonly MainSystemDevelopContext _MainSystem;
        private readonly BalimoonBMLContext _bmlContext;

        public UsersController(MainSystemDevelopContext MainSystem,
            BalimoonBMLContext bmlCtx)
        {
            _MainSystem = MainSystem;
            _bmlContext = bmlCtx;
        }
        [AuthorizedAction]
        public IActionResult Index()
        {
            var joinUserRoles = (from user in _MainSystem.AspNetUsers
                                 join roles in _MainSystem.Roles on user.RolesId equals Convert.ToString(roles.Id)
                                 into rolesA
                                 from roles in rolesA.DefaultIfEmpty()
                                 where roles.Id != 1
                                 select new UsersRoleViewModel
                                 {
                                     rolesDetail = roles,
                                     userDetail = user
                                 }).ToList();
            if(joinUserRoles == null)
            {
                return View("_NotFound");
            }
            else
            {
                return View(joinUserRoles);
            }
        }

        public IActionResult ShowUserDetailAbsahjskdUhduiagdsNBDA(string WYGAILvsdhalsvMwebhJSA)
        {
            var id = WYGAILvsdhalsvMwebhJSA;
            if (HttpContext.Session.GetString("email")== null)
            {
               var returnurl = "/Users/ShowUserDetailAbsahjskdUhduiagdsNBDA?WYGAILvsdhalsvMwebhJSA=" + WYGAILvsdhalsvMwebhJSA;
               return RedirectToAction("Login", new RouteValueDictionary(
               new { Controller = "Account", Action = "Login", returnUrl = returnurl }
               ));

            }
            var UserRole = (from user in _MainSystem.AspNetUsers
                            join roles in _MainSystem.Roles on user.RolesId equals Convert.ToString(roles.Id)
                            where roles.Id != 1 && user.Id == id
                            select new UsersRoleViewModel
                            {
                                userDetail = user,
                                rolesDetail = roles
                            }).FirstOrDefault();
            if(UserRole == null)
            {
                return View("_NotFound");
            }
            //Cari Nama User\\
            ViewBag.NamaUser = UserRole.userDetail.UserName;
            /*List Of Roles in View*/
            List<Models.MainSystem.Roles> Rolest = _MainSystem.Roles.Where(a => a.Id != 1).ToList();
            ViewBag.ListofRoles = new SelectList(Rolest, "Id", "Title");
            /*List Departement*/
            List<Models.BalimoonBML.DimensionValue> Departemen = _bmlContext.DimensionValue.Where(a => a.DimensionCode == "DEPARTMENT").ToList();
            ViewBag.DepartmentList = new SelectList(Departemen, "DimensionValueCode", "DimensionValueName");

            return View(UserRole);
        }

        //JSON
        public IActionResult DynamicRoleDescription(int? id)
        {
            var result = string.Empty;
            var getRoles = _MainSystem.Roles.FirstOrDefault(a => a.Id == id);
            result = JsonConvert.SerializeObject(getRoles, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(result);
        }

        //JSON POST
        [HttpPost]
        public IActionResult UpdateUsersRole(UsersRoleViewModel model)
        {
            
            var id = model.userDetail.Id;
            if(id == null)
            {
                return View("_NotFound");
            }

            //cek user with id
            var getUser = _MainSystem.AspNetUsers.SingleOrDefault(a => a.Id == id);
            if(getUser != null)
            {
                //lakukan update
                getUser.RolesId = Convert.ToString(model.rolesDetail.Id);
                getUser.Departement = model.userDetail.Departement;
                _MainSystem.AspNetUsers.Update(getUser);
                _MainSystem.SaveChanges();
                return Json(new { status = true, message = "Your Data Has Been Saved!" });
            }
            else
            {
                return Json(new { status = false, message = "User with ID = "+id+" cannot be found...!" });
            }
        }
    }
}