using Microsoft.Owin.Hosting;
using RestSinAuth.Storages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSinAuth
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var con = new Conexion().Open())
            {
                var command = con.CreateCommand();
                command.CommandText = @"
                CREATE TABLE IF NOT EXISTS USUARIOS (
                   ID                INTEGER   PRIMARY KEY   AUTOINCREMENT,
                   CORREO            TEXT      NOT NULL,
                   NOMBRES           TEXT      NOT NULL,
                   APELLIDOS         TEXT,
                   FECHANACIMIENTO   TEXT,
                   ACTIVO            INT
                );
                ";
                command.ExecuteNonQuery();
            }

            var url = "http://localhost:9090";
            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine($"Servicio iniciado en {url}/usuarios");
                Console.WriteLine("Enter para salir");
                Console.ReadLine();
            }
        }
    }
}
