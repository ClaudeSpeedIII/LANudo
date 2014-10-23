using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LANudo
{
    public class Jogo
    {
        SpriteBatch desenhista;
        private Tabuleiro tab;
        private Fundo toalha;
        List<Elemento> elementosEmJogo = new List<Elemento>();

        bool ativo, interativo = true;

        public bool EstaInterativo() { return interativo; }
        public void AtivaInterativo() { interativo = true; }
        public void DesativaInterativo() { interativo = false; }

        public bool Ativado() { return ativo; }
        public void Ativar() { ativo = true; }
        public void Desativar() { ativo = false; }

        Texture2D imgTabFundo;
        Texture2D imgTabCentro;
        Texture2D imgTabTile;
        Texture2D imgTabSeta;
        Texture2D imgPeao;

        public Jogo(SpriteBatch _desenhista,Texture2D imgTabFundo,
         Texture2D imgTabCentro,
         Texture2D imgTabTile,
         Texture2D imgTabSeta,
         Texture2D imgPeao)
        {
            this.imgTabFundo = imgTabFundo;
            this.imgTabCentro = imgTabCentro;
            this.imgTabTile = imgTabTile;
            this.imgTabSeta = imgTabSeta;
            this.imgPeao = imgPeao;
            desenhista = _desenhista;
            ativo = true;
            interativo = true;
        }

        public void NovoJogo()
        {

            //inicio só pra testes
            CoresLudo cores = new CoresLudo(
                Constantes.cor_P1(),
                Constantes.cor_P2(),
                Constantes.cor_P3(),
                Constantes.cor_P4(),
                Color.White
                );

            ParametrosCasa centro = new ParametrosCasa(imgTabCentro, Casa.Tipos.Chegada);
            ParametrosCasa final = new ParametrosCasa(imgTabTile, Casa.Tipos.Final);

            List<Texture2D> vetor = new List<Texture2D>();
            vetor.Add(imgTabTile);
            vetor.Add(imgTabSeta);
            ParametrosCasa entrada = new ParametrosCasa(vetor, Casa.Tipos.Entrada, -100);

            ParametrosCasa pista = new ParametrosCasa(imgTabTile, Casa.Tipos.Pista);
            ParametrosCasa saida = new ParametrosCasa(imgTabTile, Casa.Tipos.Saida);
            ParametrosCasa garagem = new ParametrosCasa(imgTabTile, Casa.Tipos.Garagem);


            tab = new Tabuleiro(desenhista, imgPeao, imgTabFundo, cores, garagem, saida, pista, entrada, final, centro, new Vector3(0.5f, 0.5f, 0.8f), 0);

            /*
            Peao[] peoes = new Peao[tab.QuantidadeLinear];
            for (int i = 0; i < tab.QuantidadeLinear; i++)
            {
                peoes[i] = new Peao(Casa.Jogadores.Publico, 0);
            }
            peoes[6] = new Peao(Casa.Jogadores.P2, 1);

            tab.PreenchePeao(peoes);
            */

            elementosEmJogo.Add(tab);
        }


        public void Redimensionado()
        {
           // foreach (Elemento e in elementosEmJogo) { e.Redimensionado(); }
            if (tab != null) { tab.Redimensionado(); }
        }

        public void Atualizar()
        {
            if (ativo && interativo)
            {
                //foreach (Elemento e in elementosEmJogo) { e.Atualizar(); }
                if (tab != null) { tab.Atualizar(); }
            }
        }

        public void Desenhar()
        {
            if (ativo)
            {
                //foreach (Elemento e in elementosEmJogo) { e.Desenhar(); }
                if (tab != null) { tab.Desenhar(); }
            }
        }

    }
}
