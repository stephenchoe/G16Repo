using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G16_2013.Models.MemberModel;
using G16_2013.BLL;
using System.Web.Routing;
using PagedList;
using PagedList.Mvc;
using G16_2013.DAL;
using System.Data.Entity;
using System.Threading.Tasks;

namespace G16MG.Controllers
{
    public class PersonController : BaseController
    {

        [HttpGet]
        public ActionResult Index(int? page, string sortBy)
        {
            ViewBag.SearchWays = MemberBL.Helper.GetPersonSearchWayOptions();
            ViewBag.SortName = string.IsNullOrEmpty(sortBy) ? "Name desc" : "";
            var people = memberBL.GetAllPerson();
            switch (sortBy)
            {
                case "Name desc":
                    people = people.OrderByDescending(m => m.Name);
                    break;
                default:
                    people = people.OrderBy(m => m.PersonId);

                    break;
            }
            return View(people.ToList().ToPagedList(page ?? 1, 1));
        }

        [HttpPost]
        public ActionResult Index(FormCollection form, int? page, string sortBy)
        {
            ViewBag.SearchWays = MemberBL.Helper.GetPersonSearchWayOptions();
            int option = Convert.ToInt32(form["searchway"]);
            string keystr = form["searchstr"];
            var selectedpeople = memberBL.GetPeopleByKeyWord(option, keystr);
            return View(selectedpeople.ToPagedList(page ?? 1, 1));
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            var viewModel = memberBL.GetPersonViewModel(id, id);
            viewModel.AllowEdit = true;
            viewModel.ContactInfoView.AllowEdit = true;

            return View(viewModel);
        }


        [HttpGet]
        public PartialViewResult ReadPerson(int id, bool allowEdit = true)
        {
            var personView = memberBL.GetPersonViewModel(id);
            if (personView == null)
            {
                //do something               
            }
            personView.AllowEdit = allowEdit;

            string viewPath = DisplayTemplateFolderPath + "Person/Person.cshtml";
            return PartialView(viewPath, personView);
        }

        [HttpGet]
        public PartialViewResult ReadPersonContactInfo(int id, bool allowEdit = true)
        {
            var contactInfoView = memberBL.GetPersonContactInfoView(id);
            if (contactInfoView == null)
            {
                return PartialView(new PersonContactInfoViewModel());
            }
            else
            {
                contactInfoView.AllowEdit = allowEdit;
                string viewPath = DisplayTemplateFolderPath + "Person/PersonContactInfo.cshtml";
                return PartialView(viewPath, contactInfoView);
            }

        }
       
        [HttpGet]
        public PartialViewResult FindPersonWithContactInfo(int id)
        {
            var personView = memberBL.GetPersonViewModel(id, id);
            if (personView == null)
            {
                //do something               
            }
            personView.AllowEdit = false;
            return PartialView("_ExsitPersonView", personView);
        }
        [HttpGet]
        public PartialViewResult EditPerson(int id)
        {

            var inputModel = memberBL.GetPersonInputModel(id);
            if (inputModel == null)
            {
                //do something
            }
            inputModel.ActionName = "UpdatePerson";

            string viewPath = EditorTemplateFolderPath + "Person/Person.cshtml";
            return PartialView(viewPath, inputModel);
        }

        //[HttpPost]
        public PartialViewResult UpdatePerson(PersonInputModel personInput)
        {
            var person = memberBL.UpdatePerson(personInput);
            if (person == null)
            {
                //do something
                TempData["message"] = "修改失敗！";
            }
            else
            {
                TempData["message"] = "修改成功！";
            }

            return ReadPerson(person.PersonId);

        }
        [HttpGet]
        public PartialViewResult EditPersonContactInfo(int id)
        {

            var inputModel = memberBL.GetPersonContactInfoInput(id);
            if (inputModel != null)
            {
                inputModel.CurrentMode = CurrentMode.Edit;
            }
            string viewPath = EditorTemplateFolderPath + "Person/PersonContactInfo.cshtml";
            return PartialView(viewPath, inputModel);

        }
        [HttpPost]
        public PartialViewResult UpdatePersonContactInfo(PersonContactInfoInputModel input)
        {
            var contactinfo = memberBL.UpdatePersonContactInfo(input);
            TempData["message"] = "修改成功！";
            return ReadPersonContactInfo(contactinfo.PersonId);
        }

        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }


        public ActionResult GetPersonViewWithContactInfo(int id = 0)
        {
            string viewPath = EditorTemplateFolderPath + "Person/GetExistPerson.cshtml";

            if (id == 0) return PartialView(viewPath);
            var personView = memberBL.GetPersonViewModel(id, id);
            return PartialView(viewPath, personView);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PersonInputModel personInput)
        {

            personInput.Birthday = DateTime.Now;

            var personId = memberBL.CreatePerson(personInput);
            return RedirectToAction("Detail", new { id = personId });
        }

        #region  Helper
        public string DisplayGender(bool gender)
        {
            if (gender) return "男";
            return "女";
        }
        #endregion

        public ActionResult FindPerson(string term)
        {
            var allPeople = memberBL.GetAllPerson();
            var somePeople = allPeople.Where(p => p.Name.Contains(term)).ToList();
            var projection = from p in somePeople
                             select new
                             {
                                 id = p.PersonId,
                                 label = p.Name,
                                 value = p.Name
                             };
            return Json(projection.ToList(), JsonRequestBehavior.AllowGet);
        }



        //== 地址 ==============================

        [HttpPost]
        [RestrictToAjax]
        public ActionResult GetDistrict(string cid)
        {
            var options = memberBL.GetDistrictOptions(Convert.ToInt32(cid));

            return PartialView("_GetOptions", options);
        }
        [HttpPost]
        [RestrictToAjax]
        public String GetZipCode(string did)
        {
            return memberBL.GetZipCodeByDistrict(Convert.ToInt32(did));
        }



    }   //== End PersonController ===============

    //取代ChildActionOnly     [RestrictToAjax]
    public class RestrictToAjaxAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsAjaxRequest())
            {
                throw new InvalidOperationException("This action is not available!");
            }
        }
    }

    public class InputPerson
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
