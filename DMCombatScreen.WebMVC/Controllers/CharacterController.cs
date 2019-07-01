using DMCombatScreen.Models;
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
            var model = new CharacterListItem[0];
            return View(model);
        }

        //GET: Character/Create
        public ActionResult CreatePlayer()
        {
            return View();
        }
        
        //POST: Character/Create{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePlayer(CharacterPlayerCreate model)
        {
            if (ModelState.IsValid)
            {

            }
            return View(model);
        }
    }
}