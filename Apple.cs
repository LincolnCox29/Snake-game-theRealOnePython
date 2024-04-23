using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theRealOnePythin
{
    internal class Apple
    {
        Brush appleColor = new SolidBrush(Color.Red);

        public Dictionary<char, int> apple = new Dictionary<char, int>();

        public int eaten = 0;

        Random rnd = new Random();

        public void PaintApple(object sender, PaintEventArgs e, int tileSize)
        {
            Graphics g = e.Graphics;
            g.FillRectangle(appleColor, apple['w'], apple['h'], tileSize, tileSize);
        }

        public void AppleSpawn(int formSize, int tileSize, Python python)
        {
            while(true)
            {
                apple = new Dictionary<char, int>
                {
                    {'h',rnd.Next(1,formSize / tileSize) * tileSize},
                    {'w',rnd.Next(1,formSize / tileSize) * tileSize}
                };
                if (!python.getPythonBody().Any(item => item['h'] == apple['h'] && item['h'] == 'h') &&
                    !python.getPythonBody().Any(item => item['w'] == apple['w'] && item['w'] == 'w'))
                        break;
            }
        }
    }
}
