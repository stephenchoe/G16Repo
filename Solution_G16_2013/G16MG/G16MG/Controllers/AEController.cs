using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

using G16_2013.Models.MemberModel;

namespace G16MG.Controllers
{
    public class AEController : BaseController
    {

        //GET: /AE/
        [HttpGet]
        public ActionResult Index(int? page, int searchWay = 0, string keyWord = "", string sortBy = "")
        {
            ViewBag.SearchWay = searchWay;

            ViewBag.KeyWord = keyWord;
            if (sortBy == "")
            {
                ViewBag.SortBy = "Name desc";
            }
            else
            {
                ViewBag.SortBy = "";
            }


            var AEs = memberBL.GetAEByKeyWord(searchWay, keyWord);
            switch (sortBy)
            {
                case "Name desc":
                    AEs = AEs.OrderByDescending(m => m.Person.Name);
                    break;
                default:
                    AEs = AEs.OrderBy(m => m.Person.PersonId);

                    break;
            }
            return View(AEs.ToList().ToPagedList(page ?? 1, 10));
        }
        [HttpPost]
        public ActionResult Index(SearchInputModel searchInput)
        {
            ViewBag.SearchWay = searchInput.SearchWayId;
            string keyWord = "";
            if (searchInput.KeyWord == null) keyWord = "";
            else keyWord = searchInput.KeyWord;

            int page = 1;
            string sortBy = "";

            return Index(page, searchInput.SearchWayId, keyWord, sortBy);
        }
        public ActionResult Detail(int id, int view = 0)
        {
            int aeId = id;
            int personId = id;
            var viewModel = memberBL.GetAEViewModel(id, personId);
            if (viewModel == null)
            {
                //do something
            }
            viewModel.PersonView.AllowEdit = true;
            viewModel.PersonView.ActionName = "EditPerson";
            viewModel.PersonView.CotrollerName = "Person";
            TempData["view"] = view;
            viewModel.PersonView.ContactInfoView.AllowEdit = true;
            return View(viewModel);
        }

        public ActionResult Create()
        {
            AECreateInputModel input = memberBL.GetAECreateInputModel();
            return View(input);
        }
        
        #region  Add
        public PartialViewResult DesignateCompanyToAE(int id)
        {
            var inputModel = memberBL.GetAECompanyCreateInput(id);
            inputModel.CurrentMode = CurrentMode.Insert;
            inputModel.ActionName = "ToDesignateCompanyToAE";
            inputModel.InputTitle = "指派公司";

            string viewPath = EditorTemplateFolderPath + "AE/AECompany.cshtml";
            return PartialView(viewPath, inputModel);

        }
        public PartialViewResult AddAEContract(int id)
        {
            var ae = memberBL.GetAEById(id);
            if (ae == null)
            {
                //do something
            }
            var inputModel = new AEContractInputModel();
            inputModel.AEId = ae.AEId;
            inputModel.AENameText = ae.Person.Name;
            inputModel.CurrentMode = CurrentMode.Insert;
            inputModel.ActionName = "CreateAEContractRecord";
            inputModel.InputTitle = "新增AE合約記錄";

            string viewPath = EditorTemplateFolderPath + "AE/AEContract.cshtml";
            return PartialView(viewPath, inputModel);
        }
        #endregion

        #region  Read
        public PartialViewResult ReadAEInfo(int id)
        {
            var viewModel = memberBL.GetAEViewModel(id);
            string viewPath = DisplayTemplateFolderPath + "AE/AEView.cshtml";
            return PartialView(viewPath, viewModel);

        }
        public PartialViewResult ReadAECompanyRecords(int id)
        {
            var viewModel = memberBL.GetAECompanyRecordsView(id);
            string viewPath = DisplayTemplateFolderPath + "AE/AECompanyList.cshtml";
            return PartialView(viewPath, viewModel);

        }
        public PartialViewResult ReadAEContractRecords(int id)
        {
            var viewModel = memberBL.GetAEContractRecordsView(id);

            string viewPath = DisplayTemplateFolderPath + "AE/AEContractList.cshtml";
            return PartialView(viewPath, viewModel);

        }
        public PartialViewResult ReadAEContract(int aeId, int id)
        {
            int contractId = id;
            var inputModel = memberBL.GetAEContractInput(aeId, contractId);

            string viewPath = DisplayTemplateFolderPath + "AE/AEContractGridView.cshtml";
            return PartialView(viewPath, inputModel);
        }
        #endregion

        #region Edit
        public PartialViewResult EditAECompanyRecord(int id)
        {
            var inputModel = memberBL.GetAECompanyInput(id);
            inputModel.CurrentMode = CurrentMode.Edit;
            inputModel.ActionName = "UpdateAECompanyRecord";
            inputModel.InputTitle = "修改AE公司記錄";
            string viewPath = EditorTemplateFolderPath + "AE/AECompany.cshtml";
            return PartialView(viewPath, inputModel);

        }
        public PartialViewResult EditAEContractRecord(int aeId, int id)
        {
            int contractId = id;
            var inputModel = memberBL.GetAEContractInput(aeId, contractId);
            inputModel.CurrentMode = CurrentMode.Edit;
            inputModel.ActionName = "UpdateAEContractRecord";
            inputModel.InputTitle = "修改AE合約記錄";

            //  string viewPath = EditorTemplateFolderPath + "AE/AEContract.cshtml";
            string viewPath = EditorTemplateFolderPath + "AE/AEContractGrid.cshtml";
            return PartialView(viewPath, inputModel);
        }
        #endregion


