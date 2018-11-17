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
    public class ConsumoController : Controller
    {
        // GET: Consumo
        public ActionResult Index()
        {
            var cmd = new MySqlCommand
            {
                CommandText = "SELECT * FROM `consumo`"
            };
            var items = SqlConnector.Instance.GetTable(cmd, out var r);
            List<Consumo> consu = new List<Consumo>();
            foreach (DataRow i in items.Rows)
            {
                var hogar = new Consumo()
                {
                    Id = int.Parse(i[0].ToString()),
                    IdToma = int.Parse(i[1].ToString()),
                    ConsumoEnergetico = float.Parse(i[2].ToString()),
                    Fecha = DateTime.Parse(i[3].ToString())
                };
                consu.Add(hogar);
            }
            return View(consu);
        }

        // GET: Consumo/Details/5
        [ActionName("Details")]
        public ActionResult Details(int id)
        {
            var cmd = new MySqlCommand
            {
                CommandText = "SELECT * FROM consumo WHERE id = @id"
            };
            cmd.Parameters.Add("@id", id);
            var items = SqlConnector.Instance.GetTable(cmd, out var r);
            List<Consumo> consu = new List<Consumo>();
            foreach (DataRow i in items.Rows)
            {
                var hogar = new Consumo()
                {
                    Id = int.Parse(i[0].ToString()),
                    IdToma = int.Parse(i[1].ToString()),
                    ConsumoEnergetico = float.Parse(i[2].ToString()),
                    Fecha = DateTime.Parse(i[3].ToString())
                };
                consu.Add(hogar);
            }

            var item = consu.First(x => x.Id == id);
            return View(item);
        }

        // GET: Consumo/Create
        public ActionResult Create()
        {
            var cmd = new MySqlCommand
            {
                CommandText = "SELECT * FROM tomasenergia"
            };
            var items = SqlConnector.Instance.GetTable(cmd, out var r);
            List<TomaEnergia> tomas = new List<TomaEnergia>();
            foreach (DataRow i in items.Rows)
            {
                var toma = new TomaEnergia()
                {
                    Id = int.Parse(i[0].ToString()),
                    IdHabitacion = int.Parse(i[1].ToString())
                };
                tomas.Add(toma);
            }
            ViewData["tomas"] = tomas;
            return View();
        }

        // POST: Consumo/Create
        [HttpPost]
        public ActionResult Create(Consumo collection)
        {
            if (collection.Fecha.Year < 2000)
                collection.Fecha = DateTime.Now;
            if (ModelState.IsValid)
            {
                var cmd = new MySqlCommand
                {
                    CommandText = "INSERT INTO `consumo` (`id`, `idToma`, `consumoEnergetico`, `fecha`) " +
                                  "VALUES (NULL, @idToma, @consumoEnergetico, @fecha);"
                };
                cmd.Parameters.Add("@idToma", collection.IdToma);
                cmd.Parameters.Add("@consumoEnergetico", collection.ConsumoEnergetico);
                cmd.Parameters.Add("@fecha", collection.Fecha);

                SqlConnector.Instance.ExecuteQuery(cmd, out var r);

                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Consumo/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Consumo/Edit/5

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Consumo item)
        {
            if (item.Fecha == null)
                item.Fecha = DateTime.Now;
            try
            {
                if (ModelState.IsValid)
                {
                    var cmd = new MySqlCommand
                    {
                        CommandText = "Update consumo set id = @id, idToma = @idToma, " +
                                      "consumoEnergetico = @consumoEnergetico. fecha = @fecha where id = @id"
                    };
                    cmd.Parameters.Add("@id", item.Id);
                    cmd.Parameters.Add("@idToma", item.IdToma);
                    cmd.Parameters.Add("@consumoEnergetico", item.ConsumoEnergetico);
                    cmd.Parameters.Add("@fecha", item.Fecha);

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

        // GET: Consumo/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Consumo/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult Delete(int id, Consumo model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var cmd = new MySqlCommand
                    {
                        CommandText = "DELETE FROM consumo WHERE id = @id"
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
