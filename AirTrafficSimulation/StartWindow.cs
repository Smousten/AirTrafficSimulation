using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirTrafficSimulation
{
    public partial class StartScreen : Form
    {
        public StartScreen()
        {
            InitializeComponent();
        }

        private void StartSimulation_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form2 = new GameWindow();
            form2.Show();
            

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
