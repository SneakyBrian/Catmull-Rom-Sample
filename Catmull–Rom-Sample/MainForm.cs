using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Catmull_Rom_Sample
{
    public partial class MainForm : Form
    {
        private List<PointF> _keyPoints = new List<PointF>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_MouseClick(object sender, MouseEventArgs e)
        {
            _keyPoints.Add(new PointF(e.X, e.Y));

            using (var gfx = this.CreateGraphics())
            {
                gfx.DrawRectangle(Pens.Red, new Rectangle(e.X, e.Y, 1, 1));
            }

            if (_keyPoints.Count >= 4)
                CalculateButton.Enabled = true;
        }

        private void CalculateButton_Click(object sender, EventArgs e)
        {
            var keyPoints = new List<PointF>();

            keyPoints.AddRange(_keyPoints);
            keyPoints.Add(_keyPoints[0]);
            keyPoints.Add(_keyPoints[1]);
            keyPoints.Add(_keyPoints[2]);

            var splinePoints = CatmullRomSpline.Generate(keyPoints.ToArray(), 10);

            using (var gfx = this.CreateGraphics())
            {
                gfx.DrawLines(Pens.Green, splinePoints);
            }
        }
    }
}
