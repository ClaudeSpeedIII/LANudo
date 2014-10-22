using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LANudo
{
    public struct Peao
    {
        Casa.Jogadores dono; public Casa.Jogadores Dono { get { return dono; } }
        int quant; public int Quantidade { get { return quant; } }
        public Peao(Casa.Jogadores jogador, int quantidade)
        {
            dono = jogador;
            quant = quantidade;
        }
        public Peao(Casa.Jogadores jogador)
        {
            dono = jogador;
            quant = 0;
        }
    }
}
