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

            

            await CheckUpdate();
            Application.Run(new Form1());
        }


        private static async Task CheckUpdate()
        {
            try
            {
                using (var mgr = UpdateManager.GitHubUpdateManager("https://github.com/rboumaza/SquirrelApp"))
                {
                    if (mgr.Result.IsInstalledApp)
                    {
                        //vérification des mises à jour
                        var updates = await mgr.Result.CheckForUpdate();                     
                        if (updates.ReleasesToApply.Any()) 
                        {
                            MessageBox.Show("Nouvelle mise à jour disponible");
                            var release = await mgr.Result.UpdateApp();
                            MessageBox.Show($"Mise à jour avec succés de la nouvelle version : {release.Version}");
                        }
                    }
                }
            }
            catch (Exception e)
            {   
                MessageBox.Show("Sources introuvables sur Git :" + e.Message);
            }
        }
    }
}
