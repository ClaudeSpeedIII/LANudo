using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LANudo
{
    public static class Recursos
    {

        public static float RegraDeTres(float A, float B, float C)
        {
            return (B * C) / A;
        }

        public static Point EscalaRelativoTela(Rectangle dimensoes, float escala)
        {
            int alturaEscalada = Convert.ToInt16(Motor.Altura * (dimensoes.Height * escala) / dimensoes.Height);
            return new Point(
                Convert.ToInt16(alturaEscalada * (((float)dimensoes.Width / (float)dimensoes.Height))),
                alturaEscalada
            );
        }

        public static Point RelTelaParaAbs(Vector2 pos)
        {
            return new Point(
                Convert.ToInt16(Motor.Largura * pos.X / 1),
                Convert.ToInt16(Motor.Altura * pos.Y / 1)
            );
        }

        public static int RelTelaParaAbs(float y)
        {
            return Convert.ToInt16(Motor.Altura * y / 1);
        }

        public static Rectangle RetanguloCentralizado(Rectangle dimensoes)
        {
            return new Rectangle((Motor.Largura / 2) - (dimensoes.Width / 2), (Motor.Altura / 2) - (dimensoes.Height / 2), dimensoes.Width, dimensoes.Height);
        }

        public static Rectangle RetanguloCentralizado(Rectangle dimensoes, float escala)
        {
            Point escalado = EscalaRelativoTela(dimensoes, escala);
            return new Rectangle((Motor.Largura / 2) - (escalado.X / 2), (Motor.Altura / 2) - (escalado.Y / 2), escalado.X, escalado.Y);
        }

        public static Rectangle RetanguloRelativamenteDeslocado(Rectangle dimensoes, float escala, float y)
        {
            Point escalado = EscalaRelativoTela(dimensoes, escala);
            return new Rectangle((Motor.Largura / 2) - (escalado.X / 2), RelTelaParaAbs(y), escalado.X, escalado.Y);
        }

        public static Rectangle RetanguloRelativamenteDeslocado(Rectangle dimensoes, float escala, Vector2 pos)
        {
            Point escalado = EscalaRelativoTela(dimensoes, escala);
            Point posicionado = RelTelaParaAbs(pos);
            return new Rectangle(posicionado.X - escalado.X / 2, posicionado.Y - escalado.Y / 2, escalado.X, escalado.Y);
        }

        public static Vector2 RetanguloCentralizadoNoRetangulo(Rectangle cantosInternos,Rectangle cantosExternos)
        {
            return new Vector2((cantosExternos.Left + cantosExternos.Width / 2) - (cantosInternos.Width / 2), (cantosExternos.Top + cantosExternos.Height / 2) - (cantosInternos.Height / 2));
        }


    }
}
