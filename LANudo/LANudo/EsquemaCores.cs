using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace LANudo
{
   public struct EsquemaCores
    {
        Color corFundo; public Color CorFundo { get { return corFundo; } }
        Color corFundoMouse; public Color CorFundoMouse { get { return corFundoMouse; } }
        Color corTexto; public Color CorTexto { get { return corTexto; } }
        Color corTextoMouse; public Color CorTextoMouse { get { return corTextoMouse; } }


        public EsquemaCores(Color _corFundo, Color _corFundoMouse, Color _corTexto, Color _corTextoMouse)
        {
            corFundo = _corFundo;
            corFundoMouse = _corFundoMouse;
            corTexto = _corTexto;
            corTextoMouse = _corTextoMouse;
        }
        public EsquemaCores(Color _corFundo, Color _corFundoMouse)
        {
            corFundo = _corFundo;
            corFundoMouse = _corFundoMouse;
            corTexto = Color.White;
            corTextoMouse = Color.White;
        }
        public EsquemaCores(Color _corFundo)
        {
            corFundo = _corFundo;
            corFundoMouse = _corFundo;
            corTexto = Color.White;
            corTextoMouse = Color.White;
        }
    }
}
