using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*******************************************************************
 * 
 *  Comentários e revisão por Pedro T. R. Pinheiro
 *  Código por Pedro L. N. Christensen
 *  Data: 23/10/2014
 *  
 *  Fuçações na tentativa de adaptar o fluxograma ao jogo e ao TCP
 * 
 *******************************************************************/

namespace LANudo
{
    public class Mestre
    {
        bool ativo;

        public bool Ativado() { return ativo; }
        public void Ativar() { ativo = true; }
        public void Desativar() { ativo = false; }

        public enum JogadasPossiveis { Dado, Tira, Anda }
        //public enum Jogadas { Jogada }
        public Jogada[] Jogadas; 

        CoresLudo cores;

        Jogador[] jogadores;
        Jogador jogadorAtual;
        int idVetorJogadorAtual; 


        int tamanhoTab;
        int tamanhoLinear;
        Peao[] tabuleiroAtual;

        Random randomizador = new Random();

        //Inicia o objeto mestre
        public Mestre(Jogador[] todosJogadores, int tamanhoTabuleiro, CoresLudo esquemaCores)
        {
            jogadores = todosJogadores;
            tamanhoTab = tamanhoTabuleiro;
            //Quero ver como vou distribuir as cores para cada jogador, nesse esquema confuso
            cores = esquemaCores;
            tamanhoLinear = (32 + (tamanhoTabuleiro * 12)) - 1;
            tabuleiroAtual = new Peao[tamanhoLinear];
            //Você vai precisar rotacionar esse tabuleiro para cada cliente. as primeiras 16 posições são as garagens, sendo 4 para cada jogador, as proximas (tamanhoTabuleiro*8) é a pista, e depois disso você tem mais (tamanhoTabuleiro*4) para o trecho final (aquele que você vai e volta) e os ultimos 4 são para a chegada de cada jogador
        }

        void Ciclo()
        {
            //Define o jogadorAtual
            if (idVetorJogadorAtual == null)
            {
                //Inicia o jogo
                idVetorJogadorAtual = 0;
            }
            else
            {
                if (idVetorJogadorAtual < 3)
                {
                    //Vai para o próximo jogador até chegar ao quarto (id 3)
                    idVetorJogadorAtual++;
                }
                else
                {
                    //Recomeça o ciclo
                    idVetorJogadorAtual = 0; 
                }
            }

            jogadorAtual = jogadores[idVetorJogadorAtual];
            int dadoRolado = randomizador.Next(1, 6);

            jogadorAtual.ResultadoDado(dadoRolado); 

            //Continua até precisar de resposta por parte do jogador
            switch (dadoRolado)
            {
                case 1:
                    //Nova peça na pista, casa 1
                    break;
                case 2 - 5:
                    //Mover peça
                    //Qual? 
                    //Requer ação
                    break;
                case 6:
                    //Requer ação
                    break;
            }

        }

        void TransferePeca()
        {
        }

        void MovePeca() 
        {

        }

        void Voltou(JogadasPossiveis decisaoJogador)
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
