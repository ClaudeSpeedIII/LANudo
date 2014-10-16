using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Microsoft.Xna.Framework.Graphics;

namespace Idioma
{
    public class Textos
    {
        string rotulo; public string Rotulo { get { return rotulo; } }
        string iso; public string ISO { get { return iso; } }
        XElement textos;

        public Textos(string _rotulo, string _iso, XElement _textos) {
            rotulo = _rotulo;
            iso = _iso;
            textos = _textos;
        }
        public Textos()
        {
            rotulo = "STRING FAIL";
            iso = "fail";
        }


        public string Val(string codigo)
        {
            try
            {
                return textos.Element(codigo).Value;
            }
            catch (Exception erro)
            {
                Console.WriteLine("Requisitado código inexistente no arquivo de idioma atual " + erro.Message);
                return "STRING FAIL";
            }
        }

    }
}
