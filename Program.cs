using Squirrel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SquirrelApp
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static async Task Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //using( var mgr = new UpdateManager("C:\\temp\\SquirrelApp\\Releases"))
            //{
            //    await mgr.UpdateApp();
            //}

            //using (var mgr = UpdateManager.GitHubUpdateManager("https://github.com/rboumaza/SquirrelApp"))
            //{
            //    var release = await mgr.Result.UpdateApp();
            //}

            await Update();
            Application.Run(new Form1());
        }


        private static async Task Update()
        {
            try
            {
                using (var mgr = UpdateManager.GitHubUpdateManager("https://github.com/rboumaza/SquirrelApp"))
                {
                    if (mgr.Result.IsInstalledApp)
                    {
                        MessageBox.Show($"Current Version: {mgr.Result.CurrentlyInstalledVersion()}");
                        var updates = await mgr.Result.CheckForUpdate();
                        if (updates.ReleasesToApply.Any())
                        {
                            MessageBox.Show("Updates found. Applying updates.");
                            var release = await mgr.Result.UpdateApp();

                            //MessageBox.Show(CleanReleaseNotes(release.GetReleaseNotes(Path.Combine(mgr.RootAppDirectory, "packages"))),
                            //$"Casual Meter Update - v{release.Version}");

                            MessageBox.Show("Updates applied. Restarting app.");
                            //UpdateManager.RestartApp();
                        }
                    }
                }
            }
            catch (Exception e)
            {   //log exception and move on
                MessageBox.Show(e.Message);
            }
        }
    }
}
