using GameForOurProject.Buttons;
using GameForOurProject.GameHandler;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GameForOurProject
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont FontStyle;

        // Load Content


        // Circles
        //CircleWithText UsedCircle;
        List<CircleWithText> UsedCircles = new List<CircleWithText>();
        Dictionary<string, object> CurrentDictData = new Dictionary<string,object>();
        JArray TransferData;

        // Button
        List<Button> RetangleLabelsButtons = new List<Button>();

        // Case 1
        TextCreator MouseOverDataTxt;

        // Case 2
        List<Button> RetangleDataButtons = new List<Button>();

        TextCreator ColorTxt;
        List<Button> CircleColorButtons = new List<Button>();

        TextCreator SizeTxt;
        List<Button> CircleSizeButtons = new List<Button>();

        List<Button> RetangleButtonDataChooser = new List<Button>();

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {

            base.Initialize();

            IsMouseVisible = true;

            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width - 200;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height - 200;
            graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            FontStyle = Content.Load<SpriteFont>("Courier New");
            GameInputQueryHandler.Init(FontStyle);
            GameLogic.init(FontStyle, graphics,this);


            // Buttons
            CircleSizeButtons.Add(new Button() { Name = "Small", Texture = GlobalHandler.createCircleText(50, GraphicsDevice), ButtonX = 300, ButtonY = graphics.PreferredBackBufferWidth + 35 });
            CircleSizeButtons.Add(new Button() { Name = "Medium", Texture = GlobalHandler.createCircleText(55, GraphicsDevice), ButtonX = 360, ButtonY = graphics.PreferredBackBufferWidth + 33 });
            CircleSizeButtons.Add(new Button() { Name = "Large", Texture = GlobalHandler.createCircleText(60, GraphicsDevice), ButtonX = 425, ButtonY = graphics.PreferredBackBufferWidth + 30 });


            CircleColorButtons.Add(new Button() { Name = "LightBlue", Texture = GlobalHandler.createCircleText(50, GraphicsDevice), ButtonX = 620, ButtonY = graphics.PreferredBackBufferWidth + 30, BtnColor = Color.LightBlue });
            CircleColorButtons.Add(new Button() { Name = "Orange", Texture = GlobalHandler.createCircleText(50, GraphicsDevice), ButtonX = 675, ButtonY = graphics.PreferredBackBufferWidth + 30, BtnColor = Color.Orange });
            CircleColorButtons.Add(new Button() { Name = "Yellow", Texture = GlobalHandler.createCircleText(50, GraphicsDevice), ButtonX = 730, ButtonY = graphics.PreferredBackBufferWidth + 30, BtnColor = Color.Yellow });
            CircleColorButtons.Add(new Button() { Name = "Pink", Texture = GlobalHandler.createCircleText(50, GraphicsDevice), ButtonX = 785, ButtonY = graphics.PreferredBackBufferWidth + 30, BtnColor = Color.Pink });
            CircleColorButtons.Add(new Button() { Name = "Blue", Texture = GlobalHandler.createCircleText(50, GraphicsDevice), ButtonX = 840, ButtonY = graphics.PreferredBackBufferWidth + 30, BtnColor = Color.Blue });
            CircleColorButtons.Add(new Button() { Name = "Red", Texture = GlobalHandler.createCircleText(50, GraphicsDevice), ButtonX = 895, ButtonY = graphics.PreferredBackBufferWidth + 30, BtnColor = Color.Red });
            CircleColorButtons.Add(new Button() { Name = "Green", Texture = GlobalHandler.createCircleText(50, GraphicsDevice), ButtonX = 950, ButtonY = graphics.PreferredBackBufferWidth + 30, BtnColor = Color.Green });
            RetangleButtonDataChooser.Add(new Button(new TextCreator("_id", new Vector2(1030, graphics.PreferredBackBufferWidth + 40), 0.7f,FontStyle), 60, 35, Color.Gray, 1020, graphics.PreferredBackBufferWidth + 40, "_id",GraphicsDevice));
            RetangleButtonDataChooser.Add(new Button(new TextCreator("name", new Vector2(1100, graphics.PreferredBackBufferWidth + 40), 0.7f, FontStyle), 80, 35, Color.Gray, 1090, graphics.PreferredBackBufferWidth + 40, "name", GraphicsDevice));
            RetangleLabelsButtons.Add(new Button(new TextCreator("People", new Vector2(10, 0), 0.7f, FontStyle), 110, 35, Color.Gray, 0, 0, "People", GraphicsDevice));
            MouseOverDataTxt = new TextCreator("MouseTxt", new Vector2(210, graphics.PreferredBackBufferWidth + 50), 0.5f,FontStyle);
            ColorTxt = new TextCreator("Color:", new Vector2(500, graphics.PreferredBackBufferWidth + 40), 1f, FontStyle);
            SizeTxt = new TextCreator("Size:", new Vector2(210, graphics.PreferredBackBufferWidth + 40), 1f, FontStyle);



             Random rnd = new Random();



            // TODO : INITILIZE HOW TEXT WILL APPEAR!


            // TODO : CONNECTION TO DATA BASE!


            TransferData = (JArray)JsonConvert.DeserializeObject("[{level: 'pro',skill: 'rookie',name: 'Ido Bleicher',_id: 'f2747825-62bb-45e4-82d8-9e1bac8e9a02',label:'People'},{level: 'pro',skill: 'rookie',name: 'Omer Shalom',_id: 'om2747825-62bb-45e4-82d8-9e1bac8e9a02',label:'People'},{level: 'tipo',skill: 'rookie',name: 'Omri Shoham',_id: 'f2747825-62bb-45e4-82d8-9e1bac8e9a02',label:'Organisations'}]");


            foreach (JObject item in TransferData)
            {
                foreach (var value in item)
                {
                    CurrentDictData.Add(value.Key, value.Value);
                }

                UsedCircles.Add(new CircleWithText() { CircleLabelColor = (CurrentDictData.ContainsKey("label")) ? GlobalHandler.colorGenerator(CurrentDictData["label"].ToString()) : Color.Red, Data = CurrentDictData, Scale = 0.5f, CircleSize = GlobalHandler.Sizes.Small, text = new TextCreator(FontStyle, item["name"].ToString(), new Vector2(rnd.Next(0, 1600), rnd.Next(0, 800)),1f), circle = GlobalHandler.createCircleText(80, GraphicsDevice) });
                UsedCircles[UsedCircles.Count - 1].text.AdjustTextToShow();
                CurrentDictData = new Dictionary<string, object>();
            }

        }


        protected override void UnloadContent()
        {
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Timer.Clock += (float)gameTime.ElapsedGameTime.TotalMilliseconds;


            try
            {
                if (Timer.Clock >= GameLogic.Delay)
                {
                    GameInputQueryHandler.UpdateKeyBoardInput();

                    GameLogic.QuerySearch(TransferData, UsedCircles, CurrentDictData, FontStyle, GraphicsDevice, RetangleLabelsButtons);
                }

                GameLogic.UpdateHoldedCirclePlace(UsedCircles);


                GameLogic.MouseHoldingAndSelectingCircleCheck(UsedCircles);

                GameLogic.RightDownLabelsUpdate(RetangleButtonDataChooser, graphics, FontStyle, GraphicsDevice);

                if (GameLogic.IsSelectedCircle && Mouse.GetState().LeftButton == ButtonState.Pressed)
                {

                    GameLogic.CircleColorEventChecking(CircleColorButtons, UsedCircles, GraphicsDevice);
                    GameLogic.CircleSizeEventChecking(CircleSizeButtons, UsedCircles, GraphicsDevice);
                    GameLogic.RetangleSelectedDataView(RetangleButtonDataChooser, UsedCircles, GraphicsDevice);
                    GameLogic.RectangleUpdateLabel(RetangleLabelsButtons, UsedCircles);
                    GameLogic.UpdateHoldedCirclePlace(UsedCircles);

                }

                GameLogic.UpdateCheckIfMouseOver(UsedCircles, MouseOverDataTxt);
            }
            catch(Exception e)
            {
                Message.Show(e.Message);
            }

//            GameLogic.LastUpdateBeforeDraW(MouseOverDataTxt,graphics);

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();
            GameInputQueryHandler.Draw(spriteBatch);
            DrawingOptions.DrawUpLabel(RetangleLabelsButtons, spriteBatch);


            // Draw Main DATA
            DrawingOptions.MainDataDisplayDraw(spriteBatch, GameLogic.Retshown, ColorTxt, CircleColorButtons, SizeTxt, CircleSizeButtons, RetangleButtonDataChooser, MouseOverDataTxt, gameTime);


            foreach (CircleWithText circle in UsedCircles)
            {
                circle.Draw(spriteBatch);

                // Todo : Relations(Line...)
            }


            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
