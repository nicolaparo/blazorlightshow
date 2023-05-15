namespace NicolaParo.LightShow.App
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            AppDomain.CurrentDomain.UnhandledException += HandleUnhandledException;
            Application.Run(new ApplicationForm());
        }

        private static void HandleUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.ToString(), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}