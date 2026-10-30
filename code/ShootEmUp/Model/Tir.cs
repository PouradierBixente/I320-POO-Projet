using ShootEmUp.Properties;

namespace ShootEmUp
{
    public class Tir
    {
        private const int _SPEED = 10;
        private int _x;
        private int _y;
        private bool _explode = false;
        public bool outofspace => Y < -Config.SHOOT_SIZE || Y > GameSpace.HEIGHT;

        public static int SPEED => _SPEED;

        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }
        public bool Explode { get => _explode; set => _explode = value; }

        public Tir.Type type;

        public enum Type { ENNEMIE, PLAYER };

        // Constructeur
        public Tir(int x, int y,  Tir.Type type)
        {
            X = x;
            Y = y;
            this.type  = type;
        }

        public void Update()
        {
            if (type == Tir.Type.PLAYER )
                Y -= 5;
            if (type == Tir.Type.ENNEMIE)
                Y += 5;
        }
        public void Render(BufferedGraphics drawingSpace)
        {
            if(type == Tir.Type.PLAYER)
                drawingSpace.Graphics.DrawImage(Resources.playerShoot, X- Config.SHOOT_SIZE/2, Y - Config.SHOOT_SIZE / 2, Config.SHOOT_SIZE, Config.SHOOT_SIZE);
            if (type == Tir.Type.ENNEMIE)
                drawingSpace.Graphics.DrawImage(Resources.TirEnnemie, X- Config.SHOOT_SIZE / 2, Y - Config.SHOOT_SIZE / 2, Config.SHOOT_SIZE, Config.SHOOT_SIZE);
            if (Explode)
                drawingSpace.Graphics.DrawImage(Resources.ExplosionTir, X - Config.SHOOT_SIZE, Y - Config.SHOOT_SIZE , Config.SHOOT_SIZE * 2, Config.SHOOT_SIZE * 2);
        }
    }
}
