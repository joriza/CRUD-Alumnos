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
            AlumnosContext db = new AlumnosContext(); // Abro una conexión.

            //List<Alumno> lista = db.Alumno.Where(a => a.Edad > 18).ToList(); //Linq

            return View(db.Alumno.ToList()); //Para enviar todos los alumnos
        }

        public  ActionResult Agregar() 
        {
            return View(); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Agregar(Alumno a)
        {
            if(!ModelState.IsValid) // Si las validaciones no son correctas, vuelve a la vista Agregar.
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
                if (!ModelState.IsValid) // Si las validaciones no son correctas, vuelve a la vista Agregar.
                    return View();

                using (var db = new AlumnosContext())
                {
                    Alumno al = db.Alumno.Find(a.Id); // Preparo los datos que voy a enviar a la base de datos.
                    al.Nombres = a.Nombres;
                    al.Apellidos = a.Apellidos;
                    al.Edad = a.Edad;
                    al.Sexo= a.Sexo;

                    db.SaveChanges();

                    return RedirectToAction("Index"); // Si todo salió bien, redireccionar a otra página que no es la vista el propio controlador.
                }

            }
            catch (Exception)
            {

                throw;
            }
        }













        // GET: Alumno/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Alumno/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Alumno/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Alumno/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Alumno/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Alumno/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Alumno/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
