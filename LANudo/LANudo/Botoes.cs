using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LANudo
{
    public class Botoes : Elemento
    {
        List<Botao> botoes = new List<Botao>();


        SpriteBatch desenhista;
        SpriteFont fonte;
        Texture2D imagem;
        Texture2D imagemMouseOver;
        EsquemaCores cores;
        float escala;
        Vector2 posicao;
        bool vertical;
        float distancia;
        float escalaTexto;


        bool ativo;

        public bool Ativado() { return ativo; }

        public void Ativar() { ativo = true; }

        public void Desativar() { ativo = false; }


        public Botoes(SpriteBatch _desenhista, SpriteFont _fonte, Texture2D _fundo, EsquemaCores _cores, Vector2 _posicao, float _escala, float _escalaTexto, float _distancia, bool _vertical, bool _ativo = true)
        {
            desenhista = _desenhista;
            fonte = _fonte;
            imagem = _fundo;
            imagemMouseOver = null;
            cores = _cores;
            posicao = _posicao;
            escala = _escala;
            vertical = _vertical;
            distancia = _distancia;
            escalaTexto = _escalaTexto;

            ativo = _ativo;
        }

        public Botoes(SpriteBatch _desenhista, SpriteFont _fonte, Texture2D _fundo, Texture2D _fundoMouseOver, EsquemaCores _cores, Vector2 _posicao, float _escala, float _escalaTexto, float _distancia, bool _vertical, bool _ativo = true)
        {
            desenhista = _desenhista;
            fonte = _fonte;
            imagem = _fundo;
            imagemMouseOver = _fundoMouseOver;
            cores = _cores;
            posicao = _posicao;
            escala = _escala;
            vertical = _vertical;
            distancia = _distancia;
            escalaTexto = _escalaTexto;

            ativo = _ativo;
        }

        public void AdicionaBotao(string rotulo, bool xml, ManipuladorBotao acao)
        {
            Vector2 lugar = posicao;
            Botao temp;
            if (imagemMouseOver != null)
            {
                temp = new Botao(desenhista, imagem, imagemMouseOver, cores, lugar, escala, fonte, rotulo,xml, escalaTexto,false);
            }
            else
            {
                temp = new Botao(desenhista, imagem, cores, lugar, escala, fonte, rotulo, xml, escalaTexto, false);
            }
            temp.AtivarSobreMouse();
            temp.Clicado += acao;
            botoes.Add(temp);
        }

        public void Redimensionado()
        {
            float tamanho = 0f, posX = posicao.X, posY = posicao.Y;
            bool first = true;
            foreach (Botao botao in botoes)
            {
                if (botoes.Count > 1)
                {
                    botao.Redimensionado();
                    if (vertical)
                    {
                        if (first)
                        {
                            tamanho = Recursos.RegraDeTres(Configuracoes.Altura, 1, (botao.Cantos.Bottom - botao.Cantos.Top));
                            posY -= (((botoes.Count * (tamanho + distancia)) - distancia) / 2) - (tamanho / 2);
                            first = false;
                        }
                        else
                        { posY += (tamanho + distancia); }
                    }
                    else
                    {
                        if (first)
                        {
                            tamanho = Recursos.RegraDeTres(Configuracoes.Largura, 1, (botao.Cantos.Right - botao.Cantos.Left));
                            posX -= (((botoes.Count * (tamanho + distancia)) - distancia) / 2) - (tamanho / 2);
                            first = false;
                        }
                        else
                        { posX += (tamanho + distancia); }
                    }
                } botao.Posicao = new Vector2(posX, posY);
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
