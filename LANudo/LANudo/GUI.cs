using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Idioma;

namespace LANudo
{
    public class GUI : Elemento
    {
        SpriteBatch desenhista;
        SpriteFont fonte;

        Cursor rato;
        Action sair;
        Configuracoes conf;


        bool ativo, interativo = true;

        public bool EstaInterativo() { return interativo; }
        public void AtivaInterativo() { interativo = true; }
        public void DesativaInterativo() { interativo = false; }

        public bool Ativado() { return ativo; }
        public void Ativar() { ativo = true; }
        public void Desativar() { ativo = false; }


        static Color corFundo; public static Color CorFundo { get { return corFundo; } }
        static Fundo background; public static Fundo ImgFundo { get { return background; } }

        void SetaFundo()
        {
            switch (estado)
            {
                case GUI.EstadoGUI.intro:
                    corFundo = Constantes.cor_de_fundo_Intro();
                    background = null;
                    break;
                case GUI.EstadoGUI.inicial:
                    corFundo = Constantes.cor_de_fundo_MenuInicial();
                    background = fundoInicial;
                    break;
                case GUI.EstadoGUI.conf:
                    corFundo = Constantes.cor_de_fundo_MenuConf();
                    background = null;
                    break;
                case GUI.EstadoGUI.iniciar:
                    corFundo = Constantes.cor_de_fundo_MenuNovoJogo();
                    background = null;
                    break;
                case GUI.EstadoGUI.pausado:
                    background = null;
                    break;
                case GUI.EstadoGUI.emJogo:
                    background = null;
                    break;
            }
        }

        public GUI(SpriteBatch _desenhista, SpriteFont _fonte, Texture2D _logo, Texture2D _intro, Texture2D _botao, Texture2D _seta, Texture2D _fundoInicial, Cursor _rato, Action _sair, Configuracoes _conf, bool _ativo = true)
        {
            desenhista = _desenhista;
            fonte = _fonte;
            imgLogo = _logo;
            imgIntro = _intro;
            imgBotao = _botao;
            imgSeta = _seta;
            rato = _rato;
            sair = _sair;
            conf = _conf;

            imgFundoInicial = _fundoInicial;

            InstanciaLogoIntro();
            InstanciaMenuInicial();
            InstanciaMenuConfiguracoes();
            InstanciaMenuNovoJogo();

            ativo = _ativo;
            estado = EstadoGUI.intro;
        }

        public enum EstadoGUI { intro, inicial, conf, iniciar, pausado, emJogo };
        static EstadoGUI estado;
        public static EstadoGUI Estado { get { return estado; } }

        //Texturas

        Texture2D imgFundoInicial;

        Texture2D imgIntro;
        Texture2D imgLogo;
        Texture2D imgBotao;
        Texture2D imgSeta;

        //Intro
        Sprite logoIntro;

        void InstanciaLogoIntro()
        {
            logoIntro = new Sprite(desenhista, imgIntro, Recursos.RetanguloCentralizado(imgIntro.Bounds, Constantes.escala_logo_intro()));
            estado = EstadoGUI.intro;
            SetaFundo();
        }

        void ContaLogoIntro()
        {
            if (estado == EstadoGUI.intro)
            {
                if (Motor.Tempo.TotalGameTime.TotalMilliseconds > Constantes.duracao_intro().TotalMilliseconds) { logoIntro.Desativar(); VaiMenuInicial(); rato.Ativar(); }
                redimensionaTodos.Add(logoIntro);
            }
        }

        //Menu inicial
        Fundo fundoInicial;
        Botoes menuInicial;
        Sprite logoInicial;
        List<Elemento> elementosMenuInicial = new List<Elemento>();

        void InstanciaMenuInicial()
        {
            fundoInicial = new Fundo(desenhista, imgFundoInicial);
            menuInicial = new Botoes(desenhista, fonte, imgBotao, Constantes.esquema_cores_botao(), Constantes.pos_menu_inicial(), Constantes.escala_menu_inicial(), Constantes.escala_texto_menu_inicial(), Constantes.distancia_botoes_menu_inicial(), true, false);
            menuInicial.AdicionaBotao("NEW_GAME", true, Jogar);
            menuInicial.AdicionaBotao("SETTINGS", true, Conf);
            menuInicial.AdicionaBotao("QUIT", true, Sair);
            logoInicial = new Sprite(desenhista, imgLogo, Recursos.RetanguloRelativamenteDeslocado(imgLogo.Bounds, Constantes.escala_logo_inicial(), Constantes.pos_logo_inicial()), false);
            elementosMenuInicial.Add(logoInicial);
            elementosMenuInicial.Add(menuInicial);
            redimensionaTodos.Add(fundoInicial);
            redimensionaTodos.AddRange(elementosMenuInicial);

        }
        void Sair(Botao remetente) { sair(); }

