using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForOurProject.Shape
{
    public class BorderWithText : DrawableGameComponent
    {
        public TextCreator TxtToShow { get; set; }
        public Rectangle Bounds { get; set; }
        public Color RetangleColor { get; set; }
        SpriteBatch spriteBatch;
        Texture2D pixel;

        public BorderWithText(Microsoft.Xna.Framework.Game game,Color colori, Rectangle bounds, TextCreator txtToShow) : base(game)
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // Somewhere in your LoadContent() method:
            pixel = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.White });
            RetangleColor = colori;
            Bounds = bounds;
            TxtToShow = txtToShow;
        }


        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            DrawBorder(Bounds, 5, RetangleColor);
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
