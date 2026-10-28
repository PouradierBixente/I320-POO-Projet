using ShootEmUp.Properties;

namespace ShootEmUp
{
    public class Tir
    {
        private const int SPEED = 10;
        public int x;
        public int y;
        public bool explode = false;
        public bool outofspace => y < -Config.SHOOT_SIZE || y > GameSpace.HEIGHT;
        public Tir.Type type;

        public enum Type { ENNEMIE, PLAYER };

        // Constructeur
        public Tir(int x, int y,  Tir.Type type)
        {
            this.x = x;
            this.y = y;
            Tir.Type Type = type;
        }

        public void Update()
        {
            if (type == Tir.Type.PLAYER )
                y -= 5;
            if (type == Tir.Type.ENNEMIE)
                y += 5;
        }
        public void Render(BufferedGraphics drawingSpace)
        {
            if(type == Tir.Type.PLAYER)
                drawingSpace.Graphics.DrawImage(Resources.playerShoot, x- Config.SHOOT_SIZE/2, y - Config.SHOOT_SIZE / 2, Config.SHOOT_SIZE, Config.SHOOT_SIZE);
            if (type == Tir.Type.ENNEMIE)
                drawingSpace.Graphics.DrawImage(Resources.TirEnnemie, x- Config.SHOOT_SIZE / 2, y - Config.SHOOT_SIZE / 2, Config.SHOOT_SIZE, Config.SHOOT_SIZE);
            if (explode)
                drawingSpace.Graphics.DrawImage(Resources.ExplosionTir, x - Config.SHOOT_SIZE, y - Config.SHOOT_SIZE , Config.SHOOT_SIZE * 2, Config.SHOOT_SIZE * 2);
        }
    }
}
