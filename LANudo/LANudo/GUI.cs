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

        public GUI(SpriteBatch _desenhista,
            SpriteFont _fonte,
            Texture2D _splash,
            Texture2D _menuFundo,
            Texture2D _menuLogo,
            Texture2D _menuDado,
            Texture2D _menuTab,
            Texture2D _botao,
            Texture2D _seta,
            Texture2D tempTabFundo,
        Texture2D tempTabCentro,
        Texture2D tempTabTile,
        Texture2D tempTabSeta,
        Texture2D tempPeao,
            Cursor _rato,
            Action _sair,
            Configuracoes _conf,
            bool _ativo = true)
        {
            this.tempTabFundo = tempTabFundo;
            this.tempTabCentro = tempTabCentro;
            this.tempTabTile = tempTabTile;
            this.tempTabSeta = tempTabSeta;
            this.tempPeao = tempPeao;
            desenhista = _desenhista;
            fonte = _fonte;

            imgSplash = _splash;

            imgFundoInicial = _menuFundo;
            imgLogoInicial = _menuLogo;
            imgDadoInicial = _menuDado;
            imgTabuleiroInicial = _menuTab;

            imgBotao = _botao;
            imgSeta = _seta;


            rato = _rato;
            sair = _sair;
            conf = _conf;


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
        Texture2D imgLogoInicial;
        Texture2D imgDadoInicial;
        Texture2D imgTabuleiroInicial;

        Texture2D imgSplash;
        Texture2D imgBotao;
        Texture2D imgSeta;

        //Intro
        Sprite splash;

        void InstanciaLogoIntro()
        {
            splash = new Sprite(desenhista, imgSplash, Recursos.RetanguloCentralizado(imgSplash.Bounds, Constantes.escala_logo_intro()));
            redimensionaTodos.Add(splash);
            estado = EstadoGUI.intro;
            SetaFundo();
        }

        void ContaLogoIntro()
        {
            if (estado == EstadoGUI.intro)
            {
                if (Motor.Tempo.TotalGameTime.TotalMilliseconds > Constantes.duracao_intro().TotalMilliseconds) { splash.Desativar(); VaiMenuInicial(); rato.Ativar(); }
            }
        }

        //Menu inicial
        Fundo fundoInicial;
        Sprite logoInicial;
        Sprite dadoInicialEsq;
        Sprite dadoInicialDir;
        Sprite tabuleiroInicial;

        Botoes menuInicial;
        List<Elemento> elementosMenuInicial = new List<Elemento>();

        void InstanciaMenuInicial()
        {
            fundoInicial = new Fundo(desenhista, imgFundoInicial);
            redimensionaTodos.Add(fundoInicial);

            logoInicial = new Sprite(desenhista, imgLogoInicial, Constantes.pos_logo_inicial(), Constantes.pivot_logo_inicial(), false);
            dadoInicialEsq = new Sprite(desenhista, imgDadoInicial, Constantes.pos_dado_esq_inicial(), Constantes.pivot_dado_esq_inicial(), false);
            dadoInicialEsq.Eff = SpriteEffects.FlipHorizontally;
            dadoInicialDir = new Sprite(desenhista, imgDadoInicial, Constantes.pos_dado_dir_inicial(), Constantes.pivot_dado_dir_inicial(), false);
            tabuleiroInicial = new Sprite(desenhista, imgTabuleiroInicial, Constantes.pos_tab_inicial(), Constantes.pivot_tab_inicial(), false);


            menuInicial = new Botoes(desenhista, fonte, imgBotao, Constantes.esquema_cores_botao(), Constantes.pos_menu_inicial(), Constantes.escala_menu_inicial(), Constantes.escala_texto_menu_inicial(), Constantes.distancia_botoes_menu_inicial(), true, false);
            menuInicial.AdicionaBotao("NEW_GAME", true, Jogar);
            menuInicial.AdicionaBotao("SETTINGS", true, Conf);
            menuInicial.AdicionaBotao("QUIT", true, Sair);

            elementosMenuInicial.Add(dadoInicialEsq);
            elementosMenuInicial.Add(dadoInicialDir);
            elementosMenuInicial.Add(tabuleiroInicial);
            elementosMenuInicial.Add(logoInicial);
            elementosMenuInicial.Add(menuInicial);

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
                Constantes.distancia_dropdown_conf(),
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
                Constantes.distancia_dropdown_conf(),
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

        Tabuleiro temp;
        Texture2D tempTabFundo;
        Texture2D tempTabCentro;
        Texture2D tempTabTile;
        Texture2D tempTabSeta;
        Texture2D tempPeao;


        Botao saiIniciarVoltaInicial;
        List<Elemento> elementosNovoJogo = new List<Elemento>();

        void InstanciaMenuNovoJogo()
        {
            saiIniciarVoltaInicial = new Botao(desenhista, imgBotao, Constantes.esquema_cores_botao(), Constantes.pos_iniciar_botao_voltar(), Constantes.escala_iniciar_botao_voltar(), fonte, "BACK", true, Constantes.escala_texto_menu_inicial(), false, false);
            saiIniciarVoltaInicial.Clicado += SaiIniciarVoltaInicial;
            saiIniciarVoltaInicial.AtivarSobreMouse();
            elementosNovoJogo.Add(saiIniciarVoltaInicial);

            //inicio só pra testes
            CoresLudo cores = new CoresLudo(
                Constantes.cor_P1(),
                Constantes.cor_P2(),
                Constantes.cor_P3(),
                Constantes.cor_P4(),
                Color.White
                );

            ParametrosCasa centro = new ParametrosCasa(tempTabCentro, Casa.Tipos.Chegada);
            ParametrosCasa final = new ParametrosCasa(tempTabTile, Casa.Tipos.Final);

            List<Texture2D> vetor = new List<Texture2D>();
            vetor.Add(tempTabTile);
            vetor.Add(tempTabSeta);
            ParametrosCasa entrada = new ParametrosCasa(vetor, Casa.Tipos.Entrada, -100);

            ParametrosCasa pista = new ParametrosCasa(tempTabTile, Casa.Tipos.Pista);
            ParametrosCasa saida = new ParametrosCasa(tempTabTile, Casa.Tipos.Saida);
            ParametrosCasa garagem = new ParametrosCasa(tempTabTile, Casa.Tipos.Garagem);


            temp = new Tabuleiro(desenhista, tempPeao, tempTabFundo, cores, garagem, saida, pista, entrada, final, centro, new Vector3(0.5f, 0.5f, 0.8f),0);


            Peao[] peoes = new Peao[temp.QuantidadeLinear];
            for (int i = 0; i < temp.QuantidadeLinear; i++)
            {
                peoes[i] = new Peao(Casa.Jogadores.Publico, 2);
            }
            peoes[6] = new Peao(Casa.Jogadores.P2, 1);

            temp.PreenchePeao(peoes);


            elementosNovoJogo.Add(temp);
            //fim só pra testes

            redimensionaTodos.AddRange(elementosNovoJogo);
        }

        void SaiIniciarVoltaInicial(Botao remetente) { SaiMenuNovoJogo(); VaiMenuInicial(); }

        void VaiMenuNovoJogo() { estado = EstadoGUI.iniciar; foreach (Elemento e in elementosNovoJogo) { e.Ativar(); } SetaFundo(); }
        void SaiMenuNovoJogo() { foreach (Elemento e in elementosNovoJogo) { e.Desativar(); } }


        List<Elemento> redimensionaTodos = new List<Elemento>();
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
                splash.Desenhar();
                foreach (Elemento e in elementosMenuInicial) { e.Desenhar(); }
                foreach (Elemento e in elementosConfiguracoes) { e.Desenhar(); }
                foreach (Elemento e in elementosNovoJogo) { e.Desenhar(); }

            }

        }

    }
}

