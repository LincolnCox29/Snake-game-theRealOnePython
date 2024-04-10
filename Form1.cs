using System.Runtime.CompilerServices;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace theRealOnePython
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            SetStyle(
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint, true
                );

            InitializeTimer();
        }

        char pressedKey = 's';
        bool pressed = false;

        Brush pythonColor = new SolidBrush(Color.Orange);

        List<Dictionary<char, int>> PythonBody = new List<Dictionary<char, int>>
        {
            new Dictionary<char, int>
            {
                {'h',200},
                {'w',200}
            },
            new Dictionary<char, int>
            {
                {'h',205},
                {'w',200}
            },
            new Dictionary<char, int>
            {
                {'h',210},
                {'w',200}
            },
        };

        private System.Windows.Forms.Timer timer;

        private void InitializeTimer()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 200;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Death()
        {
            timer.Stop();
            MessageBox.Show("Python is death");
            Application.Restart();
            Environment.Exit(0);
        }

        private void deathCertificate()
        {
            var lastPart = PythonBody.Last();
            foreach (Dictionary<char, int> pythonPart in PythonBody.Take(PythonBody.Count-1))
            {
                if (pythonPart.SequenceEqual(lastPart))
                {
                    Death();
                }
            }
        }

        private void isOnWinForm()
        {
            if (
                PythonBody.Last()['h'] > 400 ||
                PythonBody.Last()['w'] > 400 ||
                PythonBody.Last()['h'] < 0 ||
                PythonBody.Last()['w'] < 0)
            {
                Death();
            }
        }

        private void Crawl()
        {
            PythonBody.RemoveAt(0);
            switch (pressedKey)
            {
                case 's':
                    PythonBody.Add(new Dictionary<char, int>
                    {
                        {'h',PythonBody[PythonBody.Count-1]['h']+5},
                        {'w',PythonBody[PythonBody.Count-1]['w']}
                    });
                    break;
                case 'w':
                    PythonBody.Add(new Dictionary<char, int>
                    {
                        {'h',PythonBody[PythonBody.Count-1]['h']-5},
                        {'w',PythonBody[PythonBody.Count-1]['w']}
                    });
                    break;
                case 'd':
                    PythonBody.Add(new Dictionary<char, int>
                    {
                        {'h',PythonBody[PythonBody.Count-1]['h']},
                        {'w',PythonBody[PythonBody.Count-1]['w']+5}
                    });
                    break;
                case 'a':
                    PythonBody.Add(new Dictionary<char, int>
                    {
                        {'h',PythonBody[PythonBody.Count-1]['h']},
                        {'w',PythonBody[PythonBody.Count-1]['w']-5}
                    });
                    break;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Crawl();
            pressed = false;
            deathCertificate();
            isOnWinForm();
            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            foreach (var item in PythonBody)
            {
                g.FillRectangle(pythonColor, item['w'], item['h'], 5, 5);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!pressed)
            {
                switch (e.KeyCode)
                {
                    case Keys.W:
                        if (pressedKey != 's')
                            pressedKey = 'w';
                        break;
                    case Keys.A:
                        if (pressedKey != 'd')
                            pressedKey = 'a';
                        break;
                    case Keys.S:
                        if (pressedKey != 'w')
                            pressedKey = 's';
                        break;
                    case Keys.D:
                        if (pressedKey != 'a')
                            pressedKey = 'd';
                        break;
                }
                pressed = true;
            }
        }
    }
}