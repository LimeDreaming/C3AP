using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static C3AP_Client.Form1;

namespace C3AP_Client
{
    public partial class Tracker : Form
    {
        private string logFilePath = RoomSeed + "_items.c3apsave";
        private bool _dragging = false;
        private Point _start_point = new Point(0, 0);
        public Tracker()
        {
            InitializeComponent();
        }

        private void onMouseDown(object sender, MouseEventArgs e)
        {
            _dragging = true;
            _start_point = new Point(e.X, e.Y);
        }
        private void onMouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this._start_point.X, p.Y - this._start_point.Y);
            }
        }
        private void onMouseRelease(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }

        private void hideForm(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void update_Tick(object sender, EventArgs e)
        {
            
        }

        private void updateCrystalImage(long APItemID) 
        {
            for (int i = 1; i <= 25; i++)
            {
                PictureBox crystalPB =  this.Controls["crystal_lvl" + i] as PictureBox;

                if(crystalPB != null)
                {
                    if (!File.Exists(logFilePath)) return;

                    foreach (var line in File.ReadLines(logFilePath))
                    {
                        try
                        {
                            var item = JsonSerializer.Deserialize<ReceivedItemData>(line);

                            if (item != null)
                            {
                                var match = SharedAdresses.CrystalItems.FirstOrDefault(item => item.LevelAPItemId == item.LevelAPItemId);

                                if (match != null)
                                {
                                    int level = match.Level;
                                    if(i == level)
                                    {
                                        crystalPB.Image = Properties.Resources.crystal;
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Fehler beim Laden eines Log-Eintrags: " + ex.Message);
                            Console.WriteLine("Fehler beim Laden eines Log-Eintrags: " + ex.Message);
                        }
                    }
                    
                }
            }
            
        }
    }
}
