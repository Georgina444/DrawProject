using System;
using System.Drawing;

namespace Draw
{
	/// <summary>
	/// Класът правоъгълник е основен примитив, който е наследник на базовия Shape.
	/// </summary>
	public class StarShape : Shape
	{
		#region Constructor

		// creates an ellipse object within a specifies bounding rectangle
		public StarShape(RectangleF rect) : base(rect)
		{
		}
		// creates a new ellipse obj that is a copy of an existing ellipse obj
		public StarShape(StarShape star) : base(star)
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
			if (base.Contains(point))
				// Проверка дали е в обекта само, ако точката е в обхващащия правоъгълник.
				// В случая на правоъгълник - директно връщаме true
				return true;
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
			PointF[] starCoordinates = new PointF[8];

			starCoordinates[0] = new PointF(0, 150);
			starCoordinates[1] = new PointF(120, 120);
			starCoordinates[2] = new PointF(150, 0);
			starCoordinates[3] = new PointF(180, 120);
			starCoordinates[4] = new PointF(300, 150);
			starCoordinates[5] = new PointF(180, 180);
			starCoordinates[6] = new PointF(150, 300);
			starCoordinates[7] = new PointF(120, 180);

			Rectangle = new RectangleF(0, 0, 100, 200);

			FillColor = Color.FromArgb(Opacity, FillColor);

			grfx.FillPolygon(new SolidBrush(FillColor), starCoordinates);
			grfx.DrawPolygon(new Pen(StrokeColor), starCoordinates);

		}
	}
}