using Cosmos.System.Graphics;
using System.Security.Cryptography.X509Certificates;
using Mart.Controls;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using IL2CPU.API.Attribs;
using Mart.Windows;

namespace Mart.Windows
{
    internal class BgChange : Window
    {
        [ManifestResourceStream(ResourceName = "Mart.Resource.Applogos.change.bmp")]
        static byte[] logoBytes;
        public Button clickButton = new("Click for change BackGround", 10, 20, Color.Green, Kernel.defFont);
        public BgChange() : base(100, 120, 250, 100, "BgChange", Kernel.defFont, false)
        {
            logo = new(logoBytes);
            controls.Add(clickButton);
        }

        [ManifestResourceStream(ResourceName = "Mart.Resource.bg1.bmp")]
        public static readonly byte[] bg1Bytes;

        [ManifestResourceStream(ResourceName = "Mart.Resource.bg2.bmp")]
        public static readonly byte[] bg2Bytes;

        [ManifestResourceStream(ResourceName = "Mart.Resource.bg3.bmp")]
        public static readonly byte[] bg3Bytes;

        [ManifestResourceStream(ResourceName = "Mart.Resource.bg4.bmp")]
        public static readonly byte[] bg4Bytes;

        [ManifestResourceStream(ResourceName = "Mart.Resource.bg5.bmp")]
        public static readonly byte[] bg5Bytes;

        public override void Update(VBECanvas canv, int mX, int mY, bool mD, int dmX, int dmY)
        {
            base.Update(canv, mX, mY, mD, dmX, dmY);

            if (clickButton.clickedOnce)
            {
                Kernel.bgCount++;
            }

            if (Kernel.bgCount > 6)
            {
                Kernel.bgCount = 1;
            }
            switch (Kernel.bgCount)
            {
                case 1:
                    Kernel.bg = null;
                    Kernel.bg = new Bitmap(Kernel.bgBytes);
                    break;
                case 2:
                    Kernel.bg = null;
                    Kernel.bg = new Bitmap(bg1Bytes);
                    break;
                case 3:
                    Kernel.bg = null;
                    Kernel.bg = new Bitmap(bg2Bytes);
                    break;
                case 4:
                    Kernel.bg = null;
                    Kernel.bg = new Bitmap(bg3Bytes);
                    break;
                case 5:
                    Kernel.bg = null;
                    Kernel.bg = new Bitmap(bg4Bytes);
                    break;
                case 6:
                    Kernel.bg = null;
                    Kernel.bg = new Bitmap(bg5Bytes);
                    break;
            }




        }
    }
}
