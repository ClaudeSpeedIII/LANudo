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
        HashSet<Botao> botoes = new HashSet<Botao>();
        HashSet<ElementoLista> itens = new HashSet<ElementoLista>();


        SpriteBatch desenhista;
        SpriteFont fonte;
        Texture2D fundo;
        Texture2D fundoMouse;
        Texture2D fundoSeta;
        Texture2D fundoSetaMouse;
        Texture2D seta;
        int tamanho;
        float escala;
        Vector2 posicao;
        bool vertical;
        bool dropDown;
        float escalaTexto;
        EsquemaCores coresSeta;
        EsquemaCores coresVazio;
        EsquemaCores coresSelecionado;
        EsquemaCores coresDeselecionado;

        bool ativo;

        public bool Ativado() { return ativo; }

        public void Ativar() { ativo = true; }

        public void Desativar() { ativo = true; }

        public Lista(SpriteBatch _desenhista, SpriteFont _fonte, HashSet<ElementoLista> _elementos, Texture2D _fundo, Texture2D _fundoMouseOver, Texture2D _fundoSeta, EsquemaCores _coresSeta, EsquemaCores _coresVazio, EsquemaCores _coresSelecionado, EsquemaCores _coresDeselecionado, Vector2 _posicao, float _escala, float _escalaTexto, bool _dropDown = true, bool _vertical = true)
        {
            desenhista = _desenhista;
            itens = _elementos;
            fonte = _fonte;
            fundo = _fundo;
            fundoMouse = _fundoMouseOver;
            fundoSeta = _fundoSeta;

            coresDeselecionado = _coresDeselecionado;
            coresSelecionado = _coresSelecionado;
            coresVazio = _coresVazio;

            posicao = _posicao;
            escala = _escala;
            vertical = _vertical;
            escalaTexto = _escalaTexto;

            ativo = false;
        }

        public Lista(SpriteBatch _desenhista, SpriteFont _fonte, HashSet<ElementoLista> _elementos, Texture2D _fundo, Texture2D _fundoSeta, EsquemaCores _coresSeta, EsquemaCores _coresVazio, EsquemaCores _coresSelecionado, EsquemaCores _coresDeselecionado, Vector2 _posicao, float _escala, float _escalaTexto, bool _dropDown = true, bool _vertical = true)
        {
            desenhista = _desenhista;
            itens = _elementos;
            fonte = _fonte;
            fundo = _fundo;
            fundoMouse = null;
            fundoSeta = _fundoSeta;

            coresDeselecionado = _coresDeselecionado;
            coresSelecionado = _coresSelecionado;
            coresVazio = _coresVazio;

            posicao = _posicao;
            escala = _escala;
            vertical = _vertical;
            escalaTexto = _escalaTexto;

            ativo = false;
        }

        public void Redimensionado()
        {
            float tamanho = 0f, posX = posicao.X, posY = posicao.Y;
            bool first = true;
            foreach (Botao botao in botoes)
            {
                if (botoes.Count > 1)
                {
                    botao.Movimentou();
                    if (vertical)
                    {
                        if (first)
                        {
                            tamanho = Recursos.RegraDeTres(Motor.Altura, 1, (botao.Cantos.Bottom - botao.Cantos.Top));
                            posY -= ((botoes.Count * tamanho) / 2) - (tamanho / 2);
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
                            posX -= ((botoes.Count * tamanho) / 2) - (tamanho / 2);
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
                foreach (Botao botao in botoes)
                {
                    botao.Atualizar();
                }
            }
        }

        public void Desenhar()
        {
            if (ativo)
            {
                foreach (Botao botao in botoes)
                {
                    botao.Desenhar();
                }
            }
        }

    }
}
