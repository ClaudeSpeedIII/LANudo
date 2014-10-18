
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Idioma;
using System.Globalization;
using Microsoft.Xna.Framework;

namespace LANudo
{
    public class Configuracoes
    {
        Motor motor;
        Localizacao locale;
        GraphicsDeviceManager graficos;

        string failLang;

        private static int largura;
        private static int altura;

        public static int Largura { get { return largura; } }
        public static int Altura { get { return altura; } }

        public Configuracoes(Motor _motor, GraphicsDeviceManager _graficos, Localizacao idioma, string failLangISO6391)
        {
            motor = _motor;
            locale = idioma;
            graficos = _graficos;
            failLang = failLangISO6391;
        }

        public void AtualizaDimensoes()
        {
            largura = motor.GraphicsDevice.Viewport.Width;
            altura = motor.GraphicsDevice.Viewport.Height;
        }

        public void SetaRes(int x, int y, bool janela)
        {
            graficos.PreferredBackBufferWidth = x;
            graficos.PreferredBackBufferHeight = y;
            if (graficos.IsFullScreen)
            { if (janela) { graficos.ToggleFullScreen(); } }
            else
            { if (!janela) { graficos.ToggleFullScreen(); } }
            largura = x;
            altura = y;
        }

        public void SetaIdioma(string iso6391)
        {
            if (iso6391 == "auto")
            {
                foreach (Textos idioma in locale.Idiomas)
                {
                    if (idioma.ISO == CultureInfo.CurrentUICulture.TwoLetterISOLanguageName) { motor.SetaTextos = idioma; break; }
                }
                return;
            }
            else
            {
                foreach (Textos idioma in locale.Idiomas)
                {
                    if (idioma.ISO == iso6391) { motor.SetaTextos = idioma; break; }
                }
                return;
            }
            foreach (Textos idioma in locale.Idiomas)
            {
                if (idioma.ISO == failLang) { motor.SetaTextos = idioma; break; }
            }
        }

    }
}
