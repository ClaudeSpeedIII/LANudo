﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LANudo
{
    public class Tabuleiro : Elemento
    {
        bool ativo, interativo = true;

        public bool EstaInterativo() { return interativo; }
        public void AtivaInterativo() { interativo = true; }
        public void DesativaInterativo() { interativo = false; }

        public bool Ativado() { return ativo; }
        public void Ativar() { ativo = true; }
        public void Desativar() { ativo = false; }

        List<Casa.Jogadores> jogadores;

        private CoresLudo cores; public CoresLudo Cores { get { return cores; } set { cores = value; } }
        private Vector3 posicao; public Vector3 Posicao { get { return posicao; } set { posicao = value; ReInstancia(); Redimensionado(); } }
        private float escalaIndividual;
        Vector2 posicaoPx;
        private bool direcao = true; public bool Direcao { get { return direcao; } set { direcao = value; } }
        private int inicio = -2; public int CasaInicio { get { return inicio; } set { inicio = value; } }
        private int fim = 0; public int CasaFim { get { return fim; } set { fim = value; } }
        private int tamanho = 10; public int TamanhoPista { get { return tamanho; } set { tamanho = value; tamanhoTotal = (32 + (value * 12)) - 1; } }

        private int rotacao = 0;
        public int RotacaoPista
        {
            get { return rotacao; }
            set
            {
                switch (value)
                {
                    case 0:
                    case 360:
                        jogadores = new List<Casa.Jogadores>();
                        jogadores.Add(Casa.Jogadores.P1); jogadores.Add(Casa.Jogadores.P2); jogadores.Add(Casa.Jogadores.P3); jogadores.Add(Casa.Jogadores.P4);
                        break;
                    case 90:
                        jogadores = new List<Casa.Jogadores>();
                        jogadores.Add(Casa.Jogadores.P4); jogadores.Add(Casa.Jogadores.P1); jogadores.Add(Casa.Jogadores.P2); jogadores.Add(Casa.Jogadores.P3);
                        break;
                    case 180:
                        jogadores = new List<Casa.Jogadores>();
                        jogadores.Add(Casa.Jogadores.P3); jogadores.Add(Casa.Jogadores.P4); jogadores.Add(Casa.Jogadores.P1); jogadores.Add(Casa.Jogadores.P2);
                        break;
                    case 270:
                        jogadores = new List<Casa.Jogadores>();
                        jogadores.Add(Casa.Jogadores.P2); jogadores.Add(Casa.Jogadores.P3); jogadores.Add(Casa.Jogadores.P4); jogadores.Add(Casa.Jogadores.P1);
                        break;
                }
                ReInstancia();
                Redimensionado();
            }
        }

        SpriteBatch desenhista;
        Texture2D peao;
        Texture2D tabuleiro;
        ParametrosCasa casaGaragem;
        ParametrosCasa casaSaida;
        ParametrosCasa casaPista;
        ParametrosCasa casaEntrada;
        ParametrosCasa casaFinal;
        ParametrosCasa casaChegada;

        public Tabuleiro(SpriteBatch _desenhista,
            Texture2D _peao,
            Texture2D _tabuleiro,
            CoresLudo _cores,
        ParametrosCasa _casaGaragem,
        ParametrosCasa _casaSaida,
        ParametrosCasa _casaPista,
        ParametrosCasa _casaEntrada,
        ParametrosCasa _casaFinal,
        ParametrosCasa _casaChegada,
            Vector3 _posicao,
        int _rotacao)
        {
            desenhista = _desenhista;
            tabuleiro = _tabuleiro;
            peao = _peao;
            casaGaragem = _casaGaragem;
            casaSaida = _casaSaida;
            casaPista = _casaPista;
            casaEntrada = _casaEntrada;
            casaFinal = _casaFinal;
            casaChegada = _casaChegada;
            posicao = _posicao;
            cores = _cores;
            RotacaoPista = _rotacao;
            tamanhoTotal = (32 + (tamanho * 12));
            ativo = true;
        }

        void ReInstancia()
        {
            escalaIndividual = posicao.Z / ((tamanho * 2) + 7);
            InicializaFundo();
            InicializaCentro();
            PosicionaCentro();
            InicializaPista();
            InicializaFinal();
            InicializaGaragem();

        }


        List<Casa> centro = new List<Casa>();

        void InicializaCentro()
        {
            foreach (Casa.Jogadores p in jogadores)
            {
                centro.Add(new Casa(desenhista, casaChegada, cores, p, new Vector3(posicao.X, posicao.Y, escalaIndividual * 3f)));
            }
        }

        void PosicionaCentro()
        {
            int rot = 0;
            foreach (Casa peca in centro)
            {
                peca.Base.Rot = rot;
                rot += 90;
            }
        }

        List<Casa> pista = new List<Casa>();

        void InicializaPista()
        {
            //bool first = true;
            int total = (tamanho * 2) + 3;
            int zero = ((int)(total / 2f)) + 1;
            foreach (Casa.Jogadores p in jogadores)
            {
                for (int i = 1; i <= total; i++)
                {
                    if (i == zero + inicio)
                    {
                        pista.Add(new Casa(desenhista, casaSaida, cores, p, new Vector3(posicao.X, posicao.Y, escalaIndividual), true));
                    }
                    else if (i == zero + fim)
                    {
                        pista.Add(new Casa(desenhista, casaEntrada, cores, p, new Vector3(posicao.X, posicao.Y, escalaIndividual), true));
                    }
                    else
                    {
                        pista.Add(new Casa(desenhista, casaPista, cores, Casa.Jogadores.Publico, new Vector3(posicao.X, posicao.Y, escalaIndividual), true));
                    }
                }
            }
        }

        void PosicionaPista()
        {
            int total = ((tamanho * 2) + 3);
            int count = 1;
            int direcao = 0;
            int rot = 0;
            Vector2 absolutoEscalado = pista.ElementAt(0).TamanhoAbs * pista.ElementAt(0).Base.EscalaAbs;
            Vector2 L = new Vector2(absolutoEscalado.X, absolutoEscalado.Y * 2);
            Vector2 ultimo = posicaoPx - L;
            foreach (Casa peca in pista)
            {
                if (count > total) { count = 1; rot += 90; direcao++; ultimo = (posicaoPx + Recursos.Direciona((Recursos.Direcao)direcao, L)); }
                if (count == 1) { peca.PosicaoAbs = ultimo; }
                else if (count <= (tamanho + 1)) { peca.PosicaoAbs = (ultimo += Recursos.Direciona((Recursos.Direcao)direcao, new Vector2(0f, absolutoEscalado.Y))); }
                else if (count > (tamanho + 1) && count <= (tamanho + 3)) { peca.PosicaoAbs = (ultimo -= Recursos.Direciona((Recursos.Direcao)direcao, new Vector2(absolutoEscalado.Y, 0f))); }
                else if (count >= tamanho + 3 && count <= total) { peca.PosicaoAbs = (ultimo -= Recursos.Direciona((Recursos.Direcao)direcao, new Vector2(0f, absolutoEscalado.Y))); }
                peca.Rotacao = rot;
                count++;
            }
        }

        List<Casa> final = new List<Casa>();

        void InicializaFinal()
        {
            //bool first = true;
            foreach (Casa.Jogadores p in jogadores)
            {
                for (int i = 1; i <= tamanho; i++)
                {
                    final.Add(new Casa(desenhista, casaFinal, cores, p, new Vector3(posicao.X, posicao.Y, escalaIndividual), true));
                }
            }
        }

        void PosicionaFinal()
        {
            int count = 1;
            int direcao = 0;
            int rot = 0;
            Vector2 absolutoEscalado = final.ElementAt(0).TamanhoAbs * final.ElementAt(0).Base.EscalaAbs;
            Vector2 I = new Vector2(0f, absolutoEscalado.Y * 2);
            Vector2 ultimo = posicaoPx - I;
            foreach (Casa peca in final)
            {
                if (count > tamanho) { count = 1; rot += 90; direcao++; ultimo = (posicaoPx + Recursos.Direciona((Recursos.Direcao)direcao, I)); }
                if (count == 1) { peca.PosicaoAbs = ultimo; }
                else if (count <= tamanho) { peca.PosicaoAbs = (ultimo += Recursos.Direciona((Recursos.Direcao)direcao, new Vector2(0f, absolutoEscalado.Y))); }
                peca.Rotacao = rot;
                count++;
            }
        }


        List<Casa> garagem = new List<Casa>();

        void InicializaGaragem()
        {
            //bool first = true;
            foreach (Casa.Jogadores p in jogadores)
            {
                for (int i = 0; i < 4; i++)
                {
                    Casa peca = new Casa(desenhista, casaGaragem, cores, p, new Vector3(posicao.X, posicao.Y, escalaIndividual * 1.5f), true);
                    peca.Base.Pivot = new Vector2(0f, 0f);
                    peca.Peoes = new Peao(p, 1);
                    garagem.Add(peca);
                }
            }
        }

        void PosicionaGaragem()
        {
            int count = 0;
            int direcao = 0;
            int subDirecao = 0;
            int rot = 0;
            Vector2 pistaAbs = pista.ElementAt(0).TamanhoAbs * pista.ElementAt(0).Base.EscalaAbs;
            Vector2 chegadaAbs = garagem.ElementAt(0).TamanhoAbs * garagem.ElementAt(0).Base.EscalaAbs;
            Vector2 canto = new Vector2(((pistaAbs.X * (tamanho + 2)) + (pistaAbs.X / 2)) - chegadaAbs.X, ((pistaAbs.Y * (tamanho + 2)) + (pistaAbs.Y / 2)) - chegadaAbs.Y);
            Vector2 ultimo = posicaoPx - canto;
            foreach (Casa peca in garagem)
            {
                if (count >= 4) { count = 0; subDirecao = 0; rot += 90; direcao++; ultimo = (posicaoPx + Recursos.Direciona((Recursos.Direcao)direcao, canto)); }
                if (count == 0) { peca.PosicaoAbs = ultimo; }
                else if (count < 4)
                {
                    peca.PosicaoAbs = (ultimo += Recursos.Direciona((Recursos.Direcao)direcao,
                        Recursos.Direciona((Recursos.Direcao)subDirecao++, new Vector2(-chegadaAbs.X, 0f)))
                        );
                    if (subDirecao > 4) { subDirecao = 0; }
                }
                peca.Rotacao = rot;
                count++;
            }
        }

        Sprite fundo;

        void InicializaFundo()
        {
            fundo = new Sprite(desenhista, tabuleiro, posicao);
        }

        void PosicionaFundo()
        {
            float escalado = (escalaIndividual) * ((tamanho * 2) + 7);
            fundo.PosRel = posicao;//new Vector3(posicao.X, posicao.Y, escalado);
        }

        int tamanhoTotal; public int QuantidadeLinear { get { return tamanhoTotal; } }

        public bool PreenchePeao(Peao[] lista)
        {
            if (lista.Length != tamanhoTotal)
            {
                return false;
            }
            else
            {
                List<Casa> todasAsCasas = new List<Casa>();
                todasAsCasas.AddRange(garagem);
                todasAsCasas.AddRange(pista);
                todasAsCasas.AddRange(final);
                todasAsCasas.AddRange(centro);
                int contador = 0;
                foreach (Casa tile in todasAsCasas)
                {
                    tile.Peoes = lista[contador];
                    contador++;
                }
            }
            AtualizaPeoes();
            return true;
        }

        List<Sprite> peoes = new List<Sprite>(); private Vector2 pivotPeao = new Vector2(0.5f, 0.75f); public Vector2 PivotPeao { get { return pivotPeao; } set { value = pivotPeao; AtualizaPeoes(); } }

        public void AtualizaPeoes()
        {
            peoes = new List<Sprite>();
            List<Casa> todasAsCasas = new List<Casa>();
            todasAsCasas.AddRange(final);
            todasAsCasas.AddRange(pista);
            int count = 0;
            Casa.Jogadores antigoDono = Casa.Jogadores.Publico;

            Vector2 temp = Vector2.Zero;

            float escalado = (Configuracoes.Largura > Configuracoes.Altura) ?
                Recursos.RegraDeTres(pista.ElementAt(0).Base.Img.Height, escalaIndividual, peao.Height) :
                Recursos.RegraDeTres(pista.ElementAt(0).Base.Img.Width, escalaIndividual, peao.Width);

            List<Sprite> peoesInvertidos = new List<Sprite>();
            foreach (Casa central in centro)
            {
                Color corPeca = cores.CorJogador(central.Peoes.Dono);
                bool first = true;
                Vector2 px = Vector2.Zero;

                for (int i = 0; i < central.Peoes.Quantidade; i++)
                {
                    Sprite spr = new Sprite(desenhista, peao, new Vector3(central.Base.PosRel.X, central.Base.PosRel.Y, escalado), corPeca, true, true);
                    Vector2 offsetChegada = new Vector2((spr.Img.Width * spr.EscalaAbs) * 0.65f, 0f);
                    spr.Pivot = pivotPeao;
                    if (first) { px = new Vector2((((spr.Img.Width * spr.EscalaAbs) / 3) * 3), ((spr.Img.Height * spr.EscalaAbs) / 3) * 3); first = false; }
                    else
                    {
                        px -= offsetChegada;
                    }
                    spr.PosPx += Recursos.Direciona((Recursos.Direcao)count, px);
                    if (count >= 3) { peoesInvertidos.Add(spr); } else { peoes.Add(spr); }
                }
                peoesInvertidos.Reverse();
                peoes.AddRange(peoesInvertidos);
                count++;
            }
            count = -1;
            antigoDono = Casa.Jogadores.Publico;
            foreach (Casa vaga in garagem)
            {
                Color corPeca = cores.CorJogador(vaga.Peoes.Dono);
                bool first = true;
                for (int i = 0; i < vaga.Peoes.Quantidade; i++)
                {
                    Sprite spr = new Sprite(desenhista, peao, new Vector3(vaga.Base.PosRel.X, vaga.Base.PosRel.Y, escalado), corPeca, true, true);
                    spr.Pivot = pivotPeao;
                    if (first) { first = false; } else { spr.PosPx -= new Vector2(0f, (spr.TamanhoAbs.Y * spr.EscalaAbs) / 8); }
                    if (antigoDono != vaga.Dono)
                    {
                        antigoDono = vaga.Dono;
                        count++;
                        if (count > 3) { count = 0; }
                    }
                    spr.PosPx -= Recursos.Direciona((Recursos.Direcao)count, (vaga.TamanhoAbs * vaga.Base.EscalaAbs) / 2);
                    temp += new Vector2((vaga.PosicaoAbs.X * vaga.Base.EscalaAbs) / 5, 0f);
                    peoes.Add(spr);
                }
            }
            foreach (Casa peca in todasAsCasas)
            {
                Color corPeca = cores.CorJogador(peca.Peoes.Dono);
                bool first = true;
                Vector2 px = Vector2.Zero;
                for (int i = 0; i < peca.Peoes.Quantidade; i++)
                {
                    Sprite spr = new Sprite(desenhista, peao, new Vector3(peca.Base.PosRel.X, peca.Base.PosRel.Y, escalado), corPeca, true, true);
                    spr.Pivot = pivotPeao;
                    if (first) { first = false; px = spr.PosPx; }
                    else
                    {
                        spr.PosPx = px -= new Vector2(0f, (spr.TamanhoAbs.Y * spr.EscalaAbs) / 8); ;
                    }
                    peoes.Add(spr);
                }
            }
        }

        public void Redimensionado()
        {
            posicaoPx = Recursos.RelTelaParaAbs(new Vector2(posicao.X, posicao.Y));
            foreach (Casa peca in centro) { peca.Redimensionado(); }

            foreach (Casa peca in pista) { peca.Redimensionado(); }
            foreach (Casa peca in final) { peca.Redimensionado(); }
            foreach (Casa peca in garagem) { peca.Redimensionado(); }
            try
            {
                PosicionaPista(); PosicionaFinal(); PosicionaGaragem(); PosicionaFundo();
            }
            catch (Exception) { }
            foreach (Casa peca in pista) { peca.Redimensionado(); }
            foreach (Casa peca in final) { peca.Redimensionado(); }
            foreach (Casa peca in garagem) { peca.Redimensionado(); }

            AtualizaPeoes();
            foreach (Sprite peao in peoes) { peao.Redimensionado(); }
        }

        public void Atualizar()
        {
            if (ativo && interativo)
            {
                fundo.Atualizar();
                foreach (Casa peca in centro) { peca.Atualizar(); }
                foreach (Casa peca in pista) { peca.Atualizar(); }
                foreach (Casa peca in final) { peca.Atualizar(); }
                foreach (Casa peca in garagem) { peca.Atualizar(); }
                foreach (Sprite peao in peoes) { peao.Atualizar(); }
            }
        }

        public void Desenhar()
        {
            if (ativo)
            {
                fundo.Desenhar();
                foreach (Casa peca in centro) { peca.Desenhar(); }
                foreach (Casa peca in pista) { peca.Desenhar(); }
                foreach (Casa peca in final) { peca.Desenhar(); }
                foreach (Casa peca in garagem) { peca.Desenhar(); }
                foreach (Sprite peao in peoes) { peao.Desenhar(); }
            }
        }
    }
}