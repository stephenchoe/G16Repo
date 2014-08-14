using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using G16_2013.BLL;
using G16_2013.Models.MemberModel;
using System.Net;

namespace WebApplicationG16_2013.Controllers
{
    public class PersonController : BaseController
    {
        //
        // GET: /Person/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            PopulateCityDropDownList();
            PopulateDistrictDropDownList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PersonInputModel  personInput)
        {
            personInput.Birthday = new DateTime(1989, 1, 7);
            personInput.Gender = true;

            var personId = memberBL.CreatePerson(personInput);
            return RedirectToAction("Detail", new { id = personId });

            
        }
       
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var person = memberBL.GetPersonById((int)id);
            if (person == null) return HttpNotFound();

            var personView = memberBL.GetPersonViewModel((int)id, (int)id);
            return View(personView); 

        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int personId = Convert.ToInt32(id);
           
            var personInput = memberBL.GetPersonInputModel(personId);
            if (personInput == null) return HttpNotFound();
            var contactInfoInput = memberBL.GetPersonContactInfoInput(personId);
            if (contactInfoInput == null) return HttpNotFound();

            personInput.ContactInfoInput = contactInfoInput;
            return View(personInput);
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PersonInputModel personInput)
        {
            if (!ModelState.IsValid) return View(personInput);
            personInput.Birthday = new DateTime(1989, 1, 7);
           
            var contactInfoInput=personInput.ContactInfoInput;

            var person = new Person();
            if (contactInfoInput == null || contactInfoInput.Id <= 0)
            {
                person = memberBL.UpdatePerson(personInput);
                
            }
            else
            {
                person = memberBL.UpdatePerson(personInput, contactInfoInput);
               
            }


            return View(new { id = person.PersonId });

        }
        private void PopulateCityDropDownList(object selectedItem = null)
        {
            var options = memberBL.GetCityOptions();
            if (selectedItem == null) selectedItem = options.FirstOrDefault().Value;
            ViewBag.CityId = new SelectList(options, "Value", "Text", selectedItem);
        }
        private void PopulateDistrictDropDownList(string city="", object selectedItem = null)
         {
             if (city == "") city = memberBL.GetCityOptions().FirstOrDefault().Value;
             var options = memberBL.GetDistrictOptions(Convert.ToInt32(city));
             if (selectedItem == null) selectedItem = options.FirstOrDefault().Value;
             
             ViewBag.DistrictId = new SelectList(options, "Value", "Text", selectedItem);
         } 
	}
}