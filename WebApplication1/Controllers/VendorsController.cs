using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.MainSystem;
using WebApplication1.Models.MainSystem.MainSystemViewModel;
using WebApplication1.Models.ViewModel;
using WebApplication1.Models.BalimoonBML;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;

namespace WebApplication1.Controllers
{
    public class VendorsController : Controller
    {
        private readonly SignInManager<Microsoft.AspNetCore.Identity.IdentityUser> _signInManager;
        private readonly UserManager<Microsoft.AspNetCore.Identity.IdentityUser> _userManager;
        private readonly MainSystemDevelopContext _MainSystemContext;
        private readonly BalimoonBMLContext _bmlContext;

        public VendorsController(
            SignInManager<Microsoft.AspNetCore.Identity.IdentityUser> signInManager,
            MainSystemDevelopContext mainSystemContext,
            UserManager<Microsoft.AspNetCore.Identity.IdentityUser> userManager,
            BalimoonBMLContext balimoonBMLContext
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _MainSystemContext = mainSystemContext;
            _bmlContext = balimoonBMLContext;
        }

        [AuthorizedAction]
        public IActionResult Index()
        {
            var AspVendorViewModel = new AspVendorViewModel();
            AspVendorViewModel.VendorList = from vendor in _MainSystemContext.AspNetVendor select vendor;
            
            return View(AspVendorViewModel);
        }

        //[AuthorizedLogin]
        public IActionResult Show(string adhfliewgfblbksdfUEOfbiualbgfE1038RYHWEO)
        {
            //check user login
            if (HttpContext.Session.GetString("email") == null)
            {
                //return Redirect("/Account/Login");
                var returnurl = "/Vendors/Show?adhfliewgfblbksdfUEOfbiualbgfE1038RYHWEO=" + adhfliewgfblbksdfUEOfbiualbgfE1038RYHWEO;
                return RedirectToAction("Login", new RouteValueDictionary(
                new { Controller = "Account", Action = "Login", returnUrl = returnurl }
                ));
            }

            var getId = adhfliewgfblbksdfUEOfbiualbgfE1038RYHWEO;
            var getVendors = _MainSystemContext.AspNetVendor.FirstOrDefault(a => Convert.ToString(a.Id) == getId);
            if(getVendors == null)
            {
                return View("_NotFound");
            }
            var getVendor = (from vendors in _MainSystemContext.AspNetVendor
                             join users in _MainSystemContext.AspNetUsers
                             on vendors.UserId equals users.UserName
                             where Convert.ToString(vendors.Id) == getId
                             select new AspVendorViewModel
                             {
                                 Users = users,
                                 VendorDetail = vendors
                             }).FirstOrDefault();
            if(getVendor== null)
            {
                return View("_NotFound");
            }
            var pict = getVendor.Users.Picture;
            if (pict == null)
            {
                pict = "avatar5.png";
            }
            //Attempt Data To Select List
            //****Contries****\\
            List<Models.BalimoonBML.CountryRegion> Country = _bmlContext.CountryRegion.ToList();
            ViewBag.ListOfCountries = new SelectList(Country, "Code", "Name");

            //****PaymentTerms****
            List<Models.BalimoonBML.PaymentTerms> paymentTerms = _bmlContext.PaymentTerms.ToList();
            ViewBag.ListOfPaymentTerms = new SelectList(paymentTerms, "Code", "Description");

            //****Vendor Posting Group****\\
            List<Models.BalimoonBML.VendorPostingGroup> vendorPostingGroups = _bmlContext.VendorPostingGroup.ToList();
            ViewBag.ListOfVendorPostingGroup = new SelectList(vendorPostingGroups, "Code", "Code");

            //****Gen Business Posting Group****\\
            List<Models.BalimoonBML.GenBusinessPostingGroup> GenBusPostingGroups = _bmlContext.GenBusinessPostingGroup.ToList();
            ViewBag.ListOfGenBusPostingGroup = new SelectList(GenBusPostingGroups, "Code", "Description");

            //****VAT Business Posting Group****\\
            List<Models.BalimoonBML.VatbusinessPostingGroup> VatBusPostingGroups = _bmlContext.VatbusinessPostingGroup.ToList();
            ViewBag.ListOfVATBusPostingGroup = new SelectList(VatBusPostingGroups, "Code", "Description");

            //****Location Code****\\
            List<Models.BalimoonBML.Locations> locations = _bmlContext.Locations.ToList();
            ViewBag.ListOfLocationCode = new SelectList(locations, "LocationCode", "LocationName");

            ViewBag.Pict = pict;
            return View(getVendor);
        }

