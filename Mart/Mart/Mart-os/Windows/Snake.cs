using Cosmos.System;
using Cosmos.System.Graphics;
using IL2CPU.API.Attribs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mart.Controls;

namespace Mart.Windows
{
	internal class Snake : Window
	{
		[ManifestResourceStream(ResourceName = "Mart.Resource.Applogos.snake.bmp")]
		static byte[] logoBytes;
		
		[ManifestResourceStream(ResourceName = "Mart.Resource.HeadTop.bmp")]
		static byte[] headTopBytes;

		[ManifestResourceStream(ResourceName = "Mart.Resource.HeadRight.bmp")]
		static byte[] headRightBytes;

		[ManifestResourceStream(ResourceName = "Mart.Resource.HeadDown.bmp")]
		static byte[] headDownBytes;

		[ManifestResourceStream(ResourceName = "Mart.Resource.HeadLeft.bmp")]
		static byte[] headLeftBytes;

		[ManifestResourceStream(ResourceName = "Mart.Resource.Food.bmp")]
		static byte[] foodBytes;

		[ManifestResourceStream(ResourceName = "Mart.Resource.Body.bmp")]
		static byte[] bodyBytes;

		private VBECanvas canvas = Kernel.canv;

		Controls.Label scoreText, gameOverText;
		Button start;

		Bitmap headTop, food, body, headRight, headDown, headLeft;
		Image imageHeadTop, imageHeadRight, imageHeadDown, imageHeadLeft, imageFood, imageBody;

		private int _score = 0;

		private int[] direction;

		private int _headPosX = 0;
		private int _headPosY = 0;

		private int _nTails = 1;
		private int[] _tailsX = new int[150];
		private int[] _tailsY = new int[150];

		private int _prevPosX, _prevPosY;
		private int _prevPosX2, _prevPosY2;

		private int _appleX, _appleY;

		private int _sizeOfSides = 40;

		Random random = new Random();

		int[] dirrection = { 1, 0 };

		bool isTsart = true;

		public Snake() : base(100, 100, 600, 600, "SnakeGame", Kernel.defFont)
		{
			logo = new(logoBytes);

			headTop = new(headTopBytes);
			headRight = new(headRightBytes);
			headDown = new(headDownBytes);
			headLeft = new(headLeftBytes);

			food = new(foodBytes);
			body = new(bodyBytes);

			imageHeadTop = headTop;
			imageHeadRight = headRight;
			imageHeadDown = headDown;
			imageHeadLeft = headLeft;

			imageFood = food;
			imageBody = body;

			scoreText = new("Score: ", 20, 20, font, Kernel.textColDark);
			gameOverText = new("GameOver", w / 2, h / 2, font, Kernel.textColDark);
			start = new("Start Game", w / 2, h / 2 + 20, Color.Green, font);

			_headPosX = x;
			_headPosY = y + 80;

			SetNewPosApple();

			controls.Add(scoreText);
			controls.Add(gameOverText);
			controls.Add(start);

			gameOverText.Visible = false;
			start.Visible = false;
		}

		public override void Update(VBECanvas canv, int mX, int mY, bool mD, int dmX, int dmY)
		{
			base.Update(canv, mX, mY, mD, dmX, dmY);

			if (start.clickedOnce && isTsart == false)
			{
				isTsart = true;
				start.Visible = false;
				gameOverText.Visible = false;
				_score = 0;
				_nTails = 1;
				_headPosX = x;
				_headPosY = y + 80;

				SetNewPosApple();
			}

			if(!isTsart) return;

			scoreText.Text = "Score: " + _score;

			// Background
			canvas.DrawFilledRectangle(Color.DarkGray, x + 1, y + 80, w - 1, h - 80);

			GenerateMap();

			if (dragging)
			{
				_headPosX = x;
				_headPosY = y + 80;

				_appleX = x;
				_appleY = y + 80;
				return;
			}

			DrawSnake();
			CreateApple();
		}

