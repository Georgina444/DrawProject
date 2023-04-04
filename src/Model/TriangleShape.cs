using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace Draw
{
    [Serializable]   // - allows the object of the class to be serialized and deserialized
    class TriangleShape : Shape
    {
        #region Constructor

        public TriangleShape(RectangleF rect) : base(rect)
        {
        }

        public TriangleShape(RectangleShape rectangle) : base(rectangle)
        {
        }

        #endregion
        private PointF[] points;
        public virtual PointF[] Points
        {
            get { return points; }
            set { points = value; }
        }

        /// <summary>
        /// Проверка за принадлежност на точка point към правоъгълника.
        /// В случая на правоъгълник този метод може да не бъде пренаписван, защото
        /// Реализацията съвпада с тази на абстрактния клас Shape, който проверява
        /// дали точката е в обхващащия правоъгълник на елемента (а той съвпада с
        /// елемента в този случай).
        /// </summary>
        public override bool Contains(PointF point)
        {

            if (base.Contains(point))
            { // Проверка дали е в обекта само, ако точката е в обхващащия правоъгълник.
              // В случая на правоъгълник - директно връщаме true

                if (isInside(Points[0].X, Points[0].Y,
                                Points[1].X, Points[1].Y,
                                Points[2].X, Points[2].Y,
                                point.X, point.Y))
                { return true; }
                else return false;

            }
            else
                // Ако не е в обхващащия правоъгълник, то неможе да е в обекта и => false
                return false;
        }

        static double area(float x1, float y1, float x2,
                       float y2, float x3, float y3)
        {
            return Math.Abs((x1 * (y2 - y3) +
                             x2 * (y3 - y1) +
                             x3 * (y1 - y2)) / 2.0);
        }
        static bool isInside(float x1, float y1, float x2,
                        float y2, float x3, float y3,
                        float x, float y)
        {
            double A = area(x1, y1, x2, y2, x3, y3);
            double A1 = area(x, y, x2, y2, x3, y3);
            double A2 = area(x1, y1, x, y, x3, y3);
            double A3 = area(x1, y1, x2, y2, x, y);
            return (A == A1 + A2 + A3);
        }

        /// <summary>
        /// Частта, визуализираща конкретния примитив.
        /// </summary>
        public override void DrawSelf(Graphics grfx)
        {
            base.DrawSelf(grfx);
            GraphicsState state = grfx.Save();

            Matrix m = grfx.Transform.Clone();
            m.Multiply(TransformationMatrix);

            grfx.Transform = m;

            //  grfx.Transform = TransformationMatrix;


            Pen pen = new Pen(StrokeColor);
            pen.Width = LineWidth;

            PointF point1 = new PointF(Rectangle.X, Rectangle.Y + Rectangle.Height);
            PointF point2 = new PointF(Rectangle.X + Rectangle.Width / 2, Rectangle.Y);
            PointF point3 = new PointF(Rectangle.X + Rectangle.Width, Rectangle.Y + Rectangle.Height);

            PointF[] points = { point1, point2, point3 };

            Points = points;

            FillColor = Color.FromArgb(Opacity, FillColor);

            grfx.DrawPolygon(pen, Points);
            grfx.FillPolygon(new SolidBrush(FillColor), Points);

            grfx.Restore(state);
        }
    }
}
