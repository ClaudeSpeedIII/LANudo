﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LANudo
{
    public interface Elemento
    {
        bool EstaInterativo();
        void AtivaInterativo();
        void DesativaInterativo();
        bool Ativado();
        void Desativar();
        void Ativar();
        void Redimensionado();
        void Atualizar();
        void Desenhar();
        /*
         PONTO DE PARTIDA PARA UM ELEMENTO
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
        */
    }
}
