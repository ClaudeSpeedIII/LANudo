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
        //Casa.Jogadores jogador;
        //Color cor;
        //int rotacao; 
        //Peao[] peoes; 


        //Na classe mestre: public enum JogadasPossiveis { Dado, Tira, Anda }
        void SetaFeedback(Action<Mestre.JogadasPossiveis> caminho); //chama só uma vez, pra dizer qual método
        void SetaID(Casa.Jogadores jogador);
        void SetaCor(Color cor);
        void SetaRotacao(int rotacao);
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
