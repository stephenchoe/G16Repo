using G16_2013.Models.MemberModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PagedList;
using PagedList.Mvc;

namespace G16MG.Controllers
{
    public class CompanyController : BaseController
    {
        //
        // GET: /Company/

        public ActionResult Index(int? page, int searchWay = 0, int parentCompany = -1)
        {
            ViewBag.SearchWay = searchWay;

            ViewBag.ParentCompany = parentCompany;

            List<CompanyViewModel> listCompanyView = memberBL.GetCompanyBySearch(searchWay, parentCompany);
            if (listCompanyView == null)
            {
                return View();
            }
            return View(listCompanyView.ToPagedList(page ?? 1, 10));
        }
        [HttpPost]
        public ActionResult Index(CompanySearchInput searchInput)
        {
            ViewBag.SearchWay = searchInput.SearchWayId;

            ViewBag.ParentCompany = searchInput.ParentCompanyId;

            int page = 1;

            return Index(page, searchInput.SearchWayId, searchInput.ParentCompanyId);
        }
        public ActionResult Create()
        {
            var input = memberBL.GetCompanyCreateInputModel();
            input.ManagerInput.AllowSelect = true;
            return View(input);

        }
        public ActionResult Detail(int id, int view = 0)
        {
            var companyView = memberBL.GetCompanyViewModel(id);
            if (companyView == null)
            {
                //do something
            }
            TempData["view"] = view;
            return View(companyView);
        }

        //protected IPagedList<AE> GetPagedNames(int? page)
        //{
        //    // return a 404 if user browses to before the first page
        //    if (page.HasValue && page < 1)
        //        return null;

        //    // retrieve list from database/whereverand
        //    var listUnpaged = memberBL.GetAEs();


        //    // page the list
        //    const int pageSize = 1;
        //    var listPaged = listUnpaged.ToPagedList(page ?? 1, pageSize);

        //    // return a 404 if user browses to pages beyond last page. special case first page if no items exist
        //    if (listPaged.PageNumber != 1 && page.HasValue && page > listPaged.PageCount)
        //        return null;

        //    return listPaged;
        //}







        #region Read
        public PartialViewResult ReadCompany(int id)
        {
            var viewModel = memberBL.GetCompanyViewModel(id);
            string viewPath = DisplayTemplateFolderPath + "Company/Company.cshtml";
            return PartialView(viewPath, viewModel);
        }

        public PartialViewResult ReadCompanyManager(int? id)
        {
            string viewPath = DisplayTemplateFolderPath + "Company/CompanyManager.cshtml";
            if (id == null)
            {
                var view = new PersonViewModel()
                {
                    InputTitle = "無聯絡人資料",
                    ContactInfoView = new PersonContactInfoViewModel()
                };
                return PartialView(viewPath, view);
            }

            int personId = Convert.ToInt32(id);
            var personView = memberBL.GetPersonViewModel(personId, personId);
            personView.InputTitle = "公司聯絡人資料";
            personView.AllowEdit = true;
            personView.ContactInfoView.AllowEdit = true;
            return PartialView(viewPath, personView);
        }
        public PartialViewResult ReadContractRecords(int companyId)
        {
            var contractList = memberBL.GetCompanyContractViewList(companyId);
            string viewPath = DisplayTemplateFolderPath + "Company/CompanyContractList.cshtml";
            return PartialView(viewPath, contractList);
        }
        public PartialViewResult ReadContract(int companyId, int id)
        {
            int contractId = id;
            var inputModel = memberBL.GetCompanyContractInputModel(companyId, id);

            string viewPath = DisplayTemplateFolderPath + "Company/CompanyContractGridView.cshtml";
            return PartialView(viewPath, inputModel);
        }
        public PartialViewResult ReadAERecords(int companyId, int page = 1, int searchWayId = 0, string keyWord = "")
        {
            int currentpage = 1;
            if (page > 1) currentpage = page;
            int pageSize = 5;
            var records = memberBL.GetCompanyAEByKeyWord(companyId, searchWayId, keyWord);
            List<AECompanyView> recordViews = new List<AECompanyView>();
            if (records != null)
            {
                recordViews = memberBL.GetAECompanyRecordsView(records);
            }

            var pagedList = recordViews.ToPagedList(currentpage, pageSize);

            ViewBag.CompanyId = companyId;
            ViewBag.KeyWord = keyWord;
            ViewBag.SearchWay = searchWayId;

            string viewPath = DisplayTemplateFolderPath + "Company/CompanyAERecordList.cshtml";
            return PartialView(viewPath, pagedList);
        }
        [HttpPost]
        public PartialViewResult ReadAERecords(CompanyAERecordSearchInput searchInput)
        {
            ViewBag.SearchWay = searchInput.SearchWayId;
            string keyWord = "";
            if (searchInput.KeyWord == null) keyWord = "";
            else keyWord = searchInput.KeyWord;

            int page = 1;
            int companyId = searchInput.CompanyId;

            return ReadAERecords(companyId, page, searchInput.SearchWayId, keyWord);
        }

