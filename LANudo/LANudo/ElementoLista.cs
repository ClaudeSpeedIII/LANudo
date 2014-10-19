using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Idioma;

namespace LANudo
{
    public class ElementoLista
    {
        string rotulo; public string Rotulo { get { return rotulo; } }
        bool xml; public bool XML { get { return xml; } }
        object carga; public object Payload { get { return carga; } }
        ManipuladorBotao evento; public ManipuladorBotao Clicado { get { return evento; } }
        EsquemaCores esquemaSel; public EsquemaCores CoresSel { get { return esquemaSel; } }
        EsquemaCores esquemaDesel; public EsquemaCores CoresDesel { get { return esquemaDesel; } }
        Action fazer;

        public static List<ElementoLista> CriaVariosElementoLista(List<Vector2> resolucoes, EsquemaCores coresDesel, EsquemaCores coresSel)
        {
            List<ElementoLista> listaRetornada = new List<ElementoLista>();

            listaRetornada.Add(
            new ElementoLista(
                "WINDOWED", true, coresDesel, coresSel,
                ((object)new Vector3(Configuracoes.Resolucao.X, Configuracoes.Resolucao.Y, 1f))
                )
            );
            foreach (Vector2 res in resolucoes)
            {
                Vector3 resW = new Vector3(res.X, res.Y, 0f);
                if (res.X >= Constantes.resolucao_minima_x())
                {
                    if (res.Y >= Constantes.resolucao_minima_y())
                    {
                        listaRetornada.Add(new ElementoLista((res.X + " x " + res.Y), false, coresDesel, coresSel, (object)resW));
                    }
                }


            }
            return listaRetornada;
        }
        public static List<ElementoLista> CriaVariosElementoLista(List<string> rotulos, bool usaXML, EsquemaCores coresDesel, EsquemaCores coresSel)
        {
            List<ElementoLista> listaRetornada = new List<ElementoLista>();
            foreach (string rotulo in rotulos)
            {
                listaRetornada.Add(new ElementoLista((rotulo), usaXML, coresDesel, coresSel));
            }
            return listaRetornada;
        }
        public static List<ElementoLista> CriaVariosElementoLista(List<Textos> idiomas, EsquemaCores coresDesel, EsquemaCores coresSel)
        {
            List<ElementoLista> listaRetornada = new List<ElementoLista>();
            foreach (Textos idioma in idiomas)
            {
                listaRetornada.Add(new ElementoLista(idioma.Rotulo, false, coresDesel, coresSel, (object)idioma.ISO));
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


        public ElementoLista(ManipuladorBotao fazer, EsquemaCores coresDesel, EsquemaCores coresSel, object payload)
        {
            this.rotulo = null;
            this.carga = payload;
            evento = fazer;
            esquemaSel = coresSel;
            esquemaDesel = coresDesel;
        }
        public ElementoLista(Action fazer, EsquemaCores coresDesel, EsquemaCores coresSel, object payload)
        {
            this.rotulo = null;
            this.carga = payload;
            evento += Faz;
            this.fazer = fazer;
            esquemaSel = coresSel;
            esquemaDesel = coresDesel;
        }
        public ElementoLista(EsquemaCores coresDesel, EsquemaCores coresSel, object payload)
        {
            this.rotulo = null;
            this.carga = payload;
            esquemaSel = coresSel;
            esquemaDesel = coresDesel;
        }

        public ElementoLista(string rotulo, bool _xml, ManipuladorBotao fazer, EsquemaCores coresDesel, EsquemaCores coresSel, object payload)
        {
            this.rotulo = rotulo;
            this.xml = _xml;
            this.carga = payload;
            evento = fazer;
            esquemaSel = coresSel;
            esquemaDesel = coresDesel;
        }
        public ElementoLista(string rotulo, bool _xml, Action fazer, EsquemaCores coresDesel, EsquemaCores coresSel, object payload)
        {
            this.rotulo = rotulo;
            this.xml = _xml;
            this.carga = payload;
            evento += Faz;
            this.fazer = fazer;
            esquemaSel = coresSel;
            esquemaDesel = coresDesel;
        }
        public ElementoLista(string rotulo, bool _xml, EsquemaCores coresDesel, EsquemaCores coresSel, object payload)
        {
            this.rotulo = rotulo;
            this.xml = _xml;
            this.carga = payload;
            esquemaSel = coresSel;
            esquemaDesel = coresDesel;
        }

        public ElementoLista(string rotulo, bool _xml, ManipuladorBotao fazer, EsquemaCores coresDesel, object payload)
        {
            this.rotulo = rotulo;
            this.xml = _xml;
            this.carga = payload;
            evento = fazer;
            esquemaDesel = coresDesel;
        }
        public ElementoLista(string rotulo, bool _xml, Action fazer, EsquemaCores coresDesel, object payload)
        {
            this.rotulo = rotulo;
            this.xml = _xml;
            this.carga = payload;
            evento += Faz;
            this.fazer = fazer;
            esquemaDesel = coresDesel;
            esquemaSel = coresDesel;
        }
        public ElementoLista(string rotulo, bool _xml, EsquemaCores coresDesel, object payload)
        {
            this.rotulo = rotulo;
            this.xml = _xml;
            this.carga = payload;
            esquemaDesel = coresDesel;
            esquemaSel = coresDesel;
        }
        public ElementoLista(string rotulo, bool _xml, Action fazer, object payload)
        {
            this.rotulo = rotulo;
            this.xml = _xml;
            this.carga = payload;
            evento += Faz;
            this.fazer = fazer;
        }
        public ElementoLista(string rotulo, bool _xml, ManipuladorBotao fazer, object payload)
        {
            this.carga = payload;
            this.rotulo = rotulo;
            this.xml = _xml;
            this.evento = fazer;
        }

        public ElementoLista(string rotulo, bool _xml, object payload)
        {
            this.carga = payload;
            this.rotulo = rotulo;
            this.xml = _xml;
        }



        public ElementoLista(ManipuladorBotao fazer, EsquemaCores coresDesel, EsquemaCores coresSel)
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

        public ElementoLista(string rotulo, bool _xml, ManipuladorBotao fazer, EsquemaCores coresDesel, EsquemaCores coresSel)
        {
            this.rotulo = rotulo;
            this.xml = _xml;
            evento = fazer;
            esquemaSel = coresSel;
            esquemaDesel = coresDesel;
        }
        public ElementoLista(string rotulo, bool _xml, Action fazer, EsquemaCores coresDesel, EsquemaCores coresSel)
        {
            this.rotulo = rotulo;
            this.xml = _xml;
            evento += Faz;
            this.fazer = fazer;
            esquemaSel = coresSel;
            esquemaDesel = coresDesel;
        }
        public ElementoLista(string rotulo, bool _xml, EsquemaCores coresDesel, EsquemaCores coresSel)
        {
            this.rotulo = rotulo;
            this.xml = _xml;
            esquemaSel = coresSel;
            esquemaDesel = coresDesel;
        }

        public ElementoLista(string rotulo, bool _xml, ManipuladorBotao fazer, EsquemaCores coresDesel)
        {
            this.rotulo = rotulo;
            this.xml = _xml;
            evento = fazer;
            esquemaDesel = coresDesel;
        }
        public ElementoLista(string rotulo, bool _xml, Action fazer, EsquemaCores coresDesel)
        {
            this.rotulo = rotulo;
            this.xml = _xml;
            evento += Faz;
            this.fazer = fazer;
            esquemaDesel = coresDesel;
            esquemaSel = coresDesel;
        }
        public ElementoLista(string rotulo, bool _xml, EsquemaCores coresDesel)
        {
            this.rotulo = rotulo;
            this.xml = _xml;
            esquemaDesel = coresDesel;
            esquemaSel = coresDesel;
        }
        public ElementoLista(string rotulo, bool _xml, Action fazer)
        {
            this.rotulo = rotulo;
            this.xml = _xml;
            evento += Faz;
            this.fazer = fazer;
        }
        public ElementoLista(string rotulo, bool _xml, ManipuladorBotao fazer)
        {
            this.rotulo = rotulo;
            this.xml = _xml;
            this.evento = fazer;
        }

        public ElementoLista(string rotulo, bool _xml)
        {
            this.rotulo = rotulo;
            this.xml = _xml;
        }

        void Faz(Botao botao) { fazer(); }
    }
}
