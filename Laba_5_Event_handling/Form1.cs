using Laba_5_Event_handling.Objects;

namespace Laba_5_Event_handling
{
    public partial class Form1 : Form
    {
        List<BaseObject> objects = new();
        Player player;
        Marker marker;
        GreenCircle greenCircle;

        public static Random rnd = new Random();

        private int greenCircleCounter = 0; // ѕеременна€ дл€ хранени€ счЄтчика
        public Form1()
        {
            InitializeComponent();

            player = new Player(pbMain.Width / 2, pbMain.Height / 2, 0);

            player.OnOverlap += (p, obj) => //reakcia na peresechenie
            {
                txtLog.Text = $"[{DateTime.Now:HH:mm:ss:ff}] »грок пересекс€ с {obj}\n" + txtLog.Text;
            };

            player.OnMarkerOverlap += (m) => //reactia na peresechenie s markerom
            {
                objects.Remove(m);
                marker = null;
            };

            player.OnGreenCircleOverlap += (g) =>
            {
                g.X = rnd.Next() % (pbMain.Width - 50) + 25;
                g.Y = rnd.Next() % (pbMain.Height - 40) + 20;
                g.Size = 40;
                greenCircleCounter++;
                CounterLable.Text = $"ќчки: {greenCircleCounter}";
            };

            for (int i = 0; i < 3; i++)
            {
                greenCircle = new GreenCircle(rnd.Next() % (pbMain.Width - 50) + 25, rnd.Next() % (pbMain.Height - 40) + 20, 0, 40);
                objects.Add(greenCircle);
            } 

            objects.Add(player);
        }

        private void pbMain_paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Color.White);

            updatePlayer();

            // пересчитываем пересечени€
            foreach (var obj in objects.ToList())
            {
                if (obj != player && player.Overlaps(obj, g))
                {
                    player.Overlap(obj);
                    obj.Overlap(player);
                }
            }

            // рендерим объекты
            foreach (var obj in objects)
            {
                g.Transform = obj.GetTransform();
                obj.Render(g);
            }
        }

        public void updatePlayer()
        {
            if (marker != null)
            {
                float dx = marker.X - player.X;
                float dy = marker.Y - player.Y;
                float length = MathF.Sqrt(dx * dx + dy * dy);
                dx /= length;
                dy /= length;

                // по сути мы теперь используем вектор dx, dy
                // как вектор ускорени€, точнее даже вектор прит€жени€
                // который прит€гивает игрока к маркеру
                // 0.5 просто коэффициент который подобрал на глаз
                // и который дает естественное ощущение движени€
                player.vX += dx /** 0.5f*/;
                player.vY += dy /** 0.5f*/;//сделал скорость быстрее

                // расчитываем угол поворота игрока 
                player.Angle = 90 - MathF.Atan2(player.vX, player.vY) * 180 / MathF.PI;
            }

            // тормоз€щий момент,
            // нужен чтобы, когда игрок достигнет маркера произошло постепенное замедление
            player.vX += -player.vX * 0.1f;
            player.vY += -player.vY * 0.1f;

            // пересчет позици€ игрока с помощью вектора скорости
            player.X += player.vX;
            player.Y += player.vY;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (var obj in objects.OfType<GreenCircle>())
            {
                obj.Size -= 0.3f;
                if (obj.Size <= 0)
                {
                    obj.X = rnd.Next() % (pbMain.Width - 50) + 25;
                    obj.Y = rnd.Next() % (pbMain.Height - 40) + 20;
                    obj.Size = 40;
                }
            }

            pbMain.Invalidate();
        }

        private void pbMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (marker == null)
            {
                marker = new Marker(0, 0, 0);
                objects.Add(marker);
            }

            marker.X = e.X;
            marker.Y = e.Y;
        }
    }
}