        public IActionResult DownloadPDFVendorForOtherUseraaWFSMweqlacvghdasny(string wiudadvgvasdmelpwtanyqeqead)
        {
            var getParam = wiudadvgvasdmelpwtanyqeqead;
            if(getParam == null)
            {
                return View("_NotFound");
            }
            var getId = Convert.ToInt32(getParam);
            var _vendors = _MainSystemContext.AspNetVendor.FirstOrDefault(a => a.Id == getId);
            if(_vendors == null)
            {
                return View("_NotFound");
            }
            var file = _vendors.FileLocation;
            if(file == null)
            {
                return View("_NotFound");
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Pdf/Vendor/");
            var stream = new FileStream(@"" + path + "" + file, FileMode.Open);
            //byte[] fileBytes = System.IO.File.ReadAllBytes(path+""+file);
            //return new FileContentResult(fileBytes, "application/pdf");
            var namafile = _vendors.CompanyName + "-" + DateTime.Now.ToString() + ".pdf";
            //return new FileStreamResult(stream, "application/pdf");

            return File(stream, "application/pdf", namafile);
        }

        public IActionResult ActivateVendor(AspVendorViewModel model)
        {
            var id = model.VendorDetail.Id;
            //get vendor with this id
            var getVendor = _MainSystemContext.AspNetVendor.FirstOrDefault(a => a.Id == id);
            if(getVendor == null)
            {
                return RedirectToAction("Show", new RouteValueDictionary(
                new { Controller = "Vendors", Action = "Show", adhfliewgfblbksdfUEOfbiualbgfE1038RYHWEO = id }
                ));
            }

            //update vendor and activate
            getVendor.PaymentTerms = model.VendorDetail.PaymentTerms;
            getVendor.VendorPostingGroup = model.VendorDetail.VendorPostingGroup;
            getVendor.GenBusPostingGroup = model.VendorDetail.GenBusPostingGroup;
            getVendor.VATBusPostingGroup = model.VendorDetail.VATBusPostingGroup;
            getVendor.LocationCode = model.VendorDetail.LocationCode;
            getVendor.IsActive = "Yes";
            
            _MainSystemContext.Update(getVendor);
            _MainSystemContext.SaveChanges();

            //Tambahkan Vendor baru di tabel BML
            if(getVendor.VendorNo == null || getVendor.VendorNo == "")
            {
                //Generate Vendor No new
                string Vnumbers = GeneratevendorNo();

                //lakukan Insert Ke db Vendors di BML Context
                var Vendors = new Vendors
                {
                    VendorNo = Vnumbers,
                    VendorName = getVendor.CompanyName,
                    VendorName2 = "",
                    VendorAddress = getVendor.Address,
                    VendorAddress2 = "",
                    VendorCity = getVendor.VendorCity,
                    VendorContact = getVendor.ContactName,
                    VendorPhoneNo = getVendor.Contact,
                    VendorFaxNo = getVendor.VendorFaxNo,
                    VendorTelexNo = "",
                    OurAccountNo = "",
                    TerritoryCode = "",
                    GlobalDimension1Code = "",
                    GlobalDimension2Code = "",
                    BudgetedAmount = 0,
                    VendorPostingGroup = model.VendorDetail.VendorPostingGroup,
                    CurrencyCode = getVendor.Currency,
                    LanguageCode = "",
                    StatisticsGroup = 0,
                    PaymentTermsCode = model.VendorDetail.PaymentTerms,
                    FinChargeTermsCode = "",
                    PurchaserCode = "",
                    ShipmentMethodCode = "",
                    ShippingAgentCode = "",
                    InvoiceDiscCode = "",
                    Country = getVendor.Country,
                    Blocked = 0,
                    PaytoVendorNo = "",
                    Priority = 0,
                    PaymentMethodCode = "",
                    ApplicationMethod = 0,
                    PricesIncludingVat = 0,
                    TelexAnswerBack = "",
                    VatregistrationNo = "",
                    GenBusPostingGroup = model.VendorDetail.GenBusPostingGroup,
                    MobileNo = getVendor.InvoiceContact,
                    PostCode = "",
                    Email = getVendor.ContactEmail,
                    HomePage = "",
                    NoSeries = "",
                    TaxAreaCode = "",
                    TaxLiable = 0,
                    VatbusPostingGroup = model.VendorDetail.VATBusPostingGroup,
                    BlockPaymentTolerance = 0,
                    IcpartnerCode = "",
                    PrepaymentPercent = 0,
                    PrimaryContactNo = "",
                    ResponsibilityCenter = "",
                    LocationCode = model.VendorDetail.LocationCode,
                    LeadTimeCalculation = "",
                    BaseCalendarCode = "",
                    RtcfilterField = 0,
                    BuyerGroupCode = "",
                    BuyerId = "",
                    RowStatus = 0,
                    CreatedBy = _userManager.GetUserName(User),
                    CreatedTime = DateTime.Now,
                    Swiftcode = getVendor.Swiftcode,
                };
                _bmlContext.Vendors.Add(Vendors);
                _bmlContext.SaveChanges();

                //isi vendors no di aspnetvendor tabel
                getVendor.VendorNo = Vnumbers;
                _MainSystemContext.AspNetVendor.Update(getVendor);
                _MainSystemContext.SaveChanges();
            }
            else
            {
                var getVendorsInBMLContext = _bmlContext.Vendors.FirstOrDefault(a => a.VendorNo == getVendor.VendorNo);
                //lakukan update
                getVendorsInBMLContext.VendorName = getVendor.CompanyName;
                getVendorsInBMLContext.VendorAddress = getVendor.Address;
                getVendorsInBMLContext.VendorCity = getVendor.VendorCity;
                getVendorsInBMLContext.VendorContact = getVendor.ContactName;
                getVendorsInBMLContext.VendorPhoneNo = getVendor.Contact;
                getVendorsInBMLContext.VendorFaxNo = getVendor.VendorFaxNo;
                getVendorsInBMLContext.VendorPostingGroup = model.VendorDetail.VendorPostingGroup;
                getVendorsInBMLContext.CurrencyCode = getVendor.Currency;
                getVendorsInBMLContext.PaymentTermsCode = model.VendorDetail.PaymentTerms;
                getVendorsInBMLContext.Country = getVendor.Country;
                getVendorsInBMLContext.GenBusPostingGroup = model.VendorDetail.GenBusPostingGroup;
                getVendorsInBMLContext.MobileNo = getVendor.InvoiceContact;
                getVendorsInBMLContext.Email = getVendor.ContactEmail;
                getVendorsInBMLContext.VatbusPostingGroup = model.VendorDetail.VATBusPostingGroup;
                getVendorsInBMLContext.LocationCode = model.VendorDetail.LocationCode;
                getVendorsInBMLContext.Swiftcode = getVendor.Swiftcode;
                getVendorsInBMLContext.LastModifiedBy = _userManager.GetUserName(User);
                getVendorsInBMLContext.LastModifiedTime = DateTime.Now;
                _bmlContext.Vendors.Update(getVendorsInBMLContext);
                _bmlContext.SaveChanges();
            }

            return RedirectToAction("Show", new RouteValueDictionary(
                new {Controller = "Vendors", Action = "Show", adhfliewgfblbksdfUEOfbiualbgfE1038RYHWEO = id }
                ));
        }

        //Class Untuk Generate Vendor No
        public string GeneratevendorNo()
        {
            var VendorNo = string.Empty;
            var getBMLVendor = _bmlContext.Vendors.Max(a => a.VendorId);
            if (getBMLVendor == 0)
            {
                VendorNo = "V00001";
            }
            else
            {
                var Vendors = _bmlContext.Vendors.Where(a => a.VendorId == getBMLVendor).Max(a => a.VendorNo);
                char[] trimmed = { 'V' };
                var VendorsNoNow = Vendors.Trim(trimmed);
                int OldvendorsNo = Convert.ToInt32(VendorsNoNow);
                int getValues = OldvendorsNo + 1;

                if (getValues < 10)
                {
                    VendorNo = "V0000" + getValues;
                }
                else if (getValues < 100)
                {
                    VendorNo = "V000" + getValues;
                }
                else if (getValues < 1000)
                {
                    VendorNo = "V00" + getValues;
                }
                else if (getValues < 10000)
                {
                    VendorNo = "V0" + getValues;
                }
                else
                {
                    VendorNo = "V" + getValues;
                }
            }
            return VendorNo;
        } 

        public ActionResult DeactivateUsersywbccbhs3u2ioiueewue812e(string jdjjdfbb23io2idvdcmelaskjonyp)
        {
            var id = jdjjdfbb23io2idvdcmelaskjonyp;
            var getVendor = _MainSystemContext.AspNetVendor.FirstOrDefault(a => Convert.ToString(a.Id) == id);
            if(getVendor == null)
            {
                return RedirectToAction("Show", new RouteValueDictionary(
               new { Controller = "Vendors", Action = "Show", adhfliewgfblbksdfUEOfbiualbgfE1038RYHWEO = id }
               ));
            }

            //ketika vendor ditemukan, maka nonaktifkan vendor
            getVendor.IsActive = "No";
            _MainSystemContext.Update(getVendor);
            _MainSystemContext.SaveChanges();

            return RedirectToAction("Show", new RouteValueDictionary(
               new { Controller = "Vendors", Action = "Show", adhfliewgfblbksdfUEOfbiualbgfE1038RYHWEO = id }
               ));
        }

        [AuthorizedAction]
        public IActionResult VendorList()
        {
            var _mainSystemContextVendor = _MainSystemContext.AspNetVendor.ToList();
            var _bmlVendors = _bmlContext.Vendors.ToList();
            var query = (from vendor in _bmlVendors
                         join aspVendor in _mainSystemContextVendor on vendor.VendorNo equals aspVendor.VendorNo
                         into aspvendors
                         from aspVendor in aspvendors.DefaultIfEmpty()
                         select new VendorViewModel
                         {
                             aspVendors = aspVendor,
                             vendors = vendor
                         }).ToList();
            if(query == null)
            {
                return View("_NotFound");
            }

            return View(query);
        }

        [AuthorizedAction]
        public IActionResult OfflineList()
        {
            var _mainSystemContextVendor = _MainSystemContext.AspNetVendor.ToList();
            var _bmlVendors = _bmlContext.Vendors.ToList();
            var query = new VendorViewModel();

            query.vendorList = from bmlv in _bmlVendors
                         where !(from aspv in _mainSystemContextVendor select aspv.VendorNo)
                                 .Contains(bmlv.VendorNo)
                         select bmlv;
                        
            return View(query);
        }

        public IActionResult ShowOfflineVendorinBmlContext(string kirtevdamsdbcpzciajsdhgvnzbxcbajshdfgqwyriqiryew)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                //return Redirect("/Account/Login");
                var returnurl = "/Vendors/ShowOfflineVendorinBmlContext?kirtevdamsdbcpzciajsdhgvnzbxcbajshdfgqwyriqiryew=" + kirtevdamsdbcpzciajsdhgvnzbxcbajshdfgqwyriqiryew;
                return RedirectToAction("Login", new RouteValueDictionary(
                new { Controller = "Account", Action = "Login", returnUrl = returnurl }
                ));
            }

