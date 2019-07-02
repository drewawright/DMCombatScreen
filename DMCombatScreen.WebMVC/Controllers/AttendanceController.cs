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
    public class AttendanceController : Controller
    {
        // GET: Attendance/Index
        public ActionResult Index()
        {
            var svc = CreateAttendanceService();
            var model = svc.GetAttendances();
            return View(model);
        }

        //GET: Attendance/Create
        public ActionResult Create()
        {
            ViewBag.CharacterID = CharacterSelect();
            ViewBag.CombatID = CombatSelect();

            return View();
        }

        //POST: Attendance/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AttendanceCreate model)
        {
            ViewBag.CharacterID = CharacterSelect();
            ViewBag.CombatID = CombatSelect();

            if (!ModelState.IsValid) return View(model);

            var svc = CreateAttendanceService();
            
            if (svc.CreateAttendance(model))
            {
                TempData["SaveResult"] = "Character Successfully Added to Encounter";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Character could not be added to encounter.");
            return View(model);
        }

        //GET: Attendance/Detail/{id}
        public ActionResult Details(int id)
        {
            var svc = CreateAttendanceService();
            var detail = svc.GetAttendanceByID(id);
            return View(detail);
        }

        private SelectList CharacterSelect()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var characters = new CharacterService(userID).GetCharacters().ToList();
            return new SelectList(characters, "CharacterID", "Name");
        }

        private SelectList CombatSelect()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var characters = new CombatService(userID).GetCombats().ToList();
            return new SelectList(characters, "CombatID", "Name");
        }

        private AttendanceService CreateAttendanceService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new AttendanceService(userID);
            return service;
        }
    }
}