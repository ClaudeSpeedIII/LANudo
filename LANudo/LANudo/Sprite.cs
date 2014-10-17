using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LANudo
{
    public class Sprite
    {

        bool ativo;
        SpriteBatch desenhista;
        Texture2D sprite; public Texture2D Img { get { return sprite; } set { sprite = value; } }

        Rectangle pixel;
        Vector3 relativo;
        public Rectangle PosPx
        {
            get { return pixel; }
            set
            {
                pixel = value;
            }
        }
        public Vector3 PosRel
        {
            set
            {
                pixel = Recursos.RetanguloRelativamenteDeslocado(sprite.Bounds,  value.Z,new Vector2(value.X, value.Y));
                relativo = value;
            }
        }

        Color cor = Color.White; public Color Cor { get { return cor; } set { cor = value; } }
        public bool Ativado() { return ativo; }

        public void Ativar() { ativo = true; }

        public void Desativar() { ativo = false; }

        public Sprite(SpriteBatch desenhista, Texture2D imagem, Rectangle pos, Color cor, bool ativo = true)
        {
            this.desenhista = desenhista;
            this.sprite = imagem;
            this.cor = cor;
            this.pixel = pos;
            this.ativo = ativo;
        }
        public Sprite(SpriteBatch desenhista, Texture2D imagem, Rectangle pos, bool ativo = true)
        {
            this.desenhista = desenhista;
            this.sprite = imagem;
            this.pixel = pos;
            this.ativo = ativo;
        }

        public Sprite(SpriteBatch desenhista, Texture2D imagem, Vector3 pos, Color cor, bool ativo = true)
        {
            this.desenhista = desenhista;
            this.sprite = imagem;
            this.cor = cor;
            this.PosRel = pos;
            this.ativo = ativo;
        }
        public Sprite(SpriteBatch desenhista, Texture2D imagem, Vector3 pos, bool ativo = true)
        {
            this.desenhista = desenhista;
            this.sprite = imagem;
            this.PosRel = pos;
            this.ativo = ativo;
        }

        public void Desenhar()
        {
            if (ativo)
            {
                desenhista.Draw(sprite, pixel, cor);
            }
        }
    }
}
