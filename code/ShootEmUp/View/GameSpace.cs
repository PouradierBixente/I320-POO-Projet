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
        List<Barriere> barrieres = new List<Barriere>();
        private int i = 0;
        private int j = 0;

        BufferedGraphicsContext currentContext;
        BufferedGraphics gamespace;

        // Initialisation de l'espace aérien avec un certain nombre de player
        public GameSpace(Player player, List<Barriere> barrieres)
        {
            InitializeComponent();
            ClientSize = new Size(WIDTH, HEIGHT);

            // Gets a reference to the current BufferedGraphicsContext
            currentContext = BufferedGraphicsManager.Current;
            // Creates a BufferedGraphics instance associated with this form, and with
            // dimensions the same size as the drawing surface of the form.
            gamespace = currentContext.Allocate(this.CreateGraphics(), this.DisplayRectangle);
            this._player = player;
            this.barrieres = barrieres;
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

            foreach (Barriere barriere in barrieres)
            {
                barriere.Render(gamespace);
            }

            if (_player.Vieplayer <= 0 || (Ennemie.Wave >= 3 && ennemies.Count <= 0))
                gamespace.Graphics.DrawString($"Votre score : {_player.Score}", TextHelpers.drawFont, TextHelpers.writingBrush, GameSpace.WIDTH / 4 , GameSpace.HEIGHT / 4);

            if (Ennemie.Wave >= 3 && ennemies.Count <= 0)
                gamespace.Graphics.DrawString($"Victoire", TextHelpers.drawFont, TextHelpers.writingBrush, GameSpace.WIDTH / 2 - 125 , GameSpace.HEIGHT / 2);

            if (_player.Vieplayer <= 0)
                gamespace.Graphics.DrawString($"Defaite", TextHelpers.drawFont, TextHelpers.writingBrush, GameSpace.WIDTH / 2, GameSpace.HEIGHT / 2);

            gamespace.Render();
        }

        // Calcul du nouvel état après que 'interval' millisecondes se sont écoulées
        private void Update(int interval)
        {
            if (_player.Vieplayer <= 0 || (Ennemie.Wave >= 3 && ennemies.Count <= 0))
                return;

            if (i == 0)
            {
                ennemies.Add(new Ennemie(Config.ENNEMIE_SIZE / 2, Config.ENNEMIE_SIZE / 4));
                i++;
                j++;
            }

            if (Ennemie.Wave == 1 && ennemies.Count != 15 && j != 15)
            {
                if (ennemies[ennemies.Count - 1].X >= Config.ENNEMIE_SIZE * 2)
                {
                    ennemies.Add(new Ennemie(Config.ENNEMIE_SIZE / 2, Config.ENNEMIE_SIZE / 4));
                    j++;
                }
            }

            if (Ennemie.Wave == 2 && ennemies.Count != 25 && j != 25)
            {
                if (ennemies[ennemies.Count - 1].X >= Config.ENNEMIE_SIZE * 2)
                {
                    ennemies.Add(new Ennemie(Config.ENNEMIE_SIZE / 2, Config.ENNEMIE_SIZE / 4));
                    j++;
                }
            }

            if (Ennemie.Wave == 3 && ennemies.Count != 50 && j != 50)
            {
                if (ennemies[ennemies.Count - 1].X >= Config.ENNEMIE_SIZE * 2)
                {
                    ennemies.Add(new Ennemie(Config.ENNEMIE_SIZE / 2, Config.ENNEMIE_SIZE / 4));
                    j++;
                }
            }

            if (ennemies.Count <= 0)
            {
                Ennemie.Wave++;
                i = 0;
                j = 0;
            }

            _player.Update(interval);

            for (int i = tirs.Count - 1; i >= 0; i--)
            {
                tirs[i].Update();
                if (tirs[i].Explode || tirs[i].outofspace)
                    tirs.RemoveAt(i);
            }

            for (int i = barrieres.Count - 1; i >= 0; i--)
            {
                if (barrieres[i].Vie <= 0)
                    barrieres.RemoveAt(i);
            }

            for (int i = ennemies.Count - 1; i >= 0; i--)
            {
                if (ennemies[i].Dead)
                {
                    _player.Score += ennemies[i].Point;
                    ennemies.RemoveAt(i);
                }
            }

            foreach (Tir tir in tirs)
            {
                if (MathHelpers.Distance(tir.X, tir.Y, _player.X, _player.Y) <= 50 && tir.type != Tir.Type.PLAYER)
                {
                    if(_player.Timeinvincible <= 0)
                    {
                        _player.Vieplayer--;
                        _player.Timeinvincible = Player.FRAMEINVINCIBLE;
                    }
                    tir.Explode = true;
                }

                foreach (Ennemie ennemie in ennemies)
                {
                    if (MathHelpers.Distance(tir.X, tir.Y, ennemie.X, ennemie.Y) <= 50 && tir.type != Tir.Type.ENNEMIE)
                    {
                        tir.Explode = true;
                        ennemie.Vieennemie--;
                    }
                }

                foreach (Barriere barriere in barrieres)
                {
                    if (MathHelpers.Distance(tir.X, tir.Y, barriere.X, barriere.Y) <= 100 && tir.type != Tir.Type.PLAYER)
                    {
                        tir.Explode = true;
                        barriere.Vie--;
                    }
                }

                foreach (Tir tir2 in tirs)
                {
                    if (tir != tir2)
                    {
                        if (MathHelpers.Distance(tir.X, tir.Y, tir2.X, tir2.Y) <= 50 && tir.type != tir2.type)
                        {
                            int distTirX = Math.Abs(tir.X - tir2.X);
                            int distTirY = Math.Abs(tir.Y - tir2.Y);
                            tir.Explode = true;
                            tir2.Explode = true;
                            if (tir.X < tir2.X)
                            {
                                tir.X = distTirX / 2 + tir.X;
                                tir2.X = distTirX / 2 + tir.X;
                            }
                            else
                            {
                                tir.X = distTirX / 2 + tir2.X;
                                tir2.X = distTirX / 2 + tir2.X;
                            }

                            if (tir.X < tir2.X)
                            {
                                tir.Y = distTirY / 2 + tir.Y;
                                tir2.Y = distTirY / 2 + tir.Y;
                            }
                            else
                            {
                                tir.Y = distTirY / 2 + tir2.Y;
                                tir2.Y = distTirY / 2 + tir2.Y;
                            }
                        }
                    }
                }
            }


            foreach (Ennemie ennemie in ennemies)
            {
                ennemie.Update(interval);
                if(ennemie.Vieennemie <= 0)
                    ennemie.Dead = true;
                if (MathHelpers.Distance(ennemie.X, ennemie.Y, _player.X, _player.Y) <= 50)
                {
                    if (_player.Timeinvincible <= 0)
                    {
                        _player.Vieplayer--;
                        _player.Timeinvincible = Player.FRAMEINVINCIBLE;
                    }
                    ennemie.Vieennemie -= 2;
                }
                foreach (Barriere barriere in barrieres)
                {
                    if (MathHelpers.Distance(ennemie.X, ennemie.Y, barriere.X, barriere.Y) <= 50)
                    {
                        barriere.Vie--;
                        ennemie.Vieennemie -= 2;
                    }
                }
            }
        }

        // Méthode appelée à chaque frame
        private void NewFrame(object sender, EventArgs e)
        {
            Update(ticker.Interval);
            Render();
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
            if (_player.Timenomove <= 0)
            {
                tirs.Add(_player.shoot());
                _player.Timenomove = Player.COOLDOWN;
            }

            foreach (Ennemie ennemie in ennemies)
            {
                if (ennemie.Timecooldown <= 0 && (_player.X + Config.PLAYER_SIZE / 2 >= ennemie.X - Config.ENNEMIE_SIZE / 2) && (_player.X - Config.PLAYER_SIZE / 2 <= ennemie.X + Config.ENNEMIE_SIZE / 2))
                {
                    tirs.Add(ennemie.shoot());
                    ennemie.Timecooldown = Ennemie.COOLDOWN;
                }
            }
        }
    }
}