using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApplication1.Models;
using WebApplication1.Models.BalimoonBML;
using WebApplication1.Models.MainSystem;
using WebApplication1.Models.Tools;
using WebApplication1.Models.ViewModel;

namespace WebApplication1.Controllers
{
    public class PurchaseRequestController : Controller
    {
        private readonly BalimoonBMLContext _bmlContext;
        private readonly MainSystemDevelopContext _mainSysContext;
        private readonly UserManager<Microsoft.AspNetCore.Identity.IdentityUser> _userManager;
        UploadImages _uploadImages;
        DeleteImages _deleteImages;

        public PurchaseRequestController(BalimoonBMLContext bmlContext,
            MainSystemDevelopContext mainSysCtx,
            UserManager<Microsoft.AspNetCore.Identity.IdentityUser> userManager) {
            _bmlContext = bmlContext;
            _mainSysContext = mainSysCtx;
            _userManager = userManager;
            _uploadImages = new UploadImages();
            _deleteImages = new DeleteImages();
        }

        [AuthorizedAction]
        public IActionResult Index()
        {
            var getTahun = _bmlContext.PurchaseRequisitionHeader.OrderByDescending(a=>a.RequisitionHeaderId).Select(a => a.OrderDate.Value.Year).Distinct().ToList();
            
            ViewBag.TahunPR = new SelectList(getTahun);
            return View();
        }

        public ActionResult GetPRTahunan(int? tahun)
        {
            if(tahun== null)
            {
                tahun = DateTime.Now.Year;
            }
            //date
            var months = Enumerable.Range(1, 12);
            
                        
            //PR Open
            var PROpen = (from p in _bmlContext.PurchaseRequisitionHeader
            where p.Status == 0 && p.OrderDate.Value.Year == tahun
                         group p by new { p.OrderDate.Value.Month } into g
                         select new { g.Key.Month, Count = g.Count() }).ToArray();


            string[] MyCountPROpen = (from month in months
                                from p in PROpen.Where(x => month == x.Month).DefaultIfEmpty()
                                select p == null ? "0" : p.Count.ToString()).ToArray();
            

            //PR Release
            var PRRelease = (from p in _bmlContext.PurchaseRequisitionHeader
                          where p.Status == 1 && p.OrderDate.Value.Year == tahun
                          group p by new { p.OrderDate.Value.Month } into g
                          select new { g.Key.Month, Count = g.Count() }).ToArray();

            string[] MyCountPRRelease = (from month in months
                                      from p in PRRelease.Where(x => month == x.Month).DefaultIfEmpty()
                                      select p == null ? "0" : p.Count.ToString()).ToArray();

            //PR All
            var PRAll = (from p in _bmlContext.PurchaseRequisitionHeader
                             where p.OrderDate.Value.Year == tahun
                             group p by new { p.OrderDate.Value.Month } into g
                             select new { g.Key.Month, Count = g.Count() }).ToArray();

            string[] MyCountPR = (from month in months
                                         from p in PRAll.Where(x => month == x.Month).DefaultIfEmpty()
                                         select p == null ? "0" : p.Count.ToString()).ToArray();

            //PR Pending
            var PRPending = (from p in _bmlContext.PurchaseRequisitionHeader
                             where p.Status == 2 && p.OrderDate.Value.Year == tahun
                             group p by new { p.OrderDate.Value.Month } into g
                             select new { g.Key.Month, Count = g.Count() }).ToArray();

            string[] MyCountPRPending = (from month in months
                                         from p in PRPending.Where(x => month == x.Month).DefaultIfEmpty()
                                         select p == null ? "0" : p.Count.ToString()).ToArray();


            //PR Closed
            var PRClosed = (from p in _bmlContext.PurchaseRequisitionHeader
                             where p.Status == 5 && p.OrderDate.Value.Year == tahun
                             group p by new { p.OrderDate.Value.Month } into g
                             select new { g.Key.Month, Count = g.Count() }).ToArray();

            string[] MyCountPRClosed = (from month in months
                                         from p in PRClosed.Where(x => month == x.Month).DefaultIfEmpty()
                                         select p == null ? "0" : p.Count.ToString()).ToArray();

            //PR Posted
            var PRPosted = (from p in _bmlContext.PurchaseRequisitionHeader
                            where p.Status == 6 && p.OrderDate.Value.Year == tahun
                            group p by new { p.OrderDate.Value.Month } into g
                            select new { g.Key.Month, Count = g.Count() }).ToArray();

            string[] MyCountPRPosted = (from month in months
                                        from p in PRPosted.Where(x => month == x.Month).DefaultIfEmpty()
                                        select p == null ? "0" : p.Count.ToString()).ToArray();

            //PR Reject
            var PRReject = (from p in _bmlContext.PurchaseRequisitionHeader
                            where p.Status == 7 && p.OrderDate.Value.Year == tahun
                            group p by new { p.OrderDate.Value.Month } into g
                            select new { g.Key.Month, Count = g.Count() }).ToArray();

            string[] MyCountPRReject = (from month in months
                                        from p in PRReject.Where(x => month == x.Month).DefaultIfEmpty()
                                        select p == null ? "0" : p.Count.ToString()).ToArray();

            //PR Cancelled
            var PRCancelled = (from p in _bmlContext.PurchaseRequisitionHeader
                            where p.Status == 8 && p.OrderDate.Value.Year == tahun
                            group p by new { p.OrderDate.Value.Month } into g
                            select new { g.Key.Month, Count = g.Count() }).ToArray();

            string[] MyCountPRCancelled = (from month in months
                                        from p in PRCancelled.Where(x => month == x.Month).DefaultIfEmpty()
                                        select p == null ? "0" : p.Count.ToString()).ToArray();

            //PR Pending Pre Payment
            var PendingPayment = (from p in _bmlContext.PurchaseRequisitionHeader
                                  where p.Status == 3 && p.OrderDate.Value.Year == tahun
                                  group p by new { p.OrderDate.Value.Month } into g
                                  select new { g.Key.Month, Count = g.Count()}).ToArray();

            string[] MyCountPendingPayment = (from month in months
                                              from p in PendingPayment.Where(a => month == a.Month).DefaultIfEmpty()
                                              select p == null ? "0" : p.Count.ToString()).ToArray();

            //PR Archived
            var Archived = (from a in _bmlContext.PurchaseRequisitionHeader
                            where a.Status == 4 && a.OrderDate.Value.Year == tahun
                            group a by new { a.OrderDate.Value.Month } into b
                            select new { b.Key.Month, Count = b.Count() }).ToArray();

            string[] MyArchived = (from month in months
                                   from a in Archived.Where(b => month == b.Month).DefaultIfEmpty()
                                   select a == null ? "0" : a.Count.ToString()).ToArray();

            return new JsonResult(new { myDate = months,
                myCountPROpen = MyCountPROpen,
                myCountPRRelease = MyCountPRRelease,
                myCountPR = MyCountPR,
                myCountPRPending = MyCountPRPending,
                myCountPRClosed = MyCountPRClosed,
                myCountPRPosted = MyCountPRPosted,
                myCountPRReject= MyCountPRReject,
                myCountPRCancelled = MyCountPRCancelled,
                myCountPendingPayment = MyCountPendingPayment,
                myArchived = MyArchived
            });
        }

