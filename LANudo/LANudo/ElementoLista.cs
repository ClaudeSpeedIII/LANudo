using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace LANudo
{
    public class ElementoLista
    {
        string rotulo; public string Rotulo { get { return rotulo; } }
        ManipuladorClique evento; public ManipuladorClique Clicado { get { return evento; } }
        EsquemaCores esquemaSel; public EsquemaCores CoresSel { get { return esquemaSel; } }
        EsquemaCores esquemaDesel; public EsquemaCores CoresDesel { get { return esquemaDesel; } }

        public ElementoLista(string rotulo, ManipuladorClique fazer, EsquemaCores coresDesel, EsquemaCores coresSel)
        {
            this.rotulo = rotulo;
            evento = fazer;
            esquemaSel = coresSel;
            esquemaDesel = coresDesel;
        }
        public ElementoLista(string rotulo, ManipuladorClique fazer)
        {
            this.rotulo = rotulo;
            evento = fazer;
        }
    }
}
