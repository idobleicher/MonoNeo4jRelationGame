using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForOurProject
{
    public class GlobalHandler
    {

        // Color Static List
        public static Dictionary<string, Color> ColorList = new Dictionary<string, Color>();

        public enum Sizes
        {
            Small,
            Medium,
            Large,
            XLarge,
            Macdonlands
        }

        /// <summary>
        /// Generic Color Generatoer
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        public static Color colorGenerator(string label)
        {
            if (!ColorList.Keys.Contains(label))
            {
                if (ColorList.Count == 6)
                {
                    ColorList.Add(label, Color.LightBlue);
                }
                if (ColorList.Count == 5)
                {
                    ColorList.Add(label, Color.Orange);
                }
                if (ColorList.Count == 4)
                {
                    ColorList.Add(label, Color.Yellow);
                }
                if (ColorList.Count == 3)
                {
                    ColorList.Add(label, Color.Pink);
                }
                if (ColorList.Count == 2)
                {
                    ColorList.Add(label, Color.Blue);
                }
                if (ColorList.Count == 1)
                {
                    ColorList.Add(label, Color.Red);
                }
                if (ColorList.Count == 0)
                {
                    ColorList.Add(label, Color.Green);
                }

            }

            return ColorList[label];
        }



        public static void SetColor(string label, Color NewColor)
        {
            ColorList[label] = NewColor;
        }

        /// <summary>
        /// Creating a Circle
        /// </summary>
        /// <param name="radius"></param>
        /// <param name="graphic"></param>
        /// <returns></returns>
        public static Texture2D createCircleText(int radius, GraphicsDevice graphic)
        {
            Texture2D texture = new Texture2D(graphic, radius, radius);
            Color[] colorData = new Color[radius * radius];

            float diam = radius / 2f;
            float diamsq = diam * diam;

            for (int x = 0; x < radius; x++)
            {
                for (int y = 0; y < radius; y++)
                {
                    int index = x * radius + y;
                    Vector2 pos = new Vector2(x - diam, y - diam);
                    if (pos.LengthSquared() <= diamsq)
                    {
                        colorData[index] = Color.White;
                    }
                    else
                    {
                        colorData[index] = Color.Transparent;
                    }
                }
            }

            texture.SetData(colorData);
            return texture;
        }


        public static string TextBuilder(Dictionary<string,object> Dict)
        {
            string text = "";

            foreach (var curr in Dict)
            {
                text += " " +curr.Key.ToString() + " :  " + curr.Value.ToString() + " "; 
            }

            return text;
        }

    }
}
