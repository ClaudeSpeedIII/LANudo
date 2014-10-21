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
        public enum Direcao { Sobe, Direita, Desce, Esquerda }
        public static Vector2 DirecionaQuadrado(Direcao sentido, float quanto)
        {
            switch (sentido)
            {
                case Direcao.Sobe:
                    return new Vector2(0f, quanto);
                case Direcao.Direita:
                    return new Vector2(quanto, 0f);
                case Direcao.Desce:
                    return new Vector2(0f, -quanto);
                case Direcao.Esquerda:
                    return new Vector2(-quanto, 0f);
            }
            return new Vector2(0f, 0f);
        }

        public static Vector2 DirecionaLosango(Direcao sentido, float quanto)
        {
            switch (sentido)
            {
                case Direcao.Sobe:
                    return new Vector2(quanto, quanto);
                case Direcao.Direita:
                    return new Vector2(quanto, -quanto);
                case Direcao.Desce:
                    return new Vector2(-quanto, -quanto);
                case Direcao.Esquerda:
                    return new Vector2(-quanto, quanto);
            }
            return new Vector2(0f, 0f);
        }
        public static string VerificaTexto(string texto) { try { Motor.MedeTexto(texto); return texto; } catch (Exception) { Console.WriteLine("Entrou caracter impróprio no XML"); return "INVALID CHAR"; } }

        public static float RegraDeTres(float A, float B, float C)
        {
            return (B * C) / A;

        }
        public static float EscalaFonteRelativoTela(float escalaInicial)
        {
            if (Configuracoes.Largura > Configuracoes.Altura)
            {
                return RegraDeTres(Constantes.resolucao_y(), escalaInicial, Configuracoes.Altura);
            }
            else
            {
                return RegraDeTres(Constantes.resolucao_y(), escalaInicial, Configuracoes.Largura);
            }
        }

        public static Point EscalaRelativoTela(Rectangle dimensoes, float escala)
        {
            if (Configuracoes.Largura > Configuracoes.Altura)
            {
                int alturaEscalada = Convert.ToInt16(Configuracoes.Altura * (dimensoes.Height * escala) / dimensoes.Height);
                return new Point(
                Convert.ToInt16(alturaEscalada * (((float)dimensoes.Width / (float)dimensoes.Height))),
                alturaEscalada
                );
            }
            else
            {
                int alturaEscalada = Convert.ToInt16(Configuracoes.Largura * (dimensoes.Height * escala) / dimensoes.Height);
                return new Point(
                Convert.ToInt16(alturaEscalada * (((float)dimensoes.Width / (float)dimensoes.Height))),
                alturaEscalada
                );
            }
        }

        public static Vector2 RelTelaParaAbs(Vector2 pos)
        {
            return new Vector2(
                Convert.ToInt32(Configuracoes.Largura * pos.X / 1f),
                Convert.ToInt32(Configuracoes.Altura * pos.Y / 1f)
            );
        }
        public static Vector3 RelTelaParaAbs(Vector3 pos)
        {
            return new Vector3(
                Convert.ToInt16(Configuracoes.Largura * pos.X / 1f),
                Convert.ToInt16(Configuracoes.Altura * pos.Y / 1f),
                pos.Z
            );
        }

        public static int RelTelaParaAbs(float y)
        {
            return Convert.ToInt16(Configuracoes.Altura * y / 1);
        }

        public static Vector2 AbsParaRelTela(Vector2 pos)
        {
            return new Vector2(
               pos.X / Configuracoes.Largura,
               pos.Y / Configuracoes.Altura
                );

        }
        public static Vector3 AbsParaRelTela(Vector3 pos)
        {
            return new Vector3(
                pos.X / Configuracoes.Largura,
                pos.Y / Configuracoes.Altura,
                pos.Z
            );
        }

        public static Rectangle RetanguloCentralizado(Rectangle dimensoes)
        {
            return new Rectangle((Configuracoes.Largura / 2) - (dimensoes.Width / 2), (Configuracoes.Altura / 2) - (dimensoes.Height / 2), dimensoes.Width, dimensoes.Height);
        }

        public static Rectangle RetanguloCentralizado(Rectangle dimensoes, float escala)
        {
            Point escalado = EscalaRelativoTela(dimensoes, escala);
            return new Rectangle((Configuracoes.Largura / 2) - (escalado.X / 2), (Configuracoes.Altura / 2) - (escalado.Y / 2), escalado.X, escalado.Y);
        }

        public static Rectangle RetanguloRelativamenteDeslocado(Rectangle dimensoes, float escala, float y)
        {
            Point escalado = EscalaRelativoTela(dimensoes, escala);
            return new Rectangle((Configuracoes.Largura / 2) - (escalado.X / 2), RelTelaParaAbs(y), escalado.X, escalado.Y);
        }

        public static Rectangle RetanguloRelativamenteDeslocado(Rectangle dimensoes, float escala, Vector2 pos)
        {
            Point escalado = EscalaRelativoTela(dimensoes, escala);
            Vector2 posicionado = RelTelaParaAbs(pos);
            return new Rectangle(Convert.ToInt16(posicionado.X - escalado.X / 2), Convert.ToInt16(posicionado.Y - escalado.Y / 2), Convert.ToInt16(escalado.X), Convert.ToInt16(escalado.Y));
        }

        public static Vector2 RetanguloCentralizadoNoRetangulo(Rectangle cantosInternos, Rectangle cantosExternos)
        {
            return new Vector2((cantosExternos.Left + cantosExternos.Width / 2) - (cantosInternos.Width / 2), (cantosExternos.Top + cantosExternos.Height / 2) - (cantosInternos.Height / 2));
        }
        public static Rectangle RetanguloRelativamenteDeslocado(Rectangle dimensoes, Vector3 pos, Vector2 pivotAbsoluto)
        {
            Point escalado = EscalaRelativoTela(dimensoes, pos.Z);
            Vector2 posicionado = RelTelaParaAbs(new Vector2(pos.X, pos.Y));
            return new Rectangle(Convert.ToInt32(posicionado.X - pivotAbsoluto.X), Convert.ToInt32(posicionado.Y - pivotAbsoluto.Y), Convert.ToInt32(escalado.X), Convert.ToInt32(escalado.Y));
        }

    }
}
