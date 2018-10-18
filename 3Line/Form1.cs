using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3Line {
    public partial class GameOfGodsForm : Form {

        const int fieldWidth = 13;
        const int fieldHeight = 11;
        const int cellSize = 50;
        static Dictionary<int, int> values = new Dictionary<int, int> {
            { -4878899,  1 },
            { -4415197,  2 },
            { -14250703, 3 },
            { -12343820, 4 },
            { -6473689,  5 },
            { -2280570, -1 },
            { -7549182,  100 }
        };

        int[,] field;
        List<Info> infos;
        Label[] labels;
        Label[] labelInfos;

        public GameOfGodsForm() {
            InitializeComponent();
            infos = new List<Info>();
            labels = new Label[4];
            for (int i = 0; i < 4; i++) {
                labels[i] = new Label() { Location = new Point(458, 9 + 93 * i), Size = new Size(119, 16), BorderStyle = BorderStyle.Fixed3D };
                Controls.Add(labels[i]);
            }
            labels[0].BackColor = Color.White;
            labels[1].BackColor = Color.Lime;
            labels[2].BackColor = Color.Black;
            labels[3].BackColor = Color.Red;
            labelInfos = new Label[4];
            for (int i = 0; i < 4; i++) {
                labelInfos[i] = new Label() { AutoSize = true, Location = new Point(458 , 25 + 93 * i) };
                Controls.Add(labelInfos[i]);
            }
        }

        private void screenshotClick(object sender, EventArgs e) {
            field = new int[fieldWidth, fieldHeight];
            infos.Clear();
            labelInfos[0].Text = labelInfos[1].Text = labelInfos[2].Text = labelInfos[3].Text = "";
            if (GetPixels()) {
                Search();
                ShowInfo();
            }
        }

        private bool GetPixels() {
            int step = 63;
            Bitmap screen = new Bitmap(505, 379);
            Graphics graph = Graphics.FromImage(screen);
            Opacity = 0;
            graph.CopyFromScreen(779, 467, 0, 0, new Size(505, 379));
            Opacity = 100;
            if (screen.GetPixel(30, 5).ToArgb() != -12092533) {
                graph.Dispose();
                return false;
            }
            SolidBrush bruch = new SolidBrush(Color.Aqua);
            try {
                for (int x = 2; x < fieldWidth - 2; x++)
                    for (int y = 2; y < fieldHeight - 2; y++) {
                        Color color = screen.GetPixel((x - 2) * step, (y - 2) * step);
                        field[x, y] = values[color.ToArgb()];
                        bruch.Color = color;
                        graph.FillRectangle(bruch, (x - 2) * cellSize, (y - 2) * cellSize, cellSize, cellSize);
                    }
                fieldPanel.BackgroundImage = screen;
            }
            catch (Exception) {
                return false;
            }
            finally {
                graph.Dispose();
                bruch.Dispose();
            }
            return true;
        }

        private void Search() {
            RightSearch();
            DownSearch();
        }

        private void RightSearch() {
            for (int x = 2; x < fieldWidth - 3; x++)
                for (int y = 2; y < fieldHeight - 2; y++) {
                    int cell = field[x, y];
                    int right = field[x + 1, y];
                    Info infoRight = new Info(x - 2, y - 2, x - 1, y - 2);
                    // обмен с правой
                    if (cell != right) {
                        // слева для right
                        if (right == field[x - 1, y] && right == field[x - 2, y])
                            infoRight.AddDamage(right);
                        // справа для cell
                        if (cell == field[x + 2, y] && cell == field[x + 3, y])
                            infoRight.AddDamage(cell);
                        int equals = Vertical5(x, y, right);
                        if (equals > 2)
                            infoRight.AddDamage(right, equals);
                        equals = 0;
                        equals = Vertical5(x + 1, y, cell);
                        if (equals > 2)
                            infoRight.AddDamage(cell, equals);
                    }
                    if (infoRight.HasValue())
                        infos.Add(infoRight);
                }
        }

        private void DownSearch() {
            for (int x = 2; x < fieldWidth - 2; x++)
                for (int y = 2; y < fieldHeight - 3; y++) {
                    int cell = field[x, y];                                  
                    int down = field[x, y + 1];
                    Info infoDown = new Info(x - 2, y - 2, x - 2, y - 1);
                    // обмен с нижней
                    if (cell != down) {
                        if (down == field[x, y - 1] && down == field[x, y - 2])
                            infoDown.AddDamage(down);
                        if (cell == field[x, y + 2] && cell == field[x, y + 3])
                            infoDown.AddDamage(cell);
                        int equals = Horizontal5(x, y, down);
                        if (equals > 2)
                            infoDown.AddDamage(down, equals);
                        equals = 0;
                        equals = Horizontal5(x, y + 1, cell);
                        if (equals > 2)
                            infoDown.AddDamage(cell, equals);
                    }
                    if (infoDown.HasValue())
                        infos.Add(infoDown);
                }
        }

        private int Vertical5(int x, int y, int cellValue) {
            int equals = 1;
            // сверху
            if (field[x, y - 1] == cellValue) {
                equals++;
                if (field[x, y - 2] == cellValue)
                    equals++;
            }
            // снизу
            if (field[x, y + 1] == cellValue) {
                equals++;
                if (field[x, y + 2] == cellValue)
                    equals++;
            }
            return equals;
        }

        private int Horizontal5(int x, int y, int cellValue) {
            int equals = 1;
            // слева
            if (field[x - 1, y] == cellValue) {
                equals++;
                if (field[x - 2, y] == cellValue)
                    equals++;
            }
            // справа
            if (field[x + 1, y] == cellValue) {
                equals++;
                if (field[x + 2, y] == cellValue)
                    equals++;
            }
            return equals;
        }

        private void ShowInfo() {
            int n = 0;
            using (Graphics graph = fieldPanel.CreateGraphics()) {
                using (Pen pen = new Pen(Color.AliceBlue, 5f)) {
                    foreach (Info info in infos.OrderByDescending(inf => inf.Value()).Take(4)) {
                        labelInfos[n].Text = info.ToString();
                        pen.Color = labels[n].BackColor;
                        graph.DrawLine(pen, info.X1 * cellSize + 25, info.Y1 * cellSize + 25, info.X2 * cellSize + 25, info.Y2 * cellSize + 25);
                        n++;
                    }
                }
            }
        }

    }
}
