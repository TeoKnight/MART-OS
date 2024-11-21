using Cosmos.System;
using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mart.Controls;

namespace Mart.Windows
{
    internal class AddText : Window
    {
        public Text field;

        public AddText() : base(100, 100, 900, 500, "Show txt Files", Kernel.defFont, false)
        {
            field = new(20, 20, 760, font, 5);

            controls.Add(field);
        }

        public override void Update(VBECanvas canv, int mX, int mY, bool mD, int dmX, int dmY)
        {
            base.Update(canv, mX, mY, mD, dmX, dmY);

            

        }
    }
}
