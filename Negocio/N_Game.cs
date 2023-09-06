using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Negocio
{
    public class N_Game
    {
        public void AgregarVideojuego(E_Game juego)
        {
            D_Game datos = new D_Game();
            datos.AgregarVideojuego(juego);
        }

        public List<E_Game> ObtenerVideojuego()
        {
            D_Game datos = new D_Game();
            return datos.ObtenerTodos();
        }

        public E_Game ObtenerVideojuegoPorId (int id)
        {
            D_Game datos = new D_Game();
            return datos.ObtenerVideojuegoPorId(id);
        }

        public void GuardarEdicion(E_Game juego) 
        {
            D_Game datos = new D_Game();
            datos.GuardarEdicion(juego);
        }

        public void ELiminarVideojuego(int id)
        {
            D_Game datos = new D_Game();
            datos.EliminarVideojuego(id);
        }

        public bool ExisteJuego(string nombre)
        {
            D_Game datos = new D_Game();
            E_Game juego = datos.BuscarJuegoPorNombre(nombre);

            if(juego.idVideojuego > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool EsFechaFutura(DateTime fecha)
        {
            DateTime fechaActual = DateTime.Now;

            if(fecha > fechaActual)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool EsFormatoValido(string nombreArchivo)
        {
            Regex regex = new Regex(@"\.(jpg|png)$", RegexOptions.IgnoreCase);

            if (regex.IsMatch(nombreArchivo))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
