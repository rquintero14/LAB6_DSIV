using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio6
{
    public class Libros
    {
        public string Titulo { get; set; }
        public string Genero { get; set; }
        public string Autor { get; set; }
        public int Id { get; set; }
        public int CantidadDisp { get; set; }


        public Libros(string titulo, string genero, string autor, int id, int cantDisp)
        {            
            Titulo = titulo.Trim();
            Genero = genero.Trim();
            Autor = autor.Trim();
            Id = id;
            CantidadDisp = cantDisp;
        }
        public Libros()
        {
        }
    }
}
