using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

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
                if (value == 0 || value == 90 || value == 180 || value == 270) { rotacao = value; }
                else if (value == 360) { rotacao = 0; }
            }
        }

        ParametrosCasa casaGaragem;
        ParametrosCasa casaSaida;
        ParametrosCasa casaPista;
        ParametrosCasa casaEntrada;
        ParametrosCasa casaFinal;
        ParametrosCasa casaChegada;

        public Tabuleiro(SpriteBatch _desenhista,
        ParametrosCasa _casaGaragem,
        ParametrosCasa _casaSaida,
        ParametrosCasa _casaPista,
        ParametrosCasa _casaEntrada,
        ParametrosCasa _casaFinal,
        ParametrosCasa _casaChegada){
            casaGaragem = _casaGaragem;
            casaSaida = _casaSaida;
            casaPista = _casaPista;
            casaEntrada = _casaEntrada;
            casaFinal = _casaFinal;
            casaChegada = _casaChegada;
        }

        public void Redimensionado()
        {

        }

        public void Atualizar()
        {
            if (ativo && interativo)
            {
            }
        }

        public void Desenhar()
        {
            if (ativo)
            {
            }
        }
    }
}
