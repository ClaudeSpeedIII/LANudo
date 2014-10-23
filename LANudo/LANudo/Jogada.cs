using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 Implementação possível
 */

namespace LANudo
{
    class Jogada
    {
        public bool completa;
        public Dado dado = new Dado();
        public Jogador jogador;
        public enum PossiveisResultados { IndagarUsuario, MoverPeca }
        //public Action<

        public void iniciaJogada(Jogador _jogador) 
        {
            jogador = _jogador; 
            completa = false; 
            dado.jogarDado(); 

            //Continua até precisar de resposta por parte do jogador
            switch (dado.resultado)
            {
                case 1:
                    //Nova peça na pista, casa 1
                    break; 
                case 2 - 5:
                    //Mover peça
                    //Qual? 
                    //Requer ação
                    break; 
                case 6: 
                    //Requer ação
                    break; 
            }
        }
    }
}
