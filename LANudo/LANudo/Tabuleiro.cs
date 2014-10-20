using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LANudo
{
    public class Tabuleiro : Elemento
    {
        bool ativo, interativo=true;

        public bool EstaInterativo() { return interativo; }
        public void AtivaInterativo() { interativo = true; }
        public void DesativaInterativo() { interativo = false; }

        public bool Ativado() { return ativo; }
        public void Ativar() { ativo = true; }
        public void Desativar() { ativo = false; }
     
        public Tabuleiro()
        {}
   
        public void Redimensionado()
        {
        }

        public void Atualizar()
        {
            if (ativo && interativo)
            {
            }
        }

        public void Desenhar()
        {
            if (ativo)
            {
            }
        }
    }
}
