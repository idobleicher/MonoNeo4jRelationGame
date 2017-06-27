using GameForOurProject.GameHandler;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForOurProject.Buttons
{
    public class Button
    {
        public int ButtonX { get; set; }
        public int ButtonY { get; set; }
        public Texture2D Texture { get; set; }
        public string Name { get; set; }
        public Color BtnColor { get; set; }
        public TextCreator txt { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        public Button()
        {

        }

        public Button(TextCreator txtCreator,int width,int height,Color btnColor,int buttonX,int buttonY,string name,GraphicsDevice graphic)
        {
            Texture = new Texture2D(graphic, 1, 1, false, SurfaceFormat.Color);
            Texture.SetData(new[] { Color.White });
            txt = txtCreator;
            Width = width;
            Height = height;
            BtnColor = btnColor;
            ButtonX = buttonX;
            ButtonY = buttonY;
            Name = name;
        }



        public bool enterButton(MouseState MouseInput)
        {
            if (Vector2.Distance(new Vector2(ButtonX + 30  , ButtonY + 3), new Vector2(MouseInput.X , MouseInput.Y)) < 50f)
            {
                return true;
            }
            return false;
        }

        public void UpdateRtgDataButtons(MouseState MouseInput, List<CircleWithText> Circles, GraphicsDevice graphic, string label)
        {
            if (enterButton(MouseInput))
            {
                foreach (var circle in Circles.FindAll((X) => X.Data["label"].ToString() == label))
                {
                    string prevText = circle.text.Text;
                    circle.text.Text = circle.Data[Name].ToString();
                    circle.text.ShownText = circle.Data[Name].ToString();
                    circle.text.AdjustTextToShow();
                    circle.text.Text = prevText;
                }
            }
        }


        public void UpdateLabelButtons(MouseState MouseInput, List<CircleWithText> Circles)
        {
            if (enterButton(MouseInput) && !txt.Text.Equals("(0)"))
            {
               CircleWithText circle = Circles.First((X) => X.Data["label"].ToString() == Name);
                GameLogic.SelectedCircle = circle;
                GameLogic.IsSelectedCircle = true;
            }
        }


        public void UpdateCircleSize(MouseState MouseInput, List<CircleWithText> Circles, GraphicsDevice graphic, string label)
        {
            if (enterButton(MouseInput) && label != "none")
            {
                List<CircleWithText> MiniCircles = Circles.FindAll((X) => X.Data["label"].ToString() == label);
                switch (Name)
                {
                case "Small": //the name of the button

                foreach (CircleWithText circle in MiniCircles)
                {
                    circle.SetSize(GlobalHandler.Sizes.Small, graphic);
                }
                break;
                case "Medium": //the name of the button
                foreach (CircleWithText circle in MiniCircles)
                {
                    circle.SetSize(GlobalHandler.Sizes.Medium, graphic);
                }
                break;
                case "Large": //the name of the button
                foreach (CircleWithText circle in MiniCircles)
                {
                    circle.SetSize(GlobalHandler.Sizes.Large, graphic);
                }
                break;
                case "XLarge": //the name of the button
                foreach (CircleWithText circle in MiniCircles)
                {
                    circle.SetSize(GlobalHandler.Sizes.XLarge, graphic);
                }
                break;
                case "Macdonlands": //the name of the button
                foreach (CircleWithText circle in Circles)
                {
                    circle.SetSize(GlobalHandler.Sizes.Macdonlands, graphic);
                }
                break;
                default:
                break;
                    
                }
            }
        }
        public void UpdateCircleColor(MouseState MouseInput, List<CircleWithText> Circles, GraphicsDevice graphic, string label)
        {
            if (enterButton(MouseInput) && label != "none")
            {
                foreach (var circle in Circles.FindAll((X) => X.Data["label"].ToString() == label))
                {
                    circle.ChangeValueColors(BtnColor);
                }

                GlobalHandler.SetColor(label, BtnColor);
            }
        }

        public void DrawBorder(SpriteBatch spriteBatch,int thicknessOfBorder)
        {
            // Draw top line
            spriteBatch.Draw(Texture, new Rectangle(ButtonX,ButtonY, Width, thicknessOfBorder), BtnColor);

            // Draw left line
            spriteBatch.Draw(Texture, new Rectangle(ButtonX, ButtonY, thicknessOfBorder, Height), BtnColor);

            // Draw right line
            spriteBatch.Draw(Texture, new Rectangle((ButtonX + Width - thicknessOfBorder),
                                            ButtonY,
                                            thicknessOfBorder,
                                            Height), BtnColor);
            // Draw bottom line
            spriteBatch.Draw(Texture, new Rectangle(ButtonX,
                                             ButtonY + Height - thicknessOfBorder,
                                            Width,
                                            thicknessOfBorder), BtnColor);
            txt.Draw(spriteBatch);
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(Texture, new Rectangle((int)ButtonX, (int)ButtonY, Texture.Width, Texture.Height), (BtnColor != new Color(0, 0, 0, 0)) ? BtnColor : Color.Gray);
        }
    }
}
