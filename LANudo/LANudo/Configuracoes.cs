
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Idioma;
using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LANudo
{
    public class Configuracoes
    {
        Motor motor;
        Localizacao locale;
        GraphicsDeviceManager graficos;
        List<Vector2> resolucoes = new List<Vector2>();

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
            AlimentaResolucoes();
        }

        void AlimentaResolucoes()
        {
            foreach (DisplayMode modo in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
            {
                Vector2 resTemp = new Vector2(modo.Width, modo.Height);
                if (!resolucoes.Contains(resTemp)) { resolucoes.Add(resTemp); }
            }
        }

        public void AtualizaDimensoes()
        {
            largura = motor.GraphicsDevice.Viewport.Width;
            altura = motor.GraphicsDevice.Viewport.Height;
        }

        public void SetaRes(int x, int y, bool janela, bool primeira = false)
        {
            graficos.PreferredBackBufferWidth = x;
            graficos.PreferredBackBufferHeight = y;
            if (graficos.IsFullScreen)
            { if (janela) { graficos.ToggleFullScreen(); if (!primeira) { motor.Window.BeginScreenDeviceChange(true); } } }
            else
            { if (!janela) { graficos.ToggleFullScreen(); if (!primeira) { motor.Window.BeginScreenDeviceChange(false); } } }
            largura = x;
            altura = y;
            if (!primeira) { graficos.ApplyChanges(); }
        }

        public void SetaIdioma(string iso6391)
        {
            if (iso6391 == "auto")
            {
                foreach (Textos idioma in locale.Idiomas)
                {
                    if (idioma.ISO == CultureInfo.CurrentUICulture.TwoLetterISOLanguageName)
                    {
                        motor.SetaTextos = idioma;
                        return;
                    }
                }
            }
            else
            {
                foreach (Textos idioma in locale.Idiomas)
                {
                    if (idioma.ISO == iso6391)
                    {
                        motor.SetaTextos = idioma;
                        return;
                    }
                }
            }
            foreach (Textos idioma in locale.Idiomas)
            {
                if (idioma.ISO == failLang) { motor.SetaTextos = idioma; break; }
            }
        }

    }
}
