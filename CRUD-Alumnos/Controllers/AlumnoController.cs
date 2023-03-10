using CRUD_Alumnos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD_Alumnos.Controllers
{
    public class AlumnoController : Controller
    {
        // GET: Alumno
        public ActionResult Index()
        {
            //Utilizando Linq
            //using (AlumnosContext db = new AlumnosContext()) // Abro una conexión.
            //{

            //    //List<Alumno> lista = db.Alumno.Where(a => a.Edad > 18).ToList(); //Linq

            //    // Otra forma de armar un conjunto de datos con Linq.
            //    // Es lo más aconsejable para armar un conjunto de datos proveniente de varias tablas.
            //    var data = from a in db.Alumno
            //               join c in db.Ciudad on a.CodCiudad equals c.Id
            //               select new AlumnoCE
            //               {
            //                   Id = a.Id,
            //                   Nombres = a.Nombres,
            //                   Apellidos = a.Apellidos,
            //                   Edad = a.Edad,
            //                   Sexo = a.Sexo,
            //                   NombreCiudad = c.Nombre,
            //                   FechaRegistro = a.FechaRegistro
            //               };

            //    // return View(db.Alumno.ToList()); //Para enviar todos los alumnos
            //    return View(data.ToList()); //Para enviar todos los alumnos
            //}

            // Con consulta SQL
            string sql = @"SELECT a.Id , a.Nombres, a.Apellidos, a.Edad, a.Sexo, a.FechaRegistro, c.Nombre AS NombreCiudad
                                FROM Alumno a
                                inner join Ciudad c on a.CodCiudad = c.Id";

            using (AlumnosContext db = new AlumnosContext()) // Abro una conexión.
            {
                return View(db.Database.SqlQuery<AlumnoCE>(sql).ToList()); 
            }
        }

        public ActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Agregar(Alumno a)
        {
            if (!ModelState.IsValid) // Si las validaciones no son correctas, vuelve a la vista Agregar.
                return View();

            try
            {
                //using(AlumnosContext db = new AlumnosContext()) //Abro conexion y cuanto termina using cierra la conexion automáticamente.
                using (var db = new AlumnosContext()) // Otra forma de abrir conexion. Se cierra cuando finaliza el comando using.
                {
                    a.FechaRegistro = DateTime.Now;

                    db.Alumno.Add(a); // Comando para cargar registro en la BD.  
                    db.SaveChanges(); // Agregar cambios a la BD. Una expecie de Commit o Flush.
                    return RedirectToAction("Index"); // Si todo salió bien, redireccionar a otra página que no es la vista el propio controlador.
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al agregar Alumno - " + ex.Message);
                return View();
            }

        }

        public ActionResult Agregar2()
        {
            return View();
        }

        public ActionResult ListaCiudades()
        {
            using (var db = new AlumnosContext())
            {
                return PartialView(db.Ciudad.ToList());
            }
        }

        public ActionResult Editar(int Id)
        {
            using (var db = new AlumnosContext())
            {
                Alumno alu = db.Alumno.Where(a => a.Id == Id).FirstOrDefault();  // Si encuentra más de 1 registro, solo tomará el primero encontrado.
                // Alumno al2 = db.Alumno.Find(Id); // Para usar de esta forma tengo que estar seguro que solo encontrará 1 registro.
                return View(alu);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Alumno a) // Los datos que vinen de la vista.
        {
            try
            {
                if (!ModelState.IsValid) // Si las validaciones con los data annotation no son correctas, vuelve a la vista Agregar.
                    return View();

                using (var db = new AlumnosContext())
                {
                    Alumno al = db.Alumno.Find(a.Id); // Preparo los datos que voy a enviar a la base de datos.
                    al.Nombres = a.Nombres;
                    al.Apellidos = a.Apellidos;
                    al.Edad = a.Edad;
                    al.Sexo = a.Sexo;

                    db.SaveChanges();

                    return RedirectToAction("Index"); // Si todo salió bien, redireccionar a otra página que no es la vista el propio controlador.
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult DetalleAlumno(int id)
        {
            using (var db = new AlumnosContext())
            {
                Alumno alu = db.Alumno.Find(id);
                return View(alu);
            }
        }

        public ActionResult EliminarAlumno(int id)
        {
            using (var db = new AlumnosContext())
            {
                Alumno alu = db.Alumno.Find(id);
                db.Alumno.Remove(alu);
                db.SaveChanges();
                return RedirectToAction("Index"); // Para ir a una vista que no es la del propio controlador.
            }
        }

        public static string NombreCiudad(int CodCiudad) // Se crea como estático para acceder directamente sin tener que instanciarlo.
        {
            using (var db = new AlumnosContext())
            {
                return db.Ciudad.Find(CodCiudad).Nombre; // Consulta linq
            }
        }




        //// GET: Alumno/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: Alumno/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Alumno/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Alumno/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Alumno/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Alumno/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Alumno/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
