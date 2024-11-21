using Cosmos.System.Graphics.Fonts;
using Cosmos.System.Graphics;
using Cosmos.System;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mart.Windows;

namespace Mart.Controls
{
    public class Text : Control
    {
        public string Value = Kernel.copyValue;
        public int x, y, width, padding;
        VBECanvas canv;
        Font font;
        int _pX, _pY;
        bool focused = false;
        bool lmD;
        public bool submittedOnce;
        bool showCursor;
        int frames;
        int framesToUpdateCursor = 50;
        int pYstr = 10;
        int countEnter = 0;
        List<T> lastTxt = new();
        List<T> strList = new();
        public Text(int x, int y, int width, Font font, int padding, List<T> lastTxt = null)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            canv = Kernel.canv;
            this.font = font;
            this.padding = padding;
            this.lastTxt = lastTxt;
        }
        public string GetLastXCharacters(string str, int x)
        {
            if (str.Length <= x)
            {
                return str; // Return the whole string if it's not long enough
            }
            else
            {
                KeyboardManager.TryReadKey(out KeyEvent key);
                if (countEnter == 22 &&  key.Key != ConsoleKeyEx.Backspace)
                {
                    return str.Substring(0, x);
                }
                else
                {
                    countEnter++;
                    strList.Add(new T(str.Substring(0, x)));
                    Value = Convert.ToString(str[str.Length - 1]);
                    padding += 10;
                    return str.Substring(0, x); // Get the last x characters
                }
            }
        }

        public class T
        {
            public string a;
            public T(string a)
            {
                this.a = a;
            }
        }

        public override void Update(int pX, int pY)
        {
            if (!Visible)
                return;
            if (submittedOnce)
                submittedOnce = false;
            _pX = pX;
            _pY = pY;
            pYstr = pY;
            int mX = (int)MouseManager.X;
            int mY = (int)MouseManager.Y;
            bool mD = MouseManager.MouseState == MouseState.Left;


            focused = MouseInBounds(mX, mY, !mD && lmD);

            if (frames > framesToUpdateCursor)
            {
                frames = 0;
                showCursor = !showCursor;
            }

            if (focused)
            {
                if (KeyboardManager.TryReadKey(out KeyEvent key))
                {
                    if (key.Key == ConsoleKeyEx.Backspace)
                    {
                        if (Value == "" && strList.Count > 0)
                        {
                            Value = strList[strList.Count - 1].a;
                            strList.RemoveAt(strList.Count - 1);
                            countEnter--;
                            padding -= 10;
                            pYstr -= 20;
                        }
                        else
                        {
                            Value = Value.Remove(Value.Length - 1);
                        }
                    }
                    else if (key.Key == ConsoleKeyEx.Enter && countEnter < 22)
                    {
                        submittedOnce = true;
                        padding += 10;
                        pYstr += 20;
                        strList.Add(new T(Value));
                        Value = "";
                        countEnter++;
                    }
                    else if (key.Key == ConsoleKeyEx.Enter && countEnter == 22)
                    {
                        Value += "";
                    }
                    else if (Value.Length < (int)MathF.Round((width + 90) / font.Width) - (focused ? 1 : 0) && countEnter == 22)
                    {
                        Value += key.KeyChar;
                    }
                    else if (countEnter < 22)
                    //else
                    {
                       Value += key.KeyChar;

                    }
                }

                canv.DrawFilledRectangle(Color.Gray, x + pX, y + pY, width + 100, font.Height + padding * 2 + 2);
            }

            canv.DrawRectangle(Color.White, x + pX, y + pY, width + 100, font.Height + padding * 2 + 2);
            if(countEnter <= 22)
            {
                canv.DrawString(GetLastXCharacters(Value, (int)MathF.Round((width+90) / font.Width) - (focused ? 1 : 0)) + (showCursor && focused ? "_" : ""), font, Color.White, x + pX + 10, y + pYstr + 10 + 20 * countEnter);
            }
            

            if(strList.Count > 0)
            {
                for(int i = 0; i < strList.Count; i++)
                {
                    canv.DrawString(strList[i].a, font, Color.White, x + pX + 10, y + pYstr + 10 + 20 * (i + 0));
                }
            }

            lmD = mD;
            if (focused)
                frames++;
            if (frames % 10 == 0)
            {
                Kernel.strList = strList;
                Kernel.copyValue = Value;
            }

            if (lastTxt != null)
            {
                for (int i = 0; i < lastTxt.Count; i++)
                {
                    padding += 10;
                    pYstr += 20;
                    strList.Add(lastTxt[i]);
                    countEnter++;
                }
                lastTxt = null;
            }
        }

        public bool MouseInBounds(int mX, int mY, bool condition)
        {
            if (mX >= x + _pX && mX <= x + width + (padding * 2) + _pX &&
                mY >= y + _pY && mY <= y + (padding * 2) + _pY + font.Height)
            {
                if (condition)
                    return true;
            }
            else
            {
                if (condition)
                    return false;
            }

            return focused;
        }
    }
}