        [AuthorizedAction]
        public IActionResult AddPRNew()
        {
            var UserId = _userManager.GetUserId(User);
            var userName = _userManager.GetUserName(User);
            ViewBag.userName = userName;
            var getUser = _mainSysContext.AspNetUsers.FirstOrDefault(a => a.Id == UserId);
            ViewBag.Department = getUser.Departement;

            //shortcut Dimension 2 code
            List<Models.BalimoonBML.DimensionValue> getShortcut2code = _bmlContext.DimensionValue.Where(a => a.DimensionCode == "PROJECT").ToList();
            ViewBag.ProjectCode = new SelectList(getShortcut2code, "DimensionValueCode", "DimensionValueName");

            //Location Code
            List<Models.BalimoonBML.Locations> getLocation = _bmlContext.Locations.ToList();
            ViewBag.LocationCode = new SelectList(getLocation, "LocationCode", "LocationName");

            //Priority
            List<Models.MainSystem.LookupField> Priority = _mainSysContext.LookupField.Where(a => a.LookupGroup == "Priority").ToList();
            ViewBag.Priority = new SelectList(Priority, "LookupCode", "LookupDescription");

            //Status
            List<Models.MainSystem.LookupField> Status = _mainSysContext.LookupField.Where(a => a.LookupGroup == "PurchaseStatus").ToList();
            ViewBag.Status = new SelectList(Status, "LookupCode", "LookupDescription");

            //Mendapat List Tabel
            var PRList = new PurchaseRequestViewModel();
            PRList.PRHeaderList = _bmlContext.PurchaseRequisitionHeader.Where(a => (a.CreatedBy == userName || a.RequesterId == userName || a.ShortcutDimension1Code == getUser.Departement) && a.Status == 0).ToList();
            
            return View(PRList);
        }