        public PartialViewResult ReadAECodes(int companyId, int page = 1, int searchWayId = 0, int businessType = 0)
        {
            int currentpage = 1;
            if (page > 1) currentpage = page;
            int pageSize = 5;
            List<AECode> records = memberBL.GetAECodesBySearch(companyId, searchWayId, businessType);
            List<AECodeViewModel> recordViews = new List<AECodeViewModel>();
            if (records != null)
            {
                foreach (var code in records)
                {
                    AECodeViewModel view = memberBL.GetAECodeViewModel(code);
                    recordViews.Add(view);
                }
            }

            var pagedList = recordViews.ToPagedList(currentpage, pageSize);

            ViewBag.CompanyId = companyId;
            ViewBag.SearchWay = searchWayId;
            ViewBag.BusinessType = businessType;

            string viewPath = DisplayTemplateFolderPath + "AECode/AECodeViewList.cshtml";
            return PartialView(viewPath, pagedList);

        }
        [HttpPost]
        public PartialViewResult ReadAECodes(CompanyAECodesSearchInput searchInput)
        {
            int searchWayId = searchInput.SearchWayId;
            int page = 1;
            int companyId = searchInput.CompanyId;
            int businessType = searchInput.BusinessTypeId;

            return ReadAECodes(companyId, page, searchWayId, businessType);
        }
        public PartialViewResult ReadAECodeAERecords(int id, int page = 1)
        {
            int aeCodeId = id;
            ViewBag.AECodeId = aeCodeId;
            List<AECodeAEViewModel> listAECodeAEViewModel = memberBL.ReadAECodeAERecords(aeCodeId);

            int currentpage = 1;
            if (page > 1) currentpage = page;
            int pageSize = 5;
            var pagedList = listAECodeAEViewModel.ToPagedList(currentpage, pageSize);
            string viewPath = DisplayTemplateFolderPath + "AECode/AECodeAEViewList.cshtml";
            return PartialView(viewPath, pagedList);
        }
        #endregion
        #region Add
        public PartialViewResult AddCompanyContract(int companyId)
        {
            var inputModel = memberBL.GetCompanyContractCreateInput(companyId);
            if (inputModel == null)
            {
                //do something            
            }
            string viewPath = EditorTemplateFolderPath + "Company/CompanyContract.cshtml";
            return PartialView(viewPath, inputModel);
        }
        public ActionResult AddAECode(int companyId)
        {
            var input = memberBL.GetAECodeCreateInputModel(companyId);
            input.InputTitle = "新增業務代碼";
            return PartialView("_CreateAECode", input);
        }

        public PartialViewResult DesignateAECodeToAE(int aeCodeId)
        {
            var input = memberBL.GetDesignateAECodeToAEInput(aeCodeId);
            string viewPath = EditorTemplateFolderPath + "AECode/DesignateAECodeToAE.cshtml";
            return PartialView(viewPath, input);

        }
        #endregion

