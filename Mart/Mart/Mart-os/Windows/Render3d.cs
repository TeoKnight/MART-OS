using Cosmos.System;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using IL2CPU.API.Attribs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using Mart.Controls;

namespace Mart.Windows
{
    internal class Render3d : Window
	{
		[ManifestResourceStream(ResourceName = "Mart.Resource.Applogos.3d.bmp")]
		static byte[] logoBytes;

		VBECanvas canvas = Kernel.canv;
		public InputField field1;

		Color[] colors = { Color.White, Color.Green, Color.Red, Color.Blue, Color.Black };

		int indexColor = 0;
		int indexBG = 0;

		Slider slider;
		public Label result;

		Button changeColor, changeBG;

		Vector3[] verticesCube = 
			{
				new Vector3(-1, 1, 1), // 0 вершина
			    new Vector3(-1, 1, -1), // 1 вершина
			    new Vector3(1, 1, -1), // 2 вершина
			    new Vector3(1, 1, 1), // 3 вершина
			    new Vector3(-1, -1, 1), // 4 вершина
			    new Vector3(-1, -1, -1), // 5 вершина
			    new Vector3(1, -1, -1), // 6 вершина
			    new Vector3(1, -1, 1), // 7 вершина
			};

		int angle = 0;
		int sizeCube;

		public Render3d() : base(300, 100, 600, 600, "Render3d", Kernel.defFont, false)
		{
			logo = new(logoBytes);

			field1 = new(20, y + 460, 50, font, 5);

			controls.Add(field1);
			field1.Value = "30";

			changeColor = new Button("Change Color", 100, y + 455, Color.White, Kernel.defFont, 10);
			changeBG = new Button("Change BG", 250, y + 455, Color.White, Kernel.defFont, 10);

			slider = new Slider(400, y + 460, Color.White);
			result = new Label("", 370, y + 460, Kernel.defFont, Kernel.textColDark);

			controls.Add(result);
			controls.Add(slider);
			controls.Add(changeColor);
			controls.Add(changeBG);
		}

		public override void Update(VBECanvas canv, int mX, int mY, bool mD, int dmX, int dmY)
		{
			base.Update(canv, mX, mY, mD, dmX, dmY);
			result.Text = slider.Value().ToString();
			DrawCube();
		}

		public void DrawCube()
		{
			int[][] edgesCube = new int[18][];

			edgesCube[0] = new int[] { 0, 1 };
			edgesCube[1] = new int[] { 1, 2 };
			edgesCube[2] = new int[] { 2, 3 };
			edgesCube[3] = new int[] { 3, 0 };
			edgesCube[4] = new int[] { 0, 4 };
			edgesCube[5] = new int[] { 1, 5 };
			edgesCube[6] = new int[] { 2, 6 };
			edgesCube[7] = new int[] { 3, 7 };
			edgesCube[8] = new int[] { 4, 5 };
			edgesCube[9] = new int[] { 5, 6 };
			edgesCube[10] = new int[] { 6, 7 };
			edgesCube[11] = new int[] { 7, 4 };
			edgesCube[12] = new int[] { 0, 7 };
			edgesCube[13] = new int[] { 0, 5 };
			edgesCube[14] = new int[] { 6, 4 };
			edgesCube[15] = new int[] { 2, 7 };
			edgesCube[16] = new int[] { 5, 2 };
			edgesCube[17] = new int[] { 1, 3 };


			double[][] matrix;
			double[][] matrix2;
			

			List<Vector3> sceneVertices = new List<Vector3>();

			angle++;

			//if(int.TryParse(field1.Value, out int newSize) && newSize <= 150)
			//	sizeCube = Convert.ToInt32(field1.Value);

			//else field1.Value = sizeCube.ToString();

			sizeCube = slider.Value();

			matrix = Matrix.GetRotationX(20 + angle);
			matrix = Matrix.Multiply(Matrix.GetRotationY(angle), matrix);
			matrix = Matrix.Multiply(Matrix.GetRotationZ(angle / 2), matrix);
			matrix2 = matrix = Matrix.Multiply(Matrix.GetScale(sizeCube, sizeCube, sizeCube), matrix);
			matrix = Matrix.Multiply(Matrix.GetTranslationMatrix(x + w / 2, y + h / 2, 0), matrix);

			for (int i = 0; i < verticesCube.Length; i++)
			{
				var vertex = Matrix.MultiplyVector(matrix, verticesCube[i]); // Умножаем все вершины на результирующую матрицу
				sceneVertices.Add(vertex); // Добавляем полученную вершину в список Vector3
			}


			if (changeBG.clickedOnce)
			{
				if (indexBG < colors.Length - 1)
					indexBG++;

				else indexBG = 0;
			}

			canvas.DrawFilledRectangle(colors[indexBG], x + 1, y + 30, w - 1, h - 50);

			Draw(edgesCube, sceneVertices);
		}

		private void Draw(int[][] edges, List<Vector3> sceneVertices, float widthLine = 1f)
		{
			if (changeColor.clickedOnce)
			{
				if(indexColor < colors.Length - 1)
					indexColor++;

				else indexColor = 0;
			}

			for(int i = 0; i < edges.Length; i++)
			{
				canvas.DrawLine
				(
					colors[indexColor],
					(int)sceneVertices[edges[i][0]].x,
					(int)sceneVertices[edges[i][0]].y,
					(int)sceneVertices[edges[i][1]].x,
					(int)sceneVertices[edges[i][1]].y
				);
			}
		}
	}
}
