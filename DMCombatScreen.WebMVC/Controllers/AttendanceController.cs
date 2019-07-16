using DMCombatScreen.Data;
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
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.CharSortParam = sortOrder == "name" ? "name_desc" : "name";
            ViewBag.CombatSortParam = String.IsNullOrEmpty(sortOrder) ? "combat_name_desc" : "";
            var svc = CreateAttendanceService();

            var model = svc.GetAttendances();

            if (!String.IsNullOrEmpty(searchString))
            {
                model = model.Where(m => m.CombatName.Contains(searchString)
                                    || m.CharacterName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    model = model.OrderByDescending(m => m.CharacterName);
                    break;
                case "name":
                    model = model.OrderBy(m => m.CharacterName);
                    break;
                case "combat_name_desc":
                    model = model.OrderByDescending(m => m.CombatName);
                    break;
                default:
                    model = model.OrderBy(m => m.CombatName);
                    break;
            }

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
        public ActionResult CreateMultiple(int? id, string sortOrder, int? CharType)
        {
            ViewBag.CharType = CharTypeSelect();
            int charTypeID = CharType.GetValueOrDefault();

            var userID = Guid.Parse(User.Identity.GetUserId());
            if (id != null)
            {
                int combatID = (int)id;
                ViewBag.CombatID = CombatSelect(combatID);
            }
            else
            {
                ViewBag.CombatID = CombatSelect();
            }

            var model = new AttendanceAddCharacter();
            model.CharacterList = new CharacterService(userID).GetAttendanceCharacterInfos()
                .Where(m => !CharType.HasValue || m.CharacterTypeValue == charTypeID).ToList();

            ViewBag.NameSortParam = sortOrder == "name" ? "name_desc" : "name";
            ViewBag.TypeSortParam = sortOrder == "type" ? "type_desc" : "type";
            switch (sortOrder)
            {
                case "name":
                    model.CharacterList = model.CharacterList.OrderBy(m => m.CharacterName).ToList();
                    break;
                case "name_desc":
                    model.CharacterList = model.CharacterList.OrderByDescending(m => m.CharacterName).ToList();
                    break;
                case "type":
                    model.CharacterList = model.CharacterList.OrderBy(m => m.CharacterTypeName).ToList();
                    break;
                case "type_desc":
                    model.CharacterList = model.CharacterList.OrderByDescending(m => m.CharacterTypeName).ToList();
                    break;
                default:
                    break;
            }

            return View(model);
        }

        //POST: Attendance/CreateMulti/{AttendanceAddCharacter}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMultiple(AttendanceAddCharacter model, int? CharType)
        {
            ViewBag.CombatID = CombatSelect(model.CombatID);
            ViewBag.CharType = CharTypeSelect();
            int charTypeID = CharType.GetValueOrDefault();

            if (!ModelState.IsValid) return View(model);

            var svc = CreateAttendanceService();
            svc.CreateMultipleAttendances(model);
            return RedirectToAction("Detail", "RunCombat", new { id = model.CombatID });
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

        private SelectList CharTypeSelect()
        {
            var charTypes = from CharacterType c in Enum.GetValues(typeof(CharacterType))
                            select new
                            {
                                ID = (int)c,
                                Name = c.ToString(),
                            };
            return new SelectList(charTypes, "ID", "Name");
        }

        private AttendanceService CreateAttendanceService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new AttendanceService(userID);
            return service;
        }
    }
}