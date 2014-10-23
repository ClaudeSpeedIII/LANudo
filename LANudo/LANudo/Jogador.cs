using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

//Comentários e revisão por Pedro T. R. Pinheiro / Código por Pedro L. N. Christensen

namespace LANudo
{
    public interface Jogador
    {
        Casa.Jogadores jogador;
        Color cor;
        int rotacao; 
        Peao[] peoes; 

        //Já fornecido ao chegar ao mestre
        void SetaID(Casa.Jogadores _jogador);
        void SetaCor(Color _cor);
        void SetaRotacao(int _rotacao);

        //Na classe mestre: public enum JogadasPossiveis { Dado, Tira, Anda }
        void SetaFeedback(Action<Mestre.JogadasPossiveis> caminho); //chama só uma vez, pra dizer qual método
        void ResultadoDado(int resultado); //Invoca uma animação de dado e no final mostra o resultado;
        void AtualizaTabuleiro(Peao[] _peoes);
        void SuaVez(); //Chama para pedir um feedback
        //public void SuaVez();
/*
        int id(); 
        Color Cor();
        int Rotacao();
        int Vez(); 
        //void SuaVez(); 
        bool EhVez();
        Peao[] Peoes();
        int PeoesCasaFinal();
        Action<int> Acoes(); 
*/
    }
}
