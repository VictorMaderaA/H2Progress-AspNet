using Hack2ProgressAspMvc.Models;
using Library.MySQL;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Hack2ProgressAspMvc.Controllers
{
    public class HabitacionController : Controller
    {
        // GET: Habitacion
        public ActionResult Index()
        {
            var cmd = new MySqlCommand
            {
                CommandText = "SELECT * FROM habitaciones"
            };
            var items = SqlConnector.Instance.GetTable(cmd, out var r);
            List<Habitacion> habitaciones = new List<Habitacion>();
            foreach (DataRow i in items.Rows)
            {
                var hogar = new Habitacion()
                {
                    Id = int.Parse(i[0].ToString()),
                    Nombre = i[1].ToString(),
                    IdHogar = int.Parse(i[2].ToString())
                };
                habitaciones.Add(hogar);
            }

            return View(habitaciones);
        }

        // GET: Habitacion/Details/5
        [ActionName("Details")]
        public ActionResult Details(int id)
        {
            var cmd = new MySqlCommand
            {
                CommandText = "SELECT * FROM habitaciones WHERE id = @id"
            };
            cmd.Parameters.Add("@id", id);
            var items = SqlConnector.Instance.GetTable(cmd, out var r);
            List<Habitacion> habitaciones = new List<Habitacion>();
            foreach (DataRow i in items.Rows)
            {
                var hogar = new Habitacion()
                {
                    Id = int.Parse(i[0].ToString()),
                    Nombre = i[1].ToString()
                };
                habitaciones.Add(hogar);
            }

            var item = habitaciones.First(x => x.Id == id);
            return View(item);
        }

        // GET: Habitacion/Create
        public ActionResult Create()
        {
            var cmd = new MySqlCommand
            {
                CommandText = "SELECT * FROM hogares"
            };
            var hogaresI = SqlConnector.Instance.GetTable(cmd, out var r);
            List<Hogar> hogares = new List<Hogar>();
            foreach (DataRow item in hogaresI.Rows)
            {
                var hogar = new Hogar()
                {
                    Id = int.Parse(item[0].ToString()),
                    Nombre = item[1].ToString()
                };
                hogares.Add(hogar);
            }

            //var hogaresNombreList = hogares.Select(person => person.Nombre).ToList();
            var hogaresNombreList = hogares;
            ViewData["HogaresNombre"]= hogaresNombreList;
            return View();
        }

        // POST: Habitacion/Create
        [HttpPost]
        public ActionResult Create(Habitacion collection)
        {
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    var cmd = new MySqlCommand
                    {
                        CommandText = "INSERT INTO `habitaciones` (`id`, `nombre`, `id_hogar`) VALUES (NULL, @nombre, @IdHogar)"
                    };
                    cmd.Parameters.Add("@nombre", collection.Nombre);
                    cmd.Parameters.Add("@IdHogar", collection.IdHogar);

                    SqlConnector.Instance.ExecuteQuery(cmd, out var r);

                    return RedirectToAction("Index");
                }
            }

            return View();
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
        public ActionResult Edit(Habitacion item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var cmd = new MySqlCommand
                    {
                        CommandText = "Update habitaciones set id = @id, nombre = @nombre," +
                                      " id_hogar = @IdHogar   where id = @id"
                    };
                    cmd.Parameters.Add("@id", item.Id);
                    cmd.Parameters.Add("@nombre", item.Nombre);
                    cmd.Parameters.Add("@IdHogar", item.IdHogar);

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

        // GET: Habitacion/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Habitacion/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult Delete(int id, Habitacion model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var cmd = new MySqlCommand
                    {
                        CommandText = "DELETE FROM habitaciones WHERE id = @id"
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
