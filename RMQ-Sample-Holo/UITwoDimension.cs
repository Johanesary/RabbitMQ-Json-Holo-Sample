using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urho;
using Urho.Gui;

namespace RMQ_Sample_Holo
{
    class UITwoDimension
    {
        //init ui element
        UIElement ui_node;

        //init base color
        Color uicolor = Color.Green;

        //aligment
        HorizontalAlignment horizontal = HorizontalAlignment.Left;
        VerticalAlignment vertical = VerticalAlignment.Center;

        //text
        Text x;
        Text y;
        Text z;
        Text rmq;

        //font
        Font ui_font = CoreAssets.Fonts.AnonymousPro;
        int font_small = 10;
        int font_medium = 30;
        int font_large = 40;

        int positionXx = 0;
        int positionYx = 0;

        int positionXy = 0;
        int positionYy = 100;

        int positionXz = 0;
        int positionYz = 200;

        public UITwoDimension(ref UIElement uielement)
        {
            ui_node = uielement;
            drawUI();
        }

        private void drawUI()
        {
            //draw font for X
            x = new Text();
            x.SetAlignment(horizontal, vertical);
            x.Position = new IntVector2(positionXx, positionYx);
            x.Name = "X";
            x.Value = "X = ";
            x.SetFont(ui_font, font_large);
            x.SetColor(uicolor);
            ui_node.AddChild(x);

            //draw font for Y
            y = new Text();
            y.SetAlignment(horizontal, vertical);
            y.Position = new IntVector2(positionXy, positionYy);
            y.Name = "Y";
            y.Value = "Y = ";
            y.SetFont(ui_font, font_large);
            y.SetColor(uicolor);
            ui_node.AddChild(y);

            //draw font for Z
            z = new Text();
            z.SetAlignment(horizontal, vertical);
            z.Position = new IntVector2(positionXz, positionYz);
            z.Name = "Z";
            z.Value = "Z = ";
            z.SetFont(ui_font, font_large);
            z.SetColor(uicolor);
            ui_node.AddChild(z);

            //draw font for RMQ
            rmq = new Text();
            rmq.SetAlignment(horizontal, vertical);
            rmq.Position = new IntVector2(positionXz, positionYz);
            rmq.Name = "RMQ";
            rmq.Value = "RMQ = ";
            rmq.SetFont(ui_font, font_large);
            rmq.SetColor(uicolor);
            ui_node.AddChild(z);
        }

        public void updateUI(string x_val, string y_val, string z_val, string rmq_data)
        {
            x.Value = "X = " + x_val;
            y.Value = "Y = " + y_val;
            z.Value = "Z = " + z_val;
            rmq.Value = "RMQ = " + rmq_data;
        }
    }
}
