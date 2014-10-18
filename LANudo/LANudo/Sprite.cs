using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LANudo
{
    public class Sprite : Elemento
    {

        bool ativo;
        SpriteBatch desenhista;
        Texture2D sprite; public Texture2D Img { get { return sprite; } set { sprite = value; } }
        float angulo = 0; public float Rot { get { return MathHelper.ToDegrees(angulo); } set { angulo = MathHelper.ToRadians(value); } }
        SpriteEffects efeitos = SpriteEffects.None; public SpriteEffects Eff { get { return efeitos; } set { efeitos = value; } }

        Rectangle retangulo;
        Vector3 pixel;
        Vector3 relativo;
        public Rectangle PosRect
        {
            get { return new Rectangle(Convert.ToInt32(retangulo.X - (sprite.Width / 2)), Convert.ToInt32(retangulo.Y - (sprite.Height / 2)), sprite.Width, sprite.Height); }
            set
            {
                retangulo = value;
                relativo = Recursos.AbsParaRelTela(new Vector3(retangulo.Center.X, retangulo.Center.Y, Recursos.RegraDeTres(Configuracoes.Altura, 1, retangulo.Height)));
                pixel = new Vector3((float)retangulo.X, (float)retangulo.Y, relativo.Z);
            }
        }
        public Vector3 PosPx
        {
            get { return pixel; }
            set
            {
                pixel = value;
                retangulo = new Rectangle(Convert.ToInt32(value.X + ((sprite.Width * value.Z) / 2)), Convert.ToInt32(value.Y + ((sprite.Height * value.Z) / 2)), Convert.ToInt32(sprite.Width * value.Z), Convert.ToInt32(sprite.Height * value.Z));
                relativo = Recursos.AbsParaRelTela(new Vector3(retangulo.Center.X, retangulo.Center.Y, value.Z));
            }
        }
        public Vector3 PosRel
        {
            get { return relativo; }
            set
            {
                relativo = value;
                retangulo = Recursos.RetanguloRelativamenteDeslocado(sprite.Bounds, value.Z, new Vector2(relativo.X, relativo.Y));
                pixel = new Vector3(retangulo.X, retangulo.Y, value.Z);
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
            this.PosRect = pos;
            this.ativo = ativo;
        }
        public Sprite(SpriteBatch desenhista, Texture2D imagem, Rectangle pos, bool ativo = true)
        {
            this.desenhista = desenhista;
            this.sprite = imagem;
            this.PosRect = pos;
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

        public void Redimensionado()
        {
            PosRel = PosRel;
        }
        public void Atualizar() { }

        public void Desenhar()
        {
            if (ativo)
            {
                desenhista.Draw(sprite, retangulo, null, cor, angulo, Vector2.Zero, efeitos, 0f);
            }
        }
    }
}
