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
        Texture2D logo;
        Texture2D intro;
        Texture2D botao;
        Texture2D seta;
        Rectangle posLogo;
        Rectangle posIntro;
        Lista listaAlgo;
        Botoes novoJogo;
        Botoes menuInicial;
        Action sair;
        Cursor rato;

        public enum EstadoGUI { intro, inicial, conf, iniciar, pausado, emJogo };
        static EstadoGUI estado;
        public static EstadoGUI Estado { get { return estado; } }


        public GUI(SpriteBatch _desenhista, SpriteFont _fonte, Texture2D _logo, Texture2D _intro, Texture2D _botao, Texture2D _seta, Cursor _rato, Action _sair)
        {
            desenhista = _desenhista;
            fonte = _fonte;
            logo = _logo;
            intro = _intro;
            botao = _botao;
            seta = _seta;
            rato = _rato;
            sair = _sair;

            //Instancia menu inicial
            menuInicial = new Botoes(desenhista, fonte, botao, Constantes.esquema_cores_botao(), Constantes.pos_menu_inicial(), Constantes.escala_menu_inicial(), Constantes.escala_texto_menu_inicial(), Constantes.distancia_botoes_menu_inicial(), true);
            menuInicial.AdicionaBotao(Motor.Textos.Val("Novo_Jogo"), Jogar);
            menuInicial.AdicionaBotao(Motor.Textos.Val("Config"), Conf);
            menuInicial.AdicionaBotao(Motor.Textos.Val("Sair"), Sair);

            HashSet<string> vetor = new HashSet<string>();
            vetor.Add("Espaço");
            vetor.Add("Nuvens");
            vetor.Add("Superficie");
            vetor.Add("Metrô");
            vetor.Add("Lava");
            vetor.Add("Bedrock");
            vetor.Add("Void");
            HashSet<ElementoLista> result = ElementoLista.CriaVariosElementoLista(vetor, Constantes.esquema_cores_lista_deselecionada(), Constantes.esquema_cores_lista_selecionada());
            listaAlgo = new Lista(desenhista, fonte, result, Lista.TipoEvento.SelecionavelIntermanete, botao, null, botao, seta, Constantes.esquema_cores_lista_seta(), Constantes.esquema_cores_lista_vazia(), Constantes.esquema_cores_lista_inclicavel(), Constantes.esquema_cores_lista_selecionada(), Constantes.esquema_cores_lista_deselecionada(), new Vector2(0.85f, 0.5f), 0.1f, 5, 1f,0.5f,false, true,true);
            listaAlgo.Ativar();
            Redimensionado();
            estado = EstadoGUI.intro;
            ativo = false;
        }

        void Sair(Botao remetente)
        {
            sair();
        }

        void Jogar(Botao remetente) { }


        void Conf(Botao remetente) { }

        bool ativo;

        public bool Ativado() { return ativo; }

        public void Ativar() { ativo = true; }

        public void Desativar() { ativo = false; }

        public void Redimensionado()
        {
            posLogo = Recursos.RetanguloRelativamenteDeslocado(logo.Bounds, Constantes.escala_logo_inicial(), Constantes.pos_logo_inicial());
            posIntro = Recursos.RetanguloCentralizado(intro.Bounds, Constantes.escala_logo_intro());
            menuInicial.Redimensionado();
            listaAlgo.Redimensionado();
        }

        public void Atualizar()
        {
            if (ativo)
            {
                if (estado == EstadoGUI.intro)
                {
                    if (Motor.Tempo.TotalGameTime.TotalMilliseconds > Constantes.duracao_intro().TotalMilliseconds) { estado = EstadoGUI.inicial; menuInicial.Ativar(); rato.Ativar(); }
                }
                menuInicial.Atualizar();
                listaAlgo.Atualizar();
            }
        }

        public void Desenhar()
        {
            if (ativo)
            {
                if (estado == EstadoGUI.inicial || estado == EstadoGUI.conf || estado == EstadoGUI.iniciar)
                {
                    desenhista.Draw(logo, posLogo, Color.White); //Joga o logo na tela
                }
                else if (estado == EstadoGUI.intro)
                {
                    desenhista.Draw(intro, posIntro, Color.White); //tela inicial
                }
                menuInicial.Desenhar();
                listaAlgo.Desenhar();

            }

        }
    }
}