        [HttpPost]
        public IActionResult AddNewPRHeader(PurchaseRequestViewModel model)
        {
            var projectCode = model.PRHeaderDetail.ShortcutDimension2Code;
            if(projectCode == null)
            {
                projectCode = "";
            }
            //compare date
            DateTime now = DateTime.Now;
            DateTime dummy = new DateTime(1900, 1, 1);
            DateTime due = Convert.ToDateTime(model.PRHeaderDetail.DueDate);
            TimeSpan compare = due - now;
            var getDiff = compare.Days;
            if(getDiff < 14)
            {
                return Json(new { status = false, message = "The minimum date of Receipt is 14 days from now" });
            }
            else
            {
                var PRNO = newPRNO();

                //Insert Data
                var insert = new PurchaseRequisitionHeader
                {
                    RequisitionNo = PRNO,
                    DueDate = model.PRHeaderDetail.DueDate,
                    RequesterId = model.PRHeaderDetail.RequesterId,
                    ShortcutDimension1Code = model.PRHeaderDetail.ShortcutDimension1Code,
                    ShortcutDimension2Code = projectCode,
                    ExpirationDate = dummy,
                    OrderDate = now,
                    Status = 0,
                    Priority = model.PRHeaderDetail.Priority,
                    RequestNotes = model.PRHeaderDetail.RequestNotes,
                    PurchaseNo = "",
                    RowStatus = 0,
                    CreatedBy = model.PRHeaderDetail.RequesterId,
                    CreatedTime = DateTime.Now,
                    LocationCode = model.PRHeaderDetail.LocationCode
                };
                _bmlContext.PurchaseRequisitionHeader.Add(insert);
                _bmlContext.SaveChanges();

                return Json(new { status = true, message = "Data Saved succesfull" });
            }
        }

