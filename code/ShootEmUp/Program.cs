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
            List<Ennemie> GroupeEnnemie = new List<Ennemie>();
            GroupeEnnemie.Add(new Ennemie(Config.ENNEMIE_SIZE / 2,Config.ENNEMIE_SIZE / 4));

            // Démarrage
            Application.Run(new GameSpace(new Player(GameSpace.WIDTH / 2, GameSpace.HEIGHT - GameSpace.HEIGHT / 8),GroupeEnnemie));
        }
    }
}