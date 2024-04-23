using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace theRealOnePythin
{
    internal class Python
    {
        List<Dictionary<char, int>> PythonBody = new List<Dictionary<char, int>>();

        Brush pythonColor = new SolidBrush(Color.Orange);

        private static Pen outlinePen = new Pen(Color.Black, 1);

        public List<Dictionary<char, int>> getPythonBody() => PythonBody;

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

        private void EatApple(int tileSize, int formSize, Apple apple)
        {
            int pyLen = PythonBody.Count -1;
            if (PythonBody[pyLen]['h'] != apple.apple['h'] | PythonBody[pyLen]['w'] != apple.apple['w'])
            {
                PythonBody.RemoveAt(0);
            }
            else
            {
                apple.eaten += 1;
                apple.AppleSpawn(formSize, tileSize, this);
            }
        }

        public void Crawl(char pressedKey, int tileSize, int formSize, Apple apple)
        {
            EatApple(tileSize, formSize, apple);
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

            for(int i = 0; i < PythonBody.Count; i++)
            {
                var item = PythonBody[i];

                g.FillRectangle(pythonColor, item['w'], item['h'], tileSize, tileSize);
                g.DrawRectangle(outlinePen, item['w'], item['h'], tileSize, tileSize);
            }
        }
    }
}