using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using G16_2013.Models.MemberModel;
using G16_2013.BLL;

namespace WebApplicationG16_2013.Controllers
{
    public class CustomerController : BaseController
    {
        //
        // GET: /Customer/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create()
        {
            return View();
        }
	}
}