        #region Edit
        public PartialViewResult EditCompany(int id)
        {
            var inputModel = memberBL.GetCompanyInputModel(id);
            if (inputModel == null)
            {
                //do something
            }
            inputModel.CurrentMode = CurrentMode.Edit;
            inputModel.ActionName = "UpdateCompany";
            inputModel.InputTitle = "修改公司資料";

            string viewPath = EditorTemplateFolderPath + "Company/Company.cshtml";
            return PartialView(viewPath, inputModel);
        }
        public PartialViewResult EditCompanyManager(int id)
        {
            var company = memberBL.GetCompanyById(id);
            if (company == null)
            {
                //do something
            }
            var inputModel = memberBL.GetCompanyCreateInputModel();
            inputModel.CompanyId = company.CompanyId;
            inputModel.ManagerId = company.ManagerId;

            inputModel.CurrentMode = CurrentMode.Edit;
            inputModel.ActionName = "UpdateCompanyManager";
            inputModel.InputTitle = "變更公司聯絡人";

            string viewPath = EditorTemplateFolderPath + "Company/CompanyManager.cshtml";
            return PartialView(viewPath, inputModel);
        }
        public PartialViewResult EditContractRecord(int companyId, int id)
        {
            int contractId = id;
            var inputModel = memberBL.GetCompanyContractInputModel(companyId, id);
            inputModel.CurrentMode = CurrentMode.Edit;

            string viewPath = EditorTemplateFolderPath + "Company/CompanyContractGrid.cshtml";
            return PartialView(viewPath, inputModel);
        }
        public PartialViewResult EditAERecord(int id)
        {
            var inputModel = memberBL.GetAECompanyInput(id);
            inputModel.CurrentMode = CurrentMode.Edit;
            inputModel.InputTitle = "修改公司AE記錄";
            string viewPath = EditorTemplateFolderPath + "Company/CompanyAE.cshtml";
            return PartialView(viewPath, inputModel);
        }
        public PartialViewResult EditAECode(int id)
        {
            var inputModel = memberBL.GetAECodeViewModel(id);
            if (inputModel == null)
            {
                //do something
            }
            inputModel.CurrentMode = CurrentMode.Edit;
            inputModel.InputTitle = "修改業務代碼資料";
            string viewPath = "_EditAECode";
            return PartialView(viewPath, inputModel);

        }
        public PartialViewResult EditAECodeAE(int aeCodeId, int id)
        {
            AECodeAEInputModel inputModel = memberBL.GetAECodeAEInputModel(aeCodeId, id);
            if (inputModel == null)
            {
                TempData["message"] = "修改失敗！";  //修改失敗 //do something
            }
            else
            {
                TempData["message"] = "修改成功！";  //修改成功
            }
            inputModel.CurrentMode = CurrentMode.Edit;
            string viewPath = EditorTemplateFolderPath + "AECode/AECodeAEGrid.cshtml";
            return PartialView(viewPath, inputModel);

        }
        public PartialViewResult CancelEditAECodeAE(int aeCodeId, int id)
        {
            AECodeAEViewModel view = memberBL.GetAECodeAEViewModel(aeCodeId, id);
            if (view == null)
            {
                //do something
            }
            string viewPath = DisplayTemplateFolderPath + "AECode/AECodeAEViewGrid.cshtml";
            return PartialView(viewPath, view);
        }
        #endregion

