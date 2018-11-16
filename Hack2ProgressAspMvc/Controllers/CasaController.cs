using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Hack2ProgressAspMvc.BaseLogic;
using Hack2ProgressAspMvc.Models;

namespace Hack2ProgressAspMvc.Controllers
{
    public class CasaController : Controller
    {
        // GET: Casa
        [ActionName("Index")]
        public async Task<ActionResult> IndexAsync()
        {
            var items = await DocumentDbRepository<Casa>.GetItemsAsync();
            return View(items);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(/*[Bind(Include = "Id,Name,Description,Completed")]*/ Casa item)
        {
            if (ModelState.IsValid)
            {
                await DocumentDbRepository<Casa>.CreateItemAsync(item);
                return RedirectToAction("Index");
            }

            return View(item);
        }

        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult Delete()
        {
            return View();
        }

        public ActionResult Details()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync([Bind(Include = "Id,Name,Description,Completed")] Casa item)
        {
            if (ModelState.IsValid)
            {
                await DocumentDbRepository<Casa>.UpdateItemAsync(item.Id, item);
                return RedirectToAction("Index");
            }

            return View(item);
        }

        [ActionName("Edit")]
        public async Task<ActionResult> EditAsync(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Casa item = await DocumentDbRepository<Casa>.GetItemAsync(id);
            if (item == null)
            {
                return HttpNotFound();
            }

            return View(item);
        }
    }
}