using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C3AP_Client
{
    public partial class ClientMessageBox : Form
    {
        private bool _dragging = false;
        private Point _start_point = new Point(0, 0);
        public ClientMessageBox(string message, string title)
        {
            InitializeComponent();


            label1.Text = message;

            this.Text = title;

            this.StartPosition = FormStartPosition.CenterParent;

            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        public ClientMessageBox()
        {
            InitializeComponent();
        }

        public static void Show(string message, string title = "Archipelago")
        {
            using (var customBox = new ClientMessageBox(message, title))
            {
                customBox.ShowDialog();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close(); // Schließt das Custom-Fenster
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
    }
}