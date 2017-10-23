using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace HighScoreDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtName1.Text) || String.IsNullOrWhiteSpace(txtName2.Text)
                || String.IsNullOrWhiteSpace(txtName3.Text) || String.IsNullOrWhiteSpace(txtScore1.Text)
                || String.IsNullOrWhiteSpace(txtScore2.Text) || String.IsNullOrWhiteSpace(txtScore3.Text))
            {
                MessageBox.Show("All text boxes need a value to be saved.");
            }
            else
            {
                DataSet ds = new DataSet("BombSquad");

                DataTable table = new DataTable("CurrentLevel");
                DataColumn column = new DataColumn("Level");
                column.DataType = Type.GetType("System.String");
                table.Columns.Add(column);

                DataRow row = table.NewRow();
                row["Level"] = "Easy";
                table.Rows.Add(row);
                ds.Tables.Add(table);
                
                table = new DataTable("Scores");
                column = new DataColumn("Level");
                column.DataType = Type.GetType("System.String");
                table.Columns.Add(column);

                column = new DataColumn("Name");
                column.DataType = Type.GetType("System.String");
                table.Columns.Add(column);

                column = new DataColumn("Score");
                column.DataType = Type.GetType("System.String");
                table.Columns.Add(column);

                row = table.NewRow();
                row["Level"] = "Easy";
                row["Name"] = txtName1.Text;
                row["Score"] = txtScore1.Text;
                table.Rows.Add(row);

                row = table.NewRow();
                row["Level"] = "Medium";
                row["Name"] = txtName2.Text;
                row["Score"] = txtScore2.Text;
                table.Rows.Add(row);

                row = table.NewRow();
                row["Level"] = "Hard";
                row["Name"] = txtName3.Text;
                row["Score"] = txtScore3.Text;
                table.Rows.Add(row);

                ds.Tables.Add(table);

                // write to file
                ds.WriteXml("BombSquad.xml");
                ds.Dispose();
                table.Dispose();
                column.Dispose();
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            // look for file
            if (File.Exists("BombSquad.xml"))
            {
                // read in data
                XmlDocument xml = new XmlDocument();
                xml.Load("BombSquad.xml");

                // get stored level
                string level = xml.DocumentElement.FirstChild.InnerText;

                // get high scores
                XmlNodeList nl = xml.GetElementsByTagName("Scores");

                foreach(XmlNode node in nl)
                {
                    XmlElement element = (XmlElement)node;
                    string scoreLevel = element.GetElementsByTagName("Level")[0].InnerText;
                    string name = element.GetElementsByTagName("Name")[0].InnerText;
                    string score = element.GetElementsByTagName("Score")[0].InnerText;

                    if (scoreLevel == "Easy")
                    {
                        txtName1.Text = name;
                        txtScore1.Text = score;
                    }
                    else if (scoreLevel == "Medium")
                    {
                        txtName2.Text = name;
                        txtScore2.Text = score;
                    }
                    else if (scoreLevel == "Hard")
                    {
                        txtName3.Text = name;
                        txtScore3.Text = score;
                    }
                }
            }
            else
            {
                MessageBox.Show("Config file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