        #region Create
        [HttpPost]
        public ActionResult CreateAE(AECreateInputModel input)
        {
           
            AE ae = memberBL.CreateAE(input);
            if (ae == null)
            { 
              //do something
            }
            return RedirectToAction("Detail", new { id = ae.AEId });

        }
        //[HttpPost]
        //public ActionResult CreateAE(AEViewModel input)
        //{
        //    if (input.PersonExsit)
        //    {
        //        input.PersonInput.PersonId = input.ExsitPersonId;
        //    }

        //    AE ae = memberBL.CreateAE(input);
        //    return RedirectToAction("Detail", new { id = ae.AEId });

        //}
        [HttpPost]
        public ActionResult CreateAEContractRecord(AEContractInputModel input)
        {
            var ae = memberBL.CreateAEContractRecord(input);
            if (ae == null)
            {
                //新增失敗 //do something
                TempData["message"] = "新增失敗！";
            }
            else
            {
                TempData["message"] = "新增成功！";
            }
            return RedirectToAction("Detail", "AE", new { id = ae.AEId, view = 1 });

        }
        [HttpPost]
        public ActionResult ToDesignateCompanyToAE(AECompanyInput input)
        {
            AE ae = memberBL.DesignateCompanyToAE(input);
            TempData["message"] = "新增成功！";
            return RedirectToAction("Detail", new { id = ae.AEId, view = 2 });
        }

        #endregion

        #region Update
        public ActionResult UpdateAECompanyRecord(AECompanyInput input)
        {
            CompanyAE companyAERecord = memberBL.UpdateCompanyAE(input);
            if (companyAERecord == null)
            {
                TempData["message"] = "修改失敗！";  //修改失敗   //do something
            }
            else
            {
                TempData["message"] = "修改成功！";  //修改成功
            }

            int AEId = companyAERecord.AEId;
            return RedirectToAction("Detail", "AE", new { id = AEId, view = 2 });

        }
        public ActionResult UpdateAEContractRecord(AEContractInputModel input)
        {
            if (input.Id <= 0)
            {

            }
            var ae = memberBL.UpdateAEContract(input);
            if (ae == null)
            {
                //do something
                TempData["message"] = "修改失敗！";    //修改失敗
            }
            else
            {
                TempData["message"] = "修改成功！";    //修改成功
            }

            int AEId = ae.AEId;
            return RedirectToAction("Detail", "AE", new { id = AEId, view = 1 });

        }
        #endregion

        #region Delete

        public ActionResult DeleteAE(int id)
        {
            bool isOk = memberBL.RemoveAE(id);

            if (isOk)
            {
                //do something
                TempData["message"] = "刪除成功！";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message"] = "刪除失敗！";
                return RedirectToAction("Detail", "AE", new { id = id });
            }
        }
        public ActionResult DeleteAEContractRecord(int aeId, int id)
        {
            bool isOk = memberBL.RemoveAEContractRecord(aeId, id);

            if (!isOk)
            {
                TempData["message"] = "刪除失敗！"; //刪除失敗   //do something
            }
            else
            {
                TempData["message"] = "刪除成功！";    //刪除成功
            }
            return RedirectToAction("Detail", "AE", new { id = aeId, view = 1 });
        }
        public ActionResult DeleteAECompanyRecord(int aeId, int id)
        {
            bool isOk = memberBL.RemoveCompanyAE(id);

            if (!isOk)
            {
                TempData["message"] = "刪除失敗！"; //刪除失敗   //do something
            }
            else
            {
                TempData["message"] = "刪除成功！";    //刪除成功
            }
            return RedirectToAction("Detail", "AE", new { id = aeId, view = 2 });
        }
        #endregion



        #region  Helper

        public PartialViewResult GetExistPerson(int id = 0)
        {
            string viewPath = EditorTemplateFolderPath + "Person/GetExistPerson.cshtml";

            if (id == 0) return PartialView(viewPath);
            var personView = memberBL.GetPersonViewModel(id, id);
            return PartialView(viewPath, personView);

        }
        public PartialViewResult GetAESearchInputModel(string keyWord = "", int searchWay = 0)
        {
            SearchInputModel searchInput = memberBL.GetAESearchInputModel();
            searchInput.ActionName = "Index";
            searchInput.ControllerName = "AE";

            searchInput.KeyWord = keyWord;
            searchInput.SearchWayId = searchWay;
            return PartialView("_IndexSearch", searchInput);

        }
        public string DisplayAEStatus(bool isActive)
        {
            if (isActive) return "有效";
            return "已無效";
        }
        public string DisplayAECurrentCompany(int? companyId)
        {
            if (companyId == null) return "";

            int id = Convert.ToInt32(companyId);
            return memberBL.GetCompanyFullName(id);


        }
        #endregion

    }
}
