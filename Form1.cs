using System.Runtime.CompilerServices;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Policy;

namespace theRealOnePython
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeSettings();

            InitializePythonBody();

            AppleSpawn();

            InitializeComponent();

            SetStyle(
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint, true
                );

            InitializeTimer();

            ClientSize = new Size(formSize, formSize);
        }

        static Dictionary<string, int> settings;

        Random rnd = new Random();

        char pressedKey = 's';
        bool pressed = false;

        bool pause = false;

        static int milliseconds;
        static int tileSize;
        static int formSize;
        static int halfFormSize;

        Brush pythonColor = new SolidBrush(Color.Orange);
        Brush appleColor = new SolidBrush(Color.Red);

        List<Dictionary<char, int>> PythonBody = new List<Dictionary<char, int>>();
        Dictionary<char, int> apple = new Dictionary<char, int>();

        private System.Windows.Forms.Timer timer;

        private void InitializeTimer()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Interval = milliseconds;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void InitializePythonBody()
        {
            PythonBody.Add(
                new Dictionary<char, int>
                {
                    {'h',halfFormSize},
                    {'w',halfFormSize}
                }
            );
            PythonBody.Add(
                new Dictionary<char, int>
                {
                    {'h',halfFormSize + tileSize},
                    {'w',halfFormSize}
                }
            );
            PythonBody.Add(
                new Dictionary<char, int>
                {
                    {'h',halfFormSize + tileSize * 2},
                    {'w',halfFormSize}
                }
            );
        }

        private void InitializeSettings()
        {
            Settings s = new Settings();
            settings = s.LoadJson();

            milliseconds = settings["milliseconds"];
            tileSize = settings["tileSize"];
            formSize = settings["formSize"];
            halfFormSize = formSize / 2;
        }

        private void Death()
        {
            timer.Stop();
            MessageBox.Show("Python is death");
            Application.Restart();
            Environment.Exit(0);
        }

        private void AppleSpawn()
        {
            apple = new Dictionary<char, int>
            {
                {'h',rnd.Next(1,formSize / tileSize) * tileSize},
                {'w',rnd.Next(1,formSize / tileSize) * tileSize}
            };
        }

        private void FormPaint(object sender, PaintEventArgs e)
        {
            PaintApple(sender, e);
            PaintPythonBody(sender, e); 
        }

        private void PaintApple(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.FillRectangle(appleColor, apple['w'], apple['h'], tileSize, tileSize);
        }

        private void PythonCollision()
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

        private void Wall—ollision()
        {
            if (
                PythonBody.Last()['h'] > formSize -1 ||
                PythonBody.Last()['w'] > formSize -1 ||
                PythonBody.Last()['h'] < 0 ||
                PythonBody.Last()['w'] < 0)
                    Death();
        }

        private void Crawl()
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

        private void Timer_Tick(object sender, EventArgs e)
        {
            Crawl();
            pressed = false;
            PythonCollision();
            Wall—ollision();
            Refresh();
        }

        private void PaintPythonBody(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            foreach (var item in PythonBody)
            {
                g.FillRectangle(pythonColor, item['w'], item['h'], tileSize, tileSize);
            }
        }

        private void Pause()
        {
            if (pause)
                timer.Start();
            else
                timer.Stop();
            pause = !pause;
        }

        private void KeyDownEvent (object sender, KeyEventArgs e)
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
            if (e.KeyCode == Keys.Space)
                Pause();
        }
    }
}