        void Jogar(Botao remetente) { SaiMenuInicial(); VaiMenuNovoJogo(); }

        void Conf(Botao remetente) { SaiMenuInicial(); VaiMenuConf(); }

        void VaiMenuInicial() { estado = EstadoGUI.inicial; foreach (Elemento e in elementosMenuInicial) { e.Ativar(); } SetaFundo(); }
        void SaiMenuInicial() { foreach (Elemento e in elementosMenuInicial) { e.Desativar(); } }

        //Configuracoes
        Lista listaIdiomas;
        Lista listaResolucoes;
        Botao saiConfVoltaInicial;
        List<Elemento> elementosConfiguracoes = new List<Elemento>();

        void InstanciaMenuConfiguracoes()
        {
            saiConfVoltaInicial = new Botao(desenhista, imgBotao, Constantes.esquema_cores_botao(), Constantes.pos_conf_botao_voltar(), Constantes.escala_menu_inicial(), fonte, "BACK", true, Constantes.escala_texto_menu_inicial(), false, false);
            saiConfVoltaInicial.Clicado += SaiConfVoltaInicial;
            saiConfVoltaInicial.AtivarSobreMouse();

            List<ElementoLista> vetor;

            vetor = ElementoLista.CriaVariosElementoLista(Localizacao.Idiomas, Constantes.esquema_cores_lista_deselecionada(), Constantes.esquema_cores_lista_selecionada());
            listaIdiomas = new Lista(desenhista, fonte, vetor,
                Lista.TipoEvento.SelecionavelInternamente,
                imgBotao, null, imgBotao, imgSeta,
                Constantes.esquema_cores_lista_seta(),
                Constantes.esquema_cores_lista_vazia(),
                Constantes.esquema_cores_lista_inclicavel(),
                Constantes.esquema_cores_lista_selecionada(),
                Constantes.esquema_cores_lista_deselecionada(),
                Constantes.esquema_cores_lista_rotulo(),
                Constantes.pos_lista_idiomas(),
                Constantes.escala_elementos_conf(), 5,
                Constantes.escala_texto_elementos_conf(),
                Constantes.escala_setinha_conf(), true,
                "LOCALE", true,
                Constantes.escala_rotulo_conf(),
                Constantes.distancia_rotulo_conf(),
                Motor.Textos.ISO
                , false, true, true);

            object resAtual = ((object)new Vector3(Configuracoes.Resolucao.X, Configuracoes.Resolucao.Y, Configuracoes.Janela ? 1 : 0));

            vetor = ElementoLista.CriaVariosElementoLista(conf.Resolucoes, Constantes.esquema_cores_lista_deselecionada(), Constantes.esquema_cores_lista_selecionada());
            listaResolucoes = new Lista(desenhista, fonte, vetor,
                Lista.TipoEvento.SelecionavelInternamente,
                imgBotao, null, imgBotao, imgSeta,
                Constantes.esquema_cores_lista_seta(),
                Constantes.esquema_cores_lista_vazia(),
                Constantes.esquema_cores_lista_inclicavel(),
                Constantes.esquema_cores_lista_selecionada(),
                Constantes.esquema_cores_lista_deselecionada(),
                Constantes.esquema_cores_lista_rotulo(),
                Constantes.pos_lista_res(),
                Constantes.escala_elementos_conf(), 5,
                Constantes.escala_texto_elementos_conf(),
                Constantes.escala_setinha_conf(), true,
                "RES", true,
                Constantes.escala_rotulo_conf(),
                Constantes.distancia_rotulo_conf(),
                resAtual,
                false, true, true);

            listaIdiomas.BotaoRotulo.Clicado += AbriuListaIdiomas;
            listaIdiomas.NovaSelecaoDropDown += SetouListaIdioma;
            listaIdiomas.SelecionouDropDown += FechouListaIdioma;
            listaResolucoes.BotaoRotulo.Clicado += AbriuListaResolucoes;
            listaResolucoes.NovaSelecaoDropDown += SetouListaResolucoes;
            listaResolucoes.SelecionouDropDown += FechouListaResolucoes;

            // A ordem diz quem desenha na frente
            elementosConfiguracoes.Add(listaResolucoes);
            elementosConfiguracoes.Add(listaIdiomas);
            elementosConfiguracoes.Add(saiConfVoltaInicial);
            redimensionaTodos.AddRange(elementosConfiguracoes);
        }

