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
        Action fazer;
        
        public static HashSet<ElementoLista> CriaVariosElementoLista(HashSet<string> rotulos, EsquemaCores coresDesel, EsquemaCores coresSel)
        {
            HashSet<ElementoLista> listaRetornada = new HashSet<ElementoLista>();
            foreach (string rotulo in rotulos) { 
            }
            return listaRetornada;
        }

        public ElementoLista(string rotulo, ManipuladorClique fazer, EsquemaCores coresDesel, EsquemaCores coresSel)
        {
            this.rotulo = rotulo;
            evento = fazer;
            esquemaSel = coresSel;
            esquemaDesel = coresDesel;
        }
        public ElementoLista(string rotulo, Action fazer, EsquemaCores coresDesel, EsquemaCores coresSel)
        {
            this.rotulo = rotulo;
            evento += Faz;
            this.fazer = fazer;
            esquemaSel = coresSel;
            esquemaDesel = coresDesel;
        }
        public ElementoLista(string rotulo, EsquemaCores coresDesel, EsquemaCores coresSel)
        {
            this.rotulo = rotulo;
            esquemaSel = coresSel;
            esquemaDesel = coresDesel;
        }

        public ElementoLista(string rotulo, ManipuladorClique fazer, EsquemaCores coresDesel)
        {
            this.rotulo = rotulo;
            evento = fazer;
            esquemaDesel = coresDesel;
        }
        public ElementoLista(string rotulo, Action fazer, EsquemaCores coresDesel)
        {
            this.rotulo = rotulo;
            evento += Faz;
            this.fazer = fazer;
            esquemaDesel = coresDesel;
        }
        public ElementoLista(string rotulo, EsquemaCores coresDesel)
        {
            this.rotulo = rotulo;
            esquemaDesel = coresDesel;
        }
        public ElementoLista(string rotulo, Action fazer)
        {
            this.rotulo = rotulo;
            evento += Faz;
            this.fazer = fazer;
        }
        public ElementoLista(string rotulo, ManipuladorClique fazer)
        {
            this.rotulo = rotulo;
            this.evento = fazer;
        }

        public ElementoLista(string rotulo)
        {
        }

        void Faz(Botao botao) { fazer(); }
    }
}
