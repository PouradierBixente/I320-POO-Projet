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

            // Démarrage
            Application.Run(new GameSpace(new Player(GameSpace.WIDTH / 2, GameSpace.HEIGHT - 150, "Joe"), new Tir(GameSpace.WIDTH / 2, GameSpace.HEIGHT - 150)));
        }
    }
}