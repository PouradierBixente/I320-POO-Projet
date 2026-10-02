namespace ShootEmUp
{
    // La classe gamespace représente le territoire au dessus duquel les player peuvent voler
    // Il s'agit d'un formulaire (une fenêtre) qui montre une vue 2D depuis en dessus
    // Il n'y a donc pas de notion d'altitude qui intervient

    public partial class GameSpace : Form
    {
        public static readonly int WIDTH = 1920;        // Dimensions of the gamespace
        public static readonly int HEIGHT = 1080;

        // La flotte est l'ensemble des player qui évoluent dans notre espace aérien
        private Player _player;
        private List<Tir> tirs = new List<Tir>();
        private List<Ennemie> ennemies = new List<Ennemie>(); 

        BufferedGraphicsContext currentContext;
        BufferedGraphics gamespace;

        // Initialisation de l'espace aérien avec un certain nombre de player
        public GameSpace(Player player, List<Ennemie> ennemies)
        {
            InitializeComponent();
            ClientSize = new Size(WIDTH, HEIGHT);

            // Gets a reference to the current BufferedGraphicsContext
            currentContext = BufferedGraphicsManager.Current;
            // Creates a BufferedGraphics instance associated with this form, and with
            // dimensions the same size as the drawing surface of the form.
            gamespace = currentContext.Allocate(this.CreateGraphics(), this.DisplayRectangle);
            this._player = player;
            this.ennemies = ennemies;
        }

        // Affichage de la situation actuelle
        private void Render()
        {
            gamespace.Graphics.Clear(Color.AliceBlue);

            _player.Render(gamespace);

            foreach (Ennemie ennemie in ennemies)
            {
                ennemie.Render(gamespace);
            }

            foreach (Tir tir in tirs)
            {
                tir.Render(gamespace);
            }

            gamespace.Render();
        }

        // Calcul du nouvel état après que 'interval' millisecondes se sont écoulées
        private void Update(int interval)
        {
            _player.Update(interval);
            foreach (Tir tir in tirs)
            {
                if(tir.type == 1)
                    tir.y = tir.y - 5;
                if (tir.type == 2)
                    tir.y = tir.y + 5;
            }

            foreach (Ennemie ennemie in ennemies)
            {
                ennemie.Update(interval);
            }
        }

        // Méthode appelée à chaque frame
        private void NewFrame(object sender, EventArgs e)
        {
            this.Update(ticker.Interval);
            this.Render();
            Addshoot();
        }

        public void gamespace_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    _player.ChangeDirection(true);
     
                    break;

                case Keys.Left:
                    _player.ChangeDirection(false);
    
                    break;
            }
        }

        public void Addshoot()
        {
            if (_player.timenomove <= 0)
            {
                tirs.Add(_player.shoot());
                _player.timenomove = Player.COOLDOWN;
            }

            foreach (Ennemie ennemie in ennemies)
            {
                if (ennemie.timecooldown <= 0)
                {
                    tirs.Add(ennemie.shoot());
                    ennemie.timecooldown = Ennemie.COOLDOWN;
                }
            }
        }
    }
}