		private void DrawSnake()
		{
			direction = GetDirection();
			MoveSnake();

			if (_headPosX - x > w - _sizeOfSides && direction[0] == 1)
			{
				_headPosX = x;
			}
			else if (_headPosX - x < 0 && direction[0] == -1)
			{
				_headPosX = x + w - _sizeOfSides;
			}
			else if (_headPosY - y > h - _sizeOfSides && direction[1] == 1)
			{
				_headPosY = y + 80;
			}
			else if (_headPosY - y + 30 < y && direction[1] == -1)
			{
				_headPosY = y + h - _sizeOfSides;
			}

			_prevPosX = _tailsX[0];
			_prevPosY = _tailsY[0];

			_tailsX[0] = _headPosX;
			_tailsY[0] = _headPosY;

			for (int i = 1; i < _nTails; i++)
			{
				_prevPosX2 = _tailsX[i];
				_prevPosY2 = _tailsY[i];

				_tailsX[i] = _prevPosX;
				_tailsY[i] = _prevPosY;

				_prevPosX = _prevPosX2;
				_prevPosY = _prevPosY2;
			}

			for (int i = 1; i < _nTails; i++)
			{
				// Тело
				canvas.DrawImage(imageBody, _tailsX[i], _tailsY[i], _sizeOfSides, _sizeOfSides);
			}

			for (int i = 1; i < _nTails; i++)
			{
				if (_tailsX[i] == _headPosX && _tailsY[i] == _headPosY)
				{
					isTsart = false;
					gameOverText.Visible = true;
					start.Visible = true;
				}
			}

			// Голова
			if (dirrection[1] == -1)
				canvas.DrawImage(imageHeadTop, _headPosX, _headPosY, _sizeOfSides, _sizeOfSides);
			else if (dirrection[1] == 1)
				canvas.DrawImage(imageHeadDown, _headPosX, _headPosY, _sizeOfSides, _sizeOfSides);
			else if (dirrection[0] == 1)
				canvas.DrawImage(imageHeadRight, _headPosX, _headPosY, _sizeOfSides, _sizeOfSides);
			else if (dirrection[0] == -1)
				canvas.DrawImage(imageHeadLeft, _headPosX, _headPosY, _sizeOfSides, _sizeOfSides);
		}

		private int[] GetDirection()
		{
			
			if (isTsart)
			{
				if (KeyboardManager.TryReadKey(out KeyEvent key))
				{
					if(_nTails <= 1)
					{
						if (key.Key == ConsoleKeyEx.S)
						{
							dirrection[0] = 0;
							dirrection[1] = 1;
						}
						else if (key.Key == ConsoleKeyEx.D)
						{
							dirrection[1] = 0;
							dirrection[0] = 1;
						}
						else if (key.Key == ConsoleKeyEx.A)
						{
							dirrection[1] = 0;
							dirrection[0] = -1;
						}
						else if (key.Key == ConsoleKeyEx.W)
						{
							dirrection[0] = 0;
							dirrection[1] = -1;
						}
					}

                    else
                    {
						if (key.Key == ConsoleKeyEx.S && direction[1] != -1)
						{
							dirrection[0] = 0;
							dirrection[1] = 1;
						}
						else if (key.Key == ConsoleKeyEx.D && direction[0] != -1)
						{
							dirrection[1] = 0;
							dirrection[0] = 1;
						}
						else if (key.Key == ConsoleKeyEx.A && direction[0] != 1)
						{
							dirrection[1] = 0;
							dirrection[0] = -1;
						}
						else if (key.Key == ConsoleKeyEx.W && direction[1] != 1)
						{
							dirrection[0] = 0;
							dirrection[1] = -1;
						}
					}
                }
			}

			return dirrection;
		}

		private void GenerateMap()
		{
			for (int i = 0; i < w / _sizeOfSides - 1; i++)
			{
				canvas.DrawLine(Color.Gray, 0 + x, _sizeOfSides * i + y + 80, w + x - 1, _sizeOfSides * i + y + 80);
			}

			for (int i = 0; i < h / _sizeOfSides; i++)
			{
				canvas.DrawLine(Color.Gray, _sizeOfSides * i + x, y + 80, _sizeOfSides * i + x - 1, h + y);
			}
		}

		private void MoveSnake()
		{
			Thread.Sleep(2);
			_headPosX += direction[0] * _sizeOfSides;
			_headPosY += direction[1] * _sizeOfSides;
		}

		private void CreateApple()
		{
			if(_appleX == _headPosX && _appleY == _headPosY)
			{
				SetNewPosApple();
				_score++;
				_nTails++;
			}

			// Яблоко
			canvas.DrawImage(imageFood, _appleX, _appleY, _sizeOfSides, _sizeOfSides);
		}

		private void SetNewPosApple()
		{
			int newPosX = random.Next(x + _sizeOfSides, w);
			int newPosY = random.Next(y + _sizeOfSides + 80, h);

			int newPosX2 = newPosX % _sizeOfSides + _sizeOfSides / 2;
			int newPosY2 = newPosY % _sizeOfSides + _sizeOfSides / 2;

			newPosX -= newPosX2;
			newPosY -= newPosY2;

			_appleX = newPosX;
			_appleY = newPosY;
		}
	}
}
