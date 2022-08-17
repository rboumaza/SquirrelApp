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
                        MessageBox.Show($"Version actuelle: {mgr.Result.CurrentlyInstalledVersion()}");
                        //vérification des mises à jour
                        var updates = await mgr.Result.CheckForUpdate();
                       
                        if (updates.ReleasesToApply.Any()) 
                        {
                            MessageBox.Show("Nouvelle mise à jour disponible");
                            var release = await mgr.Result.UpdateApp();

                            MessageBox.Show($"Mise à jour appliquée, nouvelle version ; {release.Version}");
                           UpdateManager.RestartApp();
                        }
                    }
                }
            }
            catch (Exception e)
            {   
                MessageBox.Show(e.Message);
            }
        }
    }
}
