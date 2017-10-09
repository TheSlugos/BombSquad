using System;
using System.Drawing;
using System.Windows.Forms;

namespace BombSquad
{
    public partial class Form1 : Form
    {
        PictureBox _frame;
        Graphics _device;
        Bitmap _surface;
        Timer _timer;
        TheMap _Map;

        const int DIMENSION = 9;
        const int BOMBS = 10;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // create the map
            _Map = new TheMap(DIMENSION, DIMENSION, BOMBS);

            // setup the form
            this.Text = "Bomb Squad";
            this.MaximizeBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.AutoSize = true;

            // setup the picturebox
            _frame = new PictureBox();
            _frame.Parent = this;
            _frame.BackColor = Color.Black;
            _frame.Size = new Size( 360, 360 );
            _frame.MouseClick += new MouseEventHandler( PictureBox_Click );

            // setup the graphics device
            _surface = new Bitmap( this.Size.Width, this.Size.Height );
            _frame.Image = _surface;
            _device = Graphics.FromImage( _surface );

            _Map.Draw( _device, _surface );
            _frame.Image = _surface;
        }

        private void PictureBox_Click( object sender, MouseEventArgs e )
        {
            // me.X, me.Y, me.Button
            System.Windows.Forms.MessageBox.Show( e.X.ToString() );
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            _device.Dispose();
            _surface.Dispose();
        }
    }
}
