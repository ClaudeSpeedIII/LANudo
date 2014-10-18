using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LANudo
{
    public class Lista
    {
        List<Botao> botoesTodos = new List<Botao>();
        List<Botao> botoesDinamicos = new List<Botao>();
        Sprite setaSuperior;
        Sprite setaInferior;
        Botao botaoSuperior;
        Botao botaoInferior;
        List<ElementoLista> itens = new List<ElementoLista>();
        ElementoLista itemAtual = null; public ElementoLista ItemSelecionado { get { return itemAtual; } }
        Botao botaoAtual = null;
        int rolagem = 0;

        SpriteBatch desenhista;
        SpriteFont fonte;
        Texture2D fundo;
        Texture2D fundoMouse;
        Texture2D seta;
        Texture2D fundoSeta;
        int capacidade;
        float escala;
        Vector2 posicao;
        bool temSetas;
        bool vertical;
        public enum TipoEvento { SelecionavelIntermanete, SelecionavelExternamente, Setado };
        TipoEvento tipo;
        bool dropDown;
        float escalaTexto;
        float escalaSetinha;
        EsquemaCores coresSeta;
        EsquemaCores coresVazio;
        EsquemaCores coresInclicavel;
        EsquemaCores coresSelecionado;
        EsquemaCores coresDeselecionado;

        public List<ElementoLista> Itens
        {
            get { return itens; }
            set
            {
                itens = value;
                InicializaItens();
            }
        }

        bool ativo;

        public bool Ativado() { return ativo; }

        public void Ativar() { ativo = true; }

        public void Desativar() { ativo = false; }

        public Lista(SpriteBatch _desenhista, SpriteFont _fonte, List<ElementoLista> _elementos, TipoEvento _selecionavel, Texture2D _fundo, Texture2D _fundoMouseOver, Texture2D _fundoSeta, Texture2D _seta, EsquemaCores _coresSeta, EsquemaCores _coresVazio, EsquemaCores _coresInclicavel, EsquemaCores _coresSelecionado, EsquemaCores _coresDeselecionado, Vector2 _posicao, float _escala, int _capacidade, float _escalaTexto, float _escalaSetinha, bool _dropDown = true, bool _vertical = true, bool _temSetas = true)
        {
            desenhista = _desenhista;
            fonte = _fonte;
            itens = _elementos;

            fundo = _fundo;
            fundoMouse = _fundoMouseOver;
            fundoSeta = _fundoSeta;
            seta = _seta;

            dropDown = _dropDown;
            vertical = _vertical;
            tipo = _selecionavel;
            temSetas = _temSetas;

            coresDeselecionado = _coresDeselecionado;
            coresSelecionado = _coresSelecionado;
            coresVazio = _coresVazio;
            coresSeta = _coresSeta;
            coresInclicavel = _coresInclicavel;
            capacidade = _capacidade;
            posicao = _posicao;
            escala = _escala;
            escalaTexto = _escalaTexto;
            escalaSetinha = _escalaSetinha;

            InicializaBotoes();
            InicializaItens();

            ativo = false;
        }

        private void InicializaBotoes()
        {
            if (temSetas)
            {
                botaoSuperior = new Botao(desenhista, fundo, fundoMouse, coresSeta, new Vector2(10, 10), escala, false);
                botaoInferior = new Botao(desenhista, fundo, fundoMouse, coresSeta, new Vector2(10, 10), escala, false);
                botaoSuperior.Clicado += ClicouSobe;
                botaoInferior.Clicado += ClicouDesce;
                setaSuperior = new Sprite(desenhista, seta, new Vector3(10f, 10f, 0f), coresSeta.CorTexto);
                setaInferior = new Sprite(desenhista, seta, new Vector3(10f, 10f, 0f), coresSeta.CorTexto);
                setaInferior.Eff = SpriteEffects.FlipVertically;
                botaoInferior.MouseEmCima += this.SetaCorSetaInferiorMouse;
                botaoInferior.MouseEmVolta += this.SetaCorSetaInferiorDesel;
                botaoSuperior.MouseEmCima += this.SetaCorSetaSuperiorMouse;
                botaoSuperior.MouseEmVolta += this.SetaCorSetaSuperiorDesel;

                botoesTodos.Add(botaoSuperior);
            }
            for (int i = 1; i <= capacidade; i++)
            {
                Botao botao = new Botao(desenhista, fundo, fundoMouse, coresVazio, new Vector2(10, 10), escala, fonte, " ", escalaTexto, false);
                botao.OcultaTexto();
                botao.DesativarSobreMouse();
                botoesTodos.Add(botao);
                botoesDinamicos.Add(botao);
            }
            if (temSetas)
            {
                botoesTodos.Add(botaoInferior);
            }
        }
        public void InicializaItens()
        {
            Rolagem(rolagem);
            if (itens.Count <= botoesDinamicos.Count) { pontoAtual = Ponto.Travado; } else { pontoAtual = Ponto.Inicio; }
            SetaSetas();
        }

        enum Ponto { Inicio, Meio, Fim, Travado };
        Ponto pontoAtual;
        Ponto pontoAnterior = Ponto.Fim;
        Ponto Rolagem(int scroll)
        {
            if (pontoAtual != Ponto.Travado)
            {
                bool semMais = false;
                int contador = scroll;
                foreach (Botao botao in botoesDinamicos)
                {
                    if (semMais)
                    {
                        EsvaziaBotao(botao);
                    }
                    else
                    {
                        ElementoLista item = null;

                        try { item = itens.ElementAt(contador); }
                        catch (Exception erro)
                        { semMais = true; }
                        if (item == null)
                        {
                            EsvaziaBotao(botao);
                        }
                        else
                        {
                            SetaBotao(botao, item);
                        }

                        try { itens.ElementAt(contador + 1); }
                        catch (Exception erro)
                        { return Ponto.Fim; }
                        contador++;
                    }
                }
                if (scroll > 0) { return Ponto.Meio; } else { return Ponto.Inicio; }
            }
            return Ponto.Travado;
        }

        private void EsvaziaBotao(Botao botao)
        {
            botao.Cores = coresVazio;
            botao.DesativarSobreMouse();
            botao.OcultaTexto();
            botao.ZeraEventos();
        }

        private void SetaBotao(Botao botao, ElementoLista item)
        {
            botao.Cores = item.CoresDesel;
            if (item.Rotulo != null)
            {
                botao.Rotulo = item.Rotulo; botao.MostraTexto();
            }
            switch (tipo)
            {
                case TipoEvento.Setado:
                    botao.Clicado += item.Clicado;
                    botao.AtivarSobreMouse();
                    break;
                case TipoEvento.SelecionavelExternamente:
                    botao.DesativarSobreMouse();
                    botao.ZeraEventos();
                    break;
                case TipoEvento.SelecionavelIntermanete:
                    botao.AtivarSobreMouse();
                    botao.Clicado += Clicou;
                    break;
            }
        }

        private void Clicou(Botao botao)
        {
            foreach (ElementoLista item in itens)
            {
                if (item.Rotulo == botao.Rotulo)
                {
                    if (botaoAtual != null) { DeselecionaBotao(botaoAtual); }
                    SelecionaBotao(botao);
                    itemAtual = item;
                    botaoAtual = botao;
                }
            }
        }

        private void SelecionaBotao(Botao botao)
        {
            if (botao != null) { botao.Cores = coresSelecionado; botao.DesativarSobreMouse(); }
        }
        private void DeselecionaBotao(Botao botao)
        {
            if (botao != null) { botao.Cores = coresDeselecionado; botao.AtivarSobreMouse(); }
        }
        private void AtualizaSelecao()
        {
            if (itemAtual != null)
            {
                if (botaoAtual != null) { DeselecionaBotao(botaoAtual); }
                foreach (Botao botao in botoesDinamicos)
                {
                    if (itemAtual.Rotulo == botao.Rotulo)
                    {
                        botaoAtual = botao;
                        SelecionaBotao(botao);
                        return;
                    }
                }

            }
            botaoAtual = null;
        }

        private void ClicouSobe(Botao remetente)
        {
            ScrollaPraCima();
        }

        private void ClicouDesce(Botao remetente)
        {
            ScrollaPraBaixo();
        }

        private void ScrollaPraCima()
        {
            if (pontoAtual != Ponto.Travado && pontoAtual != Ponto.Inicio)
            {
                rolagem--;
                pontoAtual = Rolagem(rolagem);
                SetaSetas();
                AtualizaSelecao();
            }
        }


        private void ScrollaPraBaixo()
        {
            if (pontoAtual != Ponto.Travado && pontoAtual != Ponto.Fim)
            {
                rolagem++;
                pontoAtual = Rolagem(rolagem);
                SetaSetas();
                AtualizaSelecao();
            }
        }


        private void SetaSetas()
        {
            if (temSetas && pontoAtual != pontoAnterior)
            {
                switch (pontoAtual)
                {
                    case Ponto.Inicio:
                        botaoSuperior.Cores = coresInclicavel; botaoSuperior.DesativarSobreMouse();
                        botaoInferior.Cores = coresSeta; botaoInferior.AtivarSobreMouse();
                        break;
                    case Ponto.Meio:
                        botaoSuperior.Cores = coresSeta; botaoSuperior.AtivarSobreMouse();
                        botaoInferior.Cores = coresSeta; botaoInferior.AtivarSobreMouse();
                        break;
                    case Ponto.Fim:
                        botaoSuperior.Cores = coresSeta; botaoSuperior.AtivarSobreMouse();
                        botaoInferior.Cores = coresInclicavel; botaoInferior.DesativarSobreMouse();
                        break;
                    case Ponto.Travado:
                        botaoSuperior.Cores = coresInclicavel; botaoSuperior.DesativarSobreMouse();
                        botaoInferior.Cores = coresInclicavel; botaoInferior.DesativarSobreMouse();
                        break;
                }
                pontoAnterior = pontoAtual;
            }
        }
        public void Redimensionado()
        {
            float tamanho = 0f, posX = posicao.X, posY = posicao.Y;
            bool first = true;
            int contador = 1;
            foreach (Botao botao in botoesTodos)
            {
                if (botoesTodos.Count > 1)
                {
                    botao.Movimentou();
                    if (vertical)
                    {
                        if (first)
                        {
                            tamanho = Recursos.RegraDeTres(Motor.Altura, 1, (botao.Cantos.Bottom - botao.Cantos.Top));

                            if (dropDown)
                            {
                                posY -= tamanho / 2;
                            }
                            else
                            {
                                posY -= ((botoesTodos.Count * tamanho) / 2) - (tamanho / 2);
                            }
                        }
                        else
                        {
                            posY += tamanho;
                        }
                    }
                    else
                    {
                        if (first)
                        {
                            tamanho = Recursos.RegraDeTres(Motor.Largura, 1, (botao.Cantos.Right - botao.Cantos.Left));
                            posX -= ((botoesTodos.Count * tamanho) / 2) - (tamanho / 2);

                        }
                        else
                        {
                            posX += tamanho;
                        }
                    }
                }
                botao.Posicao = new Vector2(posX, posY);
                if (first)
                {
                    setaSuperior.PosRel = new Vector3(posX, posY, escala * escalaSetinha);
                    first = false;
                }
                else
                {
                    if (contador == capacidade + 2)
                    {
                        setaInferior.PosRel = new Vector3(posX, posY, escala * escalaSetinha);
                    }
                }
                contador++;
            }

        }

        void SetaCorSetaInferiorMouse(Botao botao)
        {
            setaInferior.Cor = coresSeta.CorTextoMouse;
        }
        void SetaCorSetaInferiorDesel(Botao botao)
        {
            setaInferior.Cor = coresSeta.CorTexto;
        }

        void SetaCorSetaSuperiorMouse(Botao botao)
        {
            setaSuperior.Cor = coresSeta.CorTextoMouse;
        }
        void SetaCorSetaSuperiorDesel(Botao botao)
        {
            setaSuperior.Cor = coresSeta.CorTexto;
        }

        public void Atualizar()
        {
            if (ativo)
            {
                foreach (Botao botao in botoesTodos)
                {
                    botao.Atualizar();
                }
            }
        }

        public void Desenhar()
        {
            if (ativo)
            {
                foreach (Botao botao in botoesTodos)
                {
                    botao.Desenhar();
                }
                setaSuperior.Desenhar();
                setaInferior.Desenhar();
            }
        }

    }
}
