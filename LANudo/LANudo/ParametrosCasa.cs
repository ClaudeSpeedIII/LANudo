using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace LANudo
{
    public struct ParametrosCasa
    {
        List<Texture2D> sprite; public List<Texture2D> Sprites { get { return sprite; } }
        Casa.Tipos tipo; public Casa.Tipos TipoCasa { get { return tipo; } }

        public ParametrosCasa(List<Texture2D> _sprites, Casa.Tipos _tipo)
        {
            sprite = _sprites;
            tipo = _tipo;
        }
    }
}
