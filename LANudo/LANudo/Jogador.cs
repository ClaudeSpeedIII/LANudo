using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
<<<<<<< HEAD
=======
using Microsoft.Xna.Framework.Graphics;
// //>>>>>>> 3ecbe8d89d8c2aa07b62081b74586dc46dc305ec
using Microsoft.Xna.Framework;

namespace LANudo
{
    public interface Jogador
    {
<<<<<<< HEAD
        void SetaID(Casa.Jogadores jogador);
        void SetaCor(Color cor);
        void SetaRotacao(int rotacao);
        void SetaFeedback(Action<Mestre.Jogadas> caminho); //chama só uma vez, pra dizer qual método
        void ResultadoDado(int resultado); //Invoca uma animação de dado e no final mostra o resultado;
        void AtualizaTabuleiro(Peao[] peoes);
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
>>>>>>> 3ecbe8d89d8c2aa07b62081b74586dc46dc305ec*/
    }
}
