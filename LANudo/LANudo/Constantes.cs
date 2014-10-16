using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace LANudo
{
    public static class Constantes
    {
        //***Caminhos***
        public static string caminho_fonte() { return "Arial"; }
        public static string caminho_logo_menu() { return "LogoMenu"; }
        public static string caminho_rato() { return "Cursor"; }
        public static string caminho_rato_apertado() { return "CursorApertado"; }
        public static string caminho_botao() { return "Botao"; }
        public static string caminho_seta() { return "Seta"; }
        public static string caminho_logo_intro() { return "LogoIntro"; }
        public static string caminho_idiomas() { return "Línguas/"; }

        //***Conf***

        public static int resolucao_x() { return 800; }
        public static int resolucao_y() { return 600; }
        public static bool janela() { return true; }
        public static string idioma_inicial() { return "pt"; } //Código ISO 639-1 de dois digitos, ou então "auto" pra usar o do sistema
        public static string idioma_emergencia() { return "en"; } //Código ISO 639-1 de dois digitos, auto não é permitido visto que é usado na falha do auto
        public static Color cor_P1() { return Color.Blue; }
        public static Color cor_P2() { return Color.Yellow; }
        public static Color cor_P3() { return Color.Green; }
        public static Color cor_P4() { return Color.Red; }

        //***GUI***

        //Fundos
        public static Color cor_de_fundo_Intro() { return Color.Black; }
        public static Color cor_de_fundo_MenuInicial() { return Color.Silver; }
        public static Color cor_de_fundo_MenuNovoJogo() { return Color.Black; }
        public static Color cor_de_fundo_MenuConf() { return Color.Black; }

        //Esquema Cores
        public static EsquemaCores esquema_cores_lista_selecionada() { return new EsquemaCores(Color.Black, Color.Black, Color.Black, Color.Black); }
        public static Color cor_fundo_lista_seta() { return Color.Black; }
        public static Color cor_fundo_lista_selecionado() { return Color.Black; }
        public static Color cor_fundo_lista_deselecionado() { return Color.Black; }

        public static Color cor_texto_lista_seta() { return Color.Black; }
        public static Color cor_texto_lista_selecionado() { return Color.Black; }
        public static Color cor_texto_lista_deselecionado() { return Color.Black; }


        public static Color cor_fundo_lista_seta_mouse() { return Color.Black; }
        public static Color cor_fundo_lista_deselecionado_mouse() { return Color.Black; }

        public static Color cor_texto_lista_seta_mouse() { return Color.Black; }
        public static Color cor_texto_lista_deselecionado_mouse() { return Color.Black; }

        //Botao

        public static EsquemaCores esquema_cores_botao() { return new EsquemaCores(Color.Silver, Color.Cyan, Color.White, Color.Silver); }

        //Intro
        public static TimeSpan duracao_intro() { return new TimeSpan(0, 0, 2); }
        public static float escala_logo_intro() { return 0.8f; }

        //Tela Inicial
        public static Vector2 pos_logo_inicial() { return new Vector2(0.5f, 0.15f); }
        public static float escala_logo_inicial() { return 0.25f; }
        public static Vector2 pos_menu_inicial() { return new Vector2(0.5f, 0.6f); }
        public static float escala_menu_inicial() { return 0.1f; }
        public static float escala_texto_menu_inicial() { return 1f; }
        public static float distancia_botoes_menu_inicial() { return 0.08f; }

        //***GAME***

    }
}
