using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LANudo
{
    public class Mestre
    {
        bool ativo;

        public bool Ativado() { return ativo; }
        public void Ativar() { ativo = true; }
        public void Desativar() { ativo = false; }

        public enum Jogadas { Dado, Tira, Anda }

        CoresLudo cores;

        Jogador[] jogadores;
        int jogadorAtual;


        int tamanhoTab;
        int tamanhoLinear;
        Peao[] tabuleiroAtual;

        Random randomizador = new Random();

        public Mestre(Jogador[] todosJogadores, int tamanhoTabuleiro, CoresLudo esquemaCores)
        {
            jogadores = todosJogadores;
            tamanhoTab = tamanhoTabuleiro;
            cores = esquemaCores;
            tamanhoLinear = (32 + (tamanhoTabuleiro * 12)) - 1;
            tabuleiroAtual = new Peao[tamanhoLinear];
            //Você vai precisar rotacionar esse tabuleiro para cada cliente. as primeiras 16 posições são as garagens, sendo 4 para cada jogador, as proximas (tamanhoTabuleiro*8) é a pista, e depois disso você tem mais (tamanhoTabuleiro*4) para o trecho final (aquele que você vai e volta) e os ultimos 4 são para a chegada de cada jogador
        }

        void Voltou(Jogadas decisaoJogador)
        {
        }

        public void Atualizar()
        {
            if (ativo)
            {

            }
        }
    }
}
