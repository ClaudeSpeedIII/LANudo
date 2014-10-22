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
        private SpriteBatch desenhista;

        public enum Tipos { Garagem, Saida, Pista, Entrada, Final, Chegada }
        public enum Jogadores { Publico, P1, P2, P3, P4 }
        private Tipos tipoCasa; public Tipos Tipo { get { return tipoCasa; } }
        private int rotacao = 0; public int Rotacao { get { return rotacao; } set { rotacao = value; foreach (Sprite s in sprites) { s.Rot = value; } } }


        //public Vector2 PosicaoPx { get { return new Vector2(spriteBase.PosPx.X, spriteBase.PosPx.Y); } set { foreach (Sprite s in sprites) { s.PosPx = new Vector3(value.X, value.Y, s.PosPx.Z); } } }
        public Vector2 PosicaoRel { get { return new Vector2(spriteBase.PosRel.X, spriteBase.PosRel.Y); } set { foreach (Sprite s in sprites) { s.PosRel = new Vector3(value.X, value.Y, s.PosRel.Z); } } }
        public Vector2 PosicaoAbs { get { return spriteBase.Pivot * spriteBase.PosPx; } set { foreach (Sprite s in sprites) { s.PosPx = value; } } }
        public Vector2 TamanhoAbs { get { return new Vector2(spriteBase.Img.Height, spriteBase.Img.Width); } }
        private Jogadores donoCasa; public Jogadores Dono { get { return donoCasa; } }
        private CoresLudo cores; public CoresLudo Cores { get { return cores; } }
        private Color corBase; public Color CorBase { get { return corBase; } }
        private Color corResto; public Color CorResto { get { return corResto; } }
        public Vector2 TamanhoRel { get { return spriteBase.TamanhoRel; } }

        List<Sprite> sprites;
        Sprite spriteBase; public Sprite Base { get { return spriteBase; } }

        //List<Sprite> peoes = new List<Sprite>();
        private Peao peoesAqui = new Peao(Jogadores.P3,2); public Peao Peoes { get { return peoesAqui; } set { peoesAqui = value; } }
        /*private Vector2 offsetEmpilha; public Vector2 OffsetEmpilhamento { get { return offsetEmpilha; } set { offsetEmpilha = value; AtualizaPeoes(); } }
        private Vector2 pivotPeao; public Vector2 PivotPeao { get { return pivotPeao; } set { value = pivotPeao; AtualizaPeoes(); } }
        void AtualizaPeoes()
        {
            peoes = new List<Sprite>();
            for (int i = 1; i <= peoesAqui.Quantidade; i++)
            {
                Sprite spr = new Sprite(desenhista, imgPeao, spriteBase.PosRel);
                spr.Pivot = pivotPeao;
                spr.PosRel += new Vector3(offsetEmpilha.X,offsetEmpilha.Y,spr.PosRel.Z);

                peoes.Add(spr);
            }
        }
        */
        public Casa(SpriteBatch desenhista, ParametrosCasa parm, CoresLudo esquemaCor, Jogadores dono, Vector3 pos, bool _ativo = true)
        {
            this.desenhista = desenhista;

            tipoCasa = parm.TipoCasa;
            bool first = true;
            sprites = new List<Sprite>();
            donoCasa = dono;
            cores = esquemaCor;

            switch (parm.TipoCasa)
            {
                case Tipos.Saida:
                    corBase = cores.CorJogador(dono);
                    corResto = Color.White;
                    break;
                case Tipos.Entrada:
                    corBase = Recursos.Saturacao(cores.Publico, parm.ClareadaCor);
                    corResto = cores.CorJogador(dono);
                    break;
                default:
                    corBase = cores.CorJogador(dono);
                    corResto = Color.White;
                    break;
            }
            corBase = Recursos.Alpha(corBase, -100);
            corResto = Recursos.Alpha(corResto, -20);

            foreach (Texture2D img in parm.Sprites)
            {
                if (first)
                {
                    spriteBase = new Sprite(desenhista, img, pos, corBase);
                    sprites.Add(spriteBase);
                    first = false;
                }
                else
                {
                    //float escalado = Recursos.RegraDeTres(spriteBase.Img.Height, pos.Z, img.Height); //Chegar depois se ficou legal
                    sprites.Add(new Sprite(desenhista, img, pos/*new Vector3(pos.X, pos.Y, escalado)*/, corResto));
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
            bool first = true;
            foreach (Sprite spr in sprites)
            {
                if (first) { spr.Redimensionado(); first = false; }
                else
                {
                    float escalado = (Configuracoes.Largura > Configuracoes.Altura) ?
                        Recursos.RegraDeTres(spriteBase.Img.Height, spriteBase.PosRel.Z, spr.Img.Height) :
                        Recursos.RegraDeTres(spriteBase.Img.Width, spriteBase.PosRel.Z, spr.Img.Width);
                    spr.PosRel = new Vector3(spriteBase.PosRel.X, spriteBase.PosRel.Y, escalado);
                }
            }
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
