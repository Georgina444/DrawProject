using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Draw
{

	/// Класът, който ще бъде използван при управляване на диалога.
	[Serializable]
	public class DialogProcessor : DisplayProcessor
	{
		#region Constructor
		
		public DialogProcessor()
		{
		}
		
		#endregion
		
		#region Properties


		/// Избран елемент.

		private List<Shape> selection = new List<Shape>();
		public List<Shape> Selection {
			get { return selection; }
			set { selection = value; }
		}
		
        private List<Shape> copyList = new List<Shape>();
        public List<Shape> CopyList
        {
            get { return copyList; }
            set { copyList = value; }
        }


		/// Дали в момента диалога е в състояние на "влачене" на избрания елемент.

		private bool isDragging;
		public bool IsDragging {
			get { return isDragging; }
			set { isDragging = value; }
		}
		

		/// Последна позиция на мишката при "влачене".
		/// Използва се за определяне на вектора на транслация.

		private PointF lastLocation;

        public PointF LastLocation {
			get { return lastLocation; }
			set { lastLocation = value; }
		}

        #endregion


        /// Добавя примитив - правоъгълник на произволно място върху клиентската област.

        public void AddRandomRectangle()
		{
			Random rnd = new Random();
			int x = rnd.Next(100,1000);
			int y = rnd.Next(100,600);
			
			RectangleShape rect = new RectangleShape(new Rectangle(x,y,100,200));
			rect.FillColor = Color.White;
			rect.StrokeColor = Color.Black;

			ShapeList.Add(rect);
		}

        // ELLIPSE
        public void AddRandomEllipse()
        {
            Random rnd = new Random();
            int x = rnd.Next(100, 1000);
            int y = rnd.Next(100, 600);

            EllipseShape ellipse = new EllipseShape(new Rectangle(x, y, 200, 100));

            ellipse.TransformationMatrix.RotateAt(0,
            new PointF(ellipse.Rectangle.X + ellipse.Width / 2, ellipse.Rectangle.Y + ellipse.Height / 2));

            ellipse.FillColor = Color.White;
			ellipse.StrokeColor = Color.Black;

            ShapeList.Add(ellipse);
        }

		// STAR
		public void AddRandomStar()
		{
			Random rnd = new Random();
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);

			StarShape star = new StarShape(new Rectangle(x, y, 100, 200));
			star.FillColor = Color.White;

			ShapeList.Add(star);
		}

        internal void AddRandomTriangle()
        {
            Random rnd = new Random();
            int x = rnd.Next(100, 1000);
            int y = rnd.Next(100, 600);

            TriangleShape triangle = new TriangleShape(new Rectangle(x, y, 100, 200));

            triangle.FillColor = Color.White;
            triangle.StrokeColor = Color.Black;

            ShapeList.Add(triangle);
        }

        internal void Clear()
        {
            ShapeList.Clear();
        }
        public void Copy()
        {

        }
        public void Paste()
        {
            foreach (Shape item in CopyList)
                ShapeList.Add(item);
        }
        public void Cut()
        {
            CopyList = Selection;
            foreach (Shape item in Selection)
                ShapeList.Remove(item);
        }

        internal void Delete()
        {
            foreach (Shape s in selection)
            {
                ShapeList.Remove(s);
            }
        }

        /// <summary>
        /// Проверява дали дадена точка е в елемента.
        /// Обхожда в ред обратен на визуализацията с цел намиране на
        /// "най-горния" елемент т.е. този който виждаме под мишката.
        /// </summary>
        /// <param name="point">Указана точка</param>
        /// <returns>Елемента на изображението, на който принадлежи дадената точка.</returns>
        public Shape ContainsPoint(PointF point)
		{
			for(int i = ShapeList.Count - 1; i >= 0; i--){
				if (ShapeList[i].Contains(point)){
					// ShapeList[i].FillColor = Color.Red;
						
					return ShapeList[i];
				}	
			}
			return null;
		}

        /// <summary>
        /// Транслация на избраният елемент на вектор определен от <paramref name="p>p</paramref>
        /// </summary>
        /// <param name="p">Вектор на транслация.</param>
        public void TranslateTo(PointF p)
        {
            if (selection.Count > 0)
            {

                foreach (Shape item in Selection)
                {
                    if(item.TransformationMatrix != null)
                    {

                    item.TransformationMatrix.RotateAt(item.Rotate * (-1),
                    new PointF(item.Rectangle.X + item.Width / 2, item.Rectangle.Y + item.Height / 2));

                    item.Location =
                    new PointF(item.Location.X + p.X - lastLocation.X, item.Location.Y + p.Y - lastLocation.Y);

                    item.TransformationMatrix.RotateAt(item.Rotate,
                    new PointF(item.Rectangle.X + item.Width / 2, item.Rectangle.Y + item.Height / 2));
                    }
                }
                lastLocation = p;
                //selection.Location = 
                //new PointF(selection.Location.X + p.X - lastLocation.X, selection.Location.Y + p.Y - lastLocation.Y);
                //lastLocation = p;
            }
        }

        // LINE WIDTH
        public virtual void ChangeLineWidth(int temp)
        {
            foreach (Shape s in selection)
            {
                s.LineWidth = temp;
            }
        }

        public void GroupShapes()
        {
            float minx = float.PositiveInfinity;
            float maxx = float.NegativeInfinity;
            float miny = float.PositiveInfinity;
            float maxy = float.NegativeInfinity;

            foreach (Shape shape in selection)
            {
                if (minx > shape.Location.X) minx = shape.Location.X;
                if (maxx < shape.Location.X + shape.Width) maxx = shape.Location.X + shape.Width;
                if (miny > shape.Location.Y) miny = shape.Location.Y;
                if (maxy < shape.Location.Y + shape.Height) maxy = shape.Location.Y + shape.Height;
            }

            RectangleShape rect = new RectangleShape(new Rectangle((int)minx, (int)miny, (int)maxx - (int)minx, (int)maxy - (int)miny));

            GroupShape gs = new GroupShape(rect);
            gs.SubShapes = Selection;
            Selection = new List<Shape>();
            
            ShapeList.Add(gs);

            foreach (Shape item in gs.SubShapes)
            {
                ShapeList.Remove(item);
            }
            Selection.Add(gs);

        }

        public void SetStrokeColor(Color c)
		{
	        foreach(Shape item in Selection)
			{
				item.StrokeColor = c;
			}
		}

		public void SetFillColor(Color c)
		{
		    foreach(Shape item in Selection)
			{
				item.FillColor = c;	
			}
		}

		public void SetOpacity(int o)
		{
			foreach(Shape item in Selection)
	        {
                item.Opacity = o;
		    }
        }

		//public void Rotate(int v)
		//{
  //          foreach (Shape s in selection)
  //          {
  //              s.TransformationMatrix.RotateAt(v,
  //              new PointF(s.Rectangle.X + s.Width / 2, s.Rectangle.Y + s.Height / 2));
  //              s.Rotate += v;
  //          }
  //      }

        public void Rotate(int v)
        {
            if (selection.Count > 0)
            {
                foreach(Shape item in Selection)
                {
                    Matrix m = new Matrix(
                         item.TransformationMatrix.Elements[0],
                         item.TransformationMatrix.Elements[1],
                         item.TransformationMatrix.Elements[2],
                         item.TransformationMatrix.Elements[3],
                         item.TransformationMatrix.Elements[4],
                         item.TransformationMatrix.Elements[5]
                         );
                    m.RotateAt(
                        v,
                        new PointF(
                            item.Location.X + item.Width/2,
                            item.Location.Y + item.Height/2
                            ));
                    item.TransformationMatrix = m;
                }
            }
        }

		public void ScalePrimitive(float scaleFactorX, float scaleFactorY)
		{
			if(Selection.Count > 0)
			{
				foreach(Shape item in Selection)
				item.TransformationMatrix.Scale(
					scaleFactorX,
					scaleFactorY
					);
			}
		}

        internal void ChangeHeight(int temp)
        {
            foreach (Shape s in selection)
            {
                s.Height = temp;
            }
        }

        internal void ChangeWidth(int v)
        {
            foreach (Shape s in selection)
            {
                s.Width = v;
            }
        }


        public override void DrawShape(Graphics grfx, Shape item )
        {
            //For every item in subshape visualize
            base.DrawShape(grfx, item);

            if (selection.Contains(item))
            {
                GraphicsState state = grfx.Save();

                Matrix m = grfx.Transform.Clone();
                m.Multiply(item.TransformationMatrix);

                grfx.Transform = m;
                grfx.DrawRectangle(
                    new Pen(Color.Red),
                    item.Location.X - 3,
                    item.Location.Y - 3,
                    item.Width + 6,
                    item.Height + 6

                    );
                grfx.Restore(state);
            }

        }
    }
}
