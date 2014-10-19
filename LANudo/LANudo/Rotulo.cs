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
        Vector2 origem = new Vector2(0.5f, 0.5f); public Vector2 Pivot { get { return origem; } set { origem = value; ProcessaPivot(); } }
        Vector2 pivotAbsoluto; public Vector2 PivotAbs { get { return pivotAbsoluto; } }
        Vector2 pivotRelativo; public Vector2 PivotRel { get { return pivotRelativo; } }
        void ProcessaPivot() { pivotAbsoluto = new Vector2(bordas.Width * origem.X, bordas.Height * origem.Y); pivotRelativo = Recursos.AbsParaRelTela(pivotAbsoluto); }
        SpriteEffects efeitos = SpriteEffects.None; public SpriteEffects Eff { get { return efeitos; } set { efeitos = value; } }
        float angulo = 0; public float Rot { get { return MathHelper.ToDegrees(angulo); } set { angulo = MathHelper.ToRadians(value); } }

        public Vector2 PosRel
        {
            get { return posRel; }
            set
            {
                posPx = Recursos.RelTelaParaAbs(value);
                posRel = value;
            }
        }
        Vector2 posPx;
        public Vector2 PosPx
        {
            get { return posPx; }
            set
            {
                posPx = value;
                posRel = Recursos.AbsParaRelTela(value);
            }
        }
        Rectangle bordas; public Rectangle Dimensoes { get { return bordas; } }
        Color cor; public Color Cor { get { return cor; } set { cor = value; } }
        string val; public string Val { get { return val; } set { val = value; Redimensionado(); } }
        string rotulo; public string String { get { return rotulo; } set { rotulo = value; AtualizaPosicoes(); } }
        float escalaInicial;
        float escalaAtual;

        bool ativo, interativo=true;

        public bool EstaInterativo() { return interativo; }
        public void AtivaInterativo() { interativo = true; }
        public void DesativaInterativo() { interativo = false; }

        public bool Ativado() { return ativo; }
        public void Ativar() { ativo = true; }
        public void Desativar() { ativo = false; }

        public Rotulo(SpriteBatch _desenhista, SpriteFont _fonte, string _rotulo, bool _xml, Vector3 _posicao, Color _cor, bool _ativo = true)
        {
            desenhista = _desenhista;
            fonte = _fonte;
            if (_xml) { rotulo = Motor.Textos.Val(val = _rotulo); } else { rotulo = _rotulo; val = null; }
            escalaInicial = _posicao.Z;
            escalaAtual = escalaInicial;
            cor = _cor;
            AtualizaPosicoes();
            PosRel = new Vector2(_posicao.X, _posicao.Y);
            ativo = _ativo;
        }

        public void AtualizaPosicoes()
        {
            if (rotulo != null)
            {
                escalaAtual = Recursos.EscalaFonteRelativoTela(escalaInicial);
                Vector2 dimensoes = fonte.MeasureString(rotulo);
                bordas = new Rectangle(Convert.ToInt16(posPx.X), Convert.ToInt16(posPx.Y), Convert.ToInt16(dimensoes.X * (float)escalaAtual), Convert.ToInt16(dimensoes.Y * (float)escalaAtual));
            }
        }
        public void Redimensionado()
        {
            if (val != null) { rotulo = Motor.Textos.Val(val); }
            AtualizaPosicoes();
            ProcessaPivot();
            PosRel = PosRel;
        }

        public void Atualizar() { }
        public void Desenhar()
        {

            if (ativo && interativo)
            {
                if (rotulo != null)
                {
                    desenhista.DrawString(fonte, rotulo, posPx - pivotAbsoluto, cor, angulo, Vector2.Zero, escalaAtual, efeitos, 0f);
                }
            }
        }
    }
}
