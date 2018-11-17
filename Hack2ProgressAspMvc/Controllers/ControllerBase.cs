//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Mvc;
//using System.Web.UI.WebControls;
//using Hack2ProgressAspMvc.BaseLogic;
//using Hack2ProgressAspMvc.Models;

//namespace Hack2ProgressAspMvc.Controllers
//{
//    public abstract class ControllerBase : Controller
//    {

//        [ActionName("Index")]
//        public async Task<ActionResult> IndexAsync()
//        {
//            var items = await DocumentDbRepository<MODELO>.GetItemsAsync();
//            return View(items);
//        }

//        public ActionResult Create()
//        {
//            return View();
//        }

//        [HttpPost]
//        [ActionName("Create")]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> CreateAsync([Bind(Include = "Required")] MODELO model) //TODO - MODIFY INCLUDE
//        {
//            if (ModelState.IsValid)
//            {
//                List<MODELO> data = (List<MODELO>)await DocumentDbRepository<Casa>.GetItemsAsync();
//                var maxId = 1;
//                if (data.Count > 0)
//                    maxId = data.Max(x => int.Parse(x.Id)) + 1;
//                model.Id = maxId.ToString();
//                await DocumentDbRepository<MODELO>.CreateItemAsync(model);
//                return RedirectToAction("Index");
//            }

//            return View(model);
//        }

//        public ActionResult Delete()
//        {
//            return View();
//        }

//        [HttpPost]
//        [ActionName("Delete")]
//        public async Task<ActionResult> DeleteAsync(MODEL model)
//        {
//            if (ModelState.IsValid)
//            {
//                await DocumentDbRepository<MODEL>.DeleteItemAsync(model.Id);
//                return RedirectToAction("Index");
//            }
//            return View(model);
//        }

//        public ActionResult Edit()
//        {
//            return View();
//        }

//        [HttpPost]
//        [ActionName("Edit")]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> EditAsync([Bind(Include = "Required")] MODEL model) //TODO - MODIFY INCLUDE
//        {
//            if (ModelState.IsValid)
//            {
//                await DocumentDbRepository<MODEL>.UpdateItemAsync(model.Id, model);
//                return RedirectToAction("Index");
//            }

//            return View(model);
//        }

//        [ActionName("Edit")]
//        public async Task<ActionResult> EditAsync(string id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }

//            var item = await DocumentDbRepository<MODEL>.GetItemAsync(id);
//            if (item == null)
//            {
//                return HttpNotFound();
//            }

//            return View(item);
//        }

//        public ActionResult Details()
//        {
//            return View();
//        }


//    }
//}