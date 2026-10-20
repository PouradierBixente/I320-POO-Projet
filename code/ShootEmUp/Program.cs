using System.Runtime.InteropServices;

namespace ShootEmUp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            List<Barriere> GroupBarriere = new List<Barriere>();
            GroupBarriere.Add(new Barriere(Config.BARRIERE_SIZE / 2, GameSpace.HEIGHT - GameSpace.HEIGHT / 4));
            GroupBarriere.Add(new Barriere(GameSpace.WIDTH - Config.BARRIERE_SIZE / 2, GameSpace.HEIGHT - GameSpace.HEIGHT / 4));
            GroupBarriere.Add(new Barriere(GameSpace.WIDTH - GameSpace.WIDTH / 2, GameSpace.HEIGHT - GameSpace.HEIGHT / 4));
            GroupBarriere.Add(new Barriere(GameSpace.WIDTH - GameSpace.WIDTH / 4, GameSpace.HEIGHT - GameSpace.HEIGHT / 4));
            GroupBarriere.Add(new Barriere(GameSpace.WIDTH / 4, GameSpace.HEIGHT - GameSpace.HEIGHT / 4));

            // Démarrage
            Application.Run(new GameSpace(new Player(GameSpace.WIDTH / 2, GameSpace.HEIGHT - GameSpace.HEIGHT / 8), GroupBarriere));
        }
    }
}