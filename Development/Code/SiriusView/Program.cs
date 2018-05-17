using System;
using System.Windows.Forms;
using SiriusView.LockerTrialVersion;

namespace SiriusView
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
              TrialMaker t = new TrialMaker("SiriusQuality2",Environment.GetFolderPath (Environment.SpecialFolder.System) + "\\rxsv.reg",
                Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\tndaxlasv.dbf",
                "E-mail: siriusquality@clermont.inra.fr",
                30, 100, "257");
              Console.WriteLine(Environment.GetFolderPath(Environment.SpecialFolder.System));

           byte[] MyOwnKey = { 97, 150, 1, 5, 84, 21, 7, 63,
            4, 54, 87, 56, 123, 10, 3, 62,
            7, 9, 20, 36, 37, 21, 121, 57};
            t.TripleDESKey = MyOwnKey;

            TrialMaker.RunTypes RT = t.ShowDialog();
            bool is_trial;
            if (RT != TrialMaker.RunTypes.Expired)
            {
                if (RT == TrialMaker.RunTypes.Full)
                    is_trial = false;
                else
                    is_trial = true;
                Application.Run(new MainForm(is_trial));
            }
        }
    }
}
