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

            //mise à jour automatique
            await CheckUpdate();

            Application.Run(new Form1());
        }


        private static async Task CheckUpdate()
        {
            try
            {
                //----------------Utilisation partage réseau-----------------------------------------------
                //using( var mgr = new UpdateManager("C:\\temp\\SquirrelApp\\Releases"))
                //{
                //    await mgr.UpdateApp();
                //}

                //-------------Utilisation repo Internet ---------------------------------------------
                using (var mgr = UpdateManager.GitHubUpdateManager("https://github.com/rboumaza/SquirrelApp"))
                {
                    if (mgr.Result.IsInstalledApp)
                    {
                        //------------vérification des mises à jour
                        var updates = await mgr.Result.CheckForUpdate();                     
                        if (updates.ReleasesToApply.Any()) 
                        {
                            MessageBox.Show("Nouvelle mise à jour disponible");

                            //--------Application mise à jour--------------
                            var release = await mgr.Result.UpdateApp();
                            MessageBox.Show($"Mise à jour réalisée avec succés de la nouvelle version : {release.Version}");
                        }
                    }
                }
            }
            catch (Exception e)
            {   
                MessageBox.Show("Sources introuvables sur le dépôt distant. Détail Exception :" + e.Message);
            }
        }
    }
}
