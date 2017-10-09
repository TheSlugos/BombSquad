using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BombSquad
{
    public partial class Form1 : Form
    {
        PictureBox m_frame;
        Graphics m_device;
        Bitmap m_surface;
        Timer m_timer;
        TheMap m_Map;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            m_Map = new TheMap(10, 10, 1000);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
