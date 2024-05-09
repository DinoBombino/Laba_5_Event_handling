using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba_5_Event_handling.Objects
{
    class Player : BaseObject
    {
        public float vX, vY;
        public Action<Marker> OnMarkerOverlap;
        public event Action<GreenCircle> OnGreenCircleOverlap;

        public Player(float x, float y, float angle) : base(x, y, angle) //konstruktor
        {
        }
        public override void Render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.DeepSkyBlue), -15, -15, 30, 30);//kruzhochek
            g.DrawEllipse(new Pen(Color.Black, 2), -15, -15, 30, 30);//ramka dlya kruzhochka
            g.DrawLine(new Pen(Color.Black, 2), 0, 0, 25, 0);//palka napravleniya
        }
        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-15, -15, 30, 30);
            return path;
        }
        public override void Overlap(BaseObject obj)
        {
            base.Overlap(obj);

            if (obj is Marker)
            {
                OnMarkerOverlap(obj as Marker);
            }
            else if (obj is GreenCircle)
            {
                OnGreenCircleOverlap(obj as GreenCircle);
            }
        }
    }
}
