using GameForOurProject.Buttons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForOurProject.GameHandler
{
    public class DrawingOptions
    {
        public static void DrawUpLabel(List<Button> ButtonsUpper,SpriteBatch spriteBatch)
        {
            foreach (Button btn in ButtonsUpper)
            {
                btn.DrawBorder(spriteBatch,5);
            }
        }

        public static void MainDataDisplayDraw(SpriteBatch spriteBatch,RetangleWithText RtgLabel, TextCreator Color,List<Button> btnColors,TextCreator Size,List<Button> btnSizes,List<Button> rtgDataChooser, TextCreator Data,GameTime time)
        {
            RtgLabel.Draw(time);

            if (GameLogic.MouseOverCircle)
            {
                
                Data.Draw(spriteBatch);
            }
            else
            {
                Color.Draw(spriteBatch);
                foreach (Button btn in btnColors)
                {
                    btn.Draw(spriteBatch);
                }

                Size.Draw(spriteBatch);
                foreach (Button btn in btnSizes)
                {
                    btn.Draw(spriteBatch);
                }


                // Data Save Edit Labels
                foreach (Button btn in rtgDataChooser)
                {

                    // Border Draw
                    btn.DrawBorder(spriteBatch,5);
                }
            }
            

        }
    }
}
