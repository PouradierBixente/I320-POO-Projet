using ShootEmUp.Helpers;
using ShootEmUp.Properties;

namespace ShootEmUp
{
    // Cette partie de la classe Player définit ce qu'est un modèle numérique du joueur
    public class Player
    {
        public string name;                           // Un nom
        public int x;                                 // Position en X depuis la gauche de l'espace aérien
        public int y = GameSpace.HEIGHT - 150;                                 // Position en Y depuis le haut de l'espace aérien

        // Constructeur
        public Player(int x, int y, string name)
        {
            this.x = x;
            this.y = y;
            this.name = name;
        }

        // Cette méthode calcule le nouvel état dans lequel le joueur se trouve après
        // que 'interval' millisecondes se sont écoulées
        public void Update(int interval)
        {  
        }

        // Déplacement
        public void ChangeDirection(bool side)
        {
            int space = GameSpace.WIDTH - Config.PLAYER_SIZE;
            x += side ? 10 : -10;
            x = (x + space) % space;
        }

        private Pen playerBrush = new Pen(new SolidBrush(Color.Purple), 3);

        // De manière graphique
        public void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawImage(Resources.player, x, y, Config.PLAYER_SIZE, Config.PLAYER_SIZE);
            drawingSpace.Graphics.DrawString($"{this}", TextHelpers.drawFont, TextHelpers.writingBrush, x + 5, y - 25);
        }

        // De manière textuelle
        public override string ToString()
        {
            return $"{name}";
        }
    }
}
