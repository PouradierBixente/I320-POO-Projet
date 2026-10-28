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

            if (_player.vieplayer <= 0 || (Ennemie.wave >= 3 && ennemies.Count <= 0))
                gamespace.Graphics.DrawString($"Votre score : {_player.score}", TextHelpers.drawFont, TextHelpers.writingBrush, GameSpace.WIDTH / 4 , GameSpace.HEIGHT / 4);

            if (Ennemie.wave >= 3 && ennemies.Count <= 0)
                gamespace.Graphics.DrawString($"Victoire", TextHelpers.drawFont, TextHelpers.writingBrush, GameSpace.WIDTH / 2 - 125 , GameSpace.HEIGHT / 2);

            if (_player.vieplayer <= 0)
                gamespace.Graphics.DrawString($"Defaite", TextHelpers.drawFont, TextHelpers.writingBrush, GameSpace.WIDTH / 2, GameSpace.HEIGHT / 2);

            gamespace.Render();
        }

        // Calcul du nouvel état après que 'interval' millisecondes se sont écoulées
        private void Update(int interval)
        {
            if (_player.vieplayer <= 0 || (Ennemie.wave >= 3 && ennemies.Count <= 0))
                return;

            if (i == 0)
            {
                ennemies.Add(new Ennemie(Config.ENNEMIE_SIZE / 2, Config.ENNEMIE_SIZE / 4));
                i++;
                j++;
            }

            if (Ennemie.wave == 1 && ennemies.Count != 15 && j != 15)
            {
                if (ennemies[ennemies.Count - 1].x >= Config.ENNEMIE_SIZE * 2)
                {
                    ennemies.Add(new Ennemie(Config.ENNEMIE_SIZE / 2, Config.ENNEMIE_SIZE / 4));
                    j++;
                }
            }

            if (Ennemie.wave == 2 && ennemies.Count != 25 && j != 25)
            {
                if (ennemies[ennemies.Count - 1].x >= Config.ENNEMIE_SIZE * 2)
                {
                    ennemies.Add(new Ennemie(Config.ENNEMIE_SIZE / 2, Config.ENNEMIE_SIZE / 4));
                    j++;
                }
            }

            if (Ennemie.wave == 3 && ennemies.Count != 50 && j != 50)
            {
                if (ennemies[ennemies.Count - 1].x >= Config.ENNEMIE_SIZE * 2)
                {
                    ennemies.Add(new Ennemie(Config.ENNEMIE_SIZE / 2, Config.ENNEMIE_SIZE / 4));
                    j++;
                }
            }

            if (ennemies.Count <= 0)
            {
                Ennemie.wave++;
                i = 0;
                j = 0;
            }

            _player.Update(interval);

            for (int i = tirs.Count - 1; i >= 0; i--)
            {
                tirs[i].Update();
                if (tirs[i].explode || tirs[i].outofspace)
                    tirs.RemoveAt(i);
            }

            for (int i = barrieres.Count - 1; i >= 0; i--)
            {
                if (barrieres[i].vie <= 0)
                    barrieres.RemoveAt(i);
            }

            for (int i = ennemies.Count - 1; i >= 0; i--)
            {
                if (ennemies[i].dead)
                {
                    _player.score += ennemies[i].point;
                    ennemies.RemoveAt(i);
                }
            }

            foreach (Tir tir in tirs)
            {
                if (MathHelpers.Distance(tir.x, tir.y, _player.x, _player.y) <= 50 && tir.type != Tir.Type.PLAYER)
                {
                    if(_player.timeinvincible <= 0)
                    {
                        _player.vieplayer--;
                        _player.timeinvincible = Player.FRAMEINVINCIBLE;
                    }
                    tir.explode = true;
                }

                foreach (Ennemie ennemie in ennemies)
                {
                    if (MathHelpers.Distance(tir.x, tir.y, ennemie.x, ennemie.y) <= 50 && tir.type != Tir.Type.ENNEMIE)
                    {
                        tir.explode = true;
                        ennemie.vieennemie--;
                    }
                }

                foreach (Barriere barriere in barrieres)
                {
                    if (MathHelpers.Distance(tir.x, tir.y, barriere.x, barriere.y) <= 100 && tir.type != Tir.Type.PLAYER)
                    {
                        tir.explode = true;
                        barriere.vie--;
                    }
                }

                foreach (Tir tir2 in tirs)
                {
                    if (tir != tir2)
                    {
                        if (MathHelpers.Distance(tir.x, tir.y, tir2.x, tir2.y) <= 50 && tir.type != tir2.type)
                        {
                            int distTirX = Math.Abs(tir.x - tir2.x);
                            int distTirY = Math.Abs(tir.y - tir2.y);
                            tir.explode = true;
                            tir2.explode = true;
                            if (tir.x < tir2.x)
                            {
                                tir.x = distTirX / 2 + tir.x;
                                tir2.x = distTirX / 2 + tir.x;
                            }
                            else
                            {
                                tir.x = distTirX / 2 + tir2.x;
                                tir2.x = distTirX / 2 + tir2.x;
                            }

                            if (tir.x < tir2.x)
                            {
                                tir.y = distTirY / 2 + tir.y;
                                tir2.y = distTirY / 2 + tir.y;
                            }
                            else
                            {
                                tir.y = distTirY / 2 + tir2.y;
                                tir2.y = distTirY / 2 + tir2.y;
                            }
                        }
                    }
                }
            }


            foreach (Ennemie ennemie in ennemies)
            {
                ennemie.Update(interval);
                if(ennemie.vieennemie <= 0)
                    ennemie.dead = true;
                if (MathHelpers.Distance(ennemie.x, ennemie.y, _player.x, _player.y) <= 50)
                {
                    if (_player.timeinvincible <= 0)
                    {
                        _player.vieplayer--;
                        _player.timeinvincible = Player.FRAMEINVINCIBLE;
                    }
                    ennemie.vieennemie -= 2;
                }
                foreach (Barriere barriere in barrieres)
                {
                    if (MathHelpers.Distance(ennemie.x, ennemie.y, barriere.x, barriere.y) <= 50)
                    {
                        barriere.vie--;
                        ennemie.vieennemie -= 2;
                    }
                }
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
                if (ennemie.timecooldown <= 0 && (_player.x + Config.PLAYER_SIZE / 2 >= ennemie.x - Config.ENNEMIE_SIZE / 2) && (_player.x - Config.PLAYER_SIZE / 2 <= ennemie.x + Config.ENNEMIE_SIZE / 2))
                {
                    tirs.Add(ennemie.shoot());
                    ennemie.timecooldown = Ennemie.COOLDOWN;
                }
            }
        }
    }
}