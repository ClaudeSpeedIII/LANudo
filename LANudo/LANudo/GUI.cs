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
            menuInicial.AdicionaBotao("NEW_GAME", true, Jogar);
            menuInicial.AdicionaBotao("SETTINGS", true, Conf);
            menuInicial.AdicionaBotao("QUIT", true, Sair);
            logoInicial = new Sprite(desenhista, logo, Recursos.RetanguloRelativamenteDeslocado(logo.Bounds, Constantes.escala_logo_inicial(), Constantes.pos_logo_inicial()), false);
            elementosMenuInicial.Add(logoInicial);
            elementosMenuInicial.Add(menuInicial);
        }
        void Sair(Botao remetente) { sair(); }

        void Jogar(Botao remetente) { }

        void Conf(Botao remetente) { SaiMenuInicial(); VaiMenuConf(); }

        void VaiMenuInicial() { estado = EstadoGUI.inicial; foreach (Elemento e in elementosMenuInicial) { e.Ativar(); } }
        void SaiMenuInicial() { foreach (Elemento e in elementosMenuInicial) { e.Desativar(); } }

        //Configuracoes
        Lista listaIdiomas;
        Lista listaResolucoes;
        Botao saiConfVoltaInicial;
        List<Elemento> elementosConfiguracoes = new List<Elemento>();

        void InstanciaMenuConfiguracoes()
        {
            saiConfVoltaInicial = new Botao(desenhista, botao, Constantes.esquema_cores_botao(), Constantes.pos_botao_voltar(), Constantes.escala_menu_inicial(), fonte, "BACK", true, Constantes.escala_texto_menu_inicial(), false, false);
            saiConfVoltaInicial.Clicado += SaiConfVoltaIniciar;
            saiConfVoltaInicial.AtivarSobreMouse();

            List<ElementoLista> vetor;

            vetor = ElementoLista.CriaVariosElementoLista(Localizacao.Idiomas, Constantes.esquema_cores_lista_deselecionada(), Constantes.esquema_cores_lista_selecionada());
            listaIdiomas = new Lista(desenhista, fonte, vetor,
                Lista.TipoEvento.SelecionavelInternamente,
                botao, null, botao, seta,
                Constantes.esquema_cores_lista_seta(),
                Constantes.esquema_cores_lista_vazia(),
                Constantes.esquema_cores_lista_inclicavel(),
                Constantes.esquema_cores_lista_selecionada(),
                Constantes.esquema_cores_lista_deselecionada(),
                Constantes.pos_lista_idiomas(),
                Constantes.escala_elementos_conf(), 5,
                Constantes.escala_texto_elementos_conf(),
                Constantes.escala_setinha_conf(), true,
                "LOCALE", true,
                Constantes.escala_rotulo_conf(),
                Constantes.distancia_rotulo_conf(),
                Motor.Textos.ISO
                , false, true, true);

            vetor = ElementoLista.CriaVariosElementoLista(conf.Resolucoes, Constantes.esquema_cores_lista_deselecionada(), Constantes.esquema_cores_lista_selecionada());
            listaResolucoes = new Lista(desenhista, fonte, vetor,
                Lista.TipoEvento.SelecionavelInternamente,
                botao, null, botao, seta,
                Constantes.esquema_cores_lista_seta(),
                Constantes.esquema_cores_lista_vazia(),
                Constantes.esquema_cores_lista_inclicavel(),
                Constantes.esquema_cores_lista_selecionada(),
                Constantes.esquema_cores_lista_deselecionada(),
                Constantes.pos_lista_res(),
                Constantes.escala_elementos_conf(), 5,
                Constantes.escala_texto_elementos_conf(),
                Constantes.escala_setinha_conf(), true,
                "RES", true,
                Constantes.escala_rotulo_conf(),
                Constantes.distancia_rotulo_conf(),
                Configuracoes.Resolucao,
                false, true, true);

            listaIdiomas.BotaoRotulo.Clicado += AbriuListaIdiomas;
            listaIdiomas.clicouDropDown += SetouListaIdioma;
            listaResolucoes.BotaoRotulo.Clicado += AbriuListaResolucoes;
            listaResolucoes.clicouDropDown += SetouListaResolucoes;

            // A ordem diz quem desenha na frente
            elementosConfiguracoes.Add(listaResolucoes);
            elementosConfiguracoes.Add(listaIdiomas);
            elementosConfiguracoes.Add(saiConfVoltaInicial);
        }

        void AbriuListaIdiomas(Botao origem) { listaResolucoes.Clicavel = false; }
        void SetouListaIdioma(ElementoLista elemento) { conf.SetaIdioma((string)elemento.Payload); listaResolucoes.Clicavel = true; }
        void AbriuListaResolucoes(Botao origem) { listaIdiomas.Clicavel = false; }
        void SetouListaResolucoes(ElementoLista elemento) { Vector2 res = (Vector2)elemento.Payload; conf.SetaRes(res); Redimensionado(); listaIdiomas.Clicavel = true; }
        void SaiConfVoltaIniciar(Botao remetente) { SaiMenuConf(); VaiMenuInicial(); }

        void VaiMenuConf() { estado = EstadoGUI.conf; foreach (Elemento e in elementosConfiguracoes) { e.Ativar(); } }
        void SaiMenuConf() { foreach (Elemento e in elementosConfiguracoes) { e.Desativar(); } }




        public void Redimensionado()
        {
            logoIntro.Redimensionado();
            foreach (Elemento e in elementosMenuInicial) { e.Redimensionado(); }
            foreach (Elemento e in elementosConfiguracoes)
            {
                e.Redimensionado(); listaResolucoes.Itens =
ElementoLista.CriaVariosElementoLista(conf.Resolucoes, Constantes.esquema_cores_lista_deselecionada(), Constantes.esquema_cores_lista_selecionada());
                listaResolucoes.PayloadItemSelecionado = Configuracoes.Resolucao;
            }

        }

        public void Atualizar()
        {
            if (ativo)
            {
                ContaLogoIntro();
                foreach (Elemento e in elementosMenuInicial) { e.Atualizar(); }
                foreach (Elemento e in elementosConfiguracoes) { e.Atualizar(); }

            }
        }

        public void Desenhar()
        {
            if (ativo)
            {
                logoIntro.Desenhar();

                foreach (Elemento e in elementosMenuInicial) { e.Desenhar(); }
                foreach (Elemento e in elementosConfiguracoes) { e.Desenhar(); }

            }

        }
    }
}

