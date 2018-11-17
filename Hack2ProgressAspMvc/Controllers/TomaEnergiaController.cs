using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hack2ProgressAspMvc.Models;
using Library.MySQL;
using MySql.Data.MySqlClient;

namespace Hack2ProgressAspMvc.Controllers
{
    public class TomaEnergiaController : Controller
    {
        // GET: TomaEnergia
        public ActionResult Index()
        {
            var cmd = new MySqlCommand
            {
                CommandText = "SELECT * FROM tomas"
            };
            var items = SqlConnector.Instance.GetTable(cmd, out var r);
            List<TomaEnergia> tomas = new List<TomaEnergia>();
            foreach (DataRow item in items.Rows)
            {
                var hogar = new TomaEnergia()
                {
                    Id = int.Parse(item[0].ToString())
                };
                tomas.Add(hogar);
            }

            return View(tomas);
        }

        // GET: TomaEnergia/Details/5
        [ActionName("Details")]
        public ActionResult Details(int id)
        {
            var cmd = new MySqlCommand
            {
                CommandText = "SELECT * FROM tomas WHERE id = @id"
            };
            cmd.Parameters.Add("@id", id);
            var items = SqlConnector.Instance.GetTable(cmd, out var r);
            List<TomaEnergia> tomas = new List<TomaEnergia>();
            foreach (DataRow i in items.Rows)
            {
                var hogar = new TomaEnergia()
                {
                    Id = int.Parse(i[0].ToString())
                };
                tomas.Add(hogar);
            }

            var item = tomas.First(x => x.Id == id);
            return View(item);
        }

        // GET: TomaEnergia/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TomaEnergia/Create
        [HttpPost]
        public ActionResult Create(TomaEnergia collection)
        {
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    var cmd = new MySqlCommand
                    {
                        CommandText = "INSERT INTO `tomasenergia` (`id`, `id_habitacion`) VALUES (NULL, @IdHabitacion)"
                    };
                    cmd.Parameters.Add("@IdHabitacion", collection.IdHabitacion);

                    SqlConnector.Instance.ExecuteQuery(cmd, out var r);

                    return RedirectToAction("Index");
                }
            }

            return View();
        }

        // GET: TomaEnergia/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TomaEnergia/Edit/5

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TomaEnergia item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var cmd = new MySqlCommand
                    {
                        CommandText = "Update tomasenergia set id = @id, id_habitacion = @IdHabitacion," +
                                      " where id = @id"
                    };
                    cmd.Parameters.Add("@id", item.Id);
                    cmd.Parameters.Add("@IdHabitacion", item.IdHabitacion);

                    SqlConnector.Instance.ExecuteQuery(cmd, out var r);
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(item);
            }
        }

        // GET: TomaEnergia/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TomaEnergia/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult Delete(int id, TomaEnergia model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var cmd = new MySqlCommand
                    {
                        CommandText = "DELETE FROM tomas WHERE id = @id"
                    };
                    cmd.Parameters.Add("@id", model.Id);

                    SqlConnector.Instance.ExecuteQuery(cmd, out var r);

                    return RedirectToAction("Index");
                }
                return View(model);
            }
            catch
            {
                return View();
            }
        }
    }
}