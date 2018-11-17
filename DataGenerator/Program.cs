using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hack2ProgressAspMvc.Controllers;
using Hack2ProgressAspMvc.Models;
using Library.MySQL;

namespace DataGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnector.Instance.SetBuilder("Server=127.0.0.1;Database=hackdb;Uid=root;SslMode=none;");
            //new GenerateData().GenerateHouses();
            new GenerateData().GenerateConsumo();
            Console.WriteLine("Termino 1");
            Console.ReadLine();
        }
    }

    internal class GenerateData
    {
        public string[] places = new string[]
        {
            "Hamburg","Yokohama","Kampala","Bangkok","Berlin",
            "Guadalajara","Shantou","Recife","Harare","Saitama",
            "Rio de Janeiro","Abidjan","Abidjan","Abidjan"
        };

        public string[] habitaciones = new string[]
        {
            "Cocina","Baño","Sala","Exterior","Alcoba",
            "Comedor","Pasillo","Cochera","Baño Completo","Dormitorio"
        };


        public async void GenerateHouses()
        {
            Random r = new Random();


            var cumuloHabitaciones = 0;
            for (var casaI = 1; casaI < 10; casaI++)
            {
                GenerateHogar(places[casaI-1]);
                int habitacionesInt = r.Next(2, 7);

                await Task.Delay(100);
                for (var habitacionI = 0; habitacionI < habitacionesInt; habitacionI++)
                {
                    GenerateHabitacion(habitaciones[r.Next(0, habitaciones.Length-1)], casaI);
                    int enchufesInt = r.Next(2, 7);

                    await Task.Delay(100);
                    for (var enchufeI = 0; enchufeI < enchufesInt; enchufeI++)
                    {
                        GenerateToma(habitacionI + cumuloHabitaciones);

                        await Task.Delay(100);
                    }
                }
                cumuloHabitaciones += habitacionesInt;
            }

            Console.WriteLine("Termino");
            Console.ReadLine();
        }


        public void GenerateConsumo()
        {
            Random r = new Random();

            for (int i = 1; i < 183; i++) // loop enchufes
            {
                var rMin = r.Next(25, 100);
                var rMax = r.Next(125, 250);

                
                var Date = DateTime.Today.AddMonths(-3);
                for (int j = 1; j < 150; j++) // loop dias
                {
                    var consumo = r.Next(rMin, rMax);

                    var h = new Consumo()
                    {
                        ConsumoEnergetico = consumo,
                        Fecha = Date,
                        IdToma = i
                    };
                    Date.AddDays(1);
                    new ConsumoController().Create(h);

                }
            }
        }




        public void GenerateHogar(string nombreLugar)
        {
            var h = new Hogar()
            {
                Nombre = nombreLugar
            };

            new HogarController().Create(h);
        }

        public void GenerateHabitacion(string nombreAbitacion, int idHogar)
        {
            var h = new Habitacion()
            {
                Nombre = nombreAbitacion,
                IdHogar = idHogar
            };

            new HabitacionController().Create(h);
        }

        public void GenerateToma(int idHabitacion)
        {
            var h = new TomaEnergia()
            {
                 IdHabitacion = idHabitacion
            };

            new TomaEnergiaController().Create(h);
        }

    }


}
