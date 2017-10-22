using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AddAMenu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void levelClick(object sender, EventArgs e)
        {
            if (sender==easyToolStripMenuItem)
            {
                MessageBox.Show("Easy");
                easyToolStripMenuItem.Checked = true;
                mediumToolStripMenuItem.Checked = false;
                hardToolStripMenuItem.Checked = false;
            }
            else if ( sender == mediumToolStripMenuItem )
            {
                MessageBox.Show("Medium");
                easyToolStripMenuItem.Checked = false;
                mediumToolStripMenuItem.Checked = true;
                hardToolStripMenuItem.Checked = false;
            }
            else
            {
                MessageBox.Show("Hard");
                easyToolStripMenuItem.Checked = false;
                mediumToolStripMenuItem.Checked = false;
                hardToolStripMenuItem.Checked = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            easyToolStripMenuItem.Checked = true;
        }
    }
}
