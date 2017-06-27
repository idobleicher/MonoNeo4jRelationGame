using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForOurProject.GameHandler
{
    public class GameInputQueryHandler
    {
        enum SpecialPrevChars
        {
            None,
            SHIFT
        }



        public static TextCreator Header { get; set; }
        public static TextCreator Query;
        public static bool  capsLook = false;
        private static SpecialPrevChars prevChar;

        public GameInputQueryHandler()
        {
        }

        public static void Init(SpriteFont spriteFont)
        {
            Header = new TextCreator(spriteFont,"Enter Your Cyper Query:", new Vector2(0,30), 1f);
            Query = new TextCreator(spriteFont,"", new Vector2(0, 65), 0.8f);
        }



        public static void UpdateKeyBoardInput()
        {
            foreach (Keys key in Keyboard.GetState().GetPressedKeys())
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    Query.Text += " ";
                    prevChar = SpecialPrevChars.None;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    Query.Text += "";
                    prevChar = SpecialPrevChars.None;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.D9) && prevChar.Equals(SpecialPrevChars.SHIFT))
                {
                    Query.Text += "(";
                    prevChar = SpecialPrevChars.None;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.D0) && prevChar.Equals(SpecialPrevChars.SHIFT))
                {
                    Query.Text += ")";
                    prevChar = SpecialPrevChars.None;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.OemOpenBrackets))
                {
                    Query.Text += "[";
                    prevChar = SpecialPrevChars.None;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.OemCloseBrackets))
                {
                    Query.Text += "]";
                    prevChar = SpecialPrevChars.None;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.OemMinus))
                {
                    Query.Text += "-";
                    prevChar = SpecialPrevChars.None;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.OemPeriod))
                {
                    Query.Text += ".";
                    prevChar = SpecialPrevChars.None;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.OemPlus))
                {
                    Query.Text += "=";
                    prevChar = SpecialPrevChars.None;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.OemQuotes))
                {
                    Query.Text += "'";
                    prevChar = SpecialPrevChars.None;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.OemComma))
                {
                    Query.Text += ",";
                    prevChar = SpecialPrevChars.None;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.CapsLock))
                {
                    capsLook = !capsLook;
                    prevChar = SpecialPrevChars.None;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.OemSemicolon))
                {
                    Query.Text += ";";
                    prevChar = SpecialPrevChars.None;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.OemTilde))
                {
                    Query.Text += "'";
                    prevChar = SpecialPrevChars.None;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Back))
                {
                    if (Query.Text.Length >= 1)
                        Query.Text = Query.Text.Substring(0, Query.Text.Length - 1);
                    prevChar = SpecialPrevChars.None;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.RightShift))
                {
                    prevChar = SpecialPrevChars.SHIFT;
                }
                else if (capsLook)
                {
                    Query.Text += key.ToString();
                    prevChar = SpecialPrevChars.None;
                }
                else
                {
                    Query.Text += key.ToString().ToLower();
                    prevChar = SpecialPrevChars.None;
                }


                if (!Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    Timer.Clock = 0;
                }
            }
        }
        

        public static void Draw(SpriteBatch sprite)
        {
            Header.Draw(sprite);
            Query.Draw(sprite);
        }
    }
}
