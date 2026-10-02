using ShootEmUp.Helpers;
using ShootEmUp.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    public class Tir
    {
        private const int SPEED = 10;
        public int x;
        public int y;
        public int type;

        // Constructeur
        public Tir(int x, int y, int type)
        {
            this.x = x;
            this.y = y;
            this.type = type;
        }

        public void Render(BufferedGraphics drawingSpace)
        {
            if(type == 1)
                drawingSpace.Graphics.DrawImage(Resources.playerShoot, x, y, 70, 70);
            if (type == 2)
                drawingSpace.Graphics.DrawImage(Resources.TirEnnemie, x, y, 70, 70);
        }
    }
}
