using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace LANudo
{
    public struct ParametrosCasa
    {
        List<Texture2D> sprite; public List<Texture2D> Sprites { get { return sprite; } }public Texture2D Sprite { get { return sprite.ElementAt(0); } }
        Casa.Tipos tipo; public Casa.Tipos TipoCasa { get { return tipo; } }

        public ParametrosCasa(List<Texture2D> _sprites, Casa.Tipos _tipo)
        {
            sprite = _sprites;
            tipo = _tipo;
        }

        public ParametrosCasa(Texture2D _sprite, Casa.Tipos _tipo)
        {
            sprite = new List<Texture2D>();
            sprite.Add(_sprite);
            tipo = _tipo;
        }
    }
}
