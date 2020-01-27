using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebApplication1.Models;
using WebApplication1.Models.MainSystem;
using WebApplication1.Models.MainSystem.MainSystemViewModel;
using WebApplication1.Models.Tools;
using WebApplication1.Models.BalimoonBML;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<Microsoft.AspNetCore.Identity.IdentityUser> _signInManager;
        private readonly MainSystemDevelopContext _dbContext;
        private readonly UserManager<Microsoft.AspNetCore.Identity.IdentityUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<RegisterViewModel> _logger;
        private readonly BalimoonBMLContext _bmlContext;
        UploadImages _uploadImages;
        DeleteImages _deleteImages;
        UploadPDF _uploadPDF;
        PDFDelete _deletePDF;
       
        public AccountController(
            SignInManager<Microsoft.AspNetCore.Identity.IdentityUser> signInManager,
            MainSystemDevelopContext dbContext,
            ILogger<RegisterViewModel> logger,
            BalimoonBMLContext bMLContext,
            UserManager<Microsoft.AspNetCore.Identity.IdentityUser> userManager,
            IEmailSender emailSender
           
            )
        {
            _signInManager = signInManager;
            _dbContext = dbContext;
            _logger = logger;
            _userManager = userManager;
            _emailSender = emailSender;
            _bmlContext = bMLContext;
            _uploadImages = new UploadImages();
            _deleteImages = new DeleteImages();
            _uploadPDF = new UploadPDF();
            _deletePDF = new PDFDelete();
        }

        [BindProperty]


        public string ReturnUrl { get; set; }


        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public IActionResult Login(string returnUrl = null)
        {
           ViewBag.ReturnUrl = returnUrl;
            
            return View();
        }

        public async Task<IActionResult> SignIn(LoginModel aspNetUsers, string returnUrl = null)
        {
            var UserLogin = _dbContext.AspNetUsers.Where(a => a.UserName == aspNetUsers.username).FirstOrDefault();
            if(UserLogin != null)
            {
                if(UserLogin.EmailConfirmed == true)
                {
                    var result = await _signInManager.PasswordSignInAsync(aspNetUsers.username, aspNetUsers.password, lockoutOnFailure: false, isPersistent: false);
                    if (result.Succeeded)
                    {
                        HttpContext.Session.SetString("email", UserLogin.Email);
                        HttpContext.Session.SetString("username", UserLogin.UserName);
                        HttpContext.Session.SetString("id", UserLogin.Id);
                        HttpContext.Session.SetString("roleId", UserLogin.RolesId);

                        int roleId = Convert.ToInt32(Convert.ToDouble(HttpContext.Session.GetString("roleId")));
                        List<Menus> menus = _dbContext.LinkRolesMenu.Where(a => a.RolesId == roleId).Select(a => a.Menus).ToList();

                        DataSet ds = new DataSet();
                        ds = ToDataSet(menus);
                        DataTable table = ds.Tables[0];
                        DataRow[] parentMenus = table.Select("ParentId = 0");

                        var sb = new StringBuilder();
                        string menuString = GenerateUL(parentMenus, table, sb);
                        HttpContext.Session.SetString("menuString", menuString);
                        HttpContext.Session.SetString("menus", JsonConvert.SerializeObject(menus));

                        return Json(new { status = true, message = "Login Successfull!", returnUrl = returnUrl });

                    }
                    else
                    {
                        return Json(new { status = false, message = "Invalid Password!" });
                    }
                }
                else
                {
                    return Json(new { status = false, message = "Email Not Confirmed!" });
                }
            }
            else
            {
                return Json(new { status = false, message = "Invalid UserName!" });
            }
        }

        private string GenerateUL(DataRow[] menu, DataTable table, StringBuilder sb)
        {
            if(menu.Length > 0)
            {
                foreach (DataRow dr in menu)
                {
                    string url = dr["Url"].ToString();
                    string menuText = dr["Name"].ToString();
                    string icon = dr["Icon"].ToString();

                    if (url != "#")
                    {
                        string line = String.Format(@"<li><a href=""{0}""><i class=""{2}""></i> {1} </a></li>", url, menuText, icon);
                        sb.Append(line);
                    }

                    string pid = dr["Id"].ToString();
                    string parentId = dr["ParentId"].ToString();

                    DataRow[] subMenu = table.Select(String.Format("ParentId = '{0}'", pid));
                    if (subMenu.Length > 0 && !pid.Equals(parentId))
                    {
                        string line = String.Format(@"<li class=""sub-menu""><a href=""#"" data-ma-action=""submenu-toggle""><i class=""{0}""></i>{1}</a><ul>", icon, menuText);
                        var subMenuBuilder = new StringBuilder();
                        sb.AppendLine(line);
                        sb.Append(GenerateUL(subMenu, table, subMenuBuilder));
                        sb.Append("</ul></li>");
                    }
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

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/Account/CheckYour");
            if (ModelState.IsValid)
            {
                var user = new Microsoft.AspNetCore.Identity.IdentityUser { UserName = model.UserName, Email = model.Email, PhoneNumber = model.PhoneNumber};
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action(
                         "ConfirmEmail", "Account", new { userId = user.Id, userEmail = user.Email, Code = code },
                         protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(model.Email, "Verify Your Email Address",
                        $"Thanks for signing up<br>" +
                        $"Your account has been created,<br>" +
                        $"You can login with the following credentials after you have activated your account by pressing the link below.<br>" +
                        $"-------------------------------<br>" +
                        $"User Name = " + user.UserName + " <br> " +
                        $"E-Mail = " + user.Email + "<br> " +
                        $"-------------------------------<br>" +
                        $"Please click this link to activate your account:<br>" +
                        $"<a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>Click Here</a>.");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        public IActionResult CheckYour()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if(userId == null || code == null)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if(user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Error confirming email for user with ID '{userId}':");
            }
            var addGuestRole = _dbContext.AspNetUsers.FirstOrDefault(a => a.Id == userId);
            addGuestRole.RolesId = "2";
            _dbContext.AspNetUsers.Update(addGuestRole);
            _dbContext.SaveChanges();
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if(user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    return RedirectToAction(nameof(ForgotPasswordConfirmation));
                }
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action(
                    "ResetPassword","Account",
                    new {code},
                    protocol: Request.Scheme
                    );
                await _emailSender.SendEmailAsync(
                    model.Email, "Reset Password",
                    $"Hi "+user.UserName+",<br>" +
                    $"We Received a request to reset your password for your BalimOOn account : "+user.Email+".<br>" +
                    $"We're here to help!<br><br>" +
                    $"Simply click on the link to set a new password : <br>" +
                    $"<a href='{HtmlEncoder.Default.Encode(callbackUrl)}'><h2>Click Here</h2></a><br>" +
                    $"If you didn't ask to change your password, don't worry! Your password is still safe and you can delete this email.<br><br>" +
                    $"Regards,<br><br><br>" +
                    $"BalimOOn Family"
                    );

                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            if(code == null)
            {
                throw new ApplicationException("A code must be supplied for password reset.");
            }
            var model = new ResetPasswordModel { Code = code };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); 
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }
            AddErrors(result);
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return Redirect("/Account/Login");
            }
            var user = _userManager.GetUserName(User);
            if(user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var users = _dbContext.AspNetUsers.FirstOrDefault(a => a.UserName == user);
            ViewBag.Email = users.Email;
            ViewBag.UserName = users.UserName;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            //Inisialisasi awal agar ketika error nama dan username tidak hilang
            var current_user = _userManager.GetUserName(User);
            var users = _dbContext.AspNetUsers.FirstOrDefault(a => a.UserName == current_user);
            ViewBag.Email = users.Email;
            ViewBag.UserName = users.UserName;

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }
            await _signInManager.RefreshSignInAsync(user);
            return RedirectToAction(nameof(ChangePasswordSuccess));
        }

        [HttpGet]
        public IActionResult ChangePasswordSuccess()
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return Redirect("/Account/Login");
            }
            return View();
        }

        [HttpGet]
        public IActionResult ProfileUser()
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return Redirect("/Account/Login");
            }
            //select User, Role, dan Vendor
            var userLogin = _userManager.GetUserName(User);
            if(userLogin == null)
            {
                return View();
            }
            else
            {
                var model = (from user in _dbContext.AspNetUsers
                             join role in _dbContext.Roles on user.RolesId equals Convert.ToString(role.Id)
                             join vendor in _dbContext.AspNetVendor on user.UserName equals vendor.UserId
                             into joinVendor
                             from Vendor in joinVendor.DefaultIfEmpty()
                             where user.UserName == userLogin
                             select new UserViewModel
                             {
                                 aspnetUser = user,
                                 roles = role,
                                 AspNetVendor = Vendor
                             }).FirstOrDefault();
                var photos = model.aspnetUser.Picture;
                if(photos == null)
                {
                    photos = "avatar5.png";
                }
                //Attempt Data to Select List
                //List Of Country
                List<Models.BalimoonBML.CountryRegion> Country = _bmlContext.CountryRegion.ToList();
                ViewBag.ListOfCountries = new SelectList(Country, "Code", "Name");

                //List Of Currencies
                List<Models.BalimoonBML.Currency> currencies = _bmlContext.Currency.ToList();
                ViewBag.Currency = new SelectList(currencies, "Code", "Description");

                ViewBag.Picture = photos;
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfileUser(IFormFile file, UserViewModel model)
        {
            var userLogin = _userManager.GetUserName(User);
            //variabel error jika ada
            var hasilError = "";

            //Tambahkan Link Akses Direktori
            var link1 = "wwwroot/dist/img";
            var link2 = "wwwroot\\dist\\img\\";

            //model baru jika ada error
            var modelSimpan = (from user in _dbContext.AspNetUsers
                         join role in _dbContext.Roles on user.RolesId equals Convert.ToString(role.Id)
                         join vendor in _dbContext.AspNetVendor on user.UserName equals vendor.UserId
                         into joinVendor
                         from Vendor in joinVendor.DefaultIfEmpty()
                         where user.UserName == userLogin
                         select new UserViewModel
                         {
                             aspnetUser = user,
                             roles = role,
                             AspNetVendor = Vendor
                         }).FirstOrDefault();
            var photos = modelSimpan.aspnetUser.Picture;
            if (photos == null)
            {
                photos = "avatar5.png";
            }
            ViewBag.Picture = photos;

            //Setelah Model Disimpan lakukan Proses
            if (userLogin == null)
            {
                return Redirect("/Account/Login");
            }

            var aspnetUser = _dbContext.AspNetUsers.FirstOrDefault(a => a.UserName == userLogin);

            if (file == null && model.aspnetUser.PhoneNumber != null)
            {
                aspnetUser.PhoneNumber = model.aspnetUser.PhoneNumber;
                _dbContext.AspNetUsers.Update(aspnetUser);
                await _dbContext.SaveChangesAsync();
            }
            else if(file != null && model.aspnetUser.PhoneNumber != null)
            {
                string uploadImage = await _uploadImages.ImagesUpload(file, link1, link2);

                if (uploadImage == "")
                {
                    hasilError = "Couldn't Get The File";
                    ViewBag.ErrorPage = hasilError;
                    return View("ProfileUser", modelSimpan);
                }
                else if (uploadImage != "File must be either .jpg, .jpeg, .png and Maximum Size is 4MB")
                {
                    if (aspnetUser.Picture != null)
                    {
                        string dispose = _deleteImages.Delete(aspnetUser.Picture, link2);
                    }
                    aspnetUser.Picture = uploadImage;
                    aspnetUser.PhoneNumber = model.aspnetUser.PhoneNumber;
                    _dbContext.AspNetUsers.Update(aspnetUser);
                    await _dbContext.SaveChangesAsync();

                    return RedirectToAction(nameof(ProfileUser));
                }
                else
                {
                    hasilError = uploadImage;
                    ViewBag.ErrorPage = hasilError;
                    return View("ProfileUser", modelSimpan);
                }
            }
            else if(file != null)
            {
                
                //Lempar Image dan Link Ke Class Upload
                string uploadImage = await _uploadImages.ImagesUpload(file, link1, link2);
                if(uploadImage == "")
                {
                    hasilError = "Couldn't Get The File";
                    ViewBag.ErrorPage = hasilError;
                    return View("ProfileUser", modelSimpan);
                }
                else if(uploadImage != "File must be either .jpg, .jpeg, .png and Maximum Size is 4MB")
                {
                    if(aspnetUser.Picture != null)
                    {
                        string dispose = _deleteImages.Delete(aspnetUser.Picture, link2);
                    }
                    aspnetUser.Picture = uploadImage;
                    _dbContext.AspNetUsers.Update(aspnetUser);
                    await _dbContext.SaveChangesAsync();

                    return RedirectToAction(nameof(ProfileUser));
                }
                else
                {
                    hasilError = uploadImage;
                    ViewBag.ErrorPage = hasilError;
                    return View("ProfileUser", modelSimpan);
                }
            }
            else
            {
                return RedirectToAction(nameof(ProfileUser));
            }

            return RedirectToAction(nameof(ProfileUser));
        }

       [HttpPost]
       public async Task<IActionResult>UpdateVendorData(IFormFile file1, UserViewModel model)
       {
            var userLogin = _userManager.GetUserName(User);

            //variabel error jika ada
            var hasilError = "";

            //Tambahkan Link Akses Direktori
            var link1 = "wwwroot/Pdf/Vendor";
            var link2 = "wwwroot\\Pdf\\Vendor\\";

            //Cek User yang Terhubung
            var _users = _dbContext.AspNetUsers.FirstOrDefault(a => a.UserName == userLogin);

            //model baru jika ada error
            var modelSimpan = (from user in _dbContext.AspNetUsers
                               join role in _dbContext.Roles on user.RolesId equals Convert.ToString(role.Id)
                               join vendor in _dbContext.AspNetVendor on user.UserName equals vendor.UserId
                               into joinVendor
                               from Vendor in joinVendor.DefaultIfEmpty()
                               where user.UserName == userLogin
                               select new UserViewModel
                               {
                                   aspnetUser = user,
                                   roles = role,
                                   AspNetVendor = Vendor
                               }).FirstOrDefault();

            var photos = modelSimpan.aspnetUser.Picture;
            if (photos == null)
            {
                photos = "avatar5.png";
            }
            ViewBag.Picture = photos;

            //Setelah Model Disimpan lakukan Proses
            if (userLogin == null)
            {
                return Redirect("/Account/Login");
            }
            if(_users == null)
            {
                hasilError = "User with username = "+userLogin+" Not Found !!";
                ViewBag.ErrorPage = hasilError;
                return View("ProfileUser", modelSimpan);
            }
            //Cek apakah dia ada di aspNetVendor
            var _inAspVendor = _dbContext.AspNetVendor.FirstOrDefault(a => a.UserId == userLogin);
            //Jika Tidak Ada lakukan insert Procedure
            if(_inAspVendor == null)
            {
                //Cek apakah File PDF Null
                if(file1 == null)
                {
                    hasilError = "You Must Insert Your Company File First !!";
                    ViewBag.ErrorPage = hasilError;
                    return View("ProfileUser", modelSimpan);
                }
                //Upload PDF
                string uploadPDF = await _uploadPDF.PDFUpload(file1, link1);
                if(uploadPDF == "")
                {
                    hasilError = "Cannot Get Your File, Please Upload .pdf file with 5MB Maximum size !!";
                    ViewBag.ErrorPage = hasilError;
                    return View("ProfileUser", modelSimpan);
                }
                else if(uploadPDF != "File must be .pdf extension and Maximum Size is 5MB") {
                    //Add Data Vendor in Table ASPNET Vendor
                    var VendorData = new AspNetVendor
                    {
                        UserId = userLogin,
                        CompanyName = model.AspNetVendor.CompanyName,
                        Address = model.AspNetVendor.Address,
                        NpwpNo = model.AspNetVendor.NpwpNo,
                        SiupNo = model.AspNetVendor.SiupNo,
                        SuplierType = "",
                        ContactName = model.AspNetVendor.ContactName,
                        Contact = model.AspNetVendor.Contact,
                        ContactEmail = model.AspNetVendor.ContactEmail,
                        InvoiceName = model.AspNetVendor.InvoiceName,
                        InvoiceContact = model.AspNetVendor.InvoiceContact,
                        InvoiceEmail = model.AspNetVendor.InvoiceEmail,
                        FileLocation = uploadPDF,
                        Swiftcode = model.AspNetVendor.Swiftcode,
                        Bank_Account1 = model.AspNetVendor.Bank_Account1,
                        Bank_Account2 = model.AspNetVendor.Bank_Account2,
                        VendorCity = model.AspNetVendor.VendorCity,
                        VendorFaxNo = model.AspNetVendor.VendorFaxNo,
                        Currency = model.AspNetVendor.Currency,
                        Country = model.AspNetVendor.Country,
                        IsActive = "No",
                    };
                    _dbContext.AspNetVendor.Add(VendorData);
                    await _dbContext.SaveChangesAsync();

                    //Update Users Role
                    _users.RolesId = "1002";
                    _dbContext.AspNetUsers.Update(_users);
                    await _dbContext.SaveChangesAsync();

                    //Reload Page
                    return RedirectToAction(nameof(ProfileUser));
                }
                else
                {
                    hasilError = uploadPDF;
                    ViewBag.ErrorPage = hasilError;
                    return View("ProfileUser", modelSimpan);
                }

            }
            else
            {
               
                var _aspnetVendor = _dbContext.AspNetVendor.FirstOrDefault(a => a.UserId == userLogin);
                //Cek Vendor di tabel BML
                var _vendorinBMLContext = _bmlContext.Vendors.FirstOrDefault(a => a.VendorNo == _aspnetVendor.VendorNo);

                if(_aspnetVendor.IsActive == "Yes") {

                    _vendorinBMLContext.VendorContact = model.AspNetVendor.ContactName;
                    _vendorinBMLContext.VendorPhoneNo = model.AspNetVendor.Contact;
                    _vendorinBMLContext.VendorFaxNo = model.AspNetVendor.VendorFaxNo;
                    _vendorinBMLContext.MobileNo = model.AspNetVendor.InvoiceContact;
                    _vendorinBMLContext.Email = model.AspNetVendor.ContactEmail;
                    _vendorinBMLContext.LastModifiedBy = _userManager.GetUserName(User);
                    _vendorinBMLContext.LastModifiedTime = DateTime.Now;
                    _bmlContext.Vendors.Update(_vendorinBMLContext);
                    _bmlContext.SaveChanges();

                    //Update di tabel MainSystem
                    _aspnetVendor.ContactName = model.AspNetVendor.ContactName;
                    _aspnetVendor.Contact = model.AspNetVendor.Contact;
                    _aspnetVendor.ContactEmail = model.AspNetVendor.ContactEmail;
                    _aspnetVendor.InvoiceName = model.AspNetVendor.InvoiceName;
                    _aspnetVendor.InvoiceContact = model.AspNetVendor.InvoiceContact;
                    _aspnetVendor.InvoiceEmail = model.AspNetVendor.InvoiceEmail;
                    _aspnetVendor.VendorFaxNo = model.AspNetVendor.VendorFaxNo;
                    _dbContext.AspNetVendor.Update(_aspnetVendor);
                    _dbContext.SaveChanges();

                    //reload page
                    return RedirectToAction(nameof(ProfileUser));
                }
                else
                {
                    if (file1 == null)
                    {
                        _aspnetVendor.CompanyName = model.AspNetVendor.CompanyName;
                        _aspnetVendor.Address = model.AspNetVendor.Address;
                        _aspnetVendor.NpwpNo = model.AspNetVendor.NpwpNo;
                        _aspnetVendor.SiupNo = model.AspNetVendor.SiupNo;
                        _aspnetVendor.ContactName = model.AspNetVendor.ContactName;
                        _aspnetVendor.Contact = model.AspNetVendor.Contact;
                        _aspnetVendor.ContactEmail = model.AspNetVendor.ContactEmail;
                        _aspnetVendor.InvoiceName = model.AspNetVendor.InvoiceName;
                        _aspnetVendor.InvoiceContact = model.AspNetVendor.InvoiceContact;
                        _aspnetVendor.InvoiceEmail = model.AspNetVendor.InvoiceEmail;
                        _aspnetVendor.Swiftcode = model.AspNetVendor.Swiftcode;
                        _aspnetVendor.Bank_Account1 = model.AspNetVendor.Bank_Account1;
                        _aspnetVendor.Bank_Account2 = model.AspNetVendor.Bank_Account2;
                        _aspnetVendor.Country = model.AspNetVendor.Country;
                        _aspnetVendor.Currency = model.AspNetVendor.Currency;
                        _aspnetVendor.VendorCity = model.AspNetVendor.VendorCity;
                        _aspnetVendor.VendorFaxNo = model.AspNetVendor.VendorFaxNo;
                        _dbContext.AspNetVendor.Update(_aspnetVendor);
                        await _dbContext.SaveChangesAsync();

                        //Update Users Role
                        _users.RolesId = "1002";
                        _dbContext.AspNetUsers.Update(_users);
                        await _dbContext.SaveChangesAsync();

                        //Cek apakah data sudah ada di BML Context
                        if(_aspnetVendor.VendorNo != null || _aspnetVendor.VendorNo != "")
                        {
                            _vendorinBMLContext.VendorName = model.AspNetVendor.CompanyName;
                            _vendorinBMLContext.VendorAddress = model.AspNetVendor.Address;
                            _vendorinBMLContext.VendorCity = model.AspNetVendor.VendorCity;
                            _vendorinBMLContext.VendorContact = model.AspNetVendor.ContactName;
                            _vendorinBMLContext.VendorPhoneNo = model.AspNetVendor.Contact;
                            _vendorinBMLContext.VendorFaxNo = model.AspNetVendor.VendorFaxNo;
                            _vendorinBMLContext.CurrencyCode = model.AspNetVendor.Currency;
                            _vendorinBMLContext.Country = model.AspNetVendor.Country;
                            _vendorinBMLContext.MobileNo = model.AspNetVendor.InvoiceContact;
                            _vendorinBMLContext.Email = model.AspNetVendor.InvoiceEmail;
                            _vendorinBMLContext.Swiftcode = model.AspNetVendor.Swiftcode;
                            _bmlContext.Vendors.Update(_vendorinBMLContext);
                            _bmlContext.SaveChanges();
                        }

                        //reload page
                        return RedirectToAction(nameof(ProfileUser));
                    }
                    else
                    {
                        string uploadPDF = await _uploadPDF.PDFUpload(file1, link1);
                        if (uploadPDF == "")
                        {
                            hasilError = "Cannot Get Your File, Please Upload .pdf file with 5MB Maximum size !!";
                            ViewBag.ErrorPage = hasilError;
                            return View("ProfileUser", modelSimpan);
                        }
                        else if (uploadPDF != "File must be .pdf extension and Maximum Size is 5MB")
                        {
                            //hapus existing pdf
                            string Dispose = _deletePDF.Delete(modelSimpan.AspNetVendor.FileLocation, link2);
                            _aspnetVendor.CompanyName = model.AspNetVendor.CompanyName;
                            _aspnetVendor.Address = model.AspNetVendor.Address;
                            _aspnetVendor.NpwpNo = model.AspNetVendor.NpwpNo;
                            _aspnetVendor.SiupNo = model.AspNetVendor.SiupNo;
                            _aspnetVendor.ContactName = model.AspNetVendor.ContactName;
                            _aspnetVendor.Contact = model.AspNetVendor.Contact;
                            _aspnetVendor.ContactEmail = model.AspNetVendor.ContactEmail;
                            _aspnetVendor.InvoiceName = model.AspNetVendor.InvoiceName;
                            _aspnetVendor.InvoiceContact = model.AspNetVendor.InvoiceContact;
                            _aspnetVendor.InvoiceEmail = model.AspNetVendor.InvoiceEmail;
                            _aspnetVendor.Swiftcode = model.AspNetVendor.Swiftcode;
                            _aspnetVendor.Bank_Account1 = model.AspNetVendor.Bank_Account1;
                            _aspnetVendor.Bank_Account2 = model.AspNetVendor.Bank_Account2;
                            _aspnetVendor.Country = model.AspNetVendor.Country;
                            _aspnetVendor.Currency = model.AspNetVendor.Currency;
                            _aspnetVendor.VendorCity = model.AspNetVendor.VendorCity;
                            _aspnetVendor.VendorFaxNo = model.AspNetVendor.VendorFaxNo;
                            _aspnetVendor.FileLocation = uploadPDF;
                            _dbContext.AspNetVendor.Update(_aspnetVendor);
                            await _dbContext.SaveChangesAsync();

                            //Update Users Role
                            _users.RolesId = "1002";
                            _dbContext.AspNetUsers.Update(_users);
                            await _dbContext.SaveChangesAsync();

                            //Cek apakah data sudah ada di BML Context
                            if (_aspnetVendor.VendorNo != null || _aspnetVendor.VendorNo != "")
                            {
                                _vendorinBMLContext.VendorName = model.AspNetVendor.CompanyName;
                                _vendorinBMLContext.VendorAddress = model.AspNetVendor.Address;
                                _vendorinBMLContext.VendorCity = model.AspNetVendor.VendorCity;
                                _vendorinBMLContext.VendorContact = model.AspNetVendor.ContactName;
                                _vendorinBMLContext.VendorPhoneNo = model.AspNetVendor.Contact;
                                _vendorinBMLContext.VendorFaxNo = model.AspNetVendor.VendorFaxNo;
                                _vendorinBMLContext.CurrencyCode = model.AspNetVendor.Currency;
                                _vendorinBMLContext.Country = model.AspNetVendor.Country;
                                _vendorinBMLContext.MobileNo = model.AspNetVendor.InvoiceContact;
                                _vendorinBMLContext.Email = model.AspNetVendor.InvoiceEmail;
                                _vendorinBMLContext.Swiftcode = model.AspNetVendor.Swiftcode;
                                _bmlContext.Vendors.Update(_vendorinBMLContext);
                                _bmlContext.SaveChanges();
                            }

                            //reload page
                            return RedirectToAction(nameof(ProfileUser));
                        }
                        else
                        {
                            hasilError = uploadPDF;
                            ViewBag.ErrorPage = hasilError;
                            return View("ProfileUser", modelSimpan);
                        }
                    }
                }
            }
       }

        
        public IActionResult DownloadPdf(string id)
        {
            var modelSimpan = (from user in _dbContext.AspNetUsers
                               join role in _dbContext.Roles on user.RolesId equals Convert.ToString(role.Id)
                               join vendor in _dbContext.AspNetVendor on user.UserName equals vendor.UserId
                               into joinVendor
                               from Vendor in joinVendor.DefaultIfEmpty()
                               where user.UserName == id
                               select new UserViewModel
                               {
                                   aspnetUser = user,
                                   roles = role,
                                   AspNetVendor = Vendor
                               }).FirstOrDefault();


            var file = modelSimpan.AspNetVendor.FileLocation;
            if(file== null)
            {
                return NotFound();
            }
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Pdf/Vendor/");
            var stream = new FileStream(@""+path+""+file, FileMode.Open);
            //byte[] fileBytes = System.IO.File.ReadAllBytes(path+""+file);
            //return new FileContentResult(fileBytes, "application/pdf");
            var namafile = modelSimpan.AspNetVendor.CompanyName + "-" + DateTime.Now.ToString() + ".pdf";
            //return new FileStreamResult(stream, "application/pdf");
            
            return File(stream, "application/pdf", namafile);
        }

        //***Region Helper***\\   
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

       
    }
}