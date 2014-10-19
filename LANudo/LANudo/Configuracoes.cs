
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
        private List<Vector2> resolucoes = new List<Vector2>(); public List<Vector2> Resolucoes { get { return resolucoes; } }

        string failLang;

        private static int largura;
        private static int altura;
        private static bool janela;

        public static int Largura { get { return largura; } }
        public static int Altura { get { return altura; } }
        public static Vector2 Resolucao { get { return new Vector2(largura, altura); } }
        public static bool Janela { get { return janela; } }

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
            if (!primeira)
            {
                if (graficos.IsFullScreen)
                { if (janela) { graficos.ToggleFullScreen(); motor.Window.BeginScreenDeviceChange(false); } }
                else
                { if (!janela) { graficos.ToggleFullScreen(); motor.Window.BeginScreenDeviceChange(true); } }
            }
            else { graficos.IsFullScreen = !janela; }
            largura = x;
            altura = y;
            Configuracoes.janela = janela;
            if (!primeira)
            {
                graficos.ApplyChanges();
                if (!janela) { motor.Redimensionado(this, new EventArgs()); }
            }
        }
        public void SetaRes(Vector2 vetor)
        {
            graficos.PreferredBackBufferWidth = (int)vetor.X;
            graficos.PreferredBackBufferHeight = (int)vetor.Y;
            largura = (int)vetor.X;
            altura = (int)vetor.Y;
            graficos.ApplyChanges();
            //motor.Redimensionado(this, new EventArgs());
        }

        public void ChaveiaJanela()
        {
            janela = !janela;
            if (graficos.IsFullScreen)
            { if (janela) { graficos.ToggleFullScreen(); motor.Window.BeginScreenDeviceChange(true); } }
            else
            { if (!janela) { graficos.ToggleFullScreen(); motor.Window.BeginScreenDeviceChange(false); } }
            graficos.ApplyChanges();
<<<<<<< HEAD
            if (!janela) { motor.Redimensionado(this, new EventArgs()); }
=======
            // motor.Redimensionado(this, new EventArgs());

>>>>>>> ed6f46bc12575598aa8000f482fdf171f26b1cd9
        }

        public void SetaIdioma(string iso6391, bool primeira = false)
        {
            if (iso6391 == "auto")
            {
                foreach (Textos idioma in Localizacao.Idiomas)
                {
                    if (idioma.ISO == CultureInfo.CurrentUICulture.TwoLetterISOLanguageName)
                    {
                        motor.SetaTextos = idioma;
                        if (!primeira) { motor.Redimensionado((object)this, new EventArgs()); }
                        return;
                    }
                }
            }
            else
            {
                foreach (Textos idioma in Localizacao.Idiomas)
                {
                    if (idioma.ISO == iso6391)
                    {
                        motor.SetaTextos = idioma;
                        if (!primeira) { motor.Redimensionado((object)this, new EventArgs()); }
                        return;
                    }
                }
            }
            foreach (Textos idioma in Localizacao.Idiomas)
            {
                if (idioma.ISO == failLang)
                {
                    motor.SetaTextos = idioma;
                    if (!primeira) { motor.Redimensionado((object)this, new EventArgs()); }
                }
            }
        }

    }
}
