using ShootEmUp.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    public class Barriere
    {
        public int vie;
        public int x;
        public int y;

        // Constructeur
        public Barriere(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.vie = 5;
        }

        public void Render(BufferedGraphics drawingSpace)
        {
            if(vie == 5)
                drawingSpace.Graphics.DrawImage(Resources.Bariere, x - Config.BARRIERE_SIZE / 2, y - Config.BARRIERE_SIZE / 2, Config.BARRIERE_SIZE, Config.BARRIERE_SIZE);
            if (vie == 4)
                drawingSpace.Graphics.DrawImage(Resources.Barriere2, x - Config.BARRIERE_SIZE / 2, y - Config.BARRIERE_SIZE / 6, Config.BARRIERE_SIZE, Config.BARRIERE_SIZE / 3);
            if (vie == 3)
                drawingSpace.Graphics.DrawImage(Resources.Barriere3, x - Config.BARRIERE_SIZE / 2, y - Config.BARRIERE_SIZE / 6, Config.BARRIERE_SIZE, Config.BARRIERE_SIZE / 3);
            if (vie == 2)
                drawingSpace.Graphics.DrawImage(Resources.Barriere4, x - Config.BARRIERE_SIZE / 2, y - Config.BARRIERE_SIZE / 6, Config.BARRIERE_SIZE, Config.BARRIERE_SIZE / 3);
        }
    }
}
