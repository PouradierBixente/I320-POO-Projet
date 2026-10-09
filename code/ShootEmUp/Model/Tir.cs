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
        public bool explode = false;
        public bool outofspace => y < -Config.SHOOT_SIZE || y > GameSpace.HEIGHT;

        // Constructeur
        public Tir(int x, int y, int type)
        {
            this.x = x;
            this.y = y;
            this.type = type;
        }

        public void Update()
        {
            if (type == 1)
                y -= 5;
            if (type == 2)
                y += 5;
        }
        public void Render(BufferedGraphics drawingSpace)
        {
            if(type == 1)
                drawingSpace.Graphics.DrawImage(Resources.playerShoot, x- Config.SHOOT_SIZE/2, y - Config.SHOOT_SIZE / 2, Config.SHOOT_SIZE, Config.SHOOT_SIZE);
            if (type == 2)
                drawingSpace.Graphics.DrawImage(Resources.TirEnnemie, x- Config.SHOOT_SIZE / 2, y - Config.SHOOT_SIZE / 2, Config.SHOOT_SIZE, Config.SHOOT_SIZE);
            if (explode)
                drawingSpace.Graphics.DrawImage(Resources.ExplosionTir, x - Config.SHOOT_SIZE, y - Config.SHOOT_SIZE , Config.SHOOT_SIZE * 2, Config.SHOOT_SIZE * 2);
        }
    }
}