            var getId = kirtevdamsdbcpzciajsdhgvnzbxcbajshdfgqwyriqiryew;
            //Dapatkan Vendor berdasarkan Id Vendor yang diberikan\\
            var getVendor = new VendorViewModel();
            getVendor.vendors = _bmlContext.Vendors.FirstOrDefault(a => a.VendorId == Convert.ToInt32(getId));
            if(getVendor.vendors == null)
            {
                return View("_NotFound");
            }
            ViewBag.NamaVendor = getVendor.vendors.VendorName;

            //Isi Semua DropdownList Yang Diperlukan\\
            //****Contries****\\
            List<Models.BalimoonBML.CountryRegion> Country = _bmlContext.CountryRegion.ToList();
            ViewBag.ListOfCountries = new SelectList(Country, "Code", "Name");

            //****PaymentTerms****
            List<Models.BalimoonBML.PaymentTerms> paymentTerms = _bmlContext.PaymentTerms.ToList();
            ViewBag.ListOfPaymentTerms = new SelectList(paymentTerms, "Code", "Description");

            //****Vendor Posting Group****\\
            List<Models.BalimoonBML.VendorPostingGroup> vendorPostingGroups = _bmlContext.VendorPostingGroup.ToList();
            ViewBag.ListOfVendorPostingGroup = new SelectList(vendorPostingGroups, "Code", "Code");

