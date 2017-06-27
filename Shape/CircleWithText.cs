using GameForOurProject.GameHandler;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GameForOurProject.GlobalHandler;

namespace GameForOurProject
{
    public class CircleWithText
    {
        public TextCreator text { get; set; }
        public Texture2D circle { get; set; }
        public float Scale { get; set; }
        public Sizes CircleSize { get; set; }
        public Dictionary<string, object> Data { get; set; }
        public Color CircleLabelColor { get; set; }

        public CircleWithText()
        {
            CircleSize = new Sizes();
            CircleLabelColor = new Color();
            Data = new Dictionary<string, object>();
        }

        public void ChangeValueColors(Color NewColor)
        {
            CircleLabelColor = NewColor;
           GameLogic.Retshown.ChangeValueColors(NewColor);
        }

        public void SetSize(Sizes sSizes, GraphicsDevice graphic)
        {
            switch (sSizes)
            {
                case Sizes.Small:
                    Scale = 0.5f;
                    circle = createCircleText(80, graphic);
                    break;
                case Sizes.Medium:
                    Scale = 0.75f;
                    circle = createCircleText(100, graphic);
                    break;
                case Sizes.Large:
                    Scale = 0.85f;
                    circle = createCircleText(120, graphic);
                    break;
                case Sizes.XLarge:
                    //Scale = 1.75f;
                    circle = createCircleText(140, graphic);
                    break;
                case Sizes.Macdonlands:
                    //Scale = 2f;
                    circle = createCircleText(160, graphic);
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(circle, new Vector2((float)(text.FontPos.X - text.Text.Length + 1), (float)(text.FontPos.Y - 50 * Scale)), CircleLabelColor);
            spriteBatch.DrawString(text.Font, text.ShownText, new Vector2(text.FontPos.X, text.FontPos.Y - Scale * 15), Color.Black, 0f, new Vector2(0, 0), Scale, SpriteEffects.None, 0);

        }
    }
}
