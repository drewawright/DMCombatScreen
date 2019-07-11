using DMCombatScreen.Models;
using DMCombatScreen.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DMCombatScreen.WebMVC.Controllers
{
    public class ConditionController : Controller
    {
        // GET: Condition/Index
        public ActionResult Index()
        {
            var svc = CreateConditionService();
            var model = svc.GetConditionList();
            return View(model);
        }

        //GET: Condition/Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: Condition/Create/{ConditionCreate}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ConditionCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateConditionService();

            if (service.CreateCondition(model))
            {
                TempData["SaveResult"] = "Condition Created Successfully";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Condition could not be created.");
            return View(model);
        }

        private ConditionService CreateConditionService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new ConditionService(userID);
            return service;
        }
    }
}