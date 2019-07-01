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
    public class CombatController : Controller
    {
        // GET: Combat/Index
        public ActionResult Index()
        {
            var svc = CreateCombatService();
            var model = svc.GetCombats();
            return View(model);
        }

        //GET: Combat/Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: Combat/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CombatCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var svc = CreateCombatService();

            if (svc.CreateCombat(model))
            {
                TempData["SaveResult"] = "Combat Successfully Created";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Combat could not be created.");
            return View(model);
        }

        //GET: Combat/Detail/{id}
        public ActionResult Details(int id)
        {
            var svc = CreateCombatService();
            var detail = svc.GetCombatByID(id);
            return View(detail);
        }

        //GET: Combat/Edit/{id}
        public ActionResult Edit(int id)
        {
            var svc = CreateCombatService();
            var detail = svc.GetCombatByID(id);
            var model =
                new CombatEdit()
                {
                    CombatID = detail.CombatID,
                    Name = detail.Name
                };
            return View(model);
        }

        //POST: Combat/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CombatEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if(model.CombatID != id)
            {
                ModelState.AddModelError("", "ID does not match");
                return View(model);
            }

            var svc = CreateCombatService();

            if (svc.UpdateCombat(model))
            {
                TempData["SaveResult"] = "Combat Updated";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Combat could not be updated");
            return View(model);
        }

        //GET: Delete/Character/{id}
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateCombatService();
            var model = svc.GetCombatByID(id);

            return View(model);
        }

        //POST: Delete/Character/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCombat(int id)
        {
            var svc = CreateCombatService();
            svc.DeleteCombat(id);

            TempData["SaveResult"] = "Combat Deleted";

            return RedirectToAction("Index");
        }

        private CombatService CreateCombatService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new CombatService(userID);
            return service;
        }
    }
}