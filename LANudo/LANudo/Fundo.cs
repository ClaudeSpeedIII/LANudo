using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LANudo
{
    public class Fundo : Elemento
    {
        Sprite background;

        bool ativo, interativo = true;

        public bool EstaInterativo() { return interativo; }
        public void AtivaInterativo() { interativo = true; }
        public void DesativaInterativo() { interativo = false; }

        public bool Ativado() { return ativo; }
        public void Ativar() { ativo = true; }
        public void Desativar() { ativo = false; }

        public Fundo(SpriteBatch desenhista, Texture2D imagem, bool ativo = true)
        {
            this.background = new Sprite(desenhista, imagem, TelaToda(imagem), true);
            this.ativo = ativo;
        }

        Rectangle TelaToda(Texture2D imagem)
        {
            float propLag = imagem.Width / imagem.Height;
            float propAlt = imagem.Height / imagem.Width;
            int x = 0;
            int y = 0;
            if (Configuracoes.Altura > Configuracoes.Largura)
            {
                y = Convert.ToInt32(Configuracoes.Altura);
                x = Convert.ToInt32(y * propLag);
            }
            else
            {
                x = Convert.ToInt32(Configuracoes.Largura);
                y = Convert.ToInt32(x * propAlt);
            }
            return new Rectangle(Convert.ToInt32((Configuracoes.Largura / 2) - (x / 2)), Convert.ToInt32((Configuracoes.Altura / 2) - (y / 2)), x, y); ;
        }

        public void Redimensionado()
        {
            this.background.PosRect = TelaToda(background.Img);
        }

        public void Atualizar()
        {
            if (ativo && interativo)
            {
                background.Atualizar();
            }
        }

        public void Desenhar()
        {
            if (ativo)
            {
                background.Desenhar();
            }
        }
    }
}
