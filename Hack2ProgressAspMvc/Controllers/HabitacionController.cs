using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Hack2ProgressAspMvc.BaseLogic;
using Hack2ProgressAspMvc.Models;
using Newtonsoft.Json;

namespace Hack2ProgressAspMvc.Controllers
{
    public class HabitacionController : Controller
    {
        // GET: Habitacion
        [HttpGet]
        [ActionName("Index")]
        public async Task<ActionResult> IndexAsync(int idHogar)
        {
            var hogares = (List<Hogar>)await DocumentDbRepository<Hogar>.GetItemsAsync();
            try
            {
                var hogar = hogares.First(x => x.Id == idHogar.ToString());
               
                if(hogar.Habitaciones != null)
                    return Content(hogar.Habitaciones[0]);
                return Content("{}");
                //return View(items);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        // GET: Habitacion/Details/5
        [ActionName("Details")]
        public async Task<ActionResult> DetailsAsync(string id)
        {
            var items = (List<Habitacion>)await DocumentDbRepository<Habitacion>.GetItemsAsync();
            var item = items.First(x => x.Id == id);
            return View(item);
        }

        // GET: Habitacion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Habitacion/Create
        [ActionName("Create")]
        [HttpPost]
        public async Task<ActionResult> CreateAsync(int idHogar)
        {
            try
            {
                var hogares = (List<Hogar>)await DocumentDbRepository<Hogar>.GetItemsAsync();
                var hogar = hogares.First(x => x.Id == idHogar.ToString());

                var strBuilder = new StringBuilder();
                var listHabitaciones = new List<Habitacion>();
                if (hogar.Habitaciones != null)
                    foreach (var habitacion in hogar.Habitaciones)
                    {
                        var deserializedProduct = JsonConvert.DeserializeObject<Habitacion>(habitacion);
                        listHabitaciones.Add(deserializedProduct);
                    }

                string output = JsonConvert.SerializeObject(listHabitaciones);

                if (ModelState.IsValid)
                {
                    var data = (List<Habitacion>)await DocumentDbRepository<Habitacion>.GetItemsAsync();
                    var maxId = 1;
                    if (data.Count > 0)
                    {
                        maxId = data.Max(x => int.Parse(x.Id)) + 1;
                    }
                    collection.Id = maxId.ToString();
                    await DocumentDbRepository<Habitacion>.CreateItemAsync(collection);
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

        // GET: Habitacion/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Habitacion/Edit/5

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(Habitacion item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await DocumentDbRepository<Habitacion>.UpdateItemAsync(item.Id, item);
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(item);
            }
        }

        // GET: Habitacion/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Habitacion/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteAsync(int id, Habitacion model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await DocumentDbRepository<Habitacion>.DeleteItemAsync(model.Id);
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        [ActionName("GetHabitacionesFromHogarId")]
        public async Task<ActionResult> GetHabitacionesFromHogarIdAsync(object id)
        {
            var data = (List<Hogar>)await DocumentDbRepository<Hogar>.GetItemsAsync();
            if (data.Exists(x => x.Id == id.ToString()))
            {
                var hogar = data.First(x => x.Id == id.ToString());
                var strBuilder = new StringBuilder();
                if(hogar.Habitaciones == null) return Content("{}");
                foreach (var habitacion in hogar.Habitaciones)
                {
                    strBuilder.Append(habitacion);
                }

                return Content(strBuilder.ToString());
            }

            return Content("{}");
        }

    }
}
