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
            var model = svc.GetInitiativeList(id).ToList();
            return View(model);
        }

        //POST: RunCombat/RollInitiative/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RollInitiative(List<RunCombatInitiative> model, int id)
        {
            var svc = CreateRunCombatService();

            if (!ModelState.IsValid) return View(model);

            //if (model.ID != id)
            //{
            //    ModelState.AddModelError("", "ID does not match");
            //   return View(model);
            //}

            if (svc.SetInitiatives(model))
            {
                TempData["SaveResult"] = "Initiative Successfully Rolled";
                return RedirectToAction("RunCombat", new { id = id });
            }

            ModelState.AddModelError("", "Initiative could not be rolled");
            return View(model);
        }

        //GET: RunCombat/RunCombat/{id}
        public ActionResult RunCombat(int id, int? SelectedCharacter)
        {
            var svc = CreateRunCombatService();
            var model = svc.GetCombatCharacterList(id).OrderBy(e => e.TotalInitiative).ThenBy(e => e.InitiativeAbilityScore).ThenBy(e => e.IsPlayer).Reverse().ToList();
            int turnOrder = 0;
            foreach (var character in model)
            {
                character.TurnOrderNumber = turnOrder;
                turnOrder++;
            }
            if (SelectedCharacter == null)
            {
                SelectedCharacter = 0;
            }
            else if (SelectedCharacter < model.Count())
            {
                SelectedCharacter++;
            }

            if (SelectedCharacter == model.Count())
            {
                SelectedCharacter = 0;
            }
            ViewBag.SelectedCharacter = SelectedCharacter;
            return View(model);
        }

        //GET: RunCombat/Attack/{id}
        public ActionResult Attack(int id, int currentTurn)
        {
            var svc = CreateRunCombatService();
            var model = svc.GetOneCombatant(id);
            model.CurrentTurn = currentTurn;
            return View(model);
        }

        //POST: RunCombat/Attack/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Attack(RunCombatAttack model, int id)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.ID != id)
            {
                ModelState.AddModelError("", "ID does not match");
                return View(model);
            }

            var svc = CreateRunCombatService();

            if (svc.UpdateCharacterHP(model))
            {
                TempData["SaveResult"] = "HP updated";
                return RedirectToAction("RunCombat", new { id = model.CombatID, SelectedCharacter = model.CurrentTurn - 1 });
            }

            ModelState.AddModelError("", "HP could not be updated.");
            return View(model);
        }

        //GET: RunCombat/EditCondition/{id}
        public ActionResult EditCondition(int id, int currentTurn)
        {
            var svc = CreateRunCombatService();
            var model = svc.GetEditCharacterConditionsModel(id);
            PopulateAssignedConditions(model);
            model.CurrentTurn = currentTurn;
            return View(model);
        }

        private RunCombatService CreateRunCombatService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new RunCombatService(userID);
            return service;
        }

        private void PopulateAssignedConditions(RunCombatEditCondition model)
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new ConditionService(userID);
            var allConditions = service.GetConditionList();
            var characterConditions = new HashSet<int>(model.ConditionIDs);
            var viewModel = new List<RunCombatAssignedCondition>();
            foreach (var condition in allConditions)
            {
                viewModel.Add(new RunCombatAssignedCondition
                {
                    ConditionID = condition.ConditionID,
                    ConditionName = condition.ConditionName,
                    IsAssigned = characterConditions.Contains(condition.ConditionID)
                });
            }
            ViewBag.Conditions = viewModel;
        }
    }
}