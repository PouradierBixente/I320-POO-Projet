using ShootEmUp.Properties;

namespace ShootEmUp
{
    public class Barriere
    {
        private int _vie;
        private int _x;
        private int _y;

        public int Vie { get => _vie; set => _vie = value; }
        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }

        // Constructeur
        public Barriere(int x, int y)
        {
            this.X = x;
            this.Y = y;
            Vie = 4;
        }

        public void Render(BufferedGraphics drawingSpace)
        {
            if(Vie == 4)
                drawingSpace.Graphics.DrawImage(Resources.Bariere, X - Config.BARRIERE_SIZE / 2, Y - Config.BARRIERE_SIZE / 2, Config.BARRIERE_SIZE, Config.BARRIERE_SIZE);
            if (Vie == 3)
                drawingSpace.Graphics.DrawImage(Resources.Barriere2, X - Config.BARRIERE_SIZE / 2, Y - Config.BARRIERE_SIZE / 6, Config.BARRIERE_SIZE, Config.BARRIERE_SIZE / 3);
            if (Vie == 2)
                drawingSpace.Graphics.DrawImage(Resources.Barriere3, X - Config.BARRIERE_SIZE / 2, Y - Config.BARRIERE_SIZE / 6, Config.BARRIERE_SIZE, Config.BARRIERE_SIZE / 3);
            if (Vie == 1)
                drawingSpace.Graphics.DrawImage(Resources.Barriere4, X - Config.BARRIERE_SIZE / 2, Y - Config.BARRIERE_SIZE / 6, Config.BARRIERE_SIZE, Config.BARRIERE_SIZE / 3);
        }
    }
}
