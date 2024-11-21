using Cosmos.System.Graphics;
using IL2CPU.API.Attribs;
using Mart.Controls;

namespace Mart.Windows
{
    internal class About : Window
    {
        [ManifestResourceStream(ResourceName ="Mart.Resource.zenithtext.bmp")]
        static byte[] zenithtext;
        Bitmap logoImg;

        Label creds,creds1,creds2,creds3;
        ImageView logoView;
        public About() : base(300, 300, 500, 170, "About MART", Kernel.defFont)
        {
            //logo = Kernel.logo;
            //logoImg = new Bitmap(zenithtext);
            creds = new("Created by", 20, 60, font, Kernel.textColDark);
            creds1 = new("Miras-M, Aizat-A, Roman-R, Timur-T - MART))",40,80,font,Kernel.textColDark);
            creds2 = new("Powered with the CosmosOS library DevKit", 20, 100,font, Kernel.textColDark);
            creds3 = new("ver. v0.0.3", 20, 130, font, Kernel.textColDark);
            //logoView = new(logoImg, 20, 10);
            controls.Add(creds);
            controls.Add(creds1);
            controls.Add(creds2);
            controls.Add(creds3);
            //controls.Add(logoView);
        }
    }
}
