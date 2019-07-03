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
    public class RunCombatController : Controller
    {
        // GET: RunCombat
        public ActionResult Index()
        {
            var svc = CreateRunCombatService();
            var model = svc.GetCombatList();
            return View(model);
        }

        //GET: RunCombat/Detail/{id}
        public ActionResult Detail(int id)
        {
            var svc = CreateRunCombatService();
            var model = svc.GetCombatantsList(id);
            return View(model);
        }

        //GET: RunCombat/RollInitiative/{id}
        public ActionResult RollInitiative(int id)
        {
            var svc = CreateRunCombatService();
            var model = svc.GetCombatantsList(id).ToList();
            return View(model);
        }

        //POST: RunCombat/RollInitiative/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RollInitiative(IEnumerable<RunCombatDetail> model, int id)
        {
            var svc = CreateRunCombatService();

            if (!ModelState.IsValid) return View(model);

            //if (model.ID != id)
            //{
            //    ModelState.AddModelError("", "ID does not match");
            //    return View(model);
            //}

            if (svc.SetInitiatives(model))
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Initiative could not be rolled");
            return View(model);
        }

        //GET: RunCombat/RunCombat/{id}
        public ActionResult RunCombat(int id)
        {
            var svc = CreateRunCombatService();
            var model = svc.GetCombatantsList(id).OrderBy(e => e.CurrentInitiative).ThenBy(e => e.Character.InitiativeAbilityScore).ThenBy(e => e.Character.IsPlayer).Reverse().ToArray();
            return View(model);
        }

        private RunCombatService CreateRunCombatService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new RunCombatService(userID);
            return service;
        }
    }
}