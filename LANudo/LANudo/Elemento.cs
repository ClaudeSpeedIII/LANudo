using System;
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
    }
}
