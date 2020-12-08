using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace JumpThing

{   // Access modifier and main class name (Game1)
    public class Game1 : Game

    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch; // spriteBatch is used for drawing text strings and sprites in one or more batches

        Texture2D backgroundTxr, playerSheetTxr, platformSheetTxr, whiteBox; // visible texture sheet, texture used to draw background, player and platform
        SpriteFont UITextFont, HeartFont; // fonts used in game for lives and level counter
        SoundEffect jumpSound, bumpSound, fanfareSound; // sound effects used in game
        

        Point screenSize = new Point(800, 450); // Constructs a point with X and Y from two values
        int levelNumber = 0; // integer = whole number

        PlayerSprite PlayerSprite; // class
        CoinSprite coinSprite; // class
      

        List<List<PlatformSprite>> levels = new List<List<PlatformSprite>>(); // new instance of PlatformSprite list
        List<Vector2> coins = new List<Vector2>();

        // Constructor
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true; // boolean must return true or false
        }
        // Initialize game
        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = screenSize.X;
            _graphics.PreferredBackBufferHeight = screenSize.Y;
            _graphics.ApplyChanges();

            base.Initialize();
        }
        // Load graphics and sounds
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            backgroundTxr = Content.Load<Texture2D>("JumpThing_background");
            playerSheetTxr = Content.Load<Texture2D>("JumpThing_spriteSheet1");
            platformSheetTxr = Content.Load<Texture2D>("JumpThing_spriteSheet2");
            UITextFont = Content.Load<SpriteFont>("UIText");
            HeartFont = Content.Load<SpriteFont>("HeartFont");
            jumpSound = Content.Load<SoundEffect>("jump");
            bumpSound = Content.Load<SoundEffect>("bump");
            fanfareSound = Content.Load<SoundEffect>("fanfare");

            whiteBox = new Texture2D(GraphicsDevice, 1, 1);
            whiteBox.SetData(new[] { Color.White });

            PlayerSprite = new PlayerSprite(playerSheetTxr, whiteBox, new Vector2(100, 50), jumpSound, bumpSound);
            coinSprite = new CoinSprite(playerSheetTxr, whiteBox, new Vector2(200, 200));

            BuildLevels();
          

        }
        // called when game should update
        protected override void Update(GameTime gameTime)

        {   // defines what buttons/keys are being pressed
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            PlayerSprite.Update(gameTime, levels[levelNumber]);

            if (PlayerSprite.spritePos.Y > screenSize.Y + 50)
            {
                PlayerSprite.lives--;
                if (PlayerSprite.lives <= 0) 
                {
                    PlayerSprite.lives = 3; // total number of lives
                    levelNumber = 0;
                }

            
                PlayerSprite.ResetPlayer(new Vector2(100, 50));
            }

                if (PlayerSprite.checkCollision(coinSprite)) // check if player is colliding with coin
            {
                levelNumber++; // player levels up if they collide with coin
                if (levelNumber >= levels.Count) levelNumber = 0;
                coinSprite.spritePos = coins[levelNumber];
                PlayerSprite.ResetPlayer(new Vector2(100, 50));
                fanfareSound.Play(); // fanfare sound plays when collision with coin happens

            }

            base.Update(gameTime);
        }

        // draws frame
        protected override void Draw(GameTime gameTime)
        {

            string livesString = "";
            _spriteBatch.Begin();

            _spriteBatch.Draw(backgroundTxr, new Rectangle(0, 0, screenSize.X, screenSize.Y), Color.White); // draws background

            PlayerSprite.Draw(_spriteBatch, gameTime); // draws PlayerSprite
            coinSprite.Draw(_spriteBatch, gameTime); // draws coinSprite

            foreach (PlatformSprite platform in levels[levelNumber]) platform.Draw(_spriteBatch, gameTime); // how many platforms appear in each level

            for (int i = 0; i < PlayerSprite.lives; i++) livesString += "p";
            
            // draws level and lives and colour
            _spriteBatch.DrawString(HeartFont, livesString, new Vector2(15, 5), Color.White);

            // the size in pixes of the font
            _spriteBatch.DrawString(
                UITextFont,
                "level " + (levelNumber + 1),
                new Vector2(screenSize.X - 15 - UITextFont.MeasureString("level " + (levelNumber + 1)).X, 5),
                Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        void BuildLevels()
        {
            // each level and platform in the game
            levels.Add(new List<PlatformSprite>());
            levels[0].Add(new PlatformSprite(platformSheetTxr, whiteBox, new Vector2(100, 300)));
            levels[0].Add(new PlatformSprite(platformSheetTxr, whiteBox, new Vector2(250, 300)));
            coins.Add(new Vector2(200, 200));

            levels.Add(new List<PlatformSprite>());
            levels[1].Add(new PlatformSprite(platformSheetTxr, whiteBox, new Vector2(100, 400)));
            levels[1].Add(new PlatformSprite(platformSheetTxr, whiteBox, new Vector2(250, 350)));
            coins.Add(new Vector2(400, 200));

        }
    }
}


