using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForOurProject
{
    public class RetangleWithText :  DrawableGameComponent
    {
        public TextCreator TxtToShow { get; set; }
        public Rectangle Bounds { get; set; }
        public Color RetangleColor { get; set; }
        SpriteBatch spriteBatch;
        Texture2D dummyTexture;
        Rectangle dummyRectangle;
        Texture2D pixel;

        public RetangleWithText(Rectangle rect, Color colori, Game game,TextCreator txtToShow)
            : base(game)
        {
            // Choose a high number, so we will draw on top of other components.
            DrawOrder = 1000;
            dummyRectangle = rect;
            RetangleColor = colori;
            TxtToShow = txtToShow;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            dummyTexture = new Texture2D(GraphicsDevice, 1, 1);
            dummyTexture.SetData(new Color[] { Color.White });

            // Somewhere in your LoadContent() method:
            pixel = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.White });
        }

        public RetangleWithText(Microsoft.Xna.Framework.Game game) : base(game)
        {
        }

        public void ChangeValueColors(Color NewColor)
        {
            RetangleColor = NewColor;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(dummyTexture, dummyRectangle, RetangleColor);
            //DrawBorder(new Rectangle(0,850,200,50), 5, RetangleColor);
            TxtToShow.Draw(spriteBatch);
            spriteBatch.End();
        }



        private void DrawBorder(Rectangle rectangleToDraw, int thicknessOfBorder, Color borderColor)
        {
            // Draw top line
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, rectangleToDraw.Width, thicknessOfBorder), borderColor);

            // Draw left line
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, thicknessOfBorder, rectangleToDraw.Height), borderColor);

            // Draw right line
            spriteBatch.Draw(pixel, new Rectangle((rectangleToDraw.X + rectangleToDraw.Width - thicknessOfBorder),
                                            rectangleToDraw.Y,
                                            thicknessOfBorder,
                                            rectangleToDraw.Height), borderColor);
            // Draw bottom line
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X,
                                            rectangleToDraw.Y + rectangleToDraw.Height - thicknessOfBorder,
                                            rectangleToDraw.Width,
                                            thicknessOfBorder), borderColor);
        }

    }
}
