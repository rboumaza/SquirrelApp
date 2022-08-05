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

            using (var mgr = UpdateManager.GitHubUpdateManager("C:\\temp\\SquirrelApp\\Releases"))
            {
                await mgr.UpdateApp();
            }
            Application.Run(new Form1());
        }
    }
}