        #region Create
        [HttpPost]
        public ActionResult ToCreateCompany(CompanyInputModel input)
        {
            Company company = memberBL.CreateCompany(input);
            return RedirectToAction("Detail", new { id = company.CompanyId });
        }
        [HttpPost]
        public ActionResult CreateCompanyContract(CompanyContractInputModel inputModel)
        {
            var company = memberBL.CreateCompanyContractRecord(inputModel);
            if (company == null)
            {
                TempData["message"] = "新增失敗！";  //新增失敗 //do something
            }
            else
            {
                TempData["message"] = "新增成功！";  //新增成功
            }
            return RedirectToAction("Detail", new { id = company.CompanyId, view = 1 });
        }
        public ActionResult CreateAECode(AECodeInputModel input)
        {
            Company company = memberBL.CreateAECodeFromCompany(input);
            TempData["message"] = "新增成功！";
            return RedirectToAction("Detail", new { id = company.CompanyId });
        }
        [HttpPost]
        public ActionResult DesignateAECodeToAE(DesignateAECodeToAEInput inputModel)
        {
            AECode aeCode = memberBL.DesignateAECodeToAE(inputModel);
            if (aeCode == null)
            {
                TempData["message"] = "轉移失敗";  //do something
            }
            else
            {
                TempData["message"] = "轉移成功";
            }
            int companyId = aeCode.CompanyId;
            return RedirectToAction("Detail", new { id = companyId });
        }
        #endregion

        #region Update
        [HttpPost]
        public PartialViewResult UpdateCompany(CompanyInputModel inputModel)
        {
            var company = memberBL.UpdateCompany(inputModel);
            if (company == null)
            {
                TempData["message"] = "修改失敗！";    //修改失敗   //do something
            }
            else
            {
                TempData["message"] = "修改成功！";    //修改成功   //do something
            }
            return ReadCompany(company.CompanyId);
        }
        [HttpPost]
        public ActionResult UpdateCompanyManager(CompanyInputModel inputModel)
        {
            var company = memberBL.UpdateCompanyManager(inputModel);
            if (company == null)
            {
                TempData["message"] = "修改失敗！"; //修改失敗   //do something
            }
            else
            {
                TempData["message"] = "修改成功！";    //修改成功
            }
            return RedirectToAction("Detail", new { id = company.CompanyId });
        }
        [HttpPost]
        public ActionResult UpdateContractRecord(CompanyContractInputModel inputModel)
        {
            var company = memberBL.UpdateCompanyContractRecord(inputModel);
            if (company == null)
            {
                TempData["message"] = "修改失敗！";  //修改失敗    //do something
            }
            else
            {
                TempData["message"] = "修改成功！";    //修改成功
            }
            return RedirectToAction("Detail", new { id = company.CompanyId, view = 1 });
        }
        [HttpPost]
        public ActionResult UpdateAERecord(AECompanyInput input)
        {
            CompanyAE companyAERecord = memberBL.UpdateCompanyAE(input);
            if (companyAERecord == null)
            {
                TempData["message"] = "修改失敗！";    //修改失敗   //do something
            }
            else
            {
                TempData["message"] = "修改成功！";    //修改成功 
            }

            int companyId = companyAERecord.CompanyId;
            return RedirectToAction("Detail", "Company", new { id = companyId, view = 2 });
        }
        [HttpPost]
        public ActionResult UpdateAECode(AECodeInputModel input)
        {
            AECode aeCode = memberBL.UpdateAECode(input);
            if (aeCode == null)
            {
                TempData["message"] = "修改失敗！";   //修改失敗  //do something
            }
            else
            {
                TempData["message"] = "修改成功！";   //修改成功
            }
            int companyId = aeCode.CompanyId;

            return RedirectToAction("Detail", "Company", new { id = companyId, view = 0 });
        }
        [HttpPost]
        public ActionResult UpdateAECodeAE(AECodeAEInputModel input)
        {
            AECode aeCode = memberBL.UpdateAECodeAE(input);
            if (aeCode == null)
            {
                //do something
            }
            int companyId = aeCode.CompanyId;

            return RedirectToAction("Detail", "Company", new { id = companyId });
        }
        #endregion