        void AbriuListaIdiomas(Botao origem) { foreach (Elemento e in elementosConfiguracoes) { if (!e.Equals(listaIdiomas)) { e.DesativaInterativo(); } } }
        void SetouListaIdioma(ElementoLista elemento) { conf.SetaIdioma((string)elemento.Payload); FechouListaIdioma(elemento); }
        void FechouListaIdioma(ElementoLista elemento) { foreach (Elemento e in elementosConfiguracoes) { if (!e.Equals(listaIdiomas)) { e.AtivaInterativo(); } } }
        void AbriuListaResolucoes(Botao origem) { foreach (Elemento e in elementosConfiguracoes) { if (!e.Equals(listaResolucoes)) { e.DesativaInterativo(); } } }
        void SetouListaResolucoes(ElementoLista elemento)
        {
            Vector3 res = (Vector3)elemento.Payload;
            conf.SetaRes((int)res.X, (int)res.Y, ((res.Z == 0) ? false : true));
            FechouListaResolucoes(elemento);
        }
        void FechouListaResolucoes(ElementoLista elemento) { foreach (Elemento e in elementosConfiguracoes) { if (!e.Equals(listaResolucoes)) { e.AtivaInterativo(); } } }

        void SaiConfVoltaInicial(Botao remetente) { SaiMenuConf(); VaiMenuInicial(); }

        void VaiMenuConf() { estado = EstadoGUI.conf; foreach (Elemento e in elementosConfiguracoes) { e.Ativar(); } SetaFundo(); }
        void SaiMenuConf() { foreach (Elemento e in elementosConfiguracoes) { e.Desativar(); } }

        //Novo jogo

        Botao saiIniciarVoltaInicial;
        List<Elemento> elementosNovoJogo = new List<Elemento>();

        void InstanciaMenuNovoJogo()
        {
            saiIniciarVoltaInicial = new Botao(desenhista, imgBotao, Constantes.esquema_cores_botao(), Constantes.pos_iniciar_botao_voltar(), Constantes.escala_menu_inicial(), fonte, "BACK", true, Constantes.escala_texto_menu_inicial(), false, false);
            saiIniciarVoltaInicial.Clicado += SaiIniciarVoltaInicial;
            saiIniciarVoltaInicial.AtivarSobreMouse();
            elementosNovoJogo.Add(saiIniciarVoltaInicial);



            redimensionaTodos.AddRange(elementosNovoJogo);
        }

        void SaiIniciarVoltaInicial(Botao remetente) { SaiMenuNovoJogo(); VaiMenuInicial(); }

        void VaiMenuNovoJogo() { estado = EstadoGUI.iniciar; foreach (Elemento e in elementosNovoJogo) { e.Ativar(); } SetaFundo(); }
        void SaiMenuNovoJogo() { foreach (Elemento e in elementosNovoJogo) { e.Desativar(); } }


        List<Elemento> redimensionaTodos=new List<Elemento>();
        public void Redimensionado()
        {
            foreach (Elemento e in redimensionaTodos) { e.Redimensionado(); }
        }

        public void Atualizar()
        {
            if (ativo && interativo)
            {
                ContaLogoIntro();
                foreach (Elemento e in elementosMenuInicial) { e.Atualizar(); }
                foreach (Elemento e in elementosConfiguracoes) { e.Atualizar(); }
                foreach (Elemento e in elementosNovoJogo) { e.Atualizar(); }

            }
        }

        public void Desenhar()
        {
            if (ativo)
            {
                logoIntro.Desenhar();

                foreach (Elemento e in elementosMenuInicial) { e.Desenhar(); }
                foreach (Elemento e in elementosConfiguracoes) { e.Desenhar(); }
                foreach (Elemento e in elementosNovoJogo) { e.Desenhar(); }

            }

        }

    }
}

