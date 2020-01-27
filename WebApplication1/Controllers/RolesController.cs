using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using WebApplication1.Models.MainSystem;
using WebApplication1.Models.ViewModel;

namespace WebApplication1.Controllers
{
    public class RolesController : Controller
    {
        private readonly MainSystemDevelopContext _mainSysContext;
        public RolesController(MainSystemDevelopContext mainSystemDevelopContext)
        {
            _mainSysContext = mainSystemDevelopContext;
        }
        [AuthorizedAction]
        public IActionResult Index()
        {
            var roles = new RolesViewModel();
            roles.RoleList = _mainSysContext.Roles.ToList();
            return View(roles);
        }
        [HttpPost]
        public IActionResult AddRoles(RolesViewModel model)
        {
            if (model == null)
            {
                return View("_NotFound");
            }
            var roles = new Roles();
            roles.Title = model.RoleDetail.Title;
            roles.Description = model.RoleDetail.Description;
            _mainSysContext.Roles.Add(roles);
            _mainSysContext.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult DisposeRoles(int? ajewbbrklwebrafuyuvaefjabewfiaef)
        {
            var hasil = string.Empty;
            if(ajewbbrklwebrafuyuvaefjabewfiaef == null)
            {
                return View("_NotFound");
            }
            var id = ajewbbrklwebrafuyuvaefjabewfiaef;
            var checkinRoles = _mainSysContext.LinkRolesMenu.FirstOrDefault(a => a.RolesId == id);
            if(checkinRoles != null)
            {
                hasil = "This Roles Already Use in Other Table, Please Delete The Relation First!!";
            }
            else
            {
                var getRolesinTable = _mainSysContext.Roles.FirstOrDefault(a => a.Id == id);
                if(getRolesinTable == null)
                {
                    hasil = "Roles Not Found on Database";
                }
                else
                {
                    _mainSysContext.Roles.Remove(getRolesinTable);
                    _mainSysContext.SaveChanges();
                    hasil = "Sukses";
                }
            }
            return Json(hasil);
        }
        public IActionResult EditRolesAndDetailAccess(int? tyqiasdbhasdjjiiuqwevxvzzzxcvasd)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                var returnurl = "/Roles/EditRolesAndDetailAccess?tyqiasdbhasdjjiiuqwevxvzzzxcvasd=" + tyqiasdbhasdjjiiuqwevxvzzzxcvasd;
                return RedirectToAction("Login", new RouteValueDictionary(
               new { Controller = "Account", Action = "Login", returnUrl = returnurl }
               ));
            }

            if (tyqiasdbhasdjjiiuqwevxvzzzxcvasd == null)
            {
                return View("_NotFound");
            }
            var id = tyqiasdbhasdjjiiuqwevxvzzzxcvasd;
            var roles = _mainSysContext.Roles.SingleOrDefault(a => a.Id == id);
            if(roles == null)
            {
                return View("_NotFound");
            }
            DataSet ds = new DataSet();
            List<string> menus_id = _mainSysContext.LinkRolesMenu.Where(a => a.RolesId == id).Select(b => b.MenusId.ToString()).ToList();
            ds = ToDataSet(_mainSysContext.Menus.ToList());
            DataTable table = ds.Tables[0];
            DataRow[] parentMenus = table.Select("ParentId = 0");

            var sb = new StringBuilder();
            string unorderedList = GenerateUL(parentMenus, table, sb, menus_id);
            ViewBag.roles = roles.Title;
            ViewBag.menu = unorderedList;
            return View(roles);
        }


        [HttpPost]
        public IActionResult EditTheRolesNameAndDescription(Roles model)
        {
            if(model == null)
            {
                return View("_NotFound");
            }
            //Get The Data Before Update
            var getRoles = _mainSysContext.Roles.SingleOrDefault(a => a.Id == model.Id);
            if(getRoles == null)
            {
                return View("_NotFound");
            }
            else
            {
                getRoles.Description = model.Description;
                getRoles.Title = model.Title;
                _mainSysContext.Roles.Update(getRoles);
                _mainSysContext.SaveChanges();

                return RedirectToAction("EditRolesAndDetailAccess", new RouteValueDictionary(
                    new { Controller = "Roles", Action = "EditRolesAndDetailAccess", tyqiasdbhasdjjiiuqwevxvzzzxcvasd = model.Id }
                    ));
            }
        }



        public IActionResult UpdateRoles(int id, List<int> roles)
        {
            var temp = _mainSysContext.LinkRolesMenu.Where(s => s.RolesId == id);
            foreach (var item in temp)
            {
                _mainSysContext.LinkRolesMenu.Remove(item);
            }

            foreach (var role in roles)
            {
                _mainSysContext.LinkRolesMenu.Add(new LinkRolesMenu { MenusId = role, RolesId = id });
            }

            _mainSysContext.SaveChanges();

            return Json(new { status = true, message = "Successfully Updated Role!" });
        }


        //Bagian Akhir\\
        private string GenerateUL(DataRow[] menu, DataTable table, StringBuilder sb, List<string> menus_id)
        {
            if (menu.Length > 0)
            {
                foreach (DataRow dr in menu)
                {
                    string id = dr["id"].ToString();
                    string handler = dr["url"].ToString();
                    string menuText = dr["name"].ToString();
                    string icon = dr["icon"].ToString();

                    string pid = dr["id"].ToString();
                    string parentId = dr["ParentId"].ToString();

                    string status = (menus_id.Contains(id)) ? "Checked" : "";

                    DataRow[] subMenu = table.Select(String.Format("ParentId = '{0}'", pid));
                    if (subMenu.Length > 0 && !pid.Equals(parentId))
                    {
                        string line = String.Format(@"<li class=""has""><input type=""checkbox"" name=""subdomain[]"" id=""{5}"" value=""{1}"" {4}><label>> {1}</label>", handler, menuText, icon, "target", status, id);
                        sb.Append(line);

                        var subMenuBuilder = new StringBuilder();
                        sb.AppendLine(String.Format(@"<ul>"));
                        sb.Append(GenerateUL(subMenu, table, subMenuBuilder, menus_id));
                        sb.Append("</ul>");
                    }
                    else
                    {
                        string line = String.Format(@"<li class=""""><input type=""checkbox"" name=""subdomain[]"" id=""{5}"" value=""{1}"" {4}><label>{1}</label>", handler, menuText, icon, "target", status, id);
                        sb.Append(line);
                    }
                    sb.Append("</li>");
                }
            }
            return sb.ToString();
        }

        public DataSet ToDataSet<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dataTable);
            return ds;
        }
    }
}