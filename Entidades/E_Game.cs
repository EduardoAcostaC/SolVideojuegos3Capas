using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class E_Game
    {
        public int idVideojuego { get; set; }
        public string nombre { get; set; }
        public DateTime fechaLanzamiento { get; set; }
        public decimal precio { get; set; }
        public string imagen { get; set; }
        public bool activo { get; set; }

    }
}
