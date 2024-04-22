using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theRealOnePythin
{
    internal class Python
    {
        List<Dictionary<char, int>> PythonBody = new List<Dictionary<char, int>>();

        Brush pythonColor = new SolidBrush(Color.Orange);

        public void InitializePythonBody(int center, int tile)
        {
            PythonBody.Add(
                new Dictionary<char, int>
                {
                    {'h',center},
                    {'w',center}
                }
            );
            PythonBody.Add(
                new Dictionary<char, int>
                {
                    {'h',center + tile},
                    {'w',center}
                }
            );
            PythonBody.Add(
                new Dictionary<char, int>
                {
                    {'h',center + tile * 2},
                    {'w',center}
                }
            );
        }

        private void Death(System.Windows.Forms.Timer timer)
        {
            timer.Stop();
            MessageBox.Show("Python is death");
            Application.Restart();
            Environment.Exit(0);
        }

        public void PythonCollision(System.Windows.Forms.Timer timer)
        {
            var lastPart = PythonBody.Last();
            foreach (Dictionary<char, int> pythonPart in PythonBody.Take(PythonBody.Count - 1))
            {
                if (pythonPart.SequenceEqual(lastPart))
                {
                    Death(timer);
                }
            }
        }

        public void WallСollision(int formSize, System.Windows.Forms.Timer timer)
        {
            if (
                PythonBody.Last()['h'] > formSize - 1 ||
                PythonBody.Last()['w'] > formSize - 1 ||
                PythonBody.Last()['h'] < 0 ||
                PythonBody.Last()['w'] < 0)
                Death(timer);
        }

        public void Crawl(char pressedKey, int tileSize)
        {
            PythonBody.RemoveAt(0);
            switch (pressedKey)
            {
                case 's':
                    PythonBody.Add(new Dictionary<char, int>
                    {
                        {'h',PythonBody[PythonBody.Count-1]['h']+tileSize},
                        {'w',PythonBody[PythonBody.Count-1]['w']}
                    });
                    break;
                case 'w':
                    PythonBody.Add(new Dictionary<char, int>
                    {
                        {'h',PythonBody[PythonBody.Count-1]['h']-tileSize},
                        {'w',PythonBody[PythonBody.Count-1]['w']}
                    });
                    break;
                case 'd':
                    PythonBody.Add(new Dictionary<char, int>
                    {
                        {'h',PythonBody[PythonBody.Count-1]['h']},
                        {'w',PythonBody[PythonBody.Count-1]['w']+tileSize}
                    });
                    break;
                case 'a':
                    PythonBody.Add(new Dictionary<char, int>
                    {
                        {'h',PythonBody[PythonBody.Count-1]['h']},
                        {'w',PythonBody[PythonBody.Count-1]['w']-tileSize}
                    });
                    break;
            }
        }

        public void PaintPythonBody(object sender, PaintEventArgs e, int tileSize)
        {
            Graphics g = e.Graphics;

            foreach (var item in PythonBody)
            {
                g.FillRectangle(pythonColor, item['w'], item['h'], tileSize, tileSize);
            }
        }
    }
}
