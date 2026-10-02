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

        // Constructeur
        public Tir(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawImage(Resources.playerShoot, x, y, 70, 70);
        }
    }
}
