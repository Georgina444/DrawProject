using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.AccessControl;
using System.Windows;
using System.Windows.Forms;

namespace Draw
{
	/// <summary>
	/// Върху главната форма е поставен потребителски контрол,
	/// в който се осъществява визуализацията
	/// </summary>
	public partial class MainForm : Form
	{
		/// <summary>
		/// Агрегирания диалогов процесор във формата улеснява манипулацията на модела.
		/// </summary>
		private DialogProcessor dialogProcessor = new DialogProcessor();
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}

		/// <summary>
		/// Изход от програмата. Затваря главната форма, а с това и програмата.
		/// </summary>
		void ExitToolStripMenuItemClick(object sender, EventArgs e)
		{
			Close();
		}
		
		/// <summary>
		/// Събитието, което се прихваща, за да се превизуализира при изменение на модела.
		/// </summary>
		void ViewPortPaint(object sender, PaintEventArgs e)
		{
			dialogProcessor.ReDraw(sender, e);
		}
		
		/// <summary>
		/// Бутон, който поставя на произволно място правоъгълник със зададените размери.
		/// Променя се лентата със състоянието и се инвалидира контрола, в който визуализираме.
		/// </summary>
		void DrawRectangleSpeedButtonClick(object sender, EventArgs e)
		{
			dialogProcessor.AddRandomRectangle();
			
			statusBar.Items[0].Text = "Последно действие: Рисуване на правоъгълник";
			
			viewPort.Invalidate();
		}

