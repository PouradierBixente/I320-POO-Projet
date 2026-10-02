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
        private const int SPEED = 10;
        public int x;
        public int y;
        public State state = State.RIGHT;
        
        public enum State { RIGHT, LEFT }

        // Constructeur
        public Ennemie(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void Update(int interval)
        {
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
                drawingSpace.Graphics.DrawImage(Resources.EnnmieMouvementDroite, x, y, Config.ENNEMIE_SIZE, Config.ENNEMIE_SIZE);
            
            if (state == State.LEFT)
                drawingSpace.Graphics.DrawImage(Resources.EnnemieMouvementGauche, x, y, Config.ENNEMIE_SIZE, Config.ENNEMIE_SIZE);
        }

    }
}
