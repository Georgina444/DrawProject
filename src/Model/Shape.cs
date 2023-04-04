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


        //  This is the default constructor for the Shape class. It creates a new Shape object with no initial values.
        public Shape()
		{
		}

        // This constructor is used to create a Shape object within a specified size and location.

        public Shape(RectangleF rect)
		{
			rectangle = rect;
		}


        // This constructor takes a Shape object as a parameter and creates a new Shape object
		// with the same properties as the passed in object. 
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
		
		
		/// Represent the location of an element (Горен ляв ъгъл на елемента)
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


        // The property provides a way to get or set the value of the transformationMatrix field.
        //  A transformation matrix can be used to represent various geometric transformations,
		//  including rotations, translations, scaling, and shearing. 
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


		// Височина на елемента.
		public virtual float Height {
			get { return Rectangle.Height; }
			set { rectangle.Height = value; }
		}

		// Широчина на елемента.
		public virtual float Width {
			get { return Rectangle.Width; }
			set { rectangle.Width = value; }
		}
		
        #endregion


        /// Проверка дали точка point принадлежи на елемента.
        /// <param name="point">Точка</param>
        /// <returns>Връща true, ако точката принадлежи на елемента и
        /// false, ако не пренадлежи
        public virtual bool Contains(PointF point)
		{
			return Rectangle.Contains(point.X, point.Y);
		}

        /// Визуализира елемента.
        /// <param name="grfx">Къде да бъде визуализиран елемента.</param>
        /// the "grfx" parameter represents the Graphics object that you can use to draw the shape on the drawing surface. 
        public virtual void DrawSelf(Graphics grfx)
		{
			// shape.Rectangle.Inflate(shape.BorderWidth, shape.BorderWidth);
		}

        // Implement the Clone method
        public virtual object Clone()
        {
            // Create a new instance of the same type as the current object
            Shape newShape = (Shape)Activator.CreateInstance(this.GetType());

            // Set the properties of the new instance 
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
