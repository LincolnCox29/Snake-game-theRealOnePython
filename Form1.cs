using System.Runtime.CompilerServices;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Policy;
using theRealOnePythin;

namespace theRealOnePython
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            python.InitializePythonBody();
            apple.AppleSpawn(python);
            InitializeComponent();
            SetStyle(
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint, true
                );
            InitializeTimer();
            ClientSize = new Size(settings.getFormSize, settings.getFormSize);
        }

        char pressedKey = 's';
        bool pressed = false;
        bool pause = false;

        static Settings settings = new Settings();

        Python python = new Python(settings);

        Apple apple = new Apple(settings);

        private System.Windows.Forms.Timer timer;

        private void InitializeTimer()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Interval = settings.getMilliseconds;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void FormPaint(object sender, PaintEventArgs e)
        {
            apple.PaintApple(sender, e);
            python.PaintPythonBody(sender, e); 
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            pressed = false;
            python.Update(pressedKey, apple, timer);
            this.Text = $"theRealOnePython apples: {apple.eaten}";
            Refresh();
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