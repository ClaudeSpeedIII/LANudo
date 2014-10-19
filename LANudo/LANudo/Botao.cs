using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LANudo
{
    public delegate void ManipuladorBotao(Botao origem);
    public class Botao : Elemento
    {
        public event ManipuladorBotao Clicado;
        public event ManipuladorBotao MouseEmCima;
        public event ManipuladorBotao MouseEmVolta;

        SpriteBatch desenhista;
        SpriteFont fonte;
        Texture2D imagem;
        bool dinamico;
        Texture2D imagemMouseOver;
        float tamanho;
        Vector2 posicao;
        EsquemaCores cores;
        EsquemaCores oldCores;
        Color corTextoAtual;
        Color corFundoAtual;

        float escalaTextoInicial;
        string rotulo;
        string val;
        bool temTexto;
        Rectangle dimensoesTexto;



        bool mouseSobre; public bool MouseSobre { get { return mouseSobre; } }
        bool clicouDentro;
        MouseState ratoAnterior;
        float escalaTextoAtual;
        Texture2D imagemAtual;
        Rectangle cantos;
        Vector2 posicaoTexto;


        bool ativo;

        public bool Ativado() { return ativo; }

        public void Ativar() { ativo = true; }

        public void Desativar() { ativo = false; }

        public bool TemTexto() { return temTexto; }

        public void MostraTexto() { temTexto = true; }

        public void OcultaTexto() { temTexto = false; }

        bool temMouseSobre = false;
        public bool SobreMouse() { return temMouseSobre; }
        public void AtivarSobreMouse() { temMouseSobre = true; }
        public void DesativarSobreMouse() { temMouseSobre = false; }

        public Rectangle Cantos
        {
            get { return cantos; }
        }
        public string Rotulo
        {
            get { return rotulo; }
            set
            {
                rotulo = value;
                Redimensionado();
                temTexto = true;
            }
        }
        public Vector2 Posicao
        {
            get { return posicao; }
            set
            {
                posicao = value;
                Redimensionado();
            }
        }

        public EsquemaCores Cores
        {
            get { return cores; }
            set { cores = value; }
        }

        public float Escala
        {
            get { return tamanho; }
            set
            {
                tamanho = value;
                Redimensionado();
            }
        }

        public Botao(SpriteBatch _desenhista, Texture2D _fundo, EsquemaCores _cores, Vector2 _posicao, float _tamanho, SpriteFont _fonte, string _rotulo, bool _xml, float _escalaTexto, bool _dinamico = false, bool _ativo = true)
        {
            desenhista = _desenhista;
            imagem = _fundo;
            imagemMouseOver = null;
            cores = _cores;
            if (_xml) { rotulo = Motor.Textos.Val(val = _rotulo); } else { val = null; rotulo = _rotulo; }
            posicao = _posicao;
            tamanho = _tamanho;
            fonte = _fonte;
            escalaTextoAtual = _escalaTexto;
            escalaTextoInicial = _escalaTexto;
            clicouDentro = false;
            mouseSobre = false;
            temMouseSobre = false;
            temTexto = true;
            dinamico = _dinamico;
            Redimensionado();
            CursorEmVolta();
            ativo = _ativo;
        }

        public Botao(SpriteBatch _desenhista, Texture2D _fundo, Texture2D _fundoMouseOver, EsquemaCores _cores, Vector2 _posicao, float _tamanho, SpriteFont _fonte, string _rotulo, bool _xml, float _escalaTexto, bool _dinamico = false, bool _ativo = true)
        {
            desenhista = _desenhista;
            imagem = _fundo;
            imagemMouseOver = _fundoMouseOver;
            cores = _cores;
            if (_xml) { rotulo = Motor.Textos.Val(val = _rotulo); } else { val = null; rotulo = _rotulo; }
            posicao = _posicao;
            tamanho = _tamanho;
            fonte = _fonte;
            escalaTextoAtual = _escalaTexto;
            escalaTextoInicial = _escalaTexto;
            clicouDentro = false;
            mouseSobre = false;
            temMouseSobre = false;
            temTexto = true;
            dinamico = _dinamico;
            Redimensionado();
            CursorEmVolta();
            ativo = _ativo;
        }

        public Botao(SpriteBatch _desenhista, Texture2D _fundo, EsquemaCores _cores, Vector2 _posicao, float _tamanho, bool _dinamico = false, bool _ativo = true)
        {
            desenhista = _desenhista;
            imagem = _fundo;
            imagemMouseOver = null;
            cores = _cores;
            posicao = _posicao;
            tamanho = _tamanho;
            clicouDentro = false;
            mouseSobre = false;
            temMouseSobre = false;
            temTexto = false;
            dinamico = _dinamico;
            Redimensionado();
            CursorEmVolta();
            ativo = _ativo;
        }

        public Botao(SpriteBatch _desenhista, Texture2D _fundo, Texture2D _fundoMouseOver, EsquemaCores _cores, Vector2 _posicao, float _tamanho, bool _dinamico = false, bool _ativo = true)
        {
            desenhista = _desenhista;
            imagem = _fundo;
            imagemMouseOver = _fundoMouseOver;
            cores = _cores;
            posicao = _posicao;
            tamanho = _tamanho;
            clicouDentro = false;
            mouseSobre = false;
            temMouseSobre = false;
            temTexto = false;
            dinamico = _dinamico;
            Redimensionado();
            CursorEmVolta();
            ativo = _ativo;
        }

        public void ZeraEventos()
        {
            Clicado = null;
        }

        public void MedeFonte()
        {
            Vector2 dimensoes = fonte.MeasureString(rotulo);
            dimensoesTexto = new Rectangle(0, 0, Convert.ToInt16(dimensoes.X * (float)escalaTextoAtual), Convert.ToInt16(dimensoes.Y * (float)escalaTextoAtual));
        }

        public void CursorEmCima()
        {
            clicouDentro = false;
            if (temMouseSobre)
            {
                if (MouseEmCima != null) { MouseEmCima(this); }
                corTextoAtual = cores.CorTextoMouse;
                corFundoAtual = cores.CorFundoMouse;
                if (imagemMouseOver != null) { imagemAtual = imagemMouseOver; }
            }
        }

        public void CursorEmVolta()
        {
            if (MouseEmVolta != null) { MouseEmVolta(this); }
            corTextoAtual = cores.CorTexto;
            corFundoAtual = cores.CorFundo;
            imagemAtual = imagem;
        }

        public void Redimensionado()
        {
            if (temTexto && rotulo != null) { if (val != null) { rotulo = Motor.Textos.Val(val); } MedeFonte(); }
            cantos = Recursos.RetanguloRelativamenteDeslocado(imagem.Bounds, tamanho, posicao);
            escalaTextoAtual = Recursos.EscalaFonteRelativoTela(escalaTextoInicial);
            posicaoTexto = Recursos.RetanguloCentralizadoNoRetangulo(dimensoesTexto, cantos);
        }


        public void Atualizar()
        {
            if (ativo)
            {
                MouseState rato = Mouse.GetState();
                if (rato.X > cantos.Left && rato.X < cantos.Right && rato.Y > cantos.Top && rato.Y < cantos.Bottom)
                {
                    if (!mouseSobre) { CursorEmCima(); }
                    mouseSobre = true;
                }
                else
                {
                    if (mouseSobre) { CursorEmVolta(); }
                    mouseSobre = false;
                }
                if (dinamico)
                {
                    Redimensionado();
                }
                if (mouseSobre && rato.LeftButton == ButtonState.Pressed && ratoAnterior.LeftButton == ButtonState.Released) { clicouDentro = true; }
                if (mouseSobre && rato.LeftButton == ButtonState.Released && ratoAnterior.LeftButton == ButtonState.Pressed && clicouDentro)
                {
                    if (Clicado != null) { Clicado(this); }
                    clicouDentro = false;
                }
                ratoAnterior = rato;

                if ((oldCores.CorFundo != cores.CorFundo) || (oldCores.CorFundoMouse != cores.CorFundoMouse) || (oldCores.CorTexto != cores.CorTexto) || (oldCores.CorTextoMouse != cores.CorTextoMouse))
                {
                    if (mouseSobre && temMouseSobre) { corTextoAtual = cores.CorTextoMouse; corFundoAtual = cores.CorFundoMouse; } else { corTextoAtual = cores.CorTexto; corFundoAtual = cores.CorFundo; }
                    oldCores = cores;
                }
            }
        }

        public void Desenhar()
        {
            if (ativo)
            {
                desenhista.Draw(imagemAtual, cantos, corFundoAtual);
                if (temTexto && rotulo != null)
                {
                    desenhista.DrawString(fonte, rotulo, posicaoTexto, corTextoAtual, 0f, new Vector2(0f, 0f), escalaTextoAtual, new SpriteEffects(), 0f);
                }
            }
        }
    }
}
