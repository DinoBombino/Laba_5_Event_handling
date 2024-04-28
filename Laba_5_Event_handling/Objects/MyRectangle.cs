using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba_5_Event_handling.Objects
{
    class MyRectangle : BaseObject
    {
        // создаем конструктор с тем же набором параметров что и в BaseObject
        // base(x, y, angle) -- вызывает конструктор родительского класса
        public MyRectangle(float x, float y, float angle) : base(x, y, angle)
        {
        }

        // переопределяем Render
        public override void Render(Graphics g)
        {
            // и запихиваем туда код из формы
            g.FillRectangle(new SolidBrush(Color.Yellow), 0, 0, 50, 30);
            g.DrawRectangle(new Pen(Color.Red, 2), 0, 0, 50, 30);
        }
    }
}
