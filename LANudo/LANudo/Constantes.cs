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
        public static string caminho_idiomas() { return "Locale/"; }

        //***Conf***

        public static int resolucao_x() { return 640; }
        public static int resolucao_y() { return 480; }
        public static bool janela() { return true; }

        public static string idioma_inicial() { return "auto"; } //Código ISO 639-1 de dois digitos, ou então "auto" pra usar o do sistema
        public static string idioma_emergencia() { return "en"; } //Código ISO 639-1 de dois digitos, auto não é permitido visto que é usado na falha do auto
        public static Color cor_P1() { return Color.Blue; }
        public static Color cor_P2() { return Color.Yellow; }
        public static Color cor_P3() { return Color.Green; }
        public static Color cor_P4() { return Color.Red; }

        //***GUI***

        //Fundos
        public static Color cor_de_fundo_Intro() { return Color.Black; }
        public static Color cor_de_fundo_MenuInicial() { return Color.LightPink; }
        public static Color cor_de_fundo_MenuNovoJogo() { return Color.LightBlue; }
        public static Color cor_de_fundo_MenuConf() { return Color.LightBlue; }

        //Esquema Cores
        public static EsquemaCores esquema_cores_lista_deselecionada() { return new EsquemaCores(Color.DarkCyan, Color.Cyan); }
        public static EsquemaCores esquema_cores_lista_selecionada() { return new EsquemaCores(Color.DarkBlue, Color.DarkBlue); }
        public static EsquemaCores esquema_cores_lista_seta() { return new EsquemaCores(Color.ForestGreen, Color.DarkGreen, Color.Blue, Color.Red); }
        public static EsquemaCores esquema_cores_lista_vazia() { return new EsquemaCores(Color.LightGray); }
        public static EsquemaCores esquema_cores_lista_inclicavel() { return new EsquemaCores(Color.LightGreen); }
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

        //Configuracoes
        public static Vector2 pos_botao_voltar(){return new Vector2(0.2f,0.85f);}
        public static Vector2 pos_lista_idiomas() { return new Vector2(0.5f, 0.2f); }
        public static Vector2 pos_lista_res() { return new Vector2(0.5f, 0.4f); }
        public static float escala_setinha_conf() { return 0.5f; }
        public static float escala_rotulo_conf() { return 1.5f; }
        public static float distancia_rotulo_conf() { return 0.15f; }
        public static float escala_elementos_conf() { return 0.1f; }
        public static float escala_texto_elementos_conf() { return 1f; }
        //***GAME***

    }
}
