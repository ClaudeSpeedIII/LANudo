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
        private Vector3 posicao; public Vector3 Posicao { get { return posicao; } set { posicao = value; posicaoPx = Recursos.RelTelaParaAbs(new Vector2(value.X, value.Y)); } }
        Vector2 posicaoPx;
        private bool direcao = true; public bool Direcao { get { return direcao; } set { direcao = value; } }
        private int inicio = -2; public int CasaInicio { get { return inicio; } set { inicio = value; } }
        private int fim = 0; public int CasaFim { get { return fim; } set { fim = value; } }
        private int tamanho = 5; public int TamanhoPista { get { return tamanho; } set { tamanho = value; } }

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
                Redimensionado();
            }
        }

        SpriteBatch desenhista;
        ParametrosCasa casaGaragem;
        ParametrosCasa casaSaida;
        ParametrosCasa casaPista;
        ParametrosCasa casaEntrada;
        ParametrosCasa casaFinal;
        ParametrosCasa casaChegada;

        public Tabuleiro(SpriteBatch _desenhista,
            CoresLudo _cores,
        ParametrosCasa _casaGaragem,
        ParametrosCasa _casaSaida,
        ParametrosCasa _casaPista,
        ParametrosCasa _casaEntrada,
        ParametrosCasa _casaFinal,
        ParametrosCasa _casaChegada,
            Vector3 _posicao)
        {
            desenhista = _desenhista;
            casaGaragem = _casaGaragem;
            casaSaida = _casaSaida;
            casaPista = _casaPista;
            casaEntrada = _casaEntrada;
            casaFinal = _casaFinal;
            casaChegada = _casaChegada;
            Posicao = _posicao;
            cores = _cores;
            RotacaoPista = 0;

            InicializaCentro();
            PosicionaCentro();
            InicializaPista();
            PosicionaPista();
            //    PosicionaCentro();

        }

        List<Casa> centro = new List<Casa>();

        void InicializaCentro()
        {
            foreach (Casa.Jogadores p in jogadores)
            {
                centro.Add(new Casa(desenhista, casaChegada, cores, p, posicao));
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

        HashSet<Casa> pista = new HashSet<Casa>();

        void InicializaPista()
        {
            //bool first = true;
            int total = (tamanho * 2) + 3;
            int zero = (int)(total / 2f);
            foreach (Casa.Jogadores p in jogadores)
            {
                for (int i = 1; i < total; i++)
                {
                    if (i == zero + inicio)
                    {
                        pista.Add(new Casa(desenhista, casaSaida, cores, p, new Vector3(posicao.X, posicao.Y, posicao.Z / 3), true));
                    }
                    else if (i == zero + fim)
                    {
                        pista.Add(new Casa(desenhista, casaEntrada, cores, p, new Vector3(posicao.X, posicao.Y, posicao.Z / 3), true));
                    }
                    else
                    {
                        pista.Add(new Casa(desenhista, casaPista, cores, Casa.Jogadores.Publico, new Vector3(posicao.X, posicao.Y, posicao.Z / 3), true));
                    }
                }

            }
        }

        void PosicionaPista()
        {
            int count = 0;
            int direcao = 0;
            Vector2 absolutoEscalado = pista.ElementAt(0).TamanhoAbs * pista.ElementAt(0).Base.EscalaAbs;
            Vector2 L = new Vector2(absolutoEscalado.X, absolutoEscalado.Y*2);
            Vector2 ultimo = posicaoPx - L;
            foreach (Casa peca in pista)
            {
                if (count >= (tamanho + 1 + tamanho)) { count = 0; direcao++; ultimo = (posicaoPx + Recursos.Direciona((Recursos.Direcao)direcao, L)); }
                if (count == 0) { peca.PosicaoAbs = ultimo; }
                else if (count < tamanho) { peca.PosicaoAbs = (ultimo += Recursos.Direciona((Recursos.Direcao)direcao, new Vector2(0f, absolutoEscalado.Y))); }
                else if (count >= tamanho && count < tamanho + 2) { peca.PosicaoAbs = (ultimo -= Recursos.Direciona((Recursos.Direcao)direcao, new Vector2(absolutoEscalado.Y, 0f))); }
                else if (count >= (tamanho + 2) && count < (tamanho + 1 + tamanho)) { peca.PosicaoAbs = (ultimo -= Recursos.Direciona((Recursos.Direcao)direcao, new Vector2(0f, absolutoEscalado.Y))); }
                count++;
            }
        }

        List<Casa> final = new List<Casa>();

        void InicializaFinal()
        {
            //bool first = true;
            foreach (Casa.Jogadores p in jogadores)
            {
                for (int i = 0; i < tamanho; i++)
                {
                    final.Add(new Casa(desenhista, casaPista, cores, p, new Vector3(posicao.X, posicao.Y, posicao.Z / 3), true));
                }
            }
        }

        void PosicionaFinal()
        {
            int count = 0;
            Vector2 pos = new Vector2(posicao.X, posicao.Y);
            foreach (Casa peca in final)
            {

            }
        }

        public void Redimensionado()
        {
            Posicao = posicao;
            foreach (Casa peca in centro) { peca.Redimensionado(); }
            foreach (Casa peca in pista) { peca.Redimensionado(); }
            try { PosicionaPista(); }
            catch (Exception) { }
            foreach (Casa peca in pista) { peca.Redimensionado(); }
        }

        public void Atualizar()
        {
            if (ativo && interativo)
            {
                foreach (Casa peca in centro) { peca.Atualizar(); }
                foreach (Casa peca in pista) { peca.Atualizar(); }
            }
        }

        public void Desenhar()
        {
            if (ativo)
            {
                foreach (Casa peca in centro) { peca.Desenhar(); }
                foreach (Casa peca in pista) { peca.Desenhar(); }
            }
        }
    }
}