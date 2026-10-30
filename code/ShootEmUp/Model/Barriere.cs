using ShootEmUp.Properties;

namespace ShootEmUp
{

    public class Barriere
    {
        // Champs privés pour stocker l'état et la position de la barrière
        private int _vie;
        private int _x;
        private int _y;

        // Propriétés d'accès (getters / setters)
        public int Vie { get => _vie; set => _vie = value; }
        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }

        // Constructeur : Initialise la position (X, Y) et définit les points de vie de départ à 4
        public Barriere(int x, int y)
        {
            this.X = x;
            this.Y = y;
            Vie = 4;
        }

        //Affiche l'image correspondant à l'état de santé actuel de la barrière dans le buffer graphique.

        public void Render(BufferedGraphics drawingSpace)
        {
            // État intact (4 PV) : Affiche la sprite complète de la barrière centrée sur (X, Y)
            if (Vie == 4)
                drawingSpace.Graphics.DrawImage(Resources.Bariere, X - Config.BARRIERE_SIZE / 2, Y - Config.BARRIERE_SIZE / 2, Config.BARRIERE_SIZE, Config.BARRIERE_SIZE);

            // État endommagé (3 PV) : Affiche la deuxième étape de dégradation
            if (Vie == 3)
                drawingSpace.Graphics.DrawImage(Resources.Barriere2, X - Config.BARRIERE_SIZE / 2, Y - Config.BARRIERE_SIZE / 6, Config.BARRIERE_SIZE, Config.BARRIERE_SIZE / 3);

            // État très endommagé (2 PV) : Affiche la troisième étape de dégradation
            if (Vie == 2)
                drawingSpace.Graphics.DrawImage(Resources.Barriere3, X - Config.BARRIERE_SIZE / 2, Y - Config.BARRIERE_SIZE / 6, Config.BARRIERE_SIZE, Config.BARRIERE_SIZE / 3);

            // État critique (1 PV) : Affiche la dernière étape avant destruction
            if (Vie == 1)
                drawingSpace.Graphics.DrawImage(Resources.Barriere4, X - Config.BARRIERE_SIZE / 2, Y - Config.BARRIERE_SIZE / 6, Config.BARRIERE_SIZE, Config.BARRIERE_SIZE / 3);
        }
    }
}