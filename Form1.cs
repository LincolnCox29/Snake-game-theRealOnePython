using System.Runtime.CompilerServices;
using System.Drawing;
using System.Drawing.Drawing2D;

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
            switch (e.KeyCode)
            {
                case Keys.W:
                    pressedKey = 'w';
                    break;
                case Keys.A:
                    pressedKey = 'a';
                    break;
                case Keys.S:
                    pressedKey = 's';
                    break;
                case Keys.D:
                    pressedKey = 'd';
                    break;
            }
        }
    }
}