using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TwoMouseClick
{
    public partial class Form1 : Form
    {
        PictureBox pb;
        MouseButtons buttonClicked;
        int timerInterval = 10;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load( object sender, EventArgs e )
        {
            pb = new PictureBox();
            pb.Parent = this;
            pb.BackColor = Color.Black;
            pb.Dock = DockStyle.Fill;
            buttonClicked = MouseButtons.None;
            pb.MouseDown += Pb_MouseDown;
            this.KeyDown += Form1_KeyDown;
            timer1.Interval = timerInterval;
        }

        private void Form1_KeyDown( object sender, KeyEventArgs e )
        {
            if ( e.KeyCode == Keys.Up )
            {
                timer1.Enabled = false;
                timerInterval += 10;
                timer1.Interval = timerInterval;
                Trace.WriteLine( String.Format( "Timer Interval is {0}", timer1.Interval ) );
            }

            if ( e.KeyCode == Keys.Down )
            {
                timer1.Enabled = false;
                timerInterval -= 10;
                if ( timerInterval < 10 )
                    timerInterval = 10;
                timer1.Interval = timerInterval;
                Trace.WriteLine( String.Format( "Timer Interval is {0}", timer1.Interval ) );
            }
        }

        private void Pb_MouseDown( object sender, MouseEventArgs e )
        {
            if ( buttonClicked == MouseButtons.None )
            {
                buttonClicked = e.Button;
                timer1.Enabled = true;
            }
            else
            {
                switch ( buttonClicked )
                {
                    case MouseButtons.Left:
                        if (e.Button == MouseButtons.Right )
                        {
                            Trace.WriteLine( "2 buttons" );
                            ClearButton();
                        }
                        break;

                    case MouseButtons.Right:
                        {
                            if ( e.Button == MouseButtons.Left )
                            {
                                Trace.WriteLine( "2 buttons" );
                                ClearButton();
                            }
                        }
                        break;
                }
            }
        }

        private void timer1_Tick( object sender, EventArgs e )
        {
            MouseButtons tmp = buttonClicked;
            ClearButton();
            Trace.WriteLine( tmp.ToString() );
            
        }

        private void ClearButton()
        {
            timer1.Enabled = false;
            buttonClicked = MouseButtons.None;
        }
    }
}
