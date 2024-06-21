using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using theRealOnePython;

namespace theRealOnePython
{
    internal class Python
    {
        List<Dictionary<char, int>> PythonBody = new List<Dictionary<char, int>>();

        Brush pythonColor = new SolidBrush(Color.Orange);

        private static Pen outlinePen = new Pen(Color.Black, 1);

        private readonly Settings settings;

        public Python(Settings _settings) 
        {
            settings = _settings;
        }

        public List<Dictionary<char, int>> getPythonBody() => PythonBody;

        public void InitializePythonBody()
        {
            int center = settings.getFormSize / 2;
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
                    {'h',center + settings.getTileSize},
                    {'w',center}
                }
            );
            PythonBody.Add(
                new Dictionary<char, int>
                {
                    {'h',center + settings.getTileSize * 2},
                    {'w',center}
                }
            );
        }

        public void PaintPythonBody(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            for (int i = 0; i < PythonBody.Count; i++)
            {
                var item = PythonBody[i];

                g.FillRectangle(pythonColor, item['w'], item['h'], settings.getTileSize, settings.getTileSize);
                g.DrawRectangle(outlinePen, item['w'], item['h'], settings.getTileSize, settings.getTileSize);
            }
        }

        public void Update(char pressedKey, Apple apple, System.Windows.Forms.Timer timer)
        {
            Crawl(pressedKey, apple);
            PythonCollision(timer);
            WallСollision(timer);
        }

        private void Death(System.Windows.Forms.Timer timer)
        {
            timer.Stop();
            MessageBox.Show("Python is death");
            Application.Restart();
            Environment.Exit(0);
        }

        private void PythonCollision(System.Windows.Forms.Timer timer)
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

        private void WallСollision(System.Windows.Forms.Timer timer)
        {
            if (
                PythonBody.Last()['h'] > settings.getFormSize - 1 ||
                PythonBody.Last()['w'] > settings.getFormSize - 1 ||
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
                apple.AppleSpawn(this);
            }
        }

        private void Crawl(char pressedKey, Apple apple)
        {
            EatApple(settings.getTileSize, settings.getFormSize, apple);
            switch (pressedKey)
            {
                case 's':
                    PythonBody.Add(new Dictionary<char, int>
                    {
                        {'h',PythonBody[PythonBody.Count-1]['h']+settings.getTileSize},
                        {'w',PythonBody[PythonBody.Count-1]['w']}
                    });
                    break;
                case 'w':
                    PythonBody.Add(new Dictionary<char, int>
                    {
                        {'h',PythonBody[PythonBody.Count-1]['h']-settings.getTileSize},
                        {'w',PythonBody[PythonBody.Count-1]['w']}
                    });
                    break;
                case 'd':
                    PythonBody.Add(new Dictionary<char, int>
                    {
                        {'h',PythonBody[PythonBody.Count-1]['h']},
                        {'w',PythonBody[PythonBody.Count-1]['w']+settings.getTileSize}
                    });
                    break;
                case 'a':
                    PythonBody.Add(new Dictionary<char, int>
                    {
                        {'h',PythonBody[PythonBody.Count-1]['h']},
                        {'w',PythonBody[PythonBody.Count-1]['w']-settings.getTileSize}
                    });
                    break;
            }
        }
    }
}