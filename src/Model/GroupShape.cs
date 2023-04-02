using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Draw
{
    /// <summary>
    /// Класът правоъгълник е основен примитив, който е наследник на базовия Shape.
    /// </summary>
    public class GroupShape : Shape
    {
        #region Constructor

        public GroupShape(RectangleF rect) : base(rect)
        {
        }

        public GroupShape(RectangleShape rectangle) : base(rectangle)
        {
        }

        #endregion

        public List<Shape> SubShapes = new List<Shape>();
        /// <summary>
        /// Проверка за принадлежност на точка point към правоъгълника.
        /// В случая на правоъгълник този метод може да не бъде пренаписван, защото
        /// Реализацията съвпада с тази на абстрактния клас Shape, който проверява
        /// дали точката е в обхващащия правоъгълник на елемента (а той съвпада с
        /// елемента в този случай).
        /// </summary>
        /// 

        public override bool Contains(PointF point)
        {
            //for every item  in SubShape
            //check if the point is in the item 
            foreach (Shape shape in SubShapes)
            {
                if (shape.Contains(point)) return true;
            }
            return false;

        }

        /// <summary>
        /// Частта, визуализираща конкретния примитив.
        /// </summary>
        public override void DrawSelf(Graphics grfx)
        {
                base.DrawSelf(grfx);
            foreach (Shape item in SubShapes)
            {
                item.DrawSelf(grfx);
            }

        }
        public override PointF Location
        {
            get { return base.Location; }
            set { 
                foreach(Shape item in SubShapes)
                {
                    item.Location = new PointF(item.Location.X - Location.X + value.X, item.Location.Y - Location.Y + value.Y);
                }
                base.Location = value;
            }

        }

        public override Color FillColor { get => base.FillColor; set
            {
                base.FillColor = value;
                foreach(Shape item in SubShapes)
                {
                    item.FillColor = value;
                }
            }}

        public override Color StrokeColor { get => base.StrokeColor; set
            {
                base.StrokeColor = value;
                foreach(Shape item in SubShapes)
                {
                    item.StrokeColor = value;
                }   
            } }

        public override int Opacity { get => base.Opacity; set {
                base.Opacity = value;
                foreach(Shape item in SubShapes)
                {
                    item.Opacity = value;   
                }
            }}

        public override Matrix TransformationMatrix 
        {
            get => base.TransformationMatrix;
            set
            {
                base.TransformationMatrix.Multiply(value);
                foreach(Shape item in SubShapes)
                {
                    item.TransformationMatrix.Multiply(value);
                }
            }
        }
    }
    }
