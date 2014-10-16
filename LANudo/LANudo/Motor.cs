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
using Idioma;

namespace LANudo
{
    public delegate void ManipuladorClique(Botao origem);

    public class Motor : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Localizacao locale;
        private static Textos idiomaAtual = new Textos(); public static Textos Textos { get { return idiomaAtual; } }


        public void SetaIdioma(string iso6391)
        {
            if (iso6391 == "auto")
            {
                foreach (Textos idioma in locale.Idiomas)
                {
                    if (idioma.ISO == CultureInfo.CurrentUICulture.TwoLetterISOLanguageName) { idiomaAtual = idioma; break; }
                }
            }
            else
            {
                foreach (Textos idioma in locale.Idiomas)
                {
                    if (idioma.ISO == iso6391) { idiomaAtual = idioma; break; }
                }
            }
        }

        private static GameTime tempo = new GameTime();
        public static GameTime Tempo { get { return tempo; } }

        private static bool rodando = true;
        public static bool Rodando { get { return rodando; } }

        //Todo jogo possui um logo e precisa escrever tralha na tela, certo?
        private static GUI menu;
        private Cursor rato;

        private static int largura;
        private static int altura;

        public static int Largura { get { return largura; } }
        public static int Altura { get { return altura; } }

        public void AtualizaDimensoes()
        {
            Motor.largura = this.GraphicsDevice.Viewport.Width;
            Motor.altura = this.GraphicsDevice.Viewport.Height;
        }

        void Redimensionado(object sender, EventArgs e)
        {
            AtualizaDimensoes();
            menu.Redimensionado();
        }

        Color corFundo;

        public Color ChecaCorFundo()
        {
            switch (GUI.Estado)
            {
                case GUI.EstadoGUI.intro:
                    corFundo = Constantes.cor_de_fundo_Intro();
                    break;
                case GUI.EstadoGUI.inicial:
                    corFundo = Constantes.cor_de_fundo_MenuInicial();
                    break;
                case GUI.EstadoGUI.conf:
                    corFundo = Constantes.cor_de_fundo_MenuConf();
                    break;
                case GUI.EstadoGUI.iniciar:
                    corFundo = Constantes.cor_de_fundo_MenuNovoJogo();
                    break;
                case GUI.EstadoGUI.pausado:
                    break;
                case GUI.EstadoGUI.emJogo:
                    break;
            }
            return corFundo;
        }

        void Sair()
        {
            rodando = false;
            Exit();
        }

        public Motor()
        {
            graphics = new GraphicsDeviceManager(this);
            Motor.largura = Constantes.resolucao_x();
            Motor.altura = Constantes.resolucao_y();
            graphics.PreferredBackBufferWidth = Motor.largura;
            graphics.PreferredBackBufferHeight = Motor.altura;
            this.Window.AllowUserResizing = true;
            this.Window.ClientSizeChanged += new EventHandler<EventArgs>(Redimensionado);
            Content.RootDirectory = "Content";

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            this.AtualizaDimensoes();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            locale = new Localizacao(Constantes.caminho_idiomas());
            SetaIdioma(Constantes.idioma_padrao());
            rato = new Cursor(
                spriteBatch,
                Content.Load<Texture2D>(Constantes.caminho_rato()),
                Content.Load<Texture2D>(Constantes.caminho_rato_apertado())
                );
            //Precisamos de uma interface
            menu = new GUI(
                spriteBatch,
                Content.Load<SpriteFont>(Constantes.caminho_fonte()),
                Content.Load<Texture2D>(Constantes.caminho_logo_menu()),
                Content.Load<Texture2D>(Constantes.caminho_logo_intro()),
                Content.Load<Texture2D>(Constantes.caminho_botao()),
                Content.Load<Texture2D>(Constantes.caminho_seta()),
                rato,
                Sair
                );
            ChecaCorFundo(); ;
            menu.Ativar();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            ChecaCorFundo();
            menu.Atualizar();
            rato.Atualizar();

            tempo = gameTime;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(corFundo);

            //Sequencia de "joga na tela"

            spriteBatch.Begin();

            menu.Desenhar();

            rato.Desenhar();

            spriteBatch.End();



            base.Draw(gameTime);
        }
    }
}
