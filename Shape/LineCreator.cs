using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForOurProject.Shapes
{
    public class LineCreator
    {
        public Texture2D Line { get; set; }
        public Vector2 Start { get; set; }
        public Vector2 End { get; set; }

        public LineCreator(GraphicsDevice graphic,Vector2 start,Vector2 end)
        {
            Line = new Texture2D(graphic, 1, 1);
            Line.SetData<Color>(
            new Color[] { Color.Black });
            Start = start;
            End = end;
        }

        void DrawLine(SpriteBatch sb)
        {

            Vector2 edge = End - Start;

            // calculate angle to rotate line
            float angle =
                (float)Math.Atan2(edge.Y, edge.X);


            sb.Draw(Line,
                new Rectangle(// rectangle defines shape of line and position of start of line
                    (int)Start.X,
                    (int)Start.Y,
                    (int)edge.Length(), //sb will strech the texture to fill this rectangle
                    3), //width of line, change this to make thicker line
                null,
                Color.White,
                angle,     //angle of line (calulated above)
                new Vector2(0, 0), // point in line about which to rotate
                SpriteEffects.None,
                1);

        }
    }
}
