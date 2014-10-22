using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace LANudo
{
    public struct CoresLudo
    {
        Color publico; public Color Publico { get { return publico; } }
        Color p1; public Color P1 { get { return p1; } }
        Color p2; public Color P2 { get { return p2; } }
        Color p3; public Color P3 { get { return p3; } }
        Color p4; public Color P4 { get { return p4; } }

        public CoresLudo(Color _P1, Color _P2, Color _P3, Color _P4, Color _publico)
        {
            publico = _publico;
            p1 = _P1;
            p2 = _P2;
            p3 = _P3;
            p4 = _P4;
        }

        public Color CorJogador(Casa.Jogadores jogador)
        {
            switch (jogador)
            {
                case Casa.Jogadores.P1: return p1;
                case Casa.Jogadores.P2: return p2;
                case Casa.Jogadores.P3: return p3;
                case Casa.Jogadores.P4: return p4;
            }
            return publico;
        }
    }
}
