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
        public Player _player;


        public Tir(int x, int y)
        {
            this.y = _player.y - 100;
            this.x = _player.x;
        }



        public void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawImage(Resources.playerShoot, x, y, 70, 70);
        }
    }
}
