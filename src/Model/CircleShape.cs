using System;
using System.Drawing;

namespace Draw
{
	/// <summary>
	/// Класът правоъгълник е основен примитив, който е наследник на базовия Shape.
	/// </summary>
	public class EllipseShape : Shape
	{
		#region Constructor

		// creates an ellipse object within a specifies bounding rectangle
		public EllipseShape(RectangleF rect) : base(rect)
		{
		}
		// creates a new ellipse obj that is a copy of an existing ellipse obj
		public EllipseShape(EllipseShape ellipse) : base(ellipse)
		{
		}

		#endregion

		/// <summary>
		/// Проверка за принадлежност на точка point към правоъгълника.
		/// В случая на правоъгълник този метод може да не бъде пренаписван, защото
		/// Реализацията съвпада с тази на абстрактния клас Shape, който проверява
		/// дали точката е в обхващащия правоъгълник на елемента (а той съвпада с
		/// елемента в този случай).
		/// </summary>
		public override bool Contains(PointF point)
		{

			// matrix invert
			// transform point
			// matrix invert
			// transforms point
			// var Points = new PointF[1];
			//TransformationMatrix = Transform

			double a = Rectangle.Width / 2;
			double b = Rectangle.Height / 2;
			double c = Location.X + a;
			double d = Location.Y + b;


			if (Math.Pow((point.X - c),2)/Math.Pow(a,2) + Math.Pow((point.Y - d),2)/Math.Pow(b,2) <= 1)
				return true;
				// Проверка дали е в обекта само, ако точката е в обхващащия правоъгълник.
				// В случая на правоъгълник - директно връщаме true
			else
				// Ако не е в обхващащия правоъгълник, то неможе да е в обекта и => false
				return false;
		}

		/// <summary>
		/// Частта, визуализираща конкретния примитив.
		/// </summary>
		public override void DrawSelf(Graphics grfx)
		{
			base.DrawSelf(grfx);

			grfx.Transform = TransformationMatrix;

			FillColor = Color.FromArgb(Opacity, FillColor);
			grfx.FillEllipse(new SolidBrush(FillColor), Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
			grfx.DrawEllipse(new Pen(StrokeColor, StrokeWidth), Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);

		}
	}
}

