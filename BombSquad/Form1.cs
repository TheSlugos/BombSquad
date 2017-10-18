using System;
using System.Drawing;
using System.Windows.Forms;

namespace BombSquad
{
    /// <summary>
    /// Form1 - the container form for the BombSquad application.
    /// Contains the controls and captures mouse events for the application.
    /// </summary>
    public partial class Form1 : Form
    {
        PictureBox _frame;
        Graphics _device;
        Bitmap _surface;
        TheMap _Map;

        const int COLUMNS = 9;
        const int ROWS = 9;
        const int BOMBS = 10;
        const int CELLWIDTH = 40;

        /// <summary>
        /// Constructor
        /// Initialises the controls.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // create the map
            _Map = new TheMap( COLUMNS, ROWS, BOMBS );

            // setup the form
            this.Text = "Bomb Squad";
            this.MaximizeBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.AutoSize = true;
            this.ClientSize = new Size( COLUMNS * CELLWIDTH, ROWS * CELLWIDTH );

            // setup the picturebox
            _frame = new PictureBox();
            _frame.Parent = this;
            _frame.BackColor = Color.Black;
            _frame.Dock = DockStyle.Fill;
            _frame.MouseClick += new MouseEventHandler( PictureBox_Click );
            //_frame.MouseDown += PictureBox_MouseDown;
            //_frame.MouseUp += PictureBox_MouseUp;

            // setup the graphics device
            _surface = new Bitmap( this.Size.Width, this.Size.Height );
            _frame.Image = _surface;
            _device = Graphics.FromImage( _surface );

            UpdateMap();
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Click - calculates which cell on the map is clicked and passes cell coordinates
        /// and which mouse button was clicked to the map for handling
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox_Click( object sender, MouseEventArgs e )
        {
            Point p = GetCellCoordinates(e.X, e.Y);
            
            // pass clicked cell into the map
            _Map.Click( p.X, p.Y, e.Button );

            UpdateMap();
            //MessageBox.Show( String.Format( "Cell: ({0},{1})", cellx, celly ) );
        }

        /// <summary>
        /// Determines the cell that a mouse event occurred in
        /// </summary>
        /// <param name="x">X coordinate of the mouse event</param>
        /// <param name="y">Y coordinate of the mouse event</param>
        /// <returns>Point object containing the cell coordinates</returns>
        private Point GetCellCoordinates(int x, int y)
        {
            return new Point(x / CELLWIDTH, y / CELLWIDTH);
        }

        /// <summary>
        /// FormClosed - disposes of picturebox and graphics device objects
        /// </summary>
        /// <param name="sender">Calling object</param>
        /// <param name="e">Event handler arguments</param>
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            _device.Dispose();
            _surface.Dispose();
        }

        /// <summary>
        /// UpdateMap
        /// Draws the map
        /// </summary>
        private void UpdateMap()
        {
            // draw the latest game state
            _Map.Draw( _device, _surface );
            // refresh the screen
            _frame.Image = _surface;
        }
    }
}
