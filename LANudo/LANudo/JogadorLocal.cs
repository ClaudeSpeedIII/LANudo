﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LANudo
{
    class JogadorLocal : Jogador
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
