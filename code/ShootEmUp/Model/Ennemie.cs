using ShootEmUp.Properties;

namespace ShootEmUp
{
    public class Ennemie
    {
        private const int _COOLDOWN = 30;
        private const int SPEED = 10;
        private int _x;
        private int _y;
        private State _state;
        private int _timecooldown;
        private int _vieennemie;
        private bool _dead;
        private int _point;
        public static int Wave = 1;

        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }
        public int Vieennemie { get => _vieennemie; set => _vieennemie = value; }
        public bool Dead { get => _dead; set => _dead = value; }
        public int Point { get => _point; set => _point = value; }

        public static int COOLDOWN => _COOLDOWN;

        public int Timecooldown { get => _timecooldown; set => _timecooldown = value; }

        public enum State { RIGHT, LEFT }

        // Constructeur
        public Ennemie(int x, int y)
        {
            X = x;
            Y = y;
            Vieennemie = 2;
            Timecooldown = COOLDOWN;
            _state = State.RIGHT;
            _dead = false;
            _point = 50;
        }

        public void Update(int interval)
        {
            Timecooldown--;
            if (Timecooldown <= 0)
                shoot();
            int space = GameSpace.WIDTH - Config.ENNEMIE_SIZE;
            
            
            if(X >= space)
            {
                _state = State.LEFT;
                Y += 10;
            }
            if (X <= -5)
            {
                _state = State.RIGHT;
                Y += 10;
            }

            if (_state == State.RIGHT)
            {
                X += 10;
            }

            if (_state == State.LEFT)
            {
                X -= 10;
            }

            Point--;

        }

        public void Render(BufferedGraphics drawingSpace)
        {
            if (_state == State.RIGHT)
                drawingSpace.Graphics.DrawImage(Resources.EnnmieMouvementDroite, X - Config.ENNEMIE_SIZE / 2, Y - Config.ENNEMIE_SIZE / 2, Config.ENNEMIE_SIZE, Config.ENNEMIE_SIZE);
            
            if (_state == State.LEFT)
                drawingSpace.Graphics.DrawImage(Resources.EnnemieMouvementGauche, X - Config.ENNEMIE_SIZE / 2, Y - Config.ENNEMIE_SIZE / 2, Config.ENNEMIE_SIZE, Config.ENNEMIE_SIZE);
        }

        public Tir shoot()
        {
            Tir tir = new Tir(X, Y + Config.ENNEMIE_SIZE / 2, Tir.Type.ENNEMIE);
            return tir;

        }

    }
}
