using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theRealOnePython;

namespace theRealOnePythin
{
    internal class Apple
    {
        Brush appleColor = new SolidBrush(Color.Red);

        public Dictionary<char, int> apple = new Dictionary<char, int>();

        private readonly Settings settings;

        public int eaten = 0;

        Random rnd = new Random();

        public Apple(Settings _settings) 
        {
            settings = _settings;
        }

        public void PaintApple(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.FillRectangle(appleColor, apple['w'], apple['h'], settings.getTileSize, settings.getTileSize);
        }

        public void AppleSpawn(Python python)
        {
            while(true)
            {
                apple = new Dictionary<char, int>
                {
                    {'h',rnd.Next(1,settings.getFormSize / settings.getTileSize) * settings.getTileSize},
                    {'w',rnd.Next(1,settings.getFormSize / settings.getTileSize) * settings.getTileSize}
                };
                if (!python.getPythonBody().Any(item => item['h'] == apple['h'] && item['h'] == 'h') &&
                    !python.getPythonBody().Any(item => item['w'] == apple['w'] && item['w'] == 'w'))
                        break;
            }
        }
    }
}
