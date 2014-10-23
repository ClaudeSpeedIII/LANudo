using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LANudo
{
    class JogadorRemoto : Jogador
    {
        int id();
        Color Cor();
        int Rotacao();
        int Vez();
        //void SuaVez(); 
        bool EhVez();
        Peao[] Peoes();
        int PeoesCasaFinal();
    }
}
