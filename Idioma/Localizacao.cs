using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Globalization;
using System.IO;
using System.Xml.Linq;

namespace Idioma
{
    public class Localizacao
    {
        private static List<Textos> idiomas = new List<Textos>();
        private Textos fail = new Textos();
        public static List<Textos> Idiomas { get { return idiomas; } }
        public Textos Fail { get { return fail; } }

        public Localizacao(string caminho)
        {
            try
            {
                foreach (string arquivo in Directory.EnumerateFiles(caminho, "*.xml"))
                {
                    XDocument xml = XDocument.Load(arquivo);
                    idiomas.Add(new Textos(xml.Root.Element("Name").Value, xml.Root.Element("ISO").Value, xml.Root.Element("Strings")));

                }
            }
            catch (Exception erro)
            {
                Console.WriteLine("Pau ao carregar um XML " + erro.Message);
            }
        }
    }
}
