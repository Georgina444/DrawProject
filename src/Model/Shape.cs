using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Draw
{
	/// Базовия клас на примитивите, който съдържа общите характеристики на примитивите. primitivi = shapes
	[Serializable]	
	public abstract class Shape
	{
		#region Constructors
		
		public Shape()
		{
		}
		
		public Shape(RectangleF rect)
		{
			rectangle = rect;
		}
		
		public Shape(Shape shape)
		{
			this.Height = shape.Height;
			this.Width = shape.Width;
			this.LineWidth = shape.LineWidth;
			this.Location = shape.Location;
			this.rectangle = shape.rectangle;
			
			this.FillColor =  shape.FillColor;
            this.TransformationMatrix = shape.TransformationMatrix;

        }
        #endregion

        #region Properties

        /// Обхващащ правоъгълник на елемента.
        private RectangleF rectangle;		
		public virtual RectangleF Rectangle {
			get { return rectangle; }
			set { rectangle = value; }
		}
		
		
		/// Горен ляв ъгъл на елемента.
		public virtual PointF Location {
			get { return Rectangle.Location; }
			set { rectangle.Location = value; }
		}
		
		/// Цвят на елемента.
		private Color fillColor;		
		public virtual Color FillColor {
			get { return fillColor; }
			set { fillColor = value; }
		}

		private Color strokeColor = Color.Green;
		public virtual Color StrokeColor
		{
			get { return strokeColor; }
			set { strokeColor = value; }
		}

		public int strokeWidth = 1;
		public virtual int StrokeWidth
		{
			get { return strokeWidth; }
			set { strokeWidth = value; }
		}

        // Line Width
        private int lineWidth;

        public virtual int LineWidth
        {
            get { return lineWidth; }
            set { lineWidth = value; }
        }

        private int opacity = 255;
		public virtual int Opacity
		{
			get { return opacity; }
			set { opacity = value;}
		}

		[NonSerialized]
		public Matrix transformationMatrix = new Matrix();
		public virtual Matrix TransformationMatrix
		{
			get { return transformationMatrix; }
			set { transformationMatrix = value; }
		}

        private int rotate;

        public virtual int Rotate
        {
            get { return rotate; }
            set { rotate = value; }
        }


		/// Височина на елемента.
		public virtual float Height {
			get { return Rectangle.Height; }
			set { rectangle.Height = value; }
		}

		/// Широчина на елемента.

		public virtual float Width {
			get { return Rectangle.Width; }
			set { rectangle.Width = value; }
		}
		


        // Ellipse

        #endregion


        /// Проверка дали точка point принадлежи на елемента.
        /// <param name="point">Точка</param>
        /// <returns>Връща true, ако точката принадлежи на елемента и
        /// false, ако не пренадлежи</returns>
        public virtual bool Contains(PointF point)
		{
			return Rectangle.Contains(point.X, point.Y);
		}
		
		/// Визуализира елемента.
		/// <param name="grfx">Къде да бъде визуализиран елемента.</param>
		public virtual void DrawSelf(Graphics grfx)
		{
			// shape.Rectangle.Inflate(shape.BorderWidth, shape.BorderWidth);
		}

        // Implement the Clone method
        public virtual object Clone()
        {
            // Create a new instance of the same type as the current object
            Shape newShape = (Shape)Activator.CreateInstance(this.GetType());

            // Set the properties of the new instance to the same values as the current object
            newShape.Location = this.Location;
            newShape.Height = this.Height;
            newShape.Width = this.Width;
            newShape.LineWidth = this.LineWidth;
            newShape.FillColor = this.FillColor;
            newShape.TransformationMatrix = this.TransformationMatrix;

            return newShape;
        }
    }
}
