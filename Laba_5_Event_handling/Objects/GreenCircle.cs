using System;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Laba_5_Event_handling.Objects
{
    class GreenCircle : BaseObject
    {
        public float Size; // начальный размер круга

        public GreenCircle(float x, float y, float angle/*, PictureBox pictureBox*/, float size) : base(x, y, angle)
        {
            Size = size;
        }

        public override void Render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.Lime), -Size / 2, -Size / 2, Size, Size);
        }

        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-15, -15, 30, 30);
            return path;
        }
    }
}
