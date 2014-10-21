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

        //Fonte
        public static string caminho_fonte() { return "Fonte"; }

        //Cursor
        public static string caminho_rato() { return "Cursor"; }
        public static string caminho_rato_apertado() { return "CursorApertado"; }

        //Splash

        //Menu Inicial
        public static string caminho_menu_logo() { return "MenuLogo"; }
        public static string caminho_menu_fundo() { return "MenuFundo"; }
        public static string caminho_menu_dado() { return "MenuDado"; }
        public static string caminho_menu_tabuleiro() { return "MenuTab"; }

        //Tabuleiro
        public static string caminho_tabuleiro_centro() { return "TabCentro"; }
        public static string caminho_tabuleiro_tile() { return "TabTile"; }
        public static string caminho_tabuleiro_seta() { return "TabSeta"; }


        //A organizar
        public static string caminho_botao() { return "Botao"; }
        public static string caminho_seta() { return "Seta"; }
        public static string caminho_splash_screen() { return "LogoIntro"; }
        public static string caminho_idiomas() { return "Locale/"; }

        //***Conf***

        public static int resolucao_x() { return 800; }
        public static int resolucao_y() { return 480; }
        public static bool janela() { return true; }
        public static int resolucao_minima_x() { return 640; }
        public static int resolucao_minima_y() { return 480; }

        public static string idioma_inicial() { return "auto"; } //Código ISO 639-1 de dois digitos, ou então "auto" pra usar o do sistema
        public static string idioma_emergencia() { return "en"; } //Código ISO 639-1 de dois digitos, auto não é permitido visto que é usado na falha do auto
        public static Color cor_P1() { return Color.Blue; }
        public static Color cor_P2() { return Color.Yellow; }
        public static Color cor_P3() { return Color.Green; }
        public static Color cor_P4() { return Color.Red; }

        //***GUI***

        //Fundos
        public static Color cor_de_fundo_Intro() { return Color.Gray; }
        public static Color cor_de_fundo_MenuInicial() { return Color.LightPink; }
        public static Color cor_de_fundo_MenuNovoJogo() { return Color.LightYellow; }
        public static Color cor_de_fundo_MenuConf() { return Color.LightBlue; }

        //Esquema Cores
        public static EsquemaCores esquema_cores_lista_deselecionada() { return new EsquemaCores(Color.DarkCyan, Color.Cyan); }
        public static EsquemaCores esquema_cores_lista_selecionada() { return new EsquemaCores(Color.DarkBlue, Color.DarkBlue); }
        public static EsquemaCores esquema_cores_lista_rotulo() { return new EsquemaCores(Color.Black, Color.White, Color.Silver, Color.White); }
        public static EsquemaCores esquema_cores_lista_seta() { return new EsquemaCores(Color.ForestGreen, Color.DarkGreen, Color.Blue, Color.Red); }
        public static EsquemaCores esquema_cores_lista_vazia() { return new EsquemaCores(Color.LightGray); }
        public static EsquemaCores esquema_cores_lista_inclicavel() { return new EsquemaCores(Color.LightGreen); }
        //Botao

        public static EsquemaCores esquema_cores_botao() { return new EsquemaCores(Color.Silver, Color.Cyan, Color.White, Color.Silver); }

        //Intro
        public static TimeSpan duracao_intro() { return new TimeSpan(0, 0, 2); }
        public static float escala_logo_intro() { return 0.8f; }

        //Tela Inicial
        public static Vector3 pos_logo_inicial() { return new Vector3(0.85f, 0.7f, 0.32f); }
        public static Vector3 pos_dado_esq_inicial() { return new Vector3(0.18f, 0.17f, 0.23f); }
        public static Vector3 pos_dado_dir_inicial() { return new Vector3(0.82f, 0.17f, 0.23f); }
        public static Vector3 pos_tab_inicial() { return new Vector3(0f, 1f, 0.45f); }

        public static Vector2 pivot_logo_inicial() { return new Vector2(0.8f, 0.5f); }
        public static Vector2 pivot_dado_esq_inicial() { return new Vector2(0.5f, 0.5f); }
        public static Vector2 pivot_dado_dir_inicial() { return new Vector2(0.5f, 0.5f); }
        public static Vector2 pivot_tab_inicial() { return new Vector2(0.3f, 0.7f); }


        public static Vector2 pos_menu_inicial() { return new Vector2(0.5f, 0.5f); }
        public static float escala_menu_inicial() { return 0.1f; }
        public static float escala_texto_menu_inicial() { return 0.35f; }
        public static float distancia_botoes_menu_inicial() { return 0.08f; }

        //Configuracoes
        public static Vector2 pos_conf_botao_voltar() { return new Vector2(0.2f, 0.85f); }
        public static Vector2 pos_lista_idiomas() { return new Vector2(0.5f, 0.2f); }
        public static Vector2 pos_lista_res() { return new Vector2(0.5f, 0.4f); }
        public static float escala_setinha_conf() { return 0.5f; }
        public static float escala_rotulo_conf() { return 0.4f; }
        public static float distancia_rotulo_conf() { return 0.4f; }
        public static float distancia_dropdown_conf() { return 0.08f; }
        public static float escala_elementos_conf() { return 0.08f; }
        public static float escala_texto_elementos_conf() { return 0.25f; }

        //Novo Jogo
        public static Vector2 pos_iniciar_botao_voltar() { return new Vector2(0.5f, 0.85f); }



        //***GAME***

    }
}
