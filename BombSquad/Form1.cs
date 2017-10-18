﻿using System;
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
        Point _actionCell;
        int _timerInterval = 20;
        MouseButtons _buttonDown = MouseButtons.None;
        bool _twoButtons = false;

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

        /// <summary>
        /// Initialises game objects and controls
        /// </summary>
        /// <param name="sender">Caller object</param>
        /// <param name="e">Event parameters</param>
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
            //_frame.MouseClick += new MouseEventHandler( PictureBox_Click );
            _frame.MouseDown += PictureBox_MouseDown;
            _frame.MouseUp += PictureBox_MouseUp;
            _actionCell = new Point(-1, -1);
            mouseButtonTimer.Interval = _timerInterval;
            
            // setup the graphics device
            _surface = new Bitmap( this.Size.Width, this.Size.Height );
            _frame.Image = _surface;
            _device = Graphics.FromImage( _surface );

            UpdateMap();
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if ( _twoButtons)
            {
                _twoButtons = false;
                _buttonDown = MouseButtons.None;
                mouseButtonTimer.Enabled = false;
                // send two button command
                MessageBox.Show("Two Buttons");
            }
            else if ( _buttonDown != MouseButtons.None )
            {
                mouseButtonTimer.Enabled = false;
                _buttonDown = MouseButtons.None;
                // new mouse up action
                Point testPoint = GetCellCoordinates(e.X, e.Y);
                // check if action is on same cell
                if ( testPoint == _actionCell)
                {
                    // send the click event
                    _Map.Click(_actionCell.X, _actionCell.Y, e.Button);
                }
                else
                {
                    // un-highlight cell
                }

                UpdateMap();
                
                // reset the action cell
                _actionCell.X = _actionCell.Y = -1;
            }
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            //if ( _actionCell.X == -1 && _actionCell.Y == -1 )
            //{
            //    // new mouse down action
            //    // get cell for this action
            //    _actionCell = GetCellCoordinates(e.X, e.Y);

            //    // highlight cell
            //}
            if ( _buttonDown == MouseButtons.None )
            {
                // new button down registered
                _actionCell = GetCellCoordinates(e.X, e.Y);
                _buttonDown = e.Button;
                mouseButtonTimer.Enabled = true;
            }
            else
            {
                // double mouse button, only occurs if timer has not ticked
                switch (_buttonDown)
                {
                    case MouseButtons.Left:
                        if (e.Button == MouseButtons.Right)
                        {
                            _twoButtons = true;
                        }
                        break;

                    case MouseButtons.Right:
                        if ( e.Button == MouseButtons.Left)
                        {
                            _twoButtons = true;
                        }
                        break;
                }
            }
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

        private void mouseButtonTimer_Tick(object sender, EventArgs e)
        {
            mouseButtonTimer.Enabled = false;
            _twoButtons = false;
        }
    }
}
