using ShootEmUp.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    public class Ennemie
    {
        public const int COOLDOWN = 30;
        private const int SPEED = 10;
        public int x;
        public int y;
        public State state = State.RIGHT;
        public int timecooldown = COOLDOWN;
        public int vieplayer;


        public enum State { RIGHT, LEFT }

        // Constructeur
        public Ennemie(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.vieplayer = 2;
        }

        public void Update(int interval)
        {
            timecooldown--;
            if (timecooldown <= 0)
                shoot();
            int space = GameSpace.WIDTH - Config.ENNEMIE_SIZE;
            
            
            if(x >= space)
            {
                state = State.LEFT;
                y += 10;
            }
            if (x <= -5)
            {
                state = State.RIGHT;
                y += 10;
            }

            if (state == State.RIGHT)
            {
                x += 10;
            }

            if (state == State.LEFT)
            {
                x -= 10;
            }

        }

        public void Render(BufferedGraphics drawingSpace)
        {
            if (state == State.RIGHT)
                drawingSpace.Graphics.DrawImage(Resources.EnnmieMouvementDroite, x - Config.ENNEMIE_SIZE / 2, y - Config.ENNEMIE_SIZE / 2, Config.ENNEMIE_SIZE, Config.ENNEMIE_SIZE);
            
            if (state == State.LEFT)
                drawingSpace.Graphics.DrawImage(Resources.EnnemieMouvementGauche, x - Config.ENNEMIE_SIZE / 2, y - Config.ENNEMIE_SIZE / 2, Config.ENNEMIE_SIZE, Config.ENNEMIE_SIZE);
        }

        public Tir shoot()
        {
            Tir tir = new Tir(x, y + Config.ENNEMIE_SIZE / 2, 2);
            return tir;

        }

    }
}
