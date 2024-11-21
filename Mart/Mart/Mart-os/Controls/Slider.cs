using Cosmos.System.Graphics;
using Cosmos.System;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Cosmos.System.Graphics.Fonts;
using System.IO;
using Mart.Windows;

namespace Mart.Controls
{
	class Slider : Control
	{
		public Color color;
		public int padding;
		public int width;
		public bool clicked, hovered, isClicked;
		public int minValue, maxValue, value;
		VBECanvas canv;
		int _pX, _pY;
		int knobWidth;
		int knobX;
		int newX;

		int mX, mY;

		bool isWriteToFile = true;

		public Slider(int x, int y, Color color, int width = 200, int padding = 5, int minValue = 0, int maxValue = 100, int initialValue = 0)
		{
			this.x = x;
			this.y = y;
			this.color = color;
			this.padding = padding;
			this.width = width;
			this.minValue = minValue;
			this.maxValue = maxValue;
			this.value = initialValue;
			this.knobWidth = 10; // ширина бегунка
			this.knobX = x + padding + (int)(((float)(value - minValue) / (maxValue - minValue)) * (width - padding * 2 - knobWidth)); // начальное положение бегунка
			canv = Kernel.canv;
		}

		public override void Update(int pX, int pY)
		{
			if (!Visible) return;

			_pX = pX;
			_pY = pY;
			 mX = (int)MouseManager.X;
			 mY = (int)MouseManager.Y;

			bool mD = MouseManager.MouseState == MouseState.Left;
			clicked = Clicked(mX, mY, mD);
			hovered = Hovered(mX, mY);

			if(MouseManager.MouseState == MouseState.Left && clicked)
				isClicked = true;

			if (clicked || isClicked)
			{
				// Перемещение бегунка
				newX = mX - (knobWidth / 2);

				newX = Math.Max(x + _pX + padding, Math.Min(x + _pX + width - padding - knobWidth, newX)) - _pX;

				if (newX != knobX)
				{
					knobX = newX;
					// Обновление значения слайдера
					value = (int)(((float)(knobX - x - padding) / (width - padding * 2 - knobWidth)) * (maxValue - minValue)) + minValue;
				}
			}

			if(!(MouseManager.MouseState == MouseState.Left))
			{
				isClicked = false;
			}


			// Рисование слайдера
			canv.DrawFilledRectangle(hovered ? Color.LightGray : color, x + pX, y + pY, width, padding * 2);
			// Рисование бегунка
			canv.DrawFilledRectangle(clicked ? Color.DarkGray : Color.Gray, knobX + pX, y + pY, knobWidth, padding * 2);
		}

		public int GetX()
		{
			return _pX;
		}

		public int Value()
		{
			return value;
		}

		public bool Hovered(int mX, int mY)
		{
			return mX >= _pX + x && mX <= _pX + x + width && mY >= _pY + y && mY <= _pY + y + padding * 2;
		}

		public bool Clicked(int mX, int mY, bool mD)
		{
			return mD && Hovered(mX, mY);
		}

		private void SaveValueToFile(string filePath)
		{
			try
			{
				using (StreamWriter writer = new StreamWriter(filePath))
				{
					writer.WriteLine(value);
				}
			}
			catch (Exception ex)
			{
				// Обработка ошибок при сохранении файла
				Kernel.ThrowError("Ошибка при сохранении значения в файл: " + ex.Message);
			}
		}
	}
}
