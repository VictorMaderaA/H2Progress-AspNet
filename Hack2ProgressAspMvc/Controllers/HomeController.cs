using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hack2ProgressAspMvc.Models;
using Library.MySQL;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace Hack2ProgressAspMvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GraphSpline()
        {
            List<DataPoint> dataPoints = new List<DataPoint>();
            var cmd = new MySqlCommand
            {
                CommandText = $"select * from consumo where fecha >= last_day(now()) + interval 10 day - interval 3 month and idToma = {new Random().Next(1,10)};"
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

            foreach (var i in consu)
            {
                dataPoints.Add(new DataPoint(i.Fecha.Ticks, i.ConsumoEnergetico));
            }

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            return View();
        }
    }
}