using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace LANudo
{
    public struct CoresLudo
    {
        Color publico; public Color Publico { get { return publico; } }
        Color publicoAlt; public Color PublicoAlternativo { get { return publicoAlt; } }
        Color p1; public Color P1 { get { return p1; } }
        Color p1Alt; public Color P1Alternativo { get { return p1Alt; } }
        Color p2; public Color P2 { get { return p2; } }
        Color p2Alt; public Color P2Alternativo { get { return p2Alt; } }
        Color p3; public Color P3 { get { return p3; } }
        Color p3Alt; public Color P3Alternativo { get { return p3Alt; } }
        Color p4; public Color P4 { get { return p4; } }
        Color p4Alt; public Color P4Alternativo { get { return p4Alt; } }

        public CoresLudo(Color _P1, Color _P2, Color _P3, Color _P4, Color _publico, int diferenca)
        {
            publico = _publico;
            publicoAlt = new Color((byte)_publico.R - diferenca, (byte)_publico.G - diferenca, (byte)_publico.B - diferenca, (byte)_publico.A);
            p1 = _P1;
            p1Alt = new Color((byte)_P1.R - diferenca, (byte)_P1.G - diferenca, (byte)_P1.B - diferenca, (byte)_P1.A);
            p2 = _P2;
            p2Alt = new Color((byte)_P2.R - diferenca, (byte)_P2.G - diferenca, (byte)_P2.B - diferenca, (byte)_P2.A);
            p3 = _P3;
            p3Alt = new Color((byte)_P3.R - diferenca, (byte)_P3.G - diferenca, (byte)_P3.B - diferenca, (byte)_P3.A);
            p4 = _P4;
            p4Alt = new Color((byte)_P4.R - diferenca, (byte)_P4.G - diferenca, (byte)_P4.B - diferenca, (byte)_P4.A);
        }
    }
}
