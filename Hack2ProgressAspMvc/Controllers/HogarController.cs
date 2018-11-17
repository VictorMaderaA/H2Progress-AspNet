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
        [ActionName("Details")]
        public async Task<ActionResult> DetailsAsync(string id)
        {
            var items = (List<Hogar>)await DocumentDbRepository<Hogar>.GetItemsAsync();
            var item = items.First(x => x.Id == id);
            return View(item);
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
                        maxId = data.Max(x => int.Parse(x.Id)) + 1;
                    }
                    collection.Id = maxId.ToString();
                    await DocumentDbRepository<Hogar>.CreateItemAsync(collection);
                    return RedirectToAction("Index");
                }

                return View();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
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
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(Hogar item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await DocumentDbRepository<Hogar>.UpdateItemAsync(item.Id, item);
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(item);
            }
        }

        // GET: Hogar/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Hogar/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteAsync(int id, Hogar model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await DocumentDbRepository<Hogar>.DeleteItemAsync(model.Id);
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        public async Task<List<Hogar>> GetListHogaresAsync()
        {
            var items = (List<Hogar>)await DocumentDbRepository<Hogar>.GetItemsAsync();
            return items;
        }
    }
}