            //****Gen Business Posting Group****\\
            List<Models.BalimoonBML.GenBusinessPostingGroup> GenBusPostingGroups = _bmlContext.GenBusinessPostingGroup.ToList();
            ViewBag.ListOfGenBusPostingGroup = new SelectList(GenBusPostingGroups, "Code", "Description");

            //****VAT Business Posting Group****\\
            List<Models.BalimoonBML.VatbusinessPostingGroup> VatBusPostingGroups = _bmlContext.VatbusinessPostingGroup.ToList();
            ViewBag.ListOfVATBusPostingGroup = new SelectList(VatBusPostingGroups, "Code", "Description");

            //****Location Code****\\
            List<Models.BalimoonBML.Locations> locations = _bmlContext.Locations.ToList();
            ViewBag.ListOfLocationCode = new SelectList(locations, "LocationCode", "LocationName");

            //****Currency Code****\\
            //List Of Currencies
            List<Models.BalimoonBML.Currency> currencies = _bmlContext.Currency.ToList();
            ViewBag.Currency = new SelectList(currencies, "Code", "Description");

            //show Vendors Table\\

            return View(getVendor);
        }


        public IActionResult UpdateOfflineVendors(VendorViewModel model)
        {
            //Get Current Data from this Id 
            var getId = model.vendors.VendorId;
            var currentData = _bmlContext.Vendors.FirstOrDefault(a => a.VendorId == getId);
            if(currentData == null)
            {
                return View("_NotFound");
            }
            //Create Variabel oldData
            var VendorName = model.vendors.VendorName;
            var VendorCity = model.vendors.VendorCity;
            var VendorContact = model.vendors.VendorContact;
            var VendorPhoneNo = model.vendors.VendorPhoneNo;
            var FaxNo = model.vendors.VendorFaxNo;
            var MobileNo = model.vendors.MobileNo;
            var Email = model.vendors.Email;
            var Country = model.vendors.Country;
            var CurrencyCode = model.vendors.CurrencyCode;
            var PaymentTerms = model.vendors.PaymentTermsCode;
            var VendorPostingGroup = model.vendors.VendorPostingGroup;
            var GenBusPostingGroup = model.vendors.GenBusPostingGroup;
            var VATBusPostingGroup = model.vendors.VatbusPostingGroup;
            var LocationCode = model.vendors.LocationCode;
            

            //Filter Input
            if(VendorName == null)
            {
                VendorName = currentData.VendorName;
            }
            if(VendorCity == null)
            {
                VendorCity = currentData.VendorCity;
            }
            if(VendorContact == null)
            {
                VendorContact = currentData.VendorContact;
            }
            if(VendorPhoneNo == null)
            {
                VendorPhoneNo = currentData.VendorPhoneNo;
            }
            if(FaxNo == null)
            {
                FaxNo = currentData.VendorFaxNo;
            }
            if(MobileNo == null)
            {
                MobileNo = currentData.MobileNo;
            }
            if(Email == null)
            {
                Email = currentData.Email;
            }
            if(Country == null)
            {
                Country = currentData.Country;
            }
            if(CurrencyCode == null)
            {
                CurrencyCode = currentData.CurrencyCode;
            }
            if(PaymentTerms == null)
            {
                PaymentTerms = currentData.PaymentTermsCode;
            }
            if(VendorPostingGroup== null)
            {
                VendorPostingGroup = currentData.VendorPostingGroup;
            }
            if(GenBusPostingGroup == null)
            {
                GenBusPostingGroup = currentData.GenBusPostingGroup;
            }
            if(VATBusPostingGroup == null)
            {
                VATBusPostingGroup = currentData.VatbusPostingGroup;
            }
            if(LocationCode == null)
            {
                LocationCode = currentData.LocationCode;
            }

            //Update Vendors on BML DB\\

            var updateVendors = _bmlContext.Vendors.FirstOrDefault(a => a.VendorId == getId);

            updateVendors.VendorName = VendorName;
            updateVendors.VendorCity = VendorCity;
            updateVendors.VendorContact = VendorContact;
            updateVendors.VendorPhoneNo = VendorPhoneNo;
            updateVendors.VendorFaxNo = FaxNo;
            updateVendors.MobileNo = MobileNo;
            updateVendors.Email = Email;
            updateVendors.Country = Country;
            updateVendors.CurrencyCode = CurrencyCode;
            updateVendors.PaymentTermsCode = PaymentTerms;
            updateVendors.VendorPostingGroup = VendorPostingGroup;
            updateVendors.GenBusPostingGroup = GenBusPostingGroup;
            updateVendors.VatbusPostingGroup = VATBusPostingGroup;
            updateVendors.LocationCode = LocationCode;
            _bmlContext.Vendors.Update(updateVendors);
            _bmlContext.SaveChanges();

            var getVendor = new VendorViewModel();
            getVendor.vendors = _bmlContext.Vendors.FirstOrDefault(a => a.VendorId == Convert.ToInt32(getId));
            if (getVendor.vendors == null)
            {
                return View("_NotFound");
            }
            ViewBag.NamaVendor = getVendor.vendors.VendorName;

            
            return RedirectToAction("ShowOfflineVendorinBmlContext", new RouteValueDictionary(
               new { Controller = "Vendors", Action = "ShowOfflineVendorinBmlContext", kirtevdamsdbcpzciajsdhgvnzbxcbajshdfgqwyriqiryew = getId }
               ));
        }