        #region Delete
        public ActionResult DeleteAECode(int aeCodeId)
        {
            AECode aeCode = memberBL.GetAECodeById(aeCodeId);

            if (aeCode == null)
            {
                //do something
            }
            int companyId = aeCode.CompanyId;
            bool isOK = memberBL.RemoveAECode(aeCodeId);
            if (!isOK)
            {
                TempData["message"] = "刪除失敗！";    //刪除失敗 //do something
            }
            else
            {
                TempData["message"] = "刪除成功！";    //刪除成功
            }

            return RedirectToAction("Detail", new { id = companyId });
        }
        public ActionResult DeleteCompanyContract(int companyId, int id)
        {
            int isOK = memberBL.RemoveCompanyContract(companyId, id);
            if (isOK < 1)
            {
                TempData["message"] = "刪除失敗！";    //刪除失敗   //do something
            }
            else
            {
                TempData["message"] = "刪除成功！";    //刪除成功   //do something
            }
            return RedirectToAction("Detail", new { id = companyId, view = 1 });
        }
        public ActionResult DeleteAECodeAE(int aeCodeId, int id)
        {
            AECode aeCode = memberBL.GetAECodeById(aeCodeId);
            if (aeCode == null)
            {
                //do something
            }

            bool isOK = memberBL.RemoveAECodeAE(aeCodeId, id);
            if (!isOK)
            {
                TempData["message"] = "刪除失敗！";    //刪除失敗 //do something
            }
            else
            {
                TempData["message"] = "刪除成功！";    //刪除成功
            }
            return RedirectToAction("Detail", new { id = aeCode.CompanyId });
        }
        public ActionResult DeleteAERecord(int id)
        {
            CompanyAE companyAE = memberBL.GetCompanyAEById(id);
            if (companyAE == null)
            {
                //do something
            }
            int companyId = companyAE.CompanyId;
            bool isOK = memberBL.RemoveCompanyAE(id);
            if (!isOK)
            {
                TempData["message"] = "刪除失敗！";    //刪除失敗 //do something
            }
            else
            {
                TempData["message"] = "刪除成功！";    //刪除成功
            }

            return RedirectToAction("Detail", new { id = companyId, view = 2 });
        }


        #endregion






        #region  Helper
        public PartialViewResult GetCompanySearchInput(int searchWay = 0, int parentCompany = -1)
        {
            CompanySearchInput searchInput = memberBL.GetCompanySearchInput();
            searchInput.ActionName = "Index";
            searchInput.ControllerName = "Company";

            searchInput.SearchWayId = searchWay;
            searchInput.ParentCompanyId = parentCompany;
            return PartialView("_CompanyIndexSearch", searchInput);
        }
        public PartialViewResult ShowGetPersonInput(string existPersonLabelId)
        {
            var personView = new PersonViewModel();
            ViewBag.ExistPersonLabelId = existPersonLabelId;
            return PartialView("_GetPerson", personView);
        }
        public PartialViewResult GetAERecordSearchInput(int companyId, string keyWord = "", int searchWay = 0)
        {
            CompanyAERecordSearchInput searchInput = new CompanyAERecordSearchInput();
            searchInput.SearchOptions = new List<BaseOption>();
            searchInput.SearchOptions.Add(new BaseOption() { Text = "全部資料", Value = "0" });
            searchInput.SearchOptions.Add(new BaseOption() { Text = "在職中", Value = "1" });
            searchInput.SearchOptions.Add(new BaseOption() { Text = "已離職", Value = "2" });

            searchInput.KeyWord = keyWord;
            searchInput.SearchWayId = searchWay;

            searchInput.CompanyId = companyId;


            return PartialView("_IndexSearch", searchInput);

        }
        public PartialViewResult GetAECodeSearchInput(int companyId, int searchWay = 0, int businessType = 0)
        {
            var searchInput = memberBL.GetAECodeSearchInputModel();

            searchInput.SearchWayId = searchWay;
            searchInput.CompanyId = companyId;
            searchInput.BusinessTypeId = businessType;
            return PartialView("_AECodesSearch", searchInput);

        }
        public string DisplayContractStatus(bool isActive)
        {
            if (isActive) return "有效";
            return "已無效";
        }
        public ActionResult GetBranch(string cid)
        {
            int companyId = Convert.ToInt32(cid);
            var options = memberBL.GetBranchOptions(companyId);

            return PartialView("_GetOptions", options);
        }

        #endregion


    }
}
