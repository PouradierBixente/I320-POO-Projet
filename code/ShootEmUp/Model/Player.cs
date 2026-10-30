using ShootEmUp.Properties;

namespace ShootEmUp
{
    // Modèle numérique et logique du joueur
    public class Player
    {
        // Constantes de temporisation
        private const int _FRAMEINVINCIBLE = 20;
        private const int _COOLDOWN = 20;

        // Champs privés pour l'état, la position et les compteurs
        private int _vieplayer;                               // Points de vie actuels
        private int _x;                                      // Position horizontale
        private int _y;                                      // Position verticale
        private int _timenomove = COOLDOWN;
        private int _timeinvincible = FRAMEINVINCIBLE;
        private int _score = 0;

        // Propriétés statiques
        public static int FRAMEINVINCIBLE => _FRAMEINVINCIBLE;
        public static int COOLDOWN => _COOLDOWN;

        // Propriétés d'accès
        public int Vieplayer { get => _vieplayer; set => _vieplayer = value; }
        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }
        public int Timenomove { get => _timenomove; set => _timenomove = value; }
        public int Timeinvincible { get => _timeinvincible; set => _timeinvincible = value; }
        public int Score { get => _score; set => _score = value; }

        // Constructeur : Place le joueur et fixe ses PV initiaux à 3
        public Player(int x, int y)
        {
            X = x;
            Y = y;
            Vieplayer = 3;
        }

        // Met à jour les temporisateurs du joueur
        public void Update(int interval)
        {
            Timenomove--;
            Timeinvincible--;
        }

        // Déplace le joueur et applique le passage d'un bord à l'autre de l'écran
        public void ChangeDirection(bool side)
        {
            int space = GameSpace.WIDTH - Config.PLAYER_SIZE;
            X += side ? 10 : -10;
            X = (X + space) % space;   // Recalcule la position en modulo pour réapparaître du côté opposé
            Timenomove = COOLDOWN;
        }

        private Pen playerBrush = new Pen(new SolidBrush(Color.Purple), 3);


        // Affichage du vaisseau et de l'indicateur de vie
        public void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawImage(Resources.player, X - Config.PLAYER_SIZE / 2, Y - Config.PLAYER_SIZE / 2, Config.PLAYER_SIZE, Config.PLAYER_SIZE);
            if (Vieplayer == 3)
                drawingSpace.Graphics.DrawImage(Resources._3vies, X - Config.PLAYER_LIFE_SIZE / 2, Y - (Config.PLAYER_LIFE_SIZE / 6), Config.PLAYER_LIFE_SIZE, Config.PLAYER_LIFE_SIZE);
            if (Vieplayer == 2)
                drawingSpace.Graphics.DrawImage(Resources._2vies, X - Config.PLAYER_LIFE_SIZE / 2, Y - (Config.PLAYER_LIFE_SIZE / 6), Config.PLAYER_LIFE_SIZE, Config.PLAYER_LIFE_SIZE);
            if (Vieplayer == 1)
                drawingSpace.Graphics.DrawImage(Resources._1vie, X - Config.PLAYER_LIFE_SIZE / 2, Y - (Config.PLAYER_LIFE_SIZE / 6), Config.PLAYER_LIFE_SIZE, Config.PLAYER_LIFE_SIZE);

        }

        // Instancie un nouveau tir provenant du joueur
        public Tir shoot()
        {
            Tir tir = new Tir(X, Y - Config.PLAYER_SIZE / 2, Tir.Type.PLAYER);
            return tir;

        }
    }
}