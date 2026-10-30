using ShootEmUp.Properties;

namespace ShootEmUp
{
    // Cette partie de la classe Player définit ce qu'est un modèle numérique du joueur
    public class Player
    {
        private const int _FRAMEINVINCIBLE = 20;
        private const int _COOLDOWN = 20;
        private int _vieplayer;                           // Un nom
        private int _x;                                 // Position en X depuis la gauche de l'espace aérien
        private int _y;                               // Position en Y depuis le haut de l'espace aérien
        private int _timenomove = COOLDOWN;
        private int _timeinvincible = FRAMEINVINCIBLE;
        private int _score = 0;

        public static int FRAMEINVINCIBLE => _FRAMEINVINCIBLE;

        public static int COOLDOWN => _COOLDOWN;

        public int Vieplayer { get => _vieplayer; set => _vieplayer = value; }
        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }
        public int Timenomove { get => _timenomove; set => _timenomove = value; }
        public int Timeinvincible { get => _timeinvincible; set => _timeinvincible = value; }
        public int Score { get => _score; set => _score = value; }

        // Constructeur
        public Player(int x, int y)
        {
            X = x;
            Y = y;
            Vieplayer = 3;
        }

        // Cette méthode calcule le nouvel état dans lequel le joueur se trouve après
        // que 'interval' millisecondes se sont écoulées
        public void Update(int interval)
        {
            Timenomove--;
            Timeinvincible--;
            if (Timenomove <= 0)
                shoot();
        }

        // Déplacement
        public void ChangeDirection(bool side)
        {
            int space = GameSpace.WIDTH - Config.PLAYER_SIZE;
            X += side ? 10 : -10;
            X = (X + space) % space;
            Timenomove = COOLDOWN;
        }

        private Pen playerBrush = new Pen(new SolidBrush(Color.Purple), 3);


        // De manière graphique
        public void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawImage(Resources.player, X - Config.PLAYER_SIZE/2, Y - Config.PLAYER_SIZE / 2, Config.PLAYER_SIZE, Config.PLAYER_SIZE);
            if(Vieplayer == 3)
                drawingSpace.Graphics.DrawImage(Resources._3vies, X - Config.PLAYER_LIFE_SIZE / 2, Y - (Config.PLAYER_LIFE_SIZE / 6), Config.PLAYER_LIFE_SIZE, Config.PLAYER_LIFE_SIZE);
            if (Vieplayer == 2)
                drawingSpace.Graphics.DrawImage(Resources._2vies, X - Config.PLAYER_LIFE_SIZE / 2, Y - (Config.PLAYER_LIFE_SIZE / 6), Config.PLAYER_LIFE_SIZE, Config.PLAYER_LIFE_SIZE);
            if (Vieplayer == 1)
                drawingSpace.Graphics.DrawImage(Resources._1vie, X - Config.PLAYER_LIFE_SIZE / 2, Y - (Config.PLAYER_LIFE_SIZE / 6), Config.PLAYER_LIFE_SIZE, Config.PLAYER_LIFE_SIZE);
            
        }

        public Tir shoot()
        {
            Tir tir = new Tir(X, Y - Config.PLAYER_SIZE / 2, Tir.Type.PLAYER);
            return tir;
            
        }
    }
}