        public IActionResult ShowAllVendorListDetail(string ILUghasFvwyeUGdmeLanyonevajhsdhjvid)
        {
            //GET Session Login
            if (HttpContext.Session.GetString("email") == null)
            {
                //return Redirect("/Account/Login");
                var returnurl = "/Vendors/ShowAllVendorListDetail?ILUghasFvwyeUGdmeLanyonevajhsdhjvid=" + ILUghasFvwyeUGdmeLanyonevajhsdhjvid;
                return RedirectToAction("Login", new RouteValueDictionary(
               new { Controller = "Account", Action = "Login", returnUrl = returnurl}
               ));
            }

            var id = ILUghasFvwyeUGdmeLanyonevajhsdhjvid;

            //Dapatkan Vendor berdasarkan Id Vendor yang diberikan\\
            var listBMLV = _bmlContext.Vendors.ToList();
            var listASPV = _MainSystemContext.AspNetVendor.ToList();
            var AllVendor = (from _listBMLV in listBMLV
                             join _listASPV in listASPV on _listBMLV.VendorNo equals _listASPV.VendorNo
                             into leftJoin
                             from _listASPV in leftJoin.DefaultIfEmpty()
                             where _listBMLV.VendorId == Convert.ToInt32(id)
                             select new VendorViewModel
                             {
                                 vendors = _listBMLV,
                                 aspVendors = _listASPV
                             }).FirstOrDefault();
            if(AllVendor== null)
            {
                return View("_NotFound");
            }

            ViewBag.NamaVendor = AllVendor.vendors.VendorName;
            return View(AllVendor);
        }
    }
}