        //Untuk pindah ke form Edit Header, Add Line dan Edit Line
        public IActionResult AddPRNewDetailCSCSHTMLaqilhuiz(string ReQUest)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                var returnurl = "/PurchaseRequest/AddPRNewDetailCSCSHTMLaqilhuiz?ReQUest=" + ReQUest;
                return RedirectToAction("Login", new RouteValueDictionary(
                new { Controller = "Account", Action = "Login", returnUrl = returnurl }
                ));

            }
            var UserId = _userManager.GetUserId(User);
            var getUser = _mainSysContext.AspNetUsers.FirstOrDefault(a => a.Id == UserId);
            if (ReQUest == null)
            {
                return View("_NotFound");
            }
            else {
                ViewBag.PRNumber = ReQUest;
                PurchaseRequestViewModel model = new PurchaseRequestViewModel();
                model.PRHeaderDetail = _bmlContext.PurchaseRequisitionHeader.SingleOrDefault(a => a.RequisitionNo == ReQUest && a.Status == 0 && a.ShortcutDimension1Code == getUser.Departement);
                if(model.PRHeaderDetail == null)
                {
                    return View("_NotFound");
                }
                else
                {
                    //shortcut Dimension 2 code
                    List<Models.BalimoonBML.DimensionValue> getShortcut2code = _bmlContext.DimensionValue.Where(a => a.DimensionCode == "PROJECT").ToList();
                    ViewBag.ProjectCode = new SelectList(getShortcut2code, "DimensionValueCode", "DimensionValueName");

                    //Location Code
                    List<Models.BalimoonBML.Locations> getLocation = _bmlContext.Locations.ToList();
                    ViewBag.LocationCode = new SelectList(getLocation, "LocationCode", "LocationName");

                    //Priority
                    List<Models.MainSystem.LookupField> Priority = _mainSysContext.LookupField.Where(a => a.LookupGroup == "Priority").ToList();
                    ViewBag.Priority = new SelectList(Priority, "LookupCode", "LookupDescription");

                    //Status
                    List<Models.MainSystem.LookupField> Status = _mainSysContext.LookupField.Where(a => a.LookupGroup == "PurchaseStatus").ToList();
                    ViewBag.Status = new SelectList(Status, "LookupCode", "LookupDescription");

                    //Record Type
                    List<Models.MainSystem.LookupField> RecordType = _mainSysContext.LookupField.Where(a => a.LookupGroup == "RecordType").ToList();
                    ViewBag.RecordType = new SelectList(RecordType, "LookupCode", "LookupDescription");

                    var PRList = (from line in _bmlContext.PurchaseRequisitionLine
                                  join item in _bmlContext.Items on line.RecordNo equals item.ItemNo
                                  where line.DocumentNo == ReQUest
                                  select new ItemPRViewModel() {
                                      Item = item,
                                      PRLine = line
                                  }).ToList();
                    model.ItemsPRList = PRList;
                    
                        //_bmlContext.PurchaseRequisitionLine.Where(a => a.DocumentNo == ReQUest).ToList();
                    return View(model);
                }
               
            }
           
        }

        //Populate RecordNo Using RecordType
        public IActionResult PopulateRecordNo(int? RecordType)
        {
            var result = string.Empty;
            if(RecordType == null)
            {
                return Json(new { status = false, message = "Couldn't Get Property Type" });
            }
            else if(RecordType == 0)
            {
                return Json(new { status = true, message = "Data Successfull Get", data = result });
            }
            //GLAccount
            else if(RecordType == 1)
            {
                var data = (from GLAccount in _bmlContext.GenLedgerAccount
                            select new
                            {
                                No = GLAccount.No,
                                Description = GLAccount.Name,
                            }).ToList();
                result = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                return Json(new { status = true, message = "Data Successfull Get", data = result });
            }
            //items
            else if(RecordType == 2)
            {
                var data = (from Item in _bmlContext.Items
                            select new {
                                No = Item.ItemNo,
                                Description = Item.Description,
                            }).ToList();
                result = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                return Json(new { status = true, message = "Data Successfull Get", data = result });
            }
            //fixed assets
            else if(RecordType == 3)
            {
                var data = (from FixedAsset in _bmlContext.FixedAsset
                            select new {
                                No = FixedAsset.No,
                                Description = FixedAsset.Description,
                            }).ToList();
                result = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                return Json(new { status = true, message = "Data Successfull Get", data = result });
            }
            //Item Charge
            else if(RecordType == 4)
            {
                var data =(from charge in _bmlContext.ItemCharge
                           select new {
                               No = charge.No,
                               Description = charge.Description,
                           }).ToList();
                result = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                return Json(new { status = true, message = "Data Successfull Get", data = result });
            }
            else
            {
                return Json(new { status = false, message = "Record Type Undefine" });
            }
        }

        //Populate UoM Using RecordNo
        public IActionResult PopulateUoM(string recNo, string recType)
        {
            var recordNo = recNo;
            var recTyp = recType;
            var Result1 = string.Empty;
            var Result2 = string.Empty;
            //Get UoM
            //jika recType = 1
            if(recTyp == "1")
            {
                //GL Record table
                var GLtable = (from GL in _bmlContext.GenLedgerAccount
                               where GL.No == recordNo
                               select new
                               {
                                   UoM = " ",
                                   VatBus = " ",
                                   VatProd = " ",
                                   invenPost = " ",
                                   itemCat = " ",
                                   prodGroup = " ",
                                   GenProdPost = " ",
                                   cost = 0
                               }).SingleOrDefault();
                Result2 = JsonConvert.SerializeObject(GLtable, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

                return Json(new { status = true, data1 = Result1, data2 = Result2});
            }
            else if(recTyp == "2") {
                //Item & item UoM table
                var UoMAll = (from uom in _bmlContext.ItemUnitOfMeasures
                              join UnitOfMeasures in _bmlContext.UnitOfMeasures on uom.Uomcode equals UnitOfMeasures.Uomcode
                              where uom.ItemCode == recordNo
                              select new {
                                  code = uom.Uomcode,
                                  name = UnitOfMeasures.Uomdescription
                              }).ToList();
                    //_bmlContext.ItemUnitOfMeasures.Where(a=>a.ItemCode == recordNo).ToList();
                var UoMDefault = (from item in _bmlContext.Items
                                  where item.ItemNo == recordNo
                                  select new { 
                                      UoM = item.BaseUnitofMeasure,
                                      VatBus = " ",
                                      VatProd = item.VatprodPostingGroup,
                                      invenPost = item.InventoryPostingGroup,
                                      itemCat = item.ItemCategoryCode,
                                      prodGroup = item.ProductGroupCode,
                                      GenProdPost = item.GenProdPostingGroup,
                                      cost = item.LastDirectCost
                                  }).SingleOrDefault();
                Result1 = JsonConvert.SerializeObject(UoMAll, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                Result2 = JsonConvert.SerializeObject(UoMDefault, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

                return Json(new { status = true, data1 = Result1, data2 = Result2 });
            }
            else if(recTyp == "3")
            {
                //Fixed Assets table
                var FixedAssets = (from FA in _bmlContext.FixedAsset
                                   where FA.No == recordNo
                                   select new
                                   {
                                       UoM = " ",
                                       VatBus = " ",
                                       VatProd = FA.FapostingGroup,
                                       invenPost = " ",
                                       itemCat = " ",
                                       prodGroup = " ",
                                       GenProdPost = " ",
                                       cost = 0
                                   }).SingleOrDefault();

                Result2 = JsonConvert.SerializeObject(FixedAssets, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

                return Json(new { status = true, data1 = Result1, data2 = Result2 });
            }
            else if(recTyp == "4")
            {
                //Item charge table
                var itemCharge = (from charge in _bmlContext.ItemCharge
                                  where charge.No == recordNo
                                  select new
                                  {
                                      UoM = " ",
                                      VatBus = " ",
                                      VatProd = charge.VatprodPostingGroup,
                                      invenPost = " ",
                                      itemCat = " ",
                                      prodGroup = " ",
                                      GenProdPost = " ",
                                      cost = 0,
                                  }).SingleOrDefault();

                Result2 = JsonConvert.SerializeObject(itemCharge, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

                return Json(new { status = true, data1 = Result1, data2 = Result2 });
            }
            else
            {
                return Json(new { status = true, data1 = Result1, data2 = Result2 });
            }
        }

        //JSON Edit Header
        [HttpPost]
        public IActionResult EditPRHeader (PurchaseRequestViewModel model)
        {
            var projectCode = model.PRHeaderDetail.ShortcutDimension2Code;
            if (projectCode == null)
            {
                projectCode = "";
            }

            //GET PR from DB
            var getPR = _bmlContext.PurchaseRequisitionHeader.SingleOrDefault(a => a.RequisitionNo == model.PRHeaderDetail.RequisitionNo);
            if(getPR == null)
            {
                return Json(new { status = false, message = "Couldn't Find PR with PR Number : "+model.PRHeaderDetail.RequisitionNo });
            }
            else
            {
                //compare Date Time
                DateTime orderdate = Convert.ToDateTime(getPR.OrderDate);
                DateTime dueDate = Convert.ToDateTime(model.PRHeaderDetail.DueDate);
                TimeSpan compare = dueDate - orderdate;
                var getDiff = compare.Days;
                if (getDiff < 14)
                {
                    return Json(new { status = false, message = "The minimum Date of Receipt is 14 days, but this PR only "+getDiff+" days" });
                    }
                else
                {
                    //lakukan update
                    getPR.DueDate = dueDate;
                    getPR.RequestNotes = model.PRHeaderDetail.RequestNotes;
                    getPR.LocationCode = model.PRHeaderDetail.LocationCode;
                    getPR.ShortcutDimension2Code = projectCode;
                    getPR.Priority = model.PRHeaderDetail.Priority;
                    getPR.LastModifiedBy = _userManager.GetUserName(User);
                    getPR.LastModifiedTime = DateTime.Now;
                    _bmlContext.PurchaseRequisitionHeader.Update(getPR);
                    _bmlContext.SaveChanges();
                    
                    return Json(new { status = true, message = "Your File Saved Successfull" });
                }
                
            }
            
        }
        
        //JSON Add Line Items
        [HttpPost]
        public async Task<IActionResult> AddLineItems(IFormFile gambarItem, PurchaseRequestViewModel model)
        {
            //make link to the PR images
            var link1 = "wwwroot/dist/AppsImages/PR";
            var link2 = "wwwroot\\dist\\AppsImages\\PR\\";
            string resultupload = "";
            //Upload The Images
            string upload = await _uploadImages.ImagesUpload(gambarItem, link1, link2);
            if(upload == "")
            {
                resultupload = "";
            }
            else if (upload != "File must be either .jpg, .jpeg, .png and Maximum Size is 4MB")
            {
                resultupload = upload;
            }
            else
            {
                resultupload = "";
            }
            //get not required input
            var vatProd = model.PRLineDetail.VatprodPostingGroup;
            var Desc2 = model.PRLineDetail.Description2;
            var UoM = model.PRLineDetail.UnitofMeasure;
            var VatBus = model.PRLineDetail.VatbusPostingGroup;
            var InvenPost = model.PRLineDetail.InventoryPostingGroup;
            var ItemCatCode = model.PRLineDetail.ItemCategoryCode;
            var ProductGroup = model.PRLineDetail.ProductGroupCode;
            var GenProd = model.PRLineDetail.GenProdPostingGroup;

            var a = gambarItem;
            //Change null to ""
            if (vatProd == null)
            {
                vatProd = "";
            }
            if (Desc2 == null)
            {
                Desc2 = "";
            }
            if(UoM == null)
            {
                UoM = "";
            }
            if (VatBus == null)
            {
                VatBus = "";
            }
            if(InvenPost== null)
            {
                InvenPost = "";
            }
            if(ItemCatCode == null)
            {
                ItemCatCode = "";
            }
            if(ProductGroup == null)
            {
                ProductGroup = "";
            }
            if(GenProd == null)
            {
                GenProd = "";
            }
            var SeqLineNumber = newSeqLineNoPR(model.PRLineDetail.DocumentNo);

            //Get Header PR
            var Header = _bmlContext.PurchaseRequisitionHeader.SingleOrDefault(a => a.RequisitionNo == model.PRLineDetail.DocumentNo);
            //Add PR Line to DB
            PurchaseRequisitionLine prLine = new PurchaseRequisitionLine()
            {
                RequisitionheaderId = Header.RequisitionHeaderId,
                DocumentNo = model.PRLineDetail.DocumentNo,
                SeqLineNo = SeqLineNumber,
                RecordType = model.PRLineDetail.RecordType,
                RecordNo = model.PRLineDetail.RecordNo,
                Description = model.PRLineDetail.Description,
                Description2 = Desc2,
                Quantity = model.PRLineDetail.Quantity,
                UnitofMeasure = UoM,
                VendorNo = "",
                DirectUnitCost = model.PRLineDetail.DirectUnitCost,
                VatbusPostingGroup = VatBus,
                VatprodPostingGroup = vatProd,
                InventoryPostingGroup = InvenPost,
                DueDate = Header.DueDate,
                RequesterId = _userManager.GetUserName(User),
                Confirmed = 0,
                ShortcutDimension1Code = Header.ShortcutDimension1Code,
                ShortcutDimension2Code = Header.ShortcutDimension2Code,
                LocationCode = Header.LocationCode,
                RecurringMethod = 0,
                ExpirationDate = Header.ExpirationDate,
                RecurringFrequency = "",
                OrderDate = Header.OrderDate,
                VendorItemNo = "",
                SalesOrderNo = "",
                SelltoCustomerNo = "",
                ShiptoCode = "",
                OrderAddressCode = "",
                CurrencyCode = "IDR",
                CurrencyFactor = 1,
                ProdOrderNo = "",
                VariantCode = "",
                BinCode = "",
                QtyperUnitofMeasure = 1,
                UnitofMeasureCode = UoM,
                QuantityBase = model.PRLineDetail.Quantity,
                DemandType = 0,
                DemandSubtype = 0,
                DemandOrderNo = "",
                DemandLineNo = 0,
                DemandRefNo = 0,
                Status = Header.Status,
                DemandDate = Header.ExpirationDate,
                DemandQuantity = 0,
                DemandQuantityBase = 0,
                NeededQuantity = 0,
                NeededQuantityBase = 0,
                Reserve = 0,
                QtyperUomdemand = 0,
                UnitOfMeasureCodeDemand = "",
                SupplyFrom = "",
                OriginalItemNo = "",
                OriginalVariantCode = "",
                Level = 0,
                DemandQtyAvailable = 0,
                UserId = "",
                ItemCategoryCode = ItemCatCode,
                Nonstock = 0,
                PurchasingCode = "",
                ProductGroupCode = ProductGroup,
                TransferfromCode = "",
                TransferShipmentDate = Header.ExpirationDate,
                LineDiscountPercent = 0,
                OrderPromisingSubLineNo = 0,
                RoutingNo = "",
                OperationNo = "",
                WorkCenterNo = "",
                ProdOrderLineNo = 0,
                Mpsorder = 0,
                PlanningFlexibility = 0,
                RoutingReferenceNo = 0,
                GenProdPostingGroup = GenProd,
                GenBusinessPostingGroup = "",
                LowLevelCode = 0,
                ProductionBomno = "",
                RoutingVersionCode = "",
                RoutingType = 0,
                OriginalQuantity = model.PRLineDetail.Quantity,
                FinishedQuantity = 0,
                RemainingQuantity = model.PRLineDetail.Quantity,
                OriginalDueDate = Header.ExpirationDate,
                ScrapPercent = 0,
                StartingDate = Header.ExpirationDate,
                StartingTime = Header.ExpirationDate,
                EndingDate = Header.ExpirationDate,
                EndingTime = Header.ExpirationDate,
                ProductionBomversionCode = "",
                IndirectCostPercent = 0,
                OverheadRate = 0,
                UnitCost = model.PRLineDetail.DirectUnitCost,
                CostAmount = (model.PRLineDetail.Quantity * model.PRLineDetail.DirectUnitCost),
                ReplenishmentSystem = 0,
                RefOrderNo = "",
                RefOrderType = 0,
                RefOrderStatus = 0,
                RefLineNo = 0,
                NoSeries = "",
                FinishedQtyBase = 0,
                QuantityPo = 0,
                RemainingQtyBase = model.PRLineDetail.Quantity,
                RelatedtoPlanningLine = 0,
                PlanningLevel = 0,
                PlanningLineOrigin = 0,
                ActionMessage = 0,
                AcceptActionMessage = 0,
                NetQuantityBase = 0,
                StartingDateTime = Header.ExpirationDate,
                EndingDateTime = Header.ExpirationDate,
                OrderPromisingId = "",
                OrderPromisingLineNo = 0,
                OrderPromisingLineId = 0,
                Priority = Header.Priority,
                RequestNotes = Header.RequestNotes,
                RowStatus = 0,
                CreatedBy = _userManager.GetUserName(User),
                CreatedTime = DateTime.Now,
                Picture = resultupload            
            };
            _bmlContext.PurchaseRequisitionLine.Add(prLine);
            _bmlContext.SaveChanges();

            return Json(new { status = true, message = "Your File Saved Successfull" });
        }

        //JSon Get PR Line Item
        [HttpGet]
        public IActionResult FindPRIDOpen(int? Id)
        {
            var result = string.Empty;
            var line = _bmlContext.PurchaseRequisitionLine.SingleOrDefault(a => a.RequisitionLineId == Id);
            result = JsonConvert.SerializeObject(line, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(result);
        }
        //PRNumber Auto Generate
        public string newPRNO()
        {
            var PRNO = "";
            var getReqNo = _bmlContext.PurchaseRequisitionHeader.Max(a => a.RequisitionNo);
            
            if(getReqNo== null)
            {
                PRNO = "PR-000001";
            }
            else
            {
                char[] trimmed = { 'P', 'R', '-' };
                var PRTrimmed = getReqNo.Trim(trimmed);
                int PRRecent = Convert.ToInt32(PRTrimmed);
                int PRNow = PRRecent + 1;
                if (PRNow < 10)
                {
                    PRNO = "PR-00000" + PRNow;
                }
                else if (PRNow < 100)
                {
                    PRNO = "PR-0000" + PRNow;
                }
                else if (PRNow < 1000)
                {
                    PRNO = "PR-000" + PRNow;
                }
                else if (PRNow < 10000)
                {
                    PRNO = "PR-00" + PRNow;
                }
                else if (PRNow < 100000)
                {
                    PRNO = "PR-0" + PRNow;
                }
                else
                {
                    PRNO = "PR-" + PRNow;
                }
            }
            
            return (PRNO);
        }

        //Generate New SeqLine No in PR
        public int newSeqLineNoPR(string PRNO)
        {
            int seqLineNo = 0;
            var getEmptySeqNo = _bmlContext.PurchaseRequisitionLine.Where(a => a.DocumentNo == PRNO);
            if(getEmptySeqNo == null)
            {
                seqLineNo = 10000;
            }
            else
            {
                int getCurrentSeqNo = getEmptySeqNo.Max(a => a.SeqLineNo);
                seqLineNo = getCurrentSeqNo + 10000;
            }
            return (seqLineNo);
        }
    }
}