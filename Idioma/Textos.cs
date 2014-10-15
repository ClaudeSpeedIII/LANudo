using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Idioma
{
    public class Textos
    {
        public Textos(TodosTextos aberto)
        {
            rotulo = aberto.Rotulo;
            iso = aberto.ISO;
            novoJogo = aberto.Novo_Jogo;
            config = aberto.Config;
            sair = aberto.Sair;
        }

        string rotulo; public string Rotulo { get { return rotulo; } }
        string iso; public string ISO { get { return iso; } }
        string novoJogo; public string NovoJogo { get { return novoJogo; } }
        string config; public string Config { get { return config; } }
        string sair; public string Sair { get { return sair; } }
    }
}
