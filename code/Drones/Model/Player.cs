using ShootEmUp.Helpers;
using ShootEmUp.Properties;

namespace ShootEmUp
{
    // Cette partie de la classe Drone définit ce qu'est un drone par un modèle numérique
    public class Player
    {
        public string name;                           // Un nom
        public int x;                                 // Position en X depuis la gauche de l'espace aérien
        public int y;                                 // Position en Y depuis le haut de l'espace aérien
        public int speed_x = 0;                       // Déplacement horizontal

        // Constructeur
        public Player(int x, int y, string name)
        {
            this.x = x;
            this.y = y;
            this.name = name;
        }

        // Cette méthode calcule le nouvel état dans lequel le drone se trouve après
        // que 'interval' millisecondes se sont écoulées
        public void Update(int interval)
        {  
        }

        // Déplacement
        public void ChangeDirection(bool side)
        {
            int space = AirSpace.WIDTH - Config.PLAYER_SIZE;
            x += side ? 10 : -10;
            x = (x + space) % space;
        }

        

        /// //////////////////////////////////////////////////////////////////////////////
        //  
        //  Ce qui suit appartient à la vue, pas au modèle.
        //  Il aurait été préférable de séparer la déclaration de la classe Drone en deux,
        //  Nous regroupons tout ici pour simplifier
        //  
        /// //////////////////////////////////////////////////////////////////////////////

        private Pen droneBrush = new Pen(new SolidBrush(Color.Purple), 3);

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
