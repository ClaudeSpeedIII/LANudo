﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LANudo
{
    public delegate void ManipuladorSprite(Sprite origem);
    public class Sprite : Elemento
    {
        public event ManipuladorSprite Clicado; public void ZeraClicado() { Clicado = null; }
        public event ManipuladorSprite MouseEmCima; public void ZeraMouseEmCima() { MouseEmCima = null; }
        public event ManipuladorSprite MouseEmVolta; public void ZeraMouseEmVolta() { MouseEmVolta = null; }

        bool ativo, interativo = true;

        public bool EstaInterativo() { return interativo; }
        public void AtivaInterativo() { interativo = true; }
        public void DesativaInterativo() { interativo = false; }

        public bool Ativado() { return ativo; }
        public void Ativar() { ativo = true; }
        public void Desativar() { ativo = false; }
        SpriteBatch desenhista;
        MouseState ratoAnterior;
        bool temMouseSobre = false; public bool PodeMouseSobre { set { temMouseSobre = value; } }
        bool mouseSobre = false; public bool MouseSobre { get { return mouseSobre; } }
        bool dinamico = false; public bool Dinamico { get { return dinamico; } set { dinamico = value; } }
        bool clicouDentro;

        Texture2D imagemAtual;
        Texture2D imagem; public Texture2D Img { get { return imagem; } set { imagem = value; Redimensionado(); } }
        Texture2D imagemMouseOver = null; public Texture2D ImgMouse { get { return imagemMouseOver; } set { imagemMouseOver = value; Redimensionado(); } }
        float angulo = 0; public float Rot { get { return MathHelper.ToDegrees(angulo); } set { angulo = MathHelper.ToRadians(value); } }
        SpriteEffects efeitos = SpriteEffects.None; public SpriteEffects Eff { get { return efeitos; } set { efeitos = value; } }

        Rectangle retangulo;
        //Vector3 pixel;
        Vector3 relativo;
        Vector2 pixel;
        Vector2 origin = new Vector2(0.5f, 0.5f);
        Vector2 pivotRel;
        Vector2 pivotAbs;


        public Rectangle PosRect
        {
            get { return retangulo; }
            set
            {
                retangulo = Recursos.RotacionaRetangulo(value,0);
                origin = new Vector2(0, 0);
                float escalaRel = (Configuracoes.Altura < Configuracoes.Largura) ? ((float)retangulo.Height) / ((float)Configuracoes.Altura) : ((float)retangulo.Width) / ((float)Configuracoes.Largura);
                escalaRel = escalaRel = Recursos.Truncate(escalaRel, 2);
                relativo = Recursos.AbsParaRelTela(new Vector3(retangulo.X, retangulo.Y, escalaRel));
                //pixel = new Vector3((float)retangulo.X, (float)retangulo.Y, relativo.Z);
                pivotRel = new Vector2(0f, 0f);
                pivotAbs = new Vector2(0f, 0f);
                pixel = new Vector2(retangulo.X, retangulo.Y);
                CalculaTamanhoRelativo();
            }
        }
        /* public Vector3 PosPx
         {
             get { return pixel; }
             set
             {
                 pixel = value;
                 ProcessaPivotAbs(value.Z);
                 retangulo = new Rectangle(Convert.ToInt32(value.X - pivotAbs.X), Convert.ToInt32(value.Y - pivotAbs.Y), Convert.ToInt32(imagemAtual.Width * value.Z), Convert.ToInt32(imagemAtual.Height * value.Z));
                 relativo = Recursos.AbsParaRelTela(new Vector3(retangulo.X - pivotAbs.X, retangulo.Y - pivotAbs.Y, ((float)retangulo.Height) / ((float)Configuracoes.Altura)));
                 ProcessaPivotRel(relativo.Z);

                 CalculaTamanhoRelativo();
             }
         }*/
        public Vector3 PosRel
        {
            get { return relativo; }
            set
            {
                    ProcessaPivotRel(value.Z);
                    ProcessaPivotAbs(value.Z);
                relativo = value;
                //escalaRel = (Configuracoes.Altura > Configuracoes.Largura) ? Recursos.EscalaRelativoTela(imagemAtual.Bounds, value.Z).X / ((float)Configuracoes.Largura) : Recursos.EscalaRelativoTela(imagemAtual.Bounds, value.Z).Y / ((float)Configuracoes.Altura);
                //escalaRel = Recursos.Truncate(escalaRel, 2); /\ sou retardado. já possuo a escala relativa no value
                retangulo = Recursos.RotacionaRetangulo(Recursos.RetanguloRelativamenteDeslocado(imagemAtual.Bounds, value, pivotAbs), 0);
                pixel = new Vector2(retangulo.X + pivotAbs.X, retangulo.Y + pivotAbs.Y);
                //pixel = new Vector3(retangulo.X - pivotRel.X, retangulo.Y - pivotRel.Y, escalaAbs);
                CalculaTamanhoRelativo();
            }
        }
        public Vector2 Pivot
        {
            get { return origin; }
            set
            {
                origin = value;
                ProcessaPivotRel(relativo.Z);
                ProcessaPivotAbs(relativo.Z);
                PosRel = PosRel;
            }
        }

        float escalaAbs;

        void ProcessaPivotAbs(float escalaRel)
        {
            escalaAbs = (Configuracoes.Altura < Configuracoes.Largura) ? (escalaRel * Configuracoes.Altura) / imagem.Height : (escalaRel * Configuracoes.Largura) / imagem.Width;
            Rectangle novo = Recursos.RotacionaRetangulo(imagem.Bounds, angulo);
            pivotAbs = new Vector2((novo.Width * escalaAbs) * origin.X, (novo.Height * escalaAbs) * origin.Y);
        }

        void ProcessaPivotRel(float escalaRel)
        {
            Point escaladoRel = Recursos.EscalaRelativoTela(imagemAtual.Bounds, escalaRel);
            Rectangle antigo = new Rectangle(0, 0, escaladoRel.X, escaladoRel.Y);
            Rectangle novo = Recursos.RotacionaRetangulo(new Rectangle(0, 0, escaladoRel.X, escaladoRel.Y), angulo);
            pivotRel = new Vector2(novo.X * origin.X, novo.Y * origin.Y);
        }


        Vector2 tamanhoRelativo;
        public Vector2 TamanhoRel
        {
            get { return tamanhoRelativo; }
        }
        public Vector2 TamanhoAbs
        {
            get { return new Vector2(imagem.Width, imagem.Height); }
        }

        void CalculaTamanhoRelativo()
        {
            float propLag = (float)imagemAtual.Width / (float)imagemAtual.Height;
            float propAlt = (float)imagemAtual.Height / (float)imagemAtual.Width;

            tamanhoRelativo = (Configuracoes.Largura > Configuracoes.Altura) ? new Vector2(relativo.Z, relativo.Z * propAlt) : new Vector2(relativo.Z * propLag, relativo.Z);

        }


        Color corAtual = Color.White; public Color Cor { get { return corAtual; } set { corAtual = value; } }
        EsquemaCores cores = new EsquemaCores(); public EsquemaCores Cores { get { return cores; } set { cores = value; } }


        public Sprite(SpriteBatch desenhista, Texture2D imagem, Rectangle pos, Color cor, bool ativo = true)
        {
            this.desenhista = desenhista;
            this.imagemAtual = imagem;
            this.imagem = imagem;
            this.corAtual = cor;
            this.cores = new EsquemaCores(cor, cor, cor, cor);
            this.PosRect = pos;
            this.ativo = ativo;
        }
        public Sprite(SpriteBatch desenhista, Texture2D imagem, Rectangle pos, bool ativo = true)
        {
            this.desenhista = desenhista;
            this.imagemAtual = imagem;
            this.imagem = imagem;
            this.PosRect = pos;
            this.ativo = ativo;
        }

        public Sprite(SpriteBatch desenhista, Texture2D imagem, Vector3 pos, Color cor, bool ativo = true)
        {
            this.desenhista = desenhista;
            this.imagemAtual = imagem;
            this.imagem = imagem;
            this.corAtual = cor;
            this.cores = new EsquemaCores(cor, cor, cor, cor);
            this.PosRel = pos;
            this.ativo = ativo;
        }
        public Sprite(SpriteBatch desenhista, Texture2D imagem, Vector3 pos, bool ativo = true)
        {
            this.desenhista = desenhista;
            this.imagemAtual = imagem;
            this.imagem = imagem;
            this.PosRel = pos;
            this.ativo = ativo;
        }
        public Sprite(SpriteBatch desenhista, Texture2D imagem, Vector3 pos, Vector2 pivot, Color cor, bool ativo = true)
        {
            this.desenhista = desenhista;
            this.imagemAtual = imagem;
            this.imagem = imagem;
            this.corAtual = cor;
            this.cores = new EsquemaCores(cor, cor, cor, cor);
            this.PosRel = pos;
            this.Pivot = pivot;
            this.ativo = ativo;
        }
        public Sprite(SpriteBatch desenhista, Texture2D imagem, Vector3 pos, Vector2 pivot, bool ativo = true)
        {
            this.desenhista = desenhista;
            this.imagemAtual = imagem;
            this.imagem = imagem;
            this.PosRel = pos;
            this.Pivot = pivot;
            this.ativo = ativo;
        }
        /*
        public Sprite(SpriteBatch desenhista, Texture2D imagem, Vector2 posPx, float escala, Vector2 pivot, Color cor, bool ativo = true)
        {
            this.desenhista = desenhista;
            this.imagemAtual = imagem;
            this.imagem = imagem;
            this.corAtual = cor;
            this.cores = new EsquemaCores(cor, cor, cor, cor);
            this.PosPx = new Vector3(posPx.X, posPx.Y, escala);
            this.Pivot = pivot;
            this.ativo = ativo;
        }
        public Sprite(SpriteBatch desenhista, Texture2D imagem, Vector2 posPx, float escala, Vector2 pivot, bool ativo = true)
        {
            this.desenhista = desenhista;
            this.imagemAtual = imagem;
            this.imagem = imagem;
            this.PosPx = new Vector3(posPx.X, posPx.Y, escala);
            this.Pivot = pivot;
            this.ativo = ativo;
        }*/
        public void CursorEmCima()
        {
            clicouDentro = false;
            if (temMouseSobre)
            {
                if (MouseEmCima != null) { MouseEmCima(this); }
                corAtual = cores.CorFundoMouse;
                if (imagemMouseOver != null) { imagemAtual = imagemMouseOver; }
            }
        }

        public void CursorEmVolta()
        {
            if (MouseEmVolta != null && temMouseSobre) { MouseEmVolta(this); }
            corAtual = cores.CorFundo;
            imagemAtual = imagem;
        }

        public void Redimensionado()
        {
            PosRel = PosRel;
        }

        public void Atualizar()
        {
            if (ativo && interativo)
            {
                if (dinamico)
                {
                    Redimensionado();
                }
                if (Clicado != null || MouseEmCima != null || MouseEmVolta != null)
                {
                    MouseState rato = Mouse.GetState();
                    if (rato.X > retangulo.Left && rato.X < retangulo.Right && rato.Y > retangulo.Top && rato.Y < retangulo.Bottom)
                    {
                        if (!mouseSobre) { CursorEmCima(); }
                        mouseSobre = true;
                    }
                    else
                    {
                        if (mouseSobre) { CursorEmVolta(); }
                        mouseSobre = false;
                    }
                    if (mouseSobre && rato.LeftButton == ButtonState.Pressed && ratoAnterior.LeftButton == ButtonState.Released) { clicouDentro = true; }
                    if (mouseSobre && rato.LeftButton == ButtonState.Released && ratoAnterior.LeftButton == ButtonState.Pressed && clicouDentro)
                    {
                        if (Clicado != null) { Clicado(this); }
                        clicouDentro = false;
                    }
                    ratoAnterior = rato;
                }
            }
        }

        public void Desenhar()
        {
            if (ativo)
            {
                desenhista.Draw(imagemAtual, retangulo, null, corAtual, angulo, Vector2.Zero, efeitos, 0f);
            }
        }
    }
}
