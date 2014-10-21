using System;
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
        private Vector3 posicao; public Vector3 Posicao { get { return posicao; } set { posicao = value; } }
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
        //    PosicionaCentro();

        }

        List<Casa> centro = new List<Casa>();

        void InicializaCentro()
        {
            foreach (Casa.Jogadores p in jogadores)
            {
                Casa tile;
                tile = new Casa(desenhista, casaChegada, cores, p, posicao, true);
                centro.Add(tile);

            }
        }

        void PosicionaCentro()
        {
            int rot = 0;
            int praOnde = 0;
            foreach (Casa peca in centro)
            {
                switch (praOnde)
                {
                    case 0:
                        peca.Base.Pivot = new Vector2(0.5f, 1f);
                        break;
                    case 1:
                        peca.Base.Rot = rot;
                        peca.Base.Pivot = new Vector2(0f, 0.5f);
                        break;
                    case 2:
                        peca.Base.Rot = rot;
                        peca.Base.Pivot = new Vector2(0.5f, 0f);
                        break;
                    case 3:
                        peca.Base.Rot = rot;
                        peca.Base.Pivot = new Vector2(1f, 0.5f);
                        break;
                }
                praOnde++;
                rot += 90;
            }
        }

        HashSet<Casa> pistaPrincipal = new HashSet<Casa>();

        public void Redimensionado()
        {
            Posicao = posicao;
            if (centro.Count == 4) { PosicionaCentro(); }
        }

        public void Atualizar()
        {
            if (ativo && interativo)
            {
                foreach (Casa peca in centro) { peca.Atualizar(); }
            }
        }

        public void Desenhar()
        {
            if (ativo)
            {
                foreach (Casa peca in centro) { peca.Desenhar(); }
            }
        }
    }
}
