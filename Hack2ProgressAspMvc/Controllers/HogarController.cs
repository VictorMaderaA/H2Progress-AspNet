using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Hack2ProgressAspMvc.BaseLogic;
using Hack2ProgressAspMvc.Models;

namespace Hack2ProgressAspMvc.Controllers
{
    public class HogarController : Controller
    {
        // GET: Hogar
        [ActionName("Index")]
        public async Task<ActionResult> IndexAsync()
        {
            var items = await DocumentDbRepository<Hogar>.GetItemsAsync();
            return View(items);
        }

        // GET: Hogar/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Hogar/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Hogar/Create
        [ActionName("Create")]
        [HttpPost]
        public async Task<ActionResult> CreateAsync(Hogar collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = (List<Hogar>)await DocumentDbRepository<Hogar>.GetItemsAsync();
                    var maxId = 1;
                    if (data.Count > 0)
                    {
                        maxId = data.Max(x => x.Id) + 1;
                    }
                    collection.Id = maxId;
                    await DocumentDbRepository<Hogar>.CreateItemAsync(collection);
                    return RedirectToAction("Index");
                }

                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Hogar/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Hogar/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Hogar/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Hogar/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
