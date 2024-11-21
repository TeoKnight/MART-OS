using Cosmos.System;
using Cosmos.System.Graphics;
using IL2CPU.API.Attribs;
using System;
using System.Collections.Generic;
using System.Drawing;
using Mart.Controls;

namespace Mart.Windows
{

	enum CurrentPen
	{
		Line, Circle, Sqrt
	}

	internal class Paint : Window
	{
		// Иконка для окна
		[ManifestResourceStream(ResourceName = "Mart.Resource.Applogos.paintIco.bmp")]
		static byte[] logoBytes;

		VBECanvas canvas = Kernel.canv;
		Color currentColor = Color.Red;

		public List<Button> additionalButtons = new();
		public InputField field1;


		Button red, black, green, blue, expandButton, Line, Circle, Sqrt;
		Button clear;

		List<Point> points = new List<Point>();
		List<Color> colors = new List<Color>();
		List<CurrentPen> currentPens = new List<CurrentPen>();
		List<int> sizes = new List<int>();

		int currentSize = 3;

		bool expanded = false;

		CurrentPen currentPen = CurrentPen.Circle;

		public Paint() : base(300, 100, 600, 600, "Paint", Kernel.defFont, true)
		{
			logo = new(logoBytes);

			clear = new Button("Clear", 10, 50, Kernel.mainCol, Kernel.defFont, 10);
			red = new Button("red", 10, 100, Color.Red, Kernel.defFont, 10);
			black = new Button("black", 10, 150, Color.White, Kernel.defFont, 10);
			green = new Button("green", 10, 200, Color.Green, Kernel.defFont, 10);
			blue = new Button("blue", 10, 250, Color.Blue, Kernel.defFont, 10);
			expandButton = new Button(">", 10, 300, Kernel.mainCol, Kernel.defFont, 5);
			Line = new Button("Line", 10, 350, Kernel.mainCol, Kernel.defFont, 10);
			Circle = new Button("Circle", 10, 400, Kernel.mainCol, Kernel.defFont, 10);
			Sqrt = new Button("Rectangle", 10, 450, Kernel.mainCol, Kernel.defFont, 10);

			field1 = new(20, 500, 80, font, 5);
			controls.Add(field1);

			// Добавление кнопок
			controls.Add(Line);
			controls.Add(Circle);
			controls.Add(Sqrt);
			additionalButtons.Add(Line);
			additionalButtons.Add(Circle);
			additionalButtons.Add(Sqrt);
			controls.Add(red);
			controls.Add(black);
			controls.Add(green);
			controls.Add(blue);
			controls.Add(clear);
			controls.Add(expandButton);


			foreach (Button b in additionalButtons)
			{
				b.Visible = false;
			}

			field1.Value = "3";
			field1.Visible = false;
		}

		public override void Update(VBECanvas canv, int mX, int mY, bool mD, int dmX, int dmY)
		{
			try
			{
				base.Update(canv, mX, mY, mD, dmX, dmY);
				Kernel.canv.DrawFilledCircle(currentColor, this.x + 20, this.y + 50, 10);
				canvas.DrawFilledRectangle(Color.White, this.x + 100, this.y + 30, w - 100, h - 30);

				currentSize = Convert.ToInt32(field1.Value);

				if (black.clickedOnce)
				{
					currentColor = Color.Black;
				}

				else if (red.clickedOnce)
				{
					currentColor= Color.Red;
				}

				else if (green.clickedOnce)
				{
					currentColor= Color.Green;
				}

				else if (blue.clickedOnce)
				{
					currentColor= Color.Blue;
				}

				else if (clear.clickedOnce || resizing || Clicked(mX, mY, mD))
				{
					points.Clear();
					colors.Clear();
					currentPens.Clear();
					sizes.Clear();
				}

				else if (expandButton.clickedOnce)
				{
					expanded = !expanded;
					if (expanded)
					{
						expandButton.Text = "<";
						foreach (Button b in additionalButtons)
						{
							b.Visible = true;
						}

						field1.Visible = true;
					}
					else
					{
						expandButton.Text = ">";
						foreach (Button b in additionalButtons)
						{
							b.Visible = false;
						}
						field1.Visible = false;
					}
				}

				else if (Line.clickedOnce)
				{
					currentPen = CurrentPen.Line;
				}

				else if (Circle.clickedOnce)
				{
					currentPen = CurrentPen.Circle;
				}

				else if (Sqrt.clickedOnce)
				{
					currentPen = CurrentPen.Sqrt;
				}

				else if (mD)
				{
					if (mX > this.x + 100 && mX < this.x + w && mY > this.y + 30 && mY < this.y + h)
					{
						if (MouseManager.MouseState == MouseState.Left)
						{
							points.Add(new Point(mX, mY));
							colors.Add(currentColor);
							currentPens.Add(currentPen);
							sizes.Add(currentSize);
						}
					}
				}
				

				for (int i = 0; i < points.Count; i++)
				{
					if (currentPens[i] == CurrentPen.Line)
					{
						canv.DrawPoint(colors[i], points[i].X, points[i].Y);
					}

					else if (currentPens[i] == CurrentPen.Circle)
					{
						canv.DrawFilledCircle(colors[i], points[i].X, points[i].Y, sizes[i]);
					}

					else if (currentPens[i] == CurrentPen.Sqrt)
					{
						canv.DrawFilledRectangle(colors[i], points[i].X, points[i].Y, sizes[i], sizes[i]);
					}
				}
			}
			catch (Exception ex)
			{
				Kernel.ThrowError(ex.Message, "Paint");
				Close();
			}
		}
	}
}
