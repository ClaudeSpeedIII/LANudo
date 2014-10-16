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

namespace Idioma
{
    public class Localizacao
    {

        private HashSet<Textos> idiomas = new HashSet<Textos>(); public HashSet<Textos> Idiomas { get { return idiomas; } }
        private Textos idiomaAtual; public Textos IdiomaAtual { get { return idiomaAtual; } }

        public Localizacao(TodosTextos[] xml, string iso6391, out Action<string> Redefine)
        {
            foreach (TodosTextos textos in xml)
            {
                idiomas.Add(new Textos(textos));
            }
            SetaIdioma(iso6391);
            Redefine = SetaIdioma;
        }
        private void SetaIdioma(string iso6391)
        {
            if (iso6391 == "auto")
            {
                foreach (Textos idioma in idiomas)
                {
                    if (idioma.ISO == CultureInfo.CurrentUICulture.TwoLetterISOLanguageName) { idiomaAtual = idioma; break; }
                }
            }
            else
            {
                foreach (Textos idioma in idiomas)
                {
                    if (idioma.ISO == iso6391) { idiomaAtual = idioma; break; }
                }
            }
        }
    }
}
