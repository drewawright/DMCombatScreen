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
        public ActionResult Index()
        {
            var service = CreateCharacterService();
            var model = service.GetCharacters();
            return View(model);
        }


        //GET: Character/Create
        public ActionResult Create()
        {
            return View();
        }
        
        //POST: Character/Create{id}
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
            };

            ModelState.AddModelError("", "Character could not be created.");
            return View(model);
        }

        private CharacterService CreateCharacterService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new CharacterService(userID);
            return service;
        }
    }
}