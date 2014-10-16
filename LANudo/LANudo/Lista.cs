using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LANudo
{
    public class Lista
    {
        HashSet<Botao> botoesTodos = new HashSet<Botao>();
        HashSet<Botao> botoesDinamicos = new HashSet<Botao>();
        Botao botaoSuperior;
        Botao botaoInferior;
        HashSet<ElementoLista> itens = new HashSet<ElementoLista>();

        ElementoLista itemAtual = null; public ElementoLista ItemSelecionado { get { return itemAtual; } }
        Botao botaoAtual = null;
        int rolagem = 0;

        SpriteBatch desenhista;
        SpriteFont fonte;
        Texture2D fundo;
        Texture2D fundoMouse;
        Texture2D seta;
        Texture2D fundoSeta;
        int tamanho;
        float escala;
        Vector2 posicao;
        bool vertical;
        bool destino;
        bool dropDown;
        float escalaTexto;
        EsquemaCores coresSeta;
        EsquemaCores coresVazio;
        EsquemaCores coresInclicavel;
        EsquemaCores coresSelecionado;
        EsquemaCores coresDeselecionado;

        bool ativo;

        public bool Ativado() { return ativo; }

        public void Ativar() { ativo = true; }

        public void Desativar() { ativo = true; }

        public Lista(SpriteBatch _desenhista, SpriteFont _fonte, HashSet<ElementoLista> _elementos, Texture2D _fundo, Texture2D _fundoMouseOver, Texture2D _fundoSeta, Texture2D _seta, EsquemaCores _coresSeta, EsquemaCores _coresVazio, EsquemaCores _coresInclicavel, EsquemaCores _coresSelecionado, EsquemaCores _coresDeselecionado, Vector2 _posicao, float _escala, int _tamanho, float _escalaTexto, bool _destino = true, bool _dropDown = true, bool _vertical = true)
        {
            desenhista = _desenhista;
            fonte = _fonte;
            itens = _elementos;

            fundo = _fundo;
            fundoMouse = _fundoMouseOver;
            fundoSeta = _fundoSeta;
            seta = _seta;

            dropDown = _dropDown;
            vertical = _vertical;
            destino = _destino;

            coresDeselecionado = _coresDeselecionado;
            coresSelecionado = _coresSelecionado;
            coresVazio = _coresVazio;
            coresSeta = _coresSeta;
            coresInclicavel = _coresInclicavel;
            tamanho = _tamanho;
            posicao = _posicao;
            escala = _escala;
            escalaTexto = _escalaTexto;

            InicializaBotoes();
            InicializaItens();

            ativo = false;
        }

        private void InicializaBotoes()
        {
            botaoSuperior = new Botao(desenhista, fundo, fundoMouse, coresSeta, new Vector2(10, 10), escala, false);
            botaoInferior = new Botao(desenhista, fundo, fundoMouse, coresSeta, new Vector2(10, 10), escala, false);
            botaoSuperior.Clicado += ClicouSobe;
            botaoInferior.Clicado += ClicouDesce;

            botoesTodos.Add(botaoSuperior);

            for (int i = 1; i < tamanho; i++)
            {
                Botao botao = new Botao(desenhista, fundo, fundoMouse, coresVazio, new Vector2(10, 10), escala, fonte, " ", escalaTexto, false);
                botoesTodos.Add(botao);
            }

            botoesTodos.Add(botaoInferior);
        }

        public void InicializaItens()
        {
            Rolagem(rolagem);
            if (itens.Count > botoesDinamicos.Count) { ponto = Ponto.Travado; } else { ponto = Ponto.Inicio; }
        }

        enum Ponto { Inicio, Meio, Fim, Travado };
        Ponto ponto;
        Ponto Rolagem(int scroll)
        {
            if (ponto != Ponto.Travado)
            {
                int contador = -scroll;
                ElementoLista item;
                foreach (Botao botao in botoesDinamicos)
                {
                    try { item = itens.ElementAt(contador + 1); }
                    catch (Exception erro)
                    { EsvaziaBotao(botao); return Ponto.Fim; }

                    try { item = itens.ElementAt(contador); }
                    catch (Exception erro)
                    { EsvaziaBotao(botao); return Ponto.Travado; }
                    SetaBotao(botao, item);
                    contador++;
                }
                if (scroll == 0) { return Ponto.Inicio; } else { return Ponto.Meio; };
            }
            else { return Ponto.Travado; }
        }

        private void EsvaziaBotao(Botao botao)
        {
            botao.Cores = coresVazio;
            botao.DesativarSobreMouse();
            botao.ZeraEventos();
        }

        private void SetaBotao(Botao botao, ElementoLista item)
        {
            botao.AtivarSobreMouse();
            botao.Cores = item.CoresDesel;
            botao.Rotulo = item.Rotulo;
            if (destino)
            {
                botao.Clicado += item.Clicado;
            }
            else
            {
                botao.Clicado += Clicou;
            }
        }

        private void Clicou(Botao botao)
        {
            foreach (ElementoLista item in itens)
            {
                if (item.Rotulo == botao.Rotulo)
                {
                    DeselecionaBotao(botaoAtual);
                    SelecionaBotao(botao);
                    itemAtual = item;
                    botaoAtual = botao;
                }
            }
        }

        private void SelecionaBotao(Botao botao)
        {
            if (botao != null) { botao.Cores = coresSelecionado; }
        }
        private void DeselecionaBotao(Botao botao)
        {
            if (botao != null) { botao.Cores = coresDeselecionado; }
        }


        private void ClicouSobe(Botao remetente)
        {
            ScrollaPraCima(1);
        }

        private void ClicouDesce(Botao remetente)
        {
            ScrollaPraBaixo(1);
        }

        private void ScrollaPraCima(int quanto)
        {
            if (ponto != Ponto.Travado && ponto != Ponto.Inicio)
            {
                Rolagem(rolagem += quanto);
            }
        }


        private void ScrollaPraBaixo(int quanto)
        {
            if (ponto != Ponto.Travado && ponto != Ponto.Fim)
            {
                Rolagem(rolagem -= quanto);
            }
        }

        private void Trava()
        {
            ponto = Ponto.Travado;
            botaoInferior.Cores = coresInclicavel; botaoInferior.DesativarSobreMouse();
            botaoSuperior.Cores = coresInclicavel; botaoSuperior.DesativarSobreMouse();
        }
        private void ColidiuTopo() { ponto = Ponto.Inicio; botaoSuperior.Cores = coresInclicavel; botaoSuperior.DesativarSobreMouse(); }
        private void LivreTopo() { ponto = Ponto.Meio; botaoSuperior.Cores = coresSeta; botaoSuperior.AtivarSobreMouse(); }
        private void ColidiuChao() { ponto = Ponto.Fim; botaoInferior.Cores = coresInclicavel; botaoInferior.DesativarSobreMouse(); }
        private void LivreChao() { ponto = Ponto.Meio; botaoInferior.Cores = coresSeta; botaoInferior.AtivarSobreMouse(); }

        public void Redimensionado()
        {
            float tamanho = 0f, posX = posicao.X, posY = posicao.Y;
            bool first = true;
            foreach (Botao botao in botoesTodos)
            {
                if (botoesTodos.Count > 1)
                {
                    botao.Movimentou();
                    if (vertical)
                    {
                        if (first)
                        {
                            tamanho = Recursos.RegraDeTres(Motor.Altura, 1, (botao.Cantos.Bottom - botao.Cantos.Top));
                            if (dropDown)
                            {
                                posY -= (botoesTodos.Count * tamanho) - (tamanho / 2);
                            }
                            else
                            {
                                posY -= ((botoesTodos.Count * tamanho) / 2) - (tamanho / 2);
                            }
                            first = false;
                        }
                        else
                        {
                            posY += tamanho;
                        }
                    }
                    else
                    {
                        if (first)
                        {
                            tamanho = Recursos.RegraDeTres(Motor.Largura, 1, (botao.Cantos.Right - botao.Cantos.Left));
                            if (dropDown)
                            {
                                posY -= (botoesTodos.Count * tamanho) - (tamanho / 2);
                            }
                            else
                            {
                                posX -= ((botoesTodos.Count * tamanho) / 2) - (tamanho / 2);
                            }
                            first = false;
                        }
                        else
                        {
                            posX += tamanho;
                        }
                    }
                }
                botao.Posicao = new Vector2(posX, posY);
            }

        }


        public void Atualizar()
        {
            if (ativo)
            {
                foreach (Botao botao in botoesTodos)
                {
                    botao.Atualizar();
                }
            }
        }

        public void Desenhar()
        {
            if (ativo)
            {
                foreach (Botao botao in botoesTodos)
                {
                    botao.Desenhar();
                }
            }
        }

    }
}
