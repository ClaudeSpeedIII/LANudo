using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LANudo
{
    public class Rotulo : Elemento
    {
        SpriteBatch desenhista;
        SpriteFont fonte;
        Vector2 posRel;
        SpriteEffects efeitos = SpriteEffects.None; public SpriteEffects Eff { get { return efeitos; } set { efeitos = value; } }
        float angulo = 0; public float Rot { get { return MathHelper.ToDegrees(angulo); } set { angulo = MathHelper.ToRadians(value); } }

        public Vector2 PosRel
        {
            get { return posRel; }
            set
            {
                posPx = Recursos.RelTelaParaAbs(value) - new Vector2(bordas.Width / 2, bordas.Height / 2);
                posRel = value;
            }
        }
        Vector2 posPx;
        public Vector2 PosPx
        {
            get { return posPx + new Vector2(bordas.Width / 2f, bordas.Height / 2f);}
            set
            {
                posPx = value - new Vector2(bordas.Width / 2, bordas.Height / 2);
                posRel = Recursos.AbsParaRelTela(value);
            }
        }
        Rectangle bordas; public Rectangle Dimensoes { get { return bordas; } }
        Color cor; public Color Cor { get { return cor; } set { cor = value; } }
        string rotulo; public string String { get { return rotulo; } set { rotulo = value; AtualizaPosicoes(); } }
        float escalaInicial;
        float escalaAtual;

        bool ativo;

        public bool Ativado() { return ativo; }

        public void Ativar() { ativo = true; }

        public void Desativar() { ativo = false; }

        public Rotulo(SpriteBatch _desenhista, SpriteFont _fonte, string _rotulo, Vector3 _posicao, Color _cor, bool _ativo = true)
        {
            desenhista = _desenhista;
            fonte = _fonte;
            rotulo = _rotulo;
            escalaInicial = _posicao.Z;
            escalaAtual = escalaInicial;
            cor = _cor;
            AtualizaPosicoes();
            PosRel = new Vector2(_posicao.X, _posicao.Y);
            ativo = _ativo;
        }

        public void AtualizaPosicoes()
        {
            escalaAtual = Recursos.EscalaFonteRelativoTela(escalaInicial);
            Vector2 dimensoes = fonte.MeasureString(rotulo);
            bordas = new Rectangle(Convert.ToInt16(posPx.X), Convert.ToInt16(posPx.Y), Convert.ToInt16(dimensoes.X * (float)escalaAtual), Convert.ToInt16(dimensoes.Y * (float)escalaAtual));
            
        }
        public void Redimensionado()
        {
            AtualizaPosicoes();
            PosRel = PosRel;
        }

        public void Atualizar() { }
        public void Desenhar()
        {
            if (ativo)
            {
                desenhista.DrawString(fonte, rotulo, posPx, cor, angulo, Vector2.Zero, escalaAtual, efeitos, 0f);
            }
        }
    }
}
