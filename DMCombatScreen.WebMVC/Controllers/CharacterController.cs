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
    public class CharacterController : Controller
    {
        // GET: Character/Index
        public ActionResult Index(string searchString, string sortOrder)
        {
            ViewBag.NameSortParam = sortOrder == "name" ? "name_desc" : "name";
            ViewBag.PlayerSortParam = sortOrder == "player" ? "player_desc" : "player";
            var service = CreateCharacterService();
            var model = service.GetCharacters();

            if (!String.IsNullOrEmpty(searchString))
            {
                model = model.Where(m => m.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name":
                    model = model.OrderBy(m => m.Name);
                    break;
                case "name_desc":
                    model = model.OrderByDescending(m => m.Name);
                    break;
                case "player":
                    model = model.OrderBy(m => m.IsPlayer);
                    break;
                case "player_desc":
                    model = model.OrderByDescending(m => m.IsPlayer);
                    break;
                default:
                    break;
            }

            return View(model);
        }


        //GET: Character/Create
        public ActionResult Create()
        {
            return View();
        }
        
        //POST: Character/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CharacterCreate model)
        {
            if (!ModelState.IsValid)return View(model);

            var service = CreateCharacterService();

            if (service.CreateCharacter(model))
            {
                TempData["SaveResult"] = "Character Created Successfully";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Character could not be created.");
            return View(model);
        }

        //GET: Character/Details/{id}
        public ActionResult Details(int id)
        {
            var svc = CreateCharacterService();
            var model = svc.GetCharacterByID(id);
            return View(model);
        }

        //GET: Character/Edit/{id}
        public ActionResult Edit(int id)
        {
            var svc = CreateCharacterService();
            var detail = svc.GetCharacterByID(id);
            var model =
                new CharacterEdit()

                {
                    CharacterID = detail.CharacterID,
                    Name = detail.Name,
                    MaxHP = detail.MaxHP,
                    InitiativeModifier = detail.InitiativeModifier,
                    InitiativeAbilityScore = detail.InitiativeAbilityScore,
                    IsPlayer = detail.IsPlayer,
                    CharacterType = detail.CharacterType
                };
            ViewBag.CharType = model.CharacterType;
            return View(model);
        }

        //POST: Character/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CharacterEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if(model.CharacterID != id)
            {
                ModelState.AddModelError("", "ID does not match");
                return View(model);
            }

            var svc = CreateCharacterService();

            if (svc.UpdateCharacter(model))
            {
                TempData["SaveResult"] = "Character Updated";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Character could not be updated.");
            return View(model);
        }

        //GET: Delete/Character/{id}
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateCharacterService();
            var model = svc.GetCharacterByID(id);

            return View(model);
        }

        //POST: Delete/Character/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCharacter(int id)
        {
            var svc = CreateCharacterService();

            svc.DeleteCharacter(id);

            TempData["SaveResult"] = "Character Deleted";

            return RedirectToAction("Index");
        }

        private CharacterService CreateCharacterService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new CharacterService(userID);
            return service;
        }
    }
}