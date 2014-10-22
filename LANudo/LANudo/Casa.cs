using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LANudo
{
    public class Casa : Elemento
    {
        public enum Tipos { Garagem, Saida, Pista, Entrada, Final, Chegada }
        public enum Jogadores { P1, P2, P3, P4, Publico }
        private Tipos tipoCasa; public Tipos Tipo { get { return tipoCasa; } }
        private int rotacao = 0; public int Rotacao { get { return rotacao; } set { rotacao = value; foreach (Sprite s in sprites) { s.Rot = value; } } }

        public Vector2 PosicaoPx { get { return new Vector2(spriteBase.PosPx.X, spriteBase.PosPx.Y); } set { foreach (Sprite s in sprites) { s.PosPx = new Vector2(value.X, value.Y); } } }
        public Vector2 PosicaoRel { get { return new Vector2(spriteBase.PosRel.X, spriteBase.PosRel.Y); } set { foreach (Sprite s in sprites) { s.PosRel = new Vector3(value.X, value.Y, s.PosRel.Z); } } }
        public Vector2 TamanhoRel { get { return spriteBase.TamanhoRel; } }
        public Vector2 TamanhoAbs { get { return spriteBase.TamanhoAbs; } }
        private Jogadores donoCasa; public Jogadores Dono { get { return donoCasa; } }
        private CoresLudo cores; public CoresLudo Cores { get { return cores; } }
        private Color cor; public Color Cor { get { return cor; } }

        List<Sprite> sprites;
        Sprite spriteBase; public Sprite Base { get { return spriteBase; } }


        public Casa(SpriteBatch desenhista, ParametrosCasa parm, CoresLudo esquemaCor, Jogadores dono, Vector3 pos, bool _ativo = true)
        {
            tipoCasa = parm.TipoCasa;
            bool first = true;
            sprites = new List<Sprite>();
            donoCasa = dono;
            cores = esquemaCor;

            switch (dono)
            {
                case Jogadores.Publico:
                    cor = tipoCasa == Tipos.Entrada ? esquemaCor.PublicoAlternativo : esquemaCor.Publico;
                    break;
                case Jogadores.P1:
                    cor = tipoCasa == Tipos.Entrada ? esquemaCor.P1Alternativo : esquemaCor.P1;
                    break;
                case Jogadores.P2:
                    cor = tipoCasa == Tipos.Entrada ? esquemaCor.P2Alternativo : esquemaCor.P2;
                    break;
                case Jogadores.P3:
                    cor = tipoCasa == Tipos.Entrada ? esquemaCor.P3Alternativo : esquemaCor.P3;
                    break;
                case Jogadores.P4:
                    cor = tipoCasa == Tipos.Entrada ? esquemaCor.P4Alternativo : esquemaCor.P4;
                    break;
            }
            int count = 0;
            int corCount = (int)dono;
            foreach (Texture2D img in parm.Sprites)
            {
                if (tipoCasa != Tipos.Chegada)
                {
                    if (first)
                    {
                        spriteBase = new Sprite(desenhista, img, pos, cor);
                        sprites.Add(spriteBase);
                        first = false;
                    }
                    else
                    {
                        float escalado = Recursos.RegraDeTres(spriteBase.Img.Height, pos.Z, img.Height); //Chegar depois se ficou legal
                        sprites.Add(new Sprite(desenhista, img, new Vector3(pos.X, pos.Y, escalado), cor));
                    }
                }
                else
                {
                    Vector2 pivot = Vector2.Zero;
                    spriteBase = new Sprite(desenhista, img, pos, CorJogador(cores, (Jogadores)corCount));
                    Vector2 sentido = Recursos.Direciona((Recursos.Direcao)count, new Vector2(0f, spriteBase.TamanhoRel.Y/2));
                    spriteBase.PosRel = pos + new Vector3(sentido.X, sentido.Y, (count == 0 || count == 2) ? spriteBase.PosRel.Z : spriteBase.PosRel.Z*3);
                    sprites.Add(spriteBase);
                    count++; corCount++;
                    if (corCount > 3) { corCount = 0; } //VOLTE AQUI DEPOIS PARA ARRUMAR E DEIXAR BONITOSO
                }
            }

            ativo = _ativo;
        }

        Color CorJogador(CoresLudo cores, Jogadores jogador)
        {
            switch (jogador)
            {
                case Jogadores.P1:
                    return cores.P1;
                case Jogadores.P2:
                    return cores.P2;
                case Jogadores.P3:
                    return cores.P3;
                case Jogadores.P4:
                    return cores.P4;
            }
            return cores.Publico;
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
