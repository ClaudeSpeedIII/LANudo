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

        public static List<ElementoLista> CriaVariosElementoLista(List<string> rotulos, EsquemaCores coresDesel, EsquemaCores coresSel)
        {
            List<ElementoLista> listaRetornada = new List<ElementoLista>();
            foreach (string rotulo in rotulos)
            {
                listaRetornada.Add(new ElementoLista((rotulo), coresDesel, coresSel));
            }
            return listaRetornada;
        }

        public static List<ElementoLista> CriaVariosElementoLista(EsquemaCores[] coresDesel, EsquemaCores[] coresSel)
        {
            if (coresDesel.Length != coresSel.Length) { return null; }

            List<ElementoLista> listaRetornada = new List<ElementoLista>();
            for (int i = 0; i < coresSel.Length; i++)
            {
                listaRetornada.Add(new ElementoLista(coresSel[i], coresSel[i]));
            }
            return listaRetornada;
        }
        public ElementoLista(ManipuladorClique fazer, EsquemaCores coresDesel, EsquemaCores coresSel)
        {
            this.rotulo = null;
            evento = fazer;
            esquemaSel = coresSel;
            esquemaDesel = coresDesel;
        }
        public ElementoLista(Action fazer, EsquemaCores coresDesel, EsquemaCores coresSel)
        {
            this.rotulo = null;
            evento += Faz;
            this.fazer = fazer;
            esquemaSel = coresSel;
            esquemaDesel = coresDesel;
        }
        public ElementoLista(EsquemaCores coresDesel, EsquemaCores coresSel)
        {
            this.rotulo = null;
            esquemaSel = coresSel;
            esquemaDesel = coresDesel;
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
            esquemaSel = coresDesel;
        }
        public ElementoLista(string rotulo, EsquemaCores coresDesel)
        {
            this.rotulo = rotulo;
            esquemaDesel = coresDesel;
            esquemaSel = coresDesel;
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
            this.rotulo = rotulo;
        }

        void Faz(Botao botao) { fazer(); }
    }
}
