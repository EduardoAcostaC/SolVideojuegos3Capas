using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebVideojuegos3Capas.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            List<E_Game> lista = new List<E_Game> ();
            try
            {
                N_Game negocio = new N_Game();
                lista = negocio.ObtenerVideojuego();
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return View("VistaPrincipal", lista);
        }

        public ActionResult IrFormulario ()
        {
            return View("VistaFormulario");
        }

        public ActionResult FormularioPost(E_Game videojuego, HttpPostedFileBase ArchivoImagen)
        {

            try
            {
                N_Game negocio = new N_Game();
                bool existe = negocio.ExisteJuego(videojuego.nombre);
                if(existe == true)
                {
                    TempData["error"] = $"Ya existe el juego {videojuego.nombre}";
                    return RedirectToAction("Index");
                }

                bool esFutura = negocio.EsFechaFutura(videojuego.fechaLanzamiento);
                if(esFutura == true)
                {
                    TempData["error"] = $"La fecha ingresada es una fecha futura";
                    return RedirectToAction("Index");
                }

                //Crear la ruta donde se va a guardar la imagen
                string rutaArchivo = Path.Combine(Server.MapPath("~/Imagenes"), ArchivoImagen.FileName);
                //Guardar el archivo
                ArchivoImagen.SaveAs(rutaArchivo);
                videojuego.imagen = ArchivoImagen.FileName;

                bool esValido = negocio.EsFormatoValido(ArchivoImagen.FileName);
                if (esValido == false)
                {
                    TempData["error"] = $"El formato de imagen ingresado no es valido";
                    return RedirectToAction("Index");
                }


                negocio.AgregarVideojuego(videojuego);

                TempData["mensaje"] = $"El juego {videojuego.nombre} se agrego correctamente";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                TempData["error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        public ActionResult Editar(int id)
        {
            try
            {
                N_Game negocio = new N_Game();
                E_Game juego = negocio.ObtenerVideojuegoPorId(id);

                return View("VistaEditar", juego);
            }
            catch (Exception ex)
            {

                TempData["error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        public ActionResult EditarPOST(E_Game juego, HttpPostedFileBase ArchivoImagen)
        {
            try
            {
                //Crear la ruta donde se va a guardar la imagen
                string rutaArchivo = Path.Combine(Server.MapPath("~/Imagenes"), ArchivoImagen.FileName);
                //Guardar el archivo
                ArchivoImagen.SaveAs(rutaArchivo);

                N_Game negocio = new N_Game();

                //Validaciones 
                bool existe = negocio.ExisteJuego(juego.nombre);
                if (existe == true)
                {
                    TempData["error"] = $"Ya existe el juego {juego.nombre}";
                    return RedirectToAction("Index");
                }

                bool esFutura = negocio.EsFechaFutura(juego.fechaLanzamiento);
                if (esFutura == true)
                {
                    TempData["error"] = $"La fecha ingresada es una fecha futura";
                    return RedirectToAction("Index");
                }

                juego.imagen = ArchivoImagen.FileName;
                bool esValido = negocio.EsFormatoValido(ArchivoImagen.FileName);
                if (esValido == false)
                {
                    TempData["error"] = $"El formato de imagen ingresado no es valido";
                    return RedirectToAction("Index");
                }

                negocio.GuardarEdicion(juego);

                TempData["mensaje"] = $"El juego {juego.nombre} se actualizo correctamente";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                TempData["error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        public ActionResult Eliminar(int id)
        {
            try
            {
                N_Game negocio = new N_Game();
                E_Game juego = negocio.ObtenerVideojuegoPorId(id);
                return View("VistaEliminar" ,juego);

            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index");
              
            }
        }

        public ActionResult EliminarVideojuego(int id)
        {
            try
            {
                N_Game negocio = new N_Game();
                negocio.ELiminarVideojuego(id);

                TempData["mensaje"] = $"El videojuego ha sido eliminado correctamente";
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {

                TempData["error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}