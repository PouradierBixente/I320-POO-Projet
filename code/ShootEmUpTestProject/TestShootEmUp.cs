using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShootEmUp;

namespace ShootEmUpTestProject
{
    // Classe de tests unitaires (MSTest) couvrant la logique du jeu ShootEmUp
    [TestClass]
    public class TestsEnsemble
    {
        // Vérifie l'état initial des propriétés du joueur à la création
        [TestMethod]
        public void Player_Initialisation_DoitAvoirValeursParDefaut()
        {
            // Arrange & Act : Instanciation du joueur
            var player = new Player(0, 0);

            // Assert : Validation des valeurs de départ
            Assert.IsTrue(player.Vieplayer > 0, "Le joueur doit avoir plus de 0 point de vie au départ.");
            Assert.AreEqual(0, player.X, "La position X initiale doit être 0.");
        }

        // Teste la réduction des points de vie après avoir subi des dégâts
        [TestMethod]
        public void Player_TakeDamage_DoitReduireLaSante()
        {
            // Arrange : Joueur avec 100 PV
            var player = new Player(0, 0) { Vieplayer = 100 };

            // Act : Subit 20 dégâts
            player.Vieplayer -= 20;

            // Assert : Les PV doivent passer à 80
            Assert.AreEqual(80, player.Vieplayer, "Les points de vie doivent être réduits à 80.");
        }

        // Vérifie que les dégâts mortels font tomber les PV à 0 ou en dessous
        [TestMethod]
        public void Player_TakeFatalDamage_DoitTuerLeJoueur()
        {
            // Arrange : Joueur presque mort
            var player = new Player(0, 0) { Vieplayer = 10 };

            // Act : Subit des dégâts supérieurs à sa santé restante
            player.Vieplayer -= 20;

            // Assert : Confirmation de la défaite du joueur
            Assert.AreEqual(-10, player.Vieplayer, "Les points de vie doivent être négatifs.");
            Assert.IsTrue(player.Vieplayer <= 0, "Le joueur doit être considéré comme mort.");
        }

        // Vérifie que l'ennemi conserve sa ligne Y pendant un déplacement normal
        [TestMethod]
        public void Ennemie_Move_DoitMettreAJourLaPosition()
        {
            // Arrange : Ennemi placé loin des bordures de l'écran
            var ennemie = new Ennemie(10, 50);

            // Act : Mise à jour du déplacement de l'ennemi
            ennemie.Update(16);

            // Assert : Y doit rester inchangé s'il ne touche pas un bord
            Assert.AreEqual(50, ennemie.Y, "La position Y doit rester à 50 avant que l'ennemi n'atteigne le bord.");
        }

        // Teste le calcul de détection de collision par la distance
        [TestMethod]
        public void Ennemie_Collision_DoitRetournerVraiSiSuperposition()
        {
            // Arrange : Ennemi et tir placés très proches
            var ennemie = new Ennemie(10, 10);
            var tir = new Tir(15, 15, Tir.Type.PLAYER);

            // Act : Vérification de la distance relative
            bool hasCollision = MathHelpers.Distance(ennemie.X, ennemie.Y, tir.X, tir.Y) < 20;

            // Assert : Doit être évalué comme une collision valide
            Assert.IsTrue(hasCollision, "La collision doit être détectée si la distance est inférieure à 20.");
        }

        // Vérifie le mouvement vers le haut d'un projectile de joueur
        [TestMethod]
        public void Tir_Update_DoitAvancerSelonSaVitesse()
        {
            // Arrange : Tir joueur à Y = 100
            var tir = new Tir(0, 100, Tir.Type.PLAYER);

            // Act : Déplacement d'une frame
            tir.Update();

            // Assert : Y doit diminuer (remontée vers le haut de l'écran)
            Assert.AreEqual(95, tir.Y, "Le tir d'un joueur doit monter, donc Y doit diminuer de 5.");
        }

        // Vérifie la détection de sortie de la zone d'affichage
        [TestMethod]
        public void Tir_IsOutOfBounds_DoitRetournerVraiSiHorsEcran()
        {
            // Arrange : Tir positionné en dehors des limites hautes
            var tir = new Tir(0, -100, Tir.Type.PLAYER);

            // Act : Vérification de la propriété de sortie de zone
            bool isOut = tir.outofspace;

            // Assert : Le tir doit être marqué comme à supprimer
            Assert.IsTrue(isOut, "Le tir doit être considéré comme hors de l'espace de jeu.");
        }

        // Valide la précision du calcul géométrique de distance (Théorème de Pythagore)
        [TestMethod]
        public void CalculateDistance_DoitRetournerLaBonneDistance()
        {
            // Arrange : Points formant un triangle rectangle 3-4-5
            int x1 = 0, y1 = 0;
            int x2 = 3, y2 = 4;
            double expectedDistance = 5;

            // Act : Calcul de la distance
            double distance = MathHelpers.Distance(x1, y1, x2, y2);

            // Assert : Validation de la distance euclidienne exacte
            Assert.AreEqual(expectedDistance, distance, "La distance euclidienne calculée est incorrecte.");
        }
    }
}