using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Idioma;

namespace LANudo
{
    public class GUI
    {
        SpriteBatch desenhista;
        SpriteFont fonte;

        Cursor rato;
        Action sair;
        Configuracoes conf;



        bool ativo;

        public bool Ativado() { return ativo; }

        public void Ativar() { ativo = true; }

        public void Desativar() { ativo = false; }

        public GUI(SpriteBatch _desenhista, SpriteFont _fonte, Texture2D _logo, Texture2D _intro, Texture2D _botao, Texture2D _seta, Cursor _rato, Action _sair, Configuracoes _conf, bool _ativo = true)
        {
            desenhista = _desenhista;
            fonte = _fonte;
            logo = _logo;
            intro = _intro;
            botao = _botao;
            seta = _seta;
            rato = _rato;
            sair = _sair;
            conf = _conf;

            InstanciaLogoIntro();
            InstanciaMenuInicial();
            InstanciaMenuConfiguracoes();

            ativo = _ativo;
            estado = EstadoGUI.intro;
            Redimensionado();
        }

        public enum EstadoGUI { intro, inicial, conf, iniciar, pausado, emJogo };
        static EstadoGUI estado;
        public static EstadoGUI Estado { get { return estado; } }

        //Texturas

        Texture2D intro;
        Texture2D logo;
        Texture2D botao;
        Texture2D seta;

        //Intro
        Sprite logoIntro;

        void InstanciaLogoIntro() { logoIntro = new Sprite(desenhista, intro, Recursos.RetanguloCentralizado(intro.Bounds, Constantes.escala_logo_intro())); }

        void ContaLogoIntro()
        {
            if (estado == EstadoGUI.intro)
            {
                if (Motor.Tempo.TotalGameTime.TotalMilliseconds > Constantes.duracao_intro().TotalMilliseconds) { logoIntro.Desativar(); VaiMenuInicial(); rato.Ativar(); }
            }
        }

        //Menu inicial
        Botoes menuInicial;
        Sprite logoInicial;
        List<Elemento> elementosMenuInicial = new List<Elemento>();

        void InstanciaMenuInicial()
        {
            menuInicial = new Botoes(desenhista, fonte, botao, Constantes.esquema_cores_botao(), Constantes.pos_menu_inicial(), Constantes.escala_menu_inicial(), Constantes.escala_texto_menu_inicial(), Constantes.distancia_botoes_menu_inicial(), true, false);
            menuInicial.AdicionaBotao(Motor.Textos.Val("NEW_GAME"), Jogar);
            menuInicial.AdicionaBotao(Motor.Textos.Val("SETTINGS"), Conf);
            menuInicial.AdicionaBotao(Motor.Textos.Val("QUIT"), Sair);
            logoInicial = new Sprite(desenhista, logo, Recursos.RetanguloRelativamenteDeslocado(logo.Bounds, Constantes.escala_logo_inicial(), Constantes.pos_logo_inicial()), false);
            elementosMenuInicial.Add(logoInicial);
            elementosMenuInicial.Add(menuInicial);
        }
        void Sair(Botao remetente) { sair(); }

        void Jogar(Botao remetente) { }

        void Conf(Botao remetente) { VaiMenuConf(); }

        void VaiMenuInicial() { estado = EstadoGUI.inicial; foreach (Elemento e in elementosMenuInicial) { e.Ativar(); } }
        void SaiMenuInicial() { foreach (Elemento e in elementosMenuInicial) { e.Desativar(); } }

        //Configuracoes
        Lista listaIdiomas;
        Botao saiConfVoltaInicial;

        void InstanciaMenuConfiguracoes()
        {
            saiConfVoltaInicial = new Botao(desenhista, botao, Constantes.esquema_cores_botao(), new Vector2(0.2f, 0.9f), 0.1f, fonte, "Voltar", 1f, false, false);
            saiConfVoltaInicial.Clicado += SaiConfVoltaIniciar;

            List<string> vetor = new List<string>();
            vetor.Add("Espaço");
            vetor.Add("Nuvens");
            vetor.Add("Superficie");
            vetor.Add("Metrô");
            vetor.Add("Lava");
            vetor.Add("Bedrock");
            vetor.Add("Void");
            List<ElementoLista> result = ElementoLista.CriaVariosElementoLista(vetor, Constantes.esquema_cores_lista_deselecionada(), Constantes.esquema_cores_lista_selecionada());
            listaIdiomas = new Lista(desenhista, fonte, result, Lista.TipoEvento.SelecionavelIntermanete, botao, null, botao, seta, Constantes.esquema_cores_lista_seta(), Constantes.esquema_cores_lista_vazia(), Constantes.esquema_cores_lista_inclicavel(), Constantes.esquema_cores_lista_selecionada(), Constantes.esquema_cores_lista_deselecionada(), new Vector2(0.85f, 0.5f), 0.1f, 5, 1f, 0.5f, "Idioma", true, true);
        }

        void RedimensionaConfiguracoes()
        {
            listaIdiomas.Atualizar();
            saiConfVoltaInicial.Atualizar();
        }

        void AtualizaConfiguracoes()
        {
            listaIdiomas.Atualizar();
            saiConfVoltaInicial.Redimensionado();
        }

        void DesenhaConfiguracoes()
        {
            listaIdiomas.Desenhar();
            saiConfVoltaInicial.Desenhar();
        }

        void SaiConfVoltaIniciar(Botao remetente) { SaiMenuConf(); VaiMenuInicial(); }

        void VaiMenuConf()
        {
            estado = EstadoGUI.conf;
            listaIdiomas.Ativar();
        }

        void SaiMenuConf()
        {
            listaIdiomas.Desativar();
        }




        public void Redimensionado()
        {
            logoIntro.Redimensionado();
            foreach (Elemento e in elementosMenuInicial) { e.Redimensionado(); }

            RedimensionaConfiguracoes();
        }

        public void Atualizar()
        {
            if (ativo)
            {
                ContaLogoIntro();
                foreach (Elemento e in elementosMenuInicial) { e.Atualizar(); }

                AtualizaConfiguracoes();
            }
        }

        public void Desenhar()
        {
            if (ativo)
            {
                logoIntro.Desenhar();

                foreach (Elemento e in elementosMenuInicial) { e.Desenhar(); }

                DesenhaConfiguracoes();
            }

        }
    }
}

