using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace LANudo
{
    public interface Jogador
    {
        void SetaID(Casa.Jogadores jogador);
        void SetaCor(Color cor);
        void SetaRotacao(int rotacao);
        void SetaFeedback(Action<Mestre.Jogadas> caminho); //chama só uma vez, pra dizer qual método
        void ResultadoDado(int resultado); //Invoca uma animação de dado e no final mostra o resultado;
        void AtualizaTabuleiro(Peao[] peoes);
        void SuaVez(); //Chama para pedir um feedback
        //public void SuaVez();
    }
}
