using GameForOurProject.Buttons;
using GameForOurProject.DBConnection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForOurProject.GameHandler
{
    public class GameLogic
    {
        // Delay Timer
        public static float Delay = 125f;
        public static bool IsHoldingCircle = false;
        public static bool IsSelectedCircle = false;
        public static bool MouseOverCircle = false;
        public static CircleWithText HoldedCircle = new CircleWithText();
        public static CircleWithText SelectedCircle = new CircleWithText();
        public static RetangleWithText Retshown;

        private static int FIRST_LABEL_DOWN_PLACE = 1020;

        public GameLogic()
        {
        }

        public static void init(SpriteFont FontStyle,GraphicsDeviceManager graphics,Game game)
        {
            Retshown = new RetangleWithText(new Rectangle(0, graphics.PreferredBackBufferWidth+40, 200, 40),Color.Yellow, game, new TextCreator("People", new Vector2(0, graphics.PreferredBackBufferWidth +40), 0.7f, FontStyle));
        }

        public static void QuerySearch(JArray TransferData,List<CircleWithText> UsedCircles,Dictionary<string,object> CurrentDictData,SpriteFont FontStyle,GraphicsDevice graphic,List<Button> RetangleLabelsButtons)
        {
            Dictionary<string, int> Dict = new Dictionary<string, int>() { {"", 0}};
            int Xplace = 0;
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))

            { 
                Random rnd = new Random();

                TransferData = (JArray)JsonConvert.DeserializeObject(DAL.Instance.QuerySearch(DAL.DBApiOptions.Neo4jDriver,GameInputQueryHandler.Query.Text));
                //TransferData = (JArray)JsonConvert.DeserializeObject("[{level: 'pro',skill: 'rookie',name: 'Ido Bleicher',_id: 'f2747825-62bb-45e4-82d8-9e1bac8e9a02',label: 'people'},{level: 'pro',skill: 'rookie',name: 'Holon Tim',_id: 'f2747825-62bb-45e4-82d8-9e1bac8e9a2',label: 'people'},{level: 'tipo',skill: 'rookie',name: 'Omri Shoham',_id: 'f2747825-62bb-45e4-82d8-9e1bac8e9a02',label:'Organisations'}]");

                UsedCircles.Clear();
                CurrentDictData.Clear();
                RetangleLabelsButtons.Clear();

                foreach (JObject item in TransferData)
                {
                    foreach (var value in item)
                    {
                        CurrentDictData.Add(value.Key, value.Value);

                    }

                    if (CurrentDictData.ContainsKey("label"))
                    {
                        if (Dict.ContainsKey(CurrentDictData["label"].ToString()))
                        {
                            Dict[CurrentDictData["label"].ToString()] += 1;
                        }
                        else
                        {
                            Dict.Add(CurrentDictData["label"].ToString(), 1);
                        }
                    }
                    else
                    {
                        Dict[""] += 1;
                    }


                    // Doing If Label Does Not Exists for A LINE.
                    UsedCircles.Add(new CircleWithText() { CircleLabelColor = (CurrentDictData.ContainsKey("label")) ? GlobalHandler.colorGenerator(CurrentDictData["label"].ToString()) : Color.Red, Data = CurrentDictData, Scale = 0.5f, CircleSize = GlobalHandler.Sizes.Small, text = new TextCreator(FontStyle, item["name"].ToString(), new Vector2(rnd.Next(0, 1600), rnd.Next(0, 800)), 1f), circle = GlobalHandler.createCircleText(80, graphic) });
                    UsedCircles[UsedCircles.Count - 1].text.AdjustTextToShow();
                    CurrentDictData = new Dictionary<string, object>();
                }


                foreach (var item in Dict)
                {
                    RetangleLabelsButtons.Add(new Button(new TextCreator(item.Key+"("+item.Value+")", new Vector2(Xplace+10, 0), 0.7f, FontStyle), item.Key.Count() * 18 + (item.Key.Equals("") ? 40 : 20), 35, Color.Gray, Xplace, 0, item.Key, graphic));
                    Xplace = item.Key.Count() * 18 + Xplace + 10 + (item.Key.Equals("") ? 40 : 20);
                    // RetangleLabelsButtons.Add()
                }

                GameInputQueryHandler.Query.Text = "";
                Timer.Clock = 0;
            }
        }


        public static void MouseHoldingAndSelectingCircleCheck(List<CircleWithText> UsedCircles)
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && !GameLogic.IsHoldingCircle)
            {
                var mouseRect = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1);

                foreach (CircleWithText item in UsedCircles)
                {
                    var txtRect = new Rectangle((int)item.text.FontPos.X, (int)item.text.FontPos.Y, item.text.Text.Length * 20, item.text.Text.Length * 10);

                    if (mouseRect.Intersects(txtRect))
                    {
                        GameLogic.IsHoldingCircle = true;
                        GameLogic.IsSelectedCircle = true;
                        HoldedCircle = item;
                        SelectedCircle = item;
                        break;
                    }

                }
            }

        }

        public static void RightDownLabelsUpdate(List<Button> RetangleButtonDataChooser, GraphicsDeviceManager graphics,SpriteFont FontStyle,GraphicsDevice grap)
        {
            if (IsSelectedCircle)
            {
                int Xplace = FIRST_LABEL_DOWN_PLACE;
                RetangleButtonDataChooser.Clear();

                foreach (var item in SelectedCircle.Data)
                {
                    RetangleButtonDataChooser.Add(new Button(new TextCreator(item.Key, new Vector2(Xplace + 10, graphics.PreferredBackBufferWidth /2 - 20), 0.7f, FontStyle), item.Key.Count() * 18, 35, Color.Gray, Xplace , graphics.PreferredBackBufferWidth /2 - 20, item.Key, grap));

                    Xplace = item.Key.Count() * 18 + Xplace +10;
                }
            }
        }

        public static void UpdateHoldedCirclePlace(List<CircleWithText> UsedCircles)
        {

            if (GameLogic.IsHoldingCircle)
            {
                Rectangle HoldedRect = new Rectangle((int)Mouse.GetState().X - 40, (int)Mouse.GetState().Y, HoldedCircle.text.Text.Length * 20, HoldedCircle.text.Text.Length * 10);


                foreach (CircleWithText item in UsedCircles)
                {
                    if (item != HoldedCircle)
                    {
                        Rectangle selected = new Rectangle((int)item.text.FontPos.X, (int)item.text.FontPos.Y, item.text.Text.Length * 20, item.text.Text.Length * 10);

                        if (Vector2.Distance(item.text.FontPos, new Vector2(Mouse.GetState().X - 40, Mouse.GetState().Y)) < 100f)
                        {
                            float x = Convert.ToInt32((Mouse.GetState().X - 40 - HoldedCircle.text.FontPos.X) * 1.5);
                            float y = Convert.ToInt32((Mouse.GetState().Y - HoldedCircle.text.FontPos.Y) * 1.5);
                            item.text.FontPos = new Vector2(item.text.FontPos.X + x, item.text.FontPos.Y + y);
                        }
                    }
                }
                HoldedCircle.text.FontPos = new Vector2(Mouse.GetState().X - 40, Mouse.GetState().Y);

            }

            if (Mouse.GetState().LeftButton == ButtonState.Released && GameLogic.IsHoldingCircle)
            {
                GameLogic.IsHoldingCircle = false;
            }
        }


        // Todo:
        public static void UpdateCheckIfMouseOver(List<CircleWithText> UsedCircles,TextCreator MouseOveDataTxt)
        {
            // TODO : Fitting this code
            bool found = false;
            foreach (CircleWithText item in UsedCircles)
            {
                if (Vector2.Distance(item.text.FontPos, new Vector2(Mouse.GetState().X - 40, Mouse.GetState().Y)) < 100f)
                {
                    GameLogic.MouseOverCircle = true;
                    //DownLabelRetTxt.TxtToShow.TextUpdate(item.Data);
                    Retshown.ChangeValueColors(item.CircleLabelColor);
                    Retshown.TxtToShow.ShownText = (item.Data.ContainsKey("label") ? item.Data["label"].ToString() : "NotLabel");
                    Retshown.TxtToShow.Text = (item.Data.ContainsKey("label") ? item.Data["label"].ToString() : "NotLabel");
                    //if(item.Data.ContainsKey("label"))
                    //{
                    //    Retshown.Bounds = new Rectangle(0, Retshown.Bounds.Y, item.Data["label"].ToString().Count() * 20, 40);
                    //}
                    MouseOveDataTxt.Text = GlobalHandler.TextBuilder(item.Data);
                    MouseOveDataTxt.AdjustTextToShow();
                    // MouseOverData ... mouse over TODO:

                    /// Build The Text Creator
                    found = true;
                }
            }
            if(!found)
            {
                GameLogic.MouseOverCircle = false;
                if (IsSelectedCircle)
                {
                    Retshown.ChangeValueColors(SelectedCircle.CircleLabelColor);
                    Retshown.TxtToShow.ShownText = (SelectedCircle.Data.ContainsKey("label") ? SelectedCircle.Data["label"].ToString() : "NotLabel");
                    Retshown.TxtToShow.Text = (SelectedCircle.Data.ContainsKey("label") ? SelectedCircle.Data["label"].ToString() : "NotLabel");
                }
            }
        }



        //public static void LastUpdateBeforeDraW(TextCreator txtDataDrawPlace,GraphicsDeviceManager graphic)
        //{
        //    txtDataDrawPlace.FontPos = new Vector2(Retshown.Bounds.Width + 20, 800);
        //}


        public static void CircleSizeEventChecking(List<Button> CircleSizeButtons,List<CircleWithText> UsedCircles,GraphicsDevice graphic)
        {
            foreach (Button btn in CircleSizeButtons)
            {
                 btn.UpdateCircleSize( Mouse.GetState(), UsedCircles, graphic, ((SelectedCircle.Data.ContainsKey("label")) ? SelectedCircle.Data["label"].ToString() : "none"));
            }
        }

        public static void CircleColorEventChecking( List<Button> CircleColorButtons, List<CircleWithText> UsedCircles, GraphicsDevice graphic)
        {
            foreach (Button btn in CircleColorButtons)
            {
                 btn.UpdateCircleColor(Mouse.GetState(), UsedCircles, graphic, ((SelectedCircle.Data.ContainsKey("label")) ? SelectedCircle.Data["label"].ToString() : "none"));
            }
        }

        public static void RetangleSelectedDataView(List<Button> RtgButtons, List<CircleWithText> UsedCircles, GraphicsDevice graphic)
        {
            foreach (Button btn in RtgButtons)
            {
                btn.UpdateRtgDataButtons(Mouse.GetState(), UsedCircles, graphic, ((SelectedCircle.Data.ContainsKey("label")) ? SelectedCircle.Data["label"].ToString() : "none"));
            }
        }

        public static void RectangleUpdateLabel(List<Button> RtLabelButtons, List<CircleWithText> UsedCircle)
        {
            foreach (Button btn in RtLabelButtons)
            {
                btn.UpdateLabelButtons(Mouse.GetState(), UsedCircle);
            }
        }


    }
}
