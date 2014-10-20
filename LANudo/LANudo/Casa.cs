using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LANudo
{
    class Casa : Elemento
    {
        public enum Tipos { Garagem, Saida, Pista, Entrada, Final, Chegada }
        public enum Jogadores { Publico, P1, P2, P3, P4 }
        private Tipos tipoCasa; public Tipos Tipo { get { return tipoCasa; } }
        private Jogadores donoCasa; public Jogadores Dono { get { return donoCasa; } }
        private CoresLudo cores;
        private Color cor;

        List<Sprite> sprites;
        Vector2 tamanhoBase;


        public Casa(SpriteBatch desenhista, ParametrosCasa parm, CoresLudo cores, Jogadores dono, Vector3 pos, bool _ativo = true)
        {
            tipoCasa = parm.TipoCasa;
            bool first = true;
            sprites = new List<Sprite>();
            dono = donoCasa;
            switch (dono)
            {
                case Jogadores.Publico:
                    cor = tipoCasa == Tipos.Entrada ? cores.PublicoAlternativo : cores.Publico;
                    break;
                case Jogadores.P1:
                    cor = tipoCasa == Tipos.Entrada ? cores.P1Alternativo : cores.P1;
                    break;
                case Jogadores.P2:
                    cor = tipoCasa == Tipos.Entrada ? cores.P2Alternativo : cores.P2;
                    break;
                case Jogadores.P3:
                    cor = tipoCasa == Tipos.Entrada ? cores.P3Alternativo : cores.P3;
                    break;
                case Jogadores.P4:
                    cor = tipoCasa == Tipos.Entrada ? cores.P4Alternativo : cores.P4;
                    break;
            }


            foreach (Texture2D img in parm.Sprites)
            {
                if (first)
                {
                    tamanhoBase = new Vector2(img.Width,img.Height);
                    sprites.Add(new Sprite(desenhista, img, pos, cor));
                }
                else
                {
                    Vector3 posEscalada = new Vector3(pos.X,pos.Y,Recursos.RegraDeTres(tamanhoBase.Y,pos.Z,img.Height)); //Chegar depois se ficou legal
                    sprites.Add(new Sprite(desenhista, img, posEscalada,cor));
                }
            }
            ativo = _ativo;
        }

        bool ativo, interativo = true;

        public bool EstaInterativo() { return interativo; }
        public void AtivaInterativo() { interativo = true; }
        public void DesativaInterativo() { interativo = false; }

        public bool Ativado() { return ativo; }
        public void Ativar() { ativo = true; }
        public void Desativar() { ativo = false; }


        public void Redimensionado()
        {
            foreach (Sprite spr in sprites) { spr.Redimensionado(); }
        }

        public void Atualizar()
        {
            if (ativo && interativo)
            {
                foreach (Sprite spr in sprites) { spr.Atualizar(); }
            }
        }

        public void Desenhar()
        {
            if (ativo)
            {
                foreach (Sprite spr in sprites) { spr.Desenhar(); }
            }
        }

    }
}
