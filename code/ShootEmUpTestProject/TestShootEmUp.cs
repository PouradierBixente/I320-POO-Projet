using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShootEmUp;

namespace ShootEmUpTestProject
{
    [TestClass]
    public class TestsEnsemble
    {
        [TestMethod]
        public void Player_Initialisation_DoitAvoirValeursParDefaut()
        {
            // Arrange & Act
            var player = new Player(0, 0);

            // Assert
            Assert.IsTrue(player.Vieplayer > 0, "Le joueur doit avoir plus de 0 point de vie au départ.");
            Assert.AreEqual(0, player.X, "La position X initiale doit être 0.");
        }

        [TestMethod]
        public void Player_TakeDamage_DoitReduireLaSante()
        {
            // Arrange
            var player = new Player(0, 0) { Vieplayer = 100 };

            // Act
            player.Vieplayer -= 20;

            // Assert
            Assert.AreEqual(80, player.Vieplayer, "Les points de vie doivent être réduits à 80.");
        }

        [TestMethod]
        public void Player_TakeFatalDamage_DoitTuerLeJoueur()
        {
            // Arrange
            var player = new Player(0, 0) { Vieplayer = 10 };

            // Act
            player.Vieplayer -= 20;

            // Assert
            Assert.AreEqual(-10, player.Vieplayer, "Les points de vie doivent être négatifs.");
            Assert.IsTrue(player.Vieplayer <= 0, "Le joueur doit être considéré comme mort.");
        }

        [TestMethod]
        public void Ennemie_Move_DoitMettreAJourLaPosition()
        {
            // Arrange
            var ennemie = new Ennemie(10, 50);

            // Act
            ennemie.Update(16);

            // Assert
            Assert.AreEqual(50, ennemie.Y, "La position Y doit rester à 50 avant que l'ennemi n'atteigne le bord.");
        }

        [TestMethod]
        public void Ennemie_Collision_DoitRetournerVraiSiSuperposition()
        {
            // Arrange
            var ennemie = new Ennemie(10, 10);
            var tir = new Tir(15, 15, Tir.Type.PLAYER);

            // Act
            bool hasCollision = MathHelpers.Distance(ennemie.X, ennemie.Y, tir.X, tir.Y) < 20;

            // Assert
            Assert.IsTrue(hasCollision, "La collision doit être détectée si la distance est inférieure à 20.");
        }

        [TestMethod]
        public void Tir_Update_DoitAvancerSelonSaVitesse()
        {
            // Arrange
            var tir = new Tir(0, 100, Tir.Type.PLAYER);

            // Act
            tir.Update();

            // Assert
            Assert.AreEqual(95, tir.Y, "Le tir d'un joueur doit monter, donc Y doit diminuer de 5.");
        }

        [TestMethod]
        public void Tir_IsOutOfBounds_DoitRetournerVraiSiHorsEcran()
        {
            // Arrange
            var tir = new Tir(0, -100, Tir.Type.PLAYER);

            // Act
            bool isOut = tir.outofspace;

            // Assert
            Assert.IsTrue(isOut, "Le tir doit être considéré comme hors de l'espace de jeu.");
        }

        [TestMethod]
        public void CalculateDistance_DoitRetournerLaBonneDistance()
        {
            // Arrange
            int x1 = 0, y1 = 0;
            int x2 = 3, y2 = 4;
            double expectedDistance = 5; // Triangle 3-4-5

            // Act
            double distance = MathHelpers.Distance(x1, y1, x2, y2);

            // Assert
            Assert.AreEqual(expectedDistance, distance, "La distance euclidienne calculée est incorrecte.");
        }
    }
}