using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SquirrelApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var  assembly = Assembly.GetExecutingAssembly();
            var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            label3.Text = fvi.FileVersion;
        }

        
    }
}
