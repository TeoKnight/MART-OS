using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mart.Controls;
using IL2CPU.API.Attribs;

namespace Mart.Windows
{
    internal class TheLastTxt : Window
    {
        public Text field;

        public TheLastTxt() : base(100, 100, 900, 500, "Show txt Files", Kernel.defFont, false)
        {
            field = new(20, 20, 760, font, 5, Kernel.strList);
            controls.Add(field);
        }

        public override void Update(VBECanvas canv, int mX, int mY, bool mD, int dmX, int dmY)
        {
            base.Update(canv, mX, mY, mD, dmX, dmY);
        }
    }
}
