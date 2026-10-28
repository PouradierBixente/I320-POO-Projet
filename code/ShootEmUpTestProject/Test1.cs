using ShootEmUp;

namespace ShootEmUpTestProject
{
    public class TestsEnsemble
    {
        public void Player_Initialisation_DoitAvoirValeursParDefaut()
        {
            // Arrange & Act
            var player = new Player();

            // Assert
            player.vieplayer.Should().BeGreaterThan(0);
            player.IsAlive.Should().BeTrue();
            player.PositionX.Should().Be(0);
        }


        public void Player_TakeDamage_DoitReduireLaSante()
        {
            // Arrange
            var player = new Player { Health = 100 };

            // Act
            player.TakeDamage(20);

            // Assert
            player.Health.Should().Be(80);
        }


        public void Player_TakeFatalDamage_DoitTuerLeJoueur()
        {
            // Arrange
            var player = new Player { Health = 10 };

            // Act
            player.TakeDamage(20);

            // Assert
            player.Health.Should().Be(0);
            player.IsAlive.Should().BeFalse();
        }

        public void Ennemie_Move_DoitMettreAJourLaPosition()
        {
            // Arrange
            var ennemie = new Ennemie { PositionX = 10, PositionY = 50, Speed = 5 };

            // Act
            ennemie.MoveDown();

            // Assert
            ennemie.PositionY.Should().Be(55);
        }


        public void Ennemie_Collision_DoitRetournerVraiSiSuperposition()
        {
            // Arrange
            var ennemie = new Ennemie { PositionX = 10, PositionY = 10, Width = 20, Height = 20 };
            var tir = new Tir { PositionX = 15, PositionY = 15, Width = 5, Height = 5 };

            // Act
            bool hasCollision = ennemie.CheckCollision(tir);

            // Assert
            hasCollision.Should().BeTrue();
        }

        public void Tir_Update_DoitAvancerSelonSaVitesse()
        {
            // Arrange
            var tir = new Tir { PositionY = 100, Speed = 10 };

            // Act
            tir.Update();

            // Assert
            tir.PositionY.Should().Be(90);
        }


        public void Tir_IsOutOfBounds_DoitRetournerVraiSiHorsEcran()
        {
            // Arrange
            var tir = new Tir { PositionY = -10 };

            // Act
            bool isOut = tir.IsOutOfBounds();

            // Assert
            isOut.Should().BeTrue();
        }

        public void CalculateDistance_DoitRetournerLaBonneDistance(int x1, int y1, int x2, int y2, double expectedDistance)
        {
            // Act
            double distance = MathHelpers.CalculateDistance(x1, y1, x2, y2);

            // Assert
            distance.Should().Be(expectedDistance);
        }


        public void Clamp_DoitGarderValeurDansLesLimites()
        {
            // Act
            int resultBelow = MathHelpers.Clamp(-5, 0, 100);
            int resultAbove = MathHelpers.Clamp(150, 0, 100);
            int resultInside = MathHelpers.Clamp(50, 0, 100);

            // Assert
            resultBelow.Should().Be(0);
            resultAbove.Should().Be(100);
            resultInside.Should().Be(50);
        }
    }
}