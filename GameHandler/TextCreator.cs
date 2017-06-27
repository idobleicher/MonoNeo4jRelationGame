using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForOurProject
{
    public class TextCreator
    {
        public SpriteFont Font { get; set; }
        public Vector2 FontPos { get; set; }
        public Vector2 FontOrigin { get; set; }
        public float Scale { get; set; }
        public string Text { get; set; }
        public string ShownText { get; set; }

        public TextCreator(string text, Vector2 fontPos, float scale)
        {
            FontOrigin = Font.MeasureString(text) / 2;
            FontPos = fontPos;
            Text = text;
            ShownText = text;
            Scale = scale;
        }

        public TextCreator(string text, Vector2 fontPos, float scale,SpriteFont font)
        {
            Font = font;
            FontOrigin = Font.MeasureString(text) / 2;
            FontPos = fontPos;
            Text = text;
            ShownText = text;
            Scale = scale;
        }

        public TextCreator(SpriteFont texture, string text, Vector2 fontPos,float scale)
        {
            Font = texture;
            FontOrigin = Font.MeasureString(text) / 2;
            FontPos = fontPos;
            Text = text;
            ShownText = text;
            Scale = scale;
        }

        public void AdjustTextToShow()
        {
            int wordsize = 0;
            for (int i = 0; i < Text.Length; i++)
            {
                if (wordsize >= 2 && char.IsWhiteSpace(ShownText.ToString()[i]))
                {
                    ShownText = ShownText.Insert(i, "\n");
                    wordsize = 0;
                }
                if (wordsize >= 7)
                {
                    ShownText = ShownText.Insert(i, "...");
                    ShownText = ShownText.Remove(i + 3, ShownText.Length - (i + 3));
                    break;
                }
                wordsize++;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, Text.Replace(" ", "   "), FontPos, Color.Black, 0f, new Vector2(0, 0), Scale, SpriteEffects.None, 0);
        }

    }
}
