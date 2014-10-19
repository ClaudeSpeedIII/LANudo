using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LANudo
{
    public class Cursor : Elemento
    {
        SpriteBatch desenhista;
        Texture2D ratoNormal;
        Texture2D ratoPressionado;
        Texture2D ratoAtual;
        Vector2 posAtual;
        Vector2 offSet;
        Color corAtual;

        public Color Cor
        {
            get { return corAtual; }
            set { corAtual = value; }
        }

        bool ativo;

        public bool Ativado() { return ativo; }

        public void Ativar() { ativo = true; }

        public void Desativar() { ativo = true; }

        public Cursor(SpriteBatch _desenhista, Texture2D _ratoNormal, Texture2D _ratoPressionado, Vector2 _offSet, bool _ativo = false)
        {
            desenhista = _desenhista;
            ratoNormal = _ratoNormal;
            ratoPressionado = _ratoPressionado;
            offSet = _offSet;
            corAtual = Color.White;
            ativo = _ativo;
        }

        public Cursor(SpriteBatch _desenhista, Texture2D _ratoNormal, Texture2D _ratoPressionado, bool _ativo = false)
        {
            desenhista = _desenhista;
            ratoNormal = _ratoNormal;
            ratoPressionado = _ratoPressionado;
            offSet = Vector2.Zero;
            corAtual = Color.White;
            ativo = _ativo;
        }

        public void Atualizar()
        {
            if (ativo)
            {
                MouseState rato = Mouse.GetState();
                if (rato.LeftButton == ButtonState.Pressed)
                {
                    ratoAtual = ratoPressionado;
                }
                else
                {
                    ratoAtual = ratoNormal;
                }
                posAtual = new Vector2(rato.X, rato.Y) + offSet;
            }
        }

        public void Redimensionado() { }

        public void Desenhar()
        {
            if (ativo)
            {
                desenhista.Draw(ratoAtual, posAtual, corAtual);
            }
        }
    }

}