		/// <summary>
		/// Прихващане на координатите при натискането на бутон на мишката и проверка (в обратен ред) дали не е
		/// щракнато върху елемент. Ако е така то той се отбелязва като селектиран и започва процес на "влачене".
		/// Промяна на статуса и инвалидиране на контрола, в който визуализираме.
		/// Реализацията се диалогът с потребителя, при който се избира "най-горния" елемент от екрана.
		/// </summary>
		void ViewPortMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (pickUpSpeedButton.Checked) {
				
			    Shape temp = dialogProcessor.ContainsPoint(e.Location);
				if(temp != null)
				{
					if(dialogProcessor.Selection.Contains(temp))
						dialogProcessor.Selection.Remove(temp);
					else
					{
						dialogProcessor.Selection.Add(temp);
					}
				}

				//dialogProcessor.Selection = dialogProcessor.ContainsPoint(e.Location);
				//if (dialogProcessor.Selection != null) {
					statusBar.Items[0].Text = "Последно действие: Селекция на примитив";
					dialogProcessor.IsDragging = true;
					dialogProcessor.LastLocation = e.Location;
					viewPort.Invalidate();
				}
			}


		/// <summary>
		/// Прихващане на преместването на мишката.
		/// Ако сме в режм на "влачене", то избрания елемент се транслира.
		/// </summary>
		void ViewPortMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (dialogProcessor.IsDragging) {
				if (dialogProcessor.Selection != null) statusBar.Items[0].Text = "Последно действие: Влачене";
				dialogProcessor.TranslateTo(e.Location);
				viewPort.Invalidate();
			}
		}

		/// <summary>
		/// Прихващане на отпускането на бутона на мишката.
		/// Излизаме от режим "влачене".
		/// </summary>
		void ViewPortMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			dialogProcessor.IsDragging = false;
		}

        private void viewPort_Load(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            dialogProcessor.AddRandomEllipse();

            statusBar.Items[0].Text = "Последно действие: Рисуване на елипса";

            viewPort.Invalidate();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
			if (colorDialog1.ShowDialog() == DialogResult.OK)
			{
				dialogProcessor.SetFillColor(colorDialog1.Color);
			}
			viewPort.Invalidate();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
			if(colorDialog1.ShowDialog() == DialogResult.OK)
			{
				dialogProcessor.SetStrokeColor(colorDialog1.Color);
			}
			viewPort.Invalidate();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
			dialogProcessor.SetOpacity(trackBar1.Value);
			viewPort.Invalidate();
        }


        private void toolStripButton4_Click(object sender, EventArgs e)
        {
			dialogProcessor.AddRandomStar();

			statusBar.Items[0].Text = "Последно действие: Рисуване на звезда";

			viewPort.Invalidate();
        }


		// triangle button
        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            dialogProcessor.AddRandomTriangle();

            statusBar.Items[0].Text = "Последно действие: Рисуване на триъгълник";

            viewPort.Invalidate();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (lineWidthOptions.SelectedItem != null)
            {
                dialogProcessor.ChangeLineWidth(int.Parse(lineWidthOptions.SelectedItem.ToString()));
                lineWidthOptions.SelectedItem = null;
                this.viewPort.Invalidate();
            }
			if (RotateTF.Text.Length != 0)
			{
				dialogProcessor.Rotate(int.Parse(RotateTF.ToString()));
				RotateTF.Clear();
				this.viewPort.Invalidate();
			}
			if (HeightTF.Text.Length != 0)
			{
				dialogProcessor.ChangeHeight(int.Parse(HeightTF.ToString()));
				HeightTF.Clear();
				this.viewPort.Invalidate();
			}
			if (WidthTF.Text.Length != 0)
			{
				dialogProcessor.ChangeWidth(int.Parse(WidthTF.ToString()));
				WidthTF.Clear();
				this.viewPort.Invalidate();
			}
		}

        private void RotateTF_Click(object sender, EventArgs e)
        {

        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {

        }

        private void HeightTF_Click(object sender, EventArgs e)
        {

        }

        private void toolStripLabel4_Click(object sender, EventArgs e)
        {

        }

        private void WidthTF_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
			dialogProcessor.GroupShapes();
			viewPort.Invalidate();
        }

        private List<Shape> clipboard = new List<Shape>();


        private void viewPort_KeyDown(object sender, KeyEventArgs e)
        {
			if (e.KeyCode == Keys.Delete)
			{
				foreach(Shape item in dialogProcessor.Selection)
				{
					dialogProcessor.ShapeList.Remove(item);
				}
				dialogProcessor.Selection = new List<Shape>();
			}
			else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.C) // Check if Control + C is pressed
			{
				clipboard = new List<Shape>();
				foreach (Shape item in dialogProcessor.Selection)
				{
					clipboard.Add((Shape)item.Clone()); // Copy selected shapes to clipboard
				}
			}
			else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.V) // Check if Control + P is pressed
			{
				foreach (Shape item in clipboard)
				{
					Shape copy = (Shape)item.Clone();
					copy.Location = new PointF(copy.Location.X + 10, copy.Location.Y + 10);
					dialogProcessor.ShapeList.Add(copy); // Add a new copy of each shape to the shape list at a different location
				}
			}

		}


		// ???????????????????????????????????? //
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            dialogProcessor.AddRandomEllipse();
            viewPort.Invalidate();
        }

        private void rectangleCtrlXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dialogProcessor.AddRandomRectangle();
            viewPort.Invalidate();
        }

        private void triangleCtrTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dialogProcessor.AddRandomTriangle();

            statusBar.Items[0].Text = "Последно действие: Рисуване на триъгълник";

            viewPort.Invalidate();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            dialogProcessor.Delete();
            this.viewPort.Invalidate();
        }

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dialogProcessor.Clear();
            viewPort.Invalidate();
        }

        private void backgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
			setCanvasColor();
        }

        private void setCanvasColor()
        {
            ColorDialog dlg = new ColorDialog();
            var dialogResult = dlg.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                this.BackColor = dlg.Color;
            }
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

		// Save & Load
        private void saveMenuItem1_Click(object sender, EventArgs e)
        {
            FileStream fStream = new FileStream("Draw.bin", FileMode.Create);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(fStream, dialogProcessor.ShapeList);
            fStream.Close();
        }
        private void loadToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FileStream fStream = new FileStream("Draw.bin", FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
			List<Shape> temp = (List<Shape>)binaryFormatter.Deserialize(fStream);
			dialogProcessor.ShapeList.Clear();
			dialogProcessor.ShapeList = temp;
			fStream.Close();

			viewPort.Invalidate();

        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            dialogProcessor.AddRandomCircle();
            viewPort.Invalidate();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
			dialogProcessor.AddRandomSquare();
			viewPort.Invalidate();
        }
    }
}

