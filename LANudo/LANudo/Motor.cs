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
using Idioma;

namespace LANudo
{
    public class Motor : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        static SpriteBatch desenhista;
        static SpriteFont escritor; public static Vector2 MedeTexto(string texto) { try { return escritor.MeasureString(texto); } catch (Exception erro) { throw erro; } }
        static Configuracoes config; public static Configuracoes Config { get { return config; } }


        private static Textos idiomaAtual = new Textos(); public static Textos Textos { get { return idiomaAtual; } }
        public Textos SetaTextos { set { idiomaAtual = value; ;} }


        private static GameTime tempo = new GameTime();
        public static GameTime Tempo { get { return tempo; } }

        private static bool rodando = true;
        public static bool Rodando { get { return rodando; } }

        //Todo jogo possui um logo e precisa escrever tralha na tela, certo?
        private static GUI menu;
        private static Jogo ludo;
        private Cursor rato;



        public void Redimensionado(object sender, EventArgs e)
        {
            config.AtualizaDimensoes();
            menu.Redimensionado();
            ludo.Redimensionado();
        }


        void QUIT()
        {
            rodando = false;
            Exit();
        }

        public Motor()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.SynchronizeWithVerticalRetrace = false;
            this.IsFixedTimeStep = false;
            config = new Configuracoes(this, graphics, new Localizacao(Constantes.caminho_idiomas()), Constantes.idioma_emergencia());
            config.SetaRes(Constantes.resolucao_x(), Constantes.resolucao_y(), Constantes.janela(), true);
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
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            desenhista = new SpriteBatch(GraphicsDevice);
            escritor = Content.Load<SpriteFont>(Constantes.caminho_fonte());



            config.SetaIdioma(Constantes.idioma_inicial(), true);
            rato = new Cursor(
                desenhista,
                Content.Load<Texture2D>(Constantes.caminho_rato()),
                Content.Load<Texture2D>(Constantes.caminho_rato_apertado())
                );

            ludo = new Jogo(desenhista,
                Content.Load<Texture2D>(Constantes.caminho_tabuleiro_fundo()),
                Content.Load<Texture2D>(Constantes.caminho_tabuleiro_centro()),
                Content.Load<Texture2D>(Constantes.caminho_tabuleiro_tile()),
                Content.Load<Texture2D>(Constantes.caminho_tabuleiro_seta()),
                Content.Load<Texture2D>(Constantes.caminho_peao())
                );
            menu = new GUI(
                desenhista,
                escritor,
                Content.Load<Texture2D>(Constantes.caminho_splash_screen()),
                Content.Load<Texture2D>(Constantes.caminho_menu_fundo()),
                Content.Load<Texture2D>(Constantes.caminho_menu_logo()),
                Content.Load<Texture2D>(Constantes.caminho_menu_dado()),
                Content.Load<Texture2D>(Constantes.caminho_menu_tabuleiro()),
                Content.Load<Texture2D>(Constantes.caminho_botao()),
                Content.Load<Texture2D>(Constantes.caminho_botao_alargado()),
                Content.Load<Texture2D>(Constantes.caminho_seta()),
                QUIT,
                rato,
                Config,
                ludo
                );
            menu.Ativar();
            Redimensionado(this, new EventArgs());
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

            ludo.Atualizar();
            menu.Atualizar();
            rato.Atualizar();

            tempo = gameTime;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(GUI.CorFundo);

            desenhista.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);

            if (GUI.ImgFundo != null)
            {
                GUI.ImgFundo.Desenhar();
            }
            //Sequencia de "joga na tela"

            menu.Desenhar();

            rato.Desenhar();

            ludo.Desenhar();
            desenhista.End();



            base.Draw(gameTime);
        }
    }
}
