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

        //GET: Contition/Detail/{id}
        public ActionResult Detail(int id)
        {
            var svc = CreateConditionService();
            var detail = svc.GetConditionByID(id);
            return View(detail);
        }

        //GET: Condition/Edit/{id}
        public ActionResult Edit(int id)
        {
            var svc = CreateConditionService();
            var detail = svc.GetConditionByID(id);
            var model =
                new ConditionEdit
                {
                    ConditionID = detail.ConditionID,
                    ConditionName = detail.ConditionName,
                };
            return View(model);
        }

        //POST: Condition/Edit/{id, model}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ConditionEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.ConditionID != id)
            {
                ModelState.AddModelError("", "ID does not match");
            }

            var svc = CreateConditionService();

            if (svc.UpdateCondition(model))
            {
                TempData["SaveResult"] = "Condition Updated";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Condition could not be updated");
            return View(model);
        }

        //GET: Condition/Delete/{id}
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateConditionService();
            var model = svc.GetConditionByID(id);

            return View(model);
        }

        //POST: Condition/Delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCondition(int id)
        {
            var svc = CreateConditionService();
            svc.DeleteCondition(id);

            TempData["SaveResult"] = "Condition Deleted";

            return RedirectToAction("Index");
        }

        private ConditionService CreateConditionService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new ConditionService(userID);
            return service;
        }
    }
}