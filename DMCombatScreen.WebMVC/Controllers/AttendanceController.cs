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
        public ActionResult Create(int? id)
        {
            if (id != null)
            {
                int selectedID = (int)id;
                ViewBag.CombatID = CombatSelect(selectedID);
            }
            else
            {
                ViewBag.CombatID = CombatSelect();
            }
            ViewBag.CharacterID = CharacterSelect();

            return View();
        }

        //POST: Attendance/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AttendanceCreate model)
        {
            ViewBag.CharacterID = CharacterSelect(model.CharacterID);
            ViewBag.CombatID = CombatSelect(model.CombatID);

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

        //GET: Attendance/CreateMulti/
        public ActionResult CreateMultiple()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            ViewBag.CombatID = CombatSelect();
            var model = new AttendanceAddCharacter();
            model.CharacterList = new CharacterService(userID).GetAttendanceCharacterInfos();

            return View(model);
        }

        //POST: Attendance/CreateMulti/{attendanceAddmultiple}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMultiple(AttendanceAddCharacter model)
        {
            //var userID = Guid.Parse(User.Identity.GetUserId());
            ViewBag.CombatID = CombatSelect(model.CombatID);
            //ViewBag.CharacterID = new CharacterService(userID).GetAttendanceCharacterInfos();

            if (!ModelState.IsValid) return View(model);

            var svc = CreateAttendanceService();
            svc.CreateMultipleAttendances(model);
            return RedirectToAction("Index");
        }

        //GET: Attendance/Detail/{id}
        public ActionResult Details(int id)
        {
            var svc = CreateAttendanceService();
            var detail = svc.GetAttendanceByID(id);
            return View(detail);
        }

        //GET: Attendance/Edit/{id}
        public ActionResult Edit(int id)
        {
            var svc = CreateAttendanceService();
            var detail = svc.GetAttendanceByID(id);

            ViewBag.CharacterID = CharacterSelect(detail.CharacterID);
            ViewBag.CombatID = CombatSelect(detail.CombatID);

            var model =
                new AttendanceEdit()
                {
                    ID = detail.ID,
                    CharacterID = detail.CharacterID,
                    CombatID = detail.CombatID,
                    CurrentHP = detail.CurrentHP
                };

            return View(model);
        }

        //POST: Attendance/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AttendanceEdit model)
        {
            ViewBag.CharacterID = CharacterSelect(model.CharacterID);
            ViewBag.CombatID = CombatSelect(model.CombatID);

            if (!ModelState.IsValid) return View(model);

            if(model.ID != id)
            {
                ModelState.AddModelError("", "ID does not match");
                return View(model);
            }

            var svc = CreateAttendanceService();

            if (svc.UpdateAttendance(model))
            {
                TempData["SaveResult"] = "Attendance Updated";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Attendance could not be updated");
            return View(model);
        }

        //GET: Delete/Attendance/{id}
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateAttendanceService();
            var model = svc.GetAttendanceByID(id);

            return View(model);
        }

        //POST: Delete/Attendance/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAttendance(int id)
        {
            var svc = CreateAttendanceService();
            svc.DeleteAttendance(id);

            TempData["SaveResult"] = "Attendance Removed";
            return RedirectToAction("Index");
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

        private SelectList CharacterSelect(int selectedID)
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var characters = new CharacterService(userID).GetCharacters().ToList();
            return new SelectList(characters, "CharacterID", "Name", selectedID);
        }

        private SelectList CombatSelect(int selectedID)
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var characters = new CombatService(userID).GetCombats().ToList();
            return new SelectList(characters, "CombatID", "Name", selectedID);
        }

        private AttendanceService CreateAttendanceService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new AttendanceService(userID);
            return service;
        }
    }
}