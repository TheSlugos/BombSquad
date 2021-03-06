﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

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
        Button _resetButton;
        Label _lbBombsLeft;
        Label _lbTimer;
        int _Bombs;
        int _Columns;
        int _Rows;

        int _timerInterval = 20;
        MouseButtons _buttonDown = MouseButtons.None;
        bool _twoButtons = false;

        const int COLUMNS = 8;
        const int ROWS = 8;
        const int BOMBS = 10;
        const int CELLWIDTH = 30;
        const int HEADER = 50;

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
            // read current settings from file and set Bombs,Rows,Columns
            // for now just start with EASY
            menuEasy.Checked = true;
            menuMedium.Checked = false;
            menuHard.Checked = false;
            _Bombs = BOMBS;
            _Columns = COLUMNS;
            _Rows = ROWS;

            _Map = null;

            // setup the form
            this.Text = "Bomb Squad";
            this.MaximizeBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;

            // setup the picturebox
            _frame = new PictureBox();
            _frame.Parent = this;
            _frame.BackColor = Color.Black;
            //_frame.Dock = DockStyle.Bottom;
            _frame.MouseDown += PictureBox_MouseDown;
            _frame.MouseUp += PictureBox_MouseUp;

            // setup the reset button
            _resetButton = new Button();
            _resetButton.Parent = this;
            _resetButton.Size = new Size(40, 40);
            _resetButton.Click += _resetButton_Click;
            _resetButton.Image = Properties.Resources.Smiley;

            // setup the text boxes
            _lbBombsLeft = new Label();
            _lbBombsLeft.Parent = this;
            _lbBombsLeft.Font = new Font("Verdana", 20.0f);
            _lbBombsLeft.Text = "000";
            _lbBombsLeft.AutoSize = true;
            _lbTimer = new Label();
            _lbTimer.Parent = this;
            _lbTimer.Font = new Font("Verdana", 20.0f);
            _lbTimer.Text = "000";
            _lbTimer.AutoSize = true;

            // for two-mouse button detection
            _actionCell = new Point(-1, -1);
            mouseButtonTimer.Interval = _timerInterval;

            // create the map
            _Map = new TheMap(_Columns, _Rows, _Bombs, CELLWIDTH, _lbBombsLeft, _lbTimer);

            // setup the form
            FormLayout();
        }

        private void FormLayout()
        {
            // set form client rect to required size
            this.ClientSize = new Size(_Columns * CELLWIDTH,
                            _Rows * CELLWIDTH + HEADER + menuStrip1.Height);

            // set size and position of the picturebox frame
            _frame.Size = new Size(_Columns * CELLWIDTH, _Rows * CELLWIDTH);
            _frame.Location = new Point(0, menuStrip1.Height + HEADER);

            // setup the graphics device
            _surface = new Bitmap(_frame.Size.Width, _frame.Size.Height);
            _frame.Image = _surface;
            _device = Graphics.FromImage(_surface);

            _resetButton.Location = new Point((ClientRectangle.Width - _resetButton.Width) / 2,
                ((HEADER - _resetButton.Height) / 2) + menuStrip1.Height);
            _lbBombsLeft.Location = new Point(10, ((HEADER - _lbBombsLeft.Height) / 2) + menuStrip1.Height);
            _lbTimer.Location = new Point(ClientSize.Width - 10 - _lbBombsLeft.Width, ((HEADER - _lbBombsLeft.Height) / 2) + menuStrip1.Height);

            UpdateMap();
        }

        private void _resetButton_Click(object sender, EventArgs e)
        {
            _Map.InitialiseMap();
            UpdateMap();
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            // do not handle if game has ended
            if (_Map.GameState == TheMap.GameStateEnum.INIT || _Map.GameState == TheMap.GameStateEnum.PLAY)
            {
                Trace.WriteLine("MouseUp");

                // get cell for this action
                Point testPoint = GetCellCoordinates(e.X, e.Y);

                // check if action is on same cell, only handle actions in same cell
                // as mouse down
                if (testPoint == _actionCell)
                {
                    if (_twoButtons)
                    {
                        _twoButtons = false;
                        //_buttonDown = MouseButtons.None;
                        //mouseButtonTimer.Enabled = false;
                        // send two button command
                        _Map.QuickClear(_actionCell.X, _actionCell.Y);
                    }
                    else if (_buttonDown != MouseButtons.None)
                    {
                        //mouseButtonTimer.Enabled = false;
                        //_buttonDown = MouseButtons.None;
                        // new mouse up action

                        // send the click event
                        _Map.Click(_actionCell.X, _actionCell.Y, e.Button);
                    }
                }
                else
                {
                    // un-highlight cell
                }

                UpdateMap();

                // check for new highscore
                if (_Map.GameState == TheMap.GameStateEnum.WON)
                {
                    _Map.CheckHighScores();
                }

                // reset the action cell
                _actionCell.X = _actionCell.Y = -1;
                mouseButtonTimer.Enabled = false;
                _buttonDown = MouseButtons.None;
            }
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            // don't register mouse clicks if game has ended
            if (_Map.GameState == TheMap.GameStateEnum.INIT || _Map.GameState == TheMap.GameStateEnum.PLAY)
            {
                if (_buttonDown == MouseButtons.None)
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
                                mouseButtonTimer.Enabled = false;
                            }
                            break;

                        case MouseButtons.Right:
                            if (e.Button == MouseButtons.Left)
                            {
                                _twoButtons = true;
                                mouseButtonTimer.Enabled = false;
                            }
                            break;
                    }
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

        private void menuClick(object sender, EventArgs e)
        {
            if ( sender == menuNew)
            {
                _Map.InitialiseMap();
                UpdateMap();
            }
            else if ( sender == menuExit)
            {
                Close();
            }
            else if ( sender == menuHighScores)
            {
                _Map.ShowHighScores();
            }
            else if ( sender == menuEasy )
            {
                // set new rows,columns,bombs
                _Bombs = 10;
                _Rows = 8;
                _Columns = 8;

                // Resize form and layout
                _Map.InitialiseMap(_Columns, _Rows, _Bombs);
                FormLayout();

                // Menu stuff
                menuMedium.Checked = false;
                menuEasy.Checked = true;
                menuHard.Checked = false;
            }
            else if ( sender == menuMedium )
            {
                // set new rows,columns,bombs
                _Bombs = 40;
                _Rows = 16;
                _Columns = 16;

                // Resize form and layout
                _Map.InitialiseMap(_Columns, _Rows, _Bombs);
                FormLayout();

                // Menu stuff
                menuMedium.Checked = true;
                menuEasy.Checked = false;
                menuHard.Checked = false;
            }
            else
            {
                // set new rows,columns,bombs
                _Bombs = 99;
                _Rows = 16;
                _Columns = 30;

                // Resize form and layout
                _Map.InitialiseMap(_Columns, _Rows, _Bombs);
                FormLayout();

                // Menu stuff
                menuMedium.Checked = false;
                menuEasy.Checked = false;
                menuHard.Checked = true;

            }
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            FormLayout();
        }
    }
}
