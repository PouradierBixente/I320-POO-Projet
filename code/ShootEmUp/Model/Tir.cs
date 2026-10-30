using ShootEmUp.Properties;

namespace ShootEmUp
{
    // Gestion des tirs (joueur et ennemis)
    public class Tir
    {
        private const int _SPEED = 10;
        private int _x;
        private int _y;
        private bool _explode = false;

        // Indique si le projectile est sorti de la zone de jeu
        public bool outofspace => Y < -Config.SHOOT_SIZE || Y > GameSpace.HEIGHT;

        public static int SPEED => _SPEED;

        // Propriétés d'accès
        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }
        public bool Explode { get => _explode; set => _explode = value; }

        public Tir.Type type;

        // Types de projectiles
        public enum Type { ENNEMIE, PLAYER };

        // Constructeur : Initialise la position et l'origine du tir
        public Tir(int x, int y, Tir.Type type)
        {
            X = x;
            Y = y;
            this.type = type;
        }

        // Déplace le tir vers le haut (joueur) ou vers le bas (ennemi)
        public void Update()
        {
            if (type == Tir.Type.PLAYER)
                Y -= 5;
            if (type == Tir.Type.ENNEMIE)
                Y += 5;
        }

        // Affiche le sprite correspondant au tir ou l'animation d'explosion
        public void Render(BufferedGraphics drawingSpace)
        {
            if (type == Tir.Type.PLAYER)
                drawingSpace.Graphics.DrawImage(Resources.playerShoot, X - Config.SHOOT_SIZE / 2, Y - Config.SHOOT_SIZE / 2, Config.SHOOT_SIZE, Config.SHOOT_SIZE);
            if (type == Tir.Type.ENNEMIE)
                drawingSpace.Graphics.DrawImage(Resources.TirEnnemie, X - Config.SHOOT_SIZE / 2, Y - Config.SHOOT_SIZE / 2, Config.SHOOT_SIZE, Config.SHOOT_SIZE);
            if (Explode)
                drawingSpace.Graphics.DrawImage(Resources.ExplosionTir, X - Config.SHOOT_SIZE, Y - Config.SHOOT_SIZE, Config.SHOOT_SIZE * 2, Config.SHOOT_SIZE * 2);
        }
    }
}