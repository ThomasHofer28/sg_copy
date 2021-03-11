// MultiMediaTechnology / FHS
// MultiMediaProjekt 1
// Programming, Art & Music - Hofer Thomas
// Co-Music Producer - Veltman Bob

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;


namespace SG_01
{
    //  ****** ****** ****** ****** ****   *    *        ****** ****** ****** ****** ***    *    *
    //  *      *    * *      *      *   *   *  *         *      *    * *      *      *   *   *  *
    //  ****** ****** ***    ***    *    *   **          *  *** ****** ***    ***    *    *   **
    //       * *      *      *      *   *    **          *    * *  *   *      *      *   *    **
    //  ****** *      ****** ****** ****     **          ****** *   *  ****** ****** ****     **

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        enum GameState { introScreen, menuScreen, learnScreen, gameScreen, endScreen}
        GameState currentState = GameState.introScreen;

        // ****** Background Templates ************
        Texture2D backgroundTemplateGameRound;
        Texture2D backgroundTemplateGameRoundStart;
        Texture2D backgroundIntro;
        Texture2D backgroundMenu;
        Texture2D backgroundEnd;
        Texture2D backgroundLearn;
        Vector2 backgroundPosition = new Vector2(0, 0);

        // ****** players **********
        Player playerOne = new Player();
        Player playerTwo = new Player();
        Player playerThree = new Player();
        Player playerFour = new Player();
        int[] startingPositions = new int[4];
        const int startPlayerXCord = 88;

        // ******* alround *********
        float animationSpeed = 0.07f;
        const float fastSpeed = 200;
        const float slowSpeed = 100;
        const int tileSize = 32;
        const int collideSize = tileSize - 2;

        // ******** death ***********
        Texture2D deathTexture;

        // ****** sand ******
        Texture2D sandTexture;
        Vector2 sandPosition = new Vector2(0, 0);

        // ****** quicksand *****
        Texture2D quicksandTexture;
        Vector2 quicksandPosition = new Vector2(0, 0);
        Rectangle quicksandAnimationRect = new Rectangle(0, 0, tileSize, tileSize);
        Vector2 quicksandAnimationTilling = new Vector2(4, 1);
        float animationIndexQuicksand = 0;

        // ****** cactus ******
        Texture2D cactusTexture;
        Vector2 cactusPosition = new Vector2(0, 0);
        Rectangle cactusAnimationRect = new Rectangle(0, 0, tileSize, tileSize);
        Vector2 cactusAnimationTilling = new Vector2(2, 1);
        float animationIndexCactus = 0;

        // ****** cactusghosts ********
        Texture2D cactusghostTexture;
        Vector2 cactusghostPosition = new Vector2(0, 0);
        Rectangle cactusghostAnimationRect = new Rectangle(0, 0, tileSize, tileSize);
        Vector2 cactusghostAnimationTilling = new Vector2(2, 1);
        float animationIndexCactusghost = 0;
        float[,] cactusProjectileArray;
        float[,] cactusProjectileStartArray;

        // ****** vultures ***********
        Texture2D vultureTexture;
        Vector2 vulturePosition = new Vector2(0, 0);
        Rectangle vultureAnimationRect = new Rectangle(0, 0, tileSize, tileSize);
        Vector2 vultureAnimationTilling = new Vector2(2, 1);
        float animationIndexVulture = 0;
        float[,] vultureArray;
        float vultureOneSpeedX = fastSpeed;
        float vultureOneSpeedY = fastSpeed;
        float vultureTwoSpeedX = fastSpeed;
        float vultureTwoSpeedY = fastSpeed;
        float vultureThreeSpeedX = fastSpeed;
        float vultureThreeSpeedY = fastSpeed;
        float vultureFourSpeedX = fastSpeed;
        float vultureFourSpeedY = fastSpeed;

        // ****** coin *******
        Texture2D coinTexture;
        Vector2 coinPosition = new Vector2(0, 0);
        Rectangle coinAnimationRect = new Rectangle(0, 0, tileSize, tileSize);
        Vector2 coinAnimationTilling = new Vector2(2, 1);
        float animationIndexCoin = 0;
        bool gotCoin = false;
        bool setCoin = false;

        // ******* level 1 *********
        int[,] levelOneArray = new int[27, 59];

        // ******** Points *************
        bool firstInGoal = false;
        int positionInGoal;
        int highestPoints;
        bool highestPointsSet = false;

        // ******** Timer ********
        float startCountdown = 6.0f;
        int showStartCountdown;
        float playCountdown = 20.0f;
        int showPlayCountdown;
        float endTimer = 3.0f;

        // ****** game bools *****
        bool gameHasStarted = false;
        bool hasPressed = false;
        float hasPressedTimer;
        bool hasEndedEarly = false;

        // ****** menü *******
        float roundsWannaPlay = 1.0f;
        int roundsToPlay;
        int roundsCounter;
        int currentRound = 1;
        bool setRoundCounter = false;
        float menuNav = 0;
        int menuState = 0;

        // ***** end *******
        int[,] standings = new int[4,3];
        int[,] sortedStandings = new int[4,2];
        bool doStandings = false;
        Texture2D beani;
        Texture2D salsa;
        Texture2D natscho;
        Texture2D kelpi;

        // ****** fonts **********
        SpriteFont bodytext;
        SpriteFont menutext;

        // ****** music ********
        Song gameMusic;
        Song menuMusic;
        bool startMusicActive = false;
        bool endMusicActive = false;
        bool gameMusicActive = false;
        SoundEffect deathSound;

        // ********************** GAME 1 **************
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            // ******** starting position ****************
            const int startPlayerXCord = 88;
            HelperMethods.FillArrayRandomSingleOccurrence(startingPositions);
            HelperMethods.StartPositionY(startingPositions);
            playerOne.position = new Vector2(startPlayerXCord, startingPositions[0]);
            playerTwo.position = new Vector2(startPlayerXCord, startingPositions[1]);
            playerThree.position = new Vector2(startPlayerXCord, startingPositions[2]);
            playerFour.position = new Vector2(startPlayerXCord, startingPositions[3]);

            // LEVEL 1
            Leveleditor.NewLevelOneArray(levelOneArray);

            // Projectile positions start array
            cactusProjectileArray = CactusProjectiles.NewProjectileArray(levelOneArray);
            cactusProjectileStartArray = CactusProjectiles.NewProjectileArray(levelOneArray);

            // vulturearray
            vultureArray = Vultures.NewVultureArray();

            Content.RootDirectory = "Content";
        }

        // ******************************************************************************************************************************************
        // ****************************************************          INITIALIZE        **********************************************************
        // ******************************************************************************************************************************************
        
        protected override void Initialize()
        {
            playerOne.rectCollider = playerOne.animationRect;
            playerTwo.rectCollider = playerTwo.animationRect;
            playerThree.rectCollider = playerThree.animationRect;
            playerFour.rectCollider = playerFour.animationRect;
            base.Initialize();
        }

        // ******************************************************************************************************************************************
        // ****************************************************            LOAD            **********************************************************
        // ******************************************************************************************************************************************
        
        protected override void LoadContent()
        {
            // SpriteBatch, use to draw textures
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // player
            playerOne.texture = Content.Load<Texture2D>("images/Beani");
            playerTwo.texture = Content.Load<Texture2D>("images/Kelpi");
            playerThree.texture = Content.Load<Texture2D>("images/Natscho");
            playerFour.texture = Content.Load<Texture2D>("images/Salsa");

            // objects
            deathTexture = Content.Load<Texture2D>("images/blood");
            sandTexture = Content.Load<Texture2D>("images/Sand");
            quicksandTexture = Content.Load<Texture2D>("images/Quicksand");
            cactusTexture = Content.Load<Texture2D>("images/Cactus");
            cactusghostTexture = Content.Load<Texture2D>("images/CactusGhost");
            coinTexture = Content.Load<Texture2D>("images/coin");
            vultureTexture = Content.Load<Texture2D>("images/vulture");

            // fonts
            bodytext = Content.Load<SpriteFont>("Fonts/CodeMedium24");
            menutext = Content.Load<SpriteFont>("Fonts/ChangaItalic60");

            // screens
            backgroundTemplateGameRound = Content.Load<Texture2D>("images/GameTemplate");
            backgroundTemplateGameRoundStart = Content.Load<Texture2D>("images/GameTemplateStart");
            backgroundIntro = Content.Load<Texture2D>("images/IntroTemplate");
            backgroundMenu = Content.Load<Texture2D>("images/MenuTemplate");
            backgroundEnd = Content.Load<Texture2D>("images/EndTemplate");
            backgroundLearn = Content.Load<Texture2D>("images/LearnTemplate");

            // standing icons
            beani = Content.Load<Texture2D>("images/BeaniStanding");
            salsa = Content.Load<Texture2D>("images/SalsaStanding");
            natscho = Content.Load<Texture2D>("images/NatschoStanding");
            kelpi = Content.Load<Texture2D>("images/KelpiStanding");

            // player
            playerOne.animationRect = new Rectangle(0, 0, (int)(playerOne.texture.Width / playerOne.animationTilling.X),
                (int)(playerOne.texture.Height / playerOne.animationTilling.Y));
            playerTwo.animationRect = new Rectangle(0, 0, (int)(playerTwo.texture.Width / playerTwo.animationTilling.X),
                (int)(playerTwo.texture.Height / playerTwo.animationTilling.Y));
            playerThree.animationRect = new Rectangle(0, 0, (int)(playerThree.texture.Width / playerThree.animationTilling.X),
                (int)(playerThree.texture.Height / playerThree.animationTilling.Y));
            playerFour.animationRect = new Rectangle(0, 0, (int)(playerFour.texture.Width / playerFour.animationTilling.X),
                (int)(playerFour.texture.Height / playerFour.animationTilling.Y));
            
            // death
            playerOne.deathAnimationRect = new Rectangle(0, 0, (int)(deathTexture.Width / playerOne.deathAnimationTilling.X), tileSize);
            playerTwo.deathAnimationRect = new Rectangle(0, 0, (int)(deathTexture.Width / playerTwo.deathAnimationTilling.X), tileSize);
            playerThree.deathAnimationRect = new Rectangle(0, 0, (int)(deathTexture.Width / playerThree.deathAnimationTilling.X), tileSize);
            playerFour.deathAnimationRect = new Rectangle(0, 0, (int)(deathTexture.Width / playerFour.deathAnimationTilling.X), tileSize);

            // quicksand
            quicksandAnimationRect = new Rectangle(0, 0, (int)(quicksandTexture.Width / quicksandAnimationTilling.X), tileSize);

            // cactus
            cactusAnimationRect = new Rectangle(0, 0, (int)(cactusTexture.Width / cactusAnimationTilling.X), tileSize);

            // cactusGhost
            cactusghostAnimationRect = new Rectangle(0, 0, (int)(cactusghostTexture.Width / cactusghostAnimationTilling.X), tileSize);

            // coin
            coinAnimationRect = new Rectangle(0, 0, (int)(coinTexture.Width / coinAnimationTilling.X), tileSize);

            // vulture
            vultureAnimationRect = new Rectangle(0, 0, (int)(vultureTexture.Width / vultureAnimationTilling.X), tileSize);

            // Music
            gameMusic = Content.Load<Song>("music/SpeedyGreedyThemeGame");
            menuMusic = Content.Load<Song>("music/SpeedyGreedyThemeMenu");
            deathSound = Content.Load<SoundEffect>("music/deathsound");
        }

        // ******************************************************************************************************************************************
        // ****************************************************           UNLOAD           **********************************************************
        // ******************************************************************************************************************************************

        protected override void UnloadContent()
        {
        }

        // ******************************************************************************************************************************************
        // ****************************************************           UPDATE           **********************************************************
        // ****************************************************************************************************************************************** 
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        
        protected override void Update(GameTime gameTime)
        {
            // **** Exit Game ****
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // ***** deltatime *****
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds; // 60 fps 0,016*
            
            // *** Controller ****
            GamePadCapabilities p1C = GamePad.GetCapabilities(PlayerIndex.One);
            GamePadState gamePadOneState = GamePad.GetState(PlayerIndex.One);
            GamePadCapabilities p2C = GamePad.GetCapabilities(PlayerIndex.Two);
            GamePadState gamePadTwoState = GamePad.GetState(PlayerIndex.Two);
            GamePadCapabilities p3C = GamePad.GetCapabilities(PlayerIndex.Three);
            GamePadState gamePadThreeState = GamePad.GetState(PlayerIndex.Three);
            GamePadCapabilities p4C = GamePad.GetCapabilities(PlayerIndex.Four);
            GamePadState gamePadFourState = GamePad.GetState(PlayerIndex.Four);

            // ************ GAMESTATES *********
            switch (currentState)
            {
                case GameState.introScreen:
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                        {
                            currentState = GameState.menuScreen;
                        }
                        if (gamePadOneState.Buttons.A == ButtonState.Pressed)
                        {
                            currentState = GameState.menuScreen;
                        }
                    }
                    break;
                case GameState.menuScreen:
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.Back))
                        {
                            currentState = GameState.gameScreen;
                        }
                        if (gamePadOneState.Buttons.A == ButtonState.Pressed && menuState == 0 && hasPressedTimer < 0)
                        {
                            currentState = GameState.learnScreen;
                        }
                        if (gamePadOneState.Buttons.A == ButtonState.Pressed && menuState == 3 && hasPressedTimer < 0)
                        {
                            Exit();
                        }
                    }
                    break;
                case GameState.learnScreen:
                    {
                        if (gamePadOneState.Buttons.A == ButtonState.Pressed && hasPressedTimer < 0)
                        {
                            currentState = GameState.gameScreen;
                        }
                        if (gamePadOneState.Buttons.B == ButtonState.Pressed)
                        {
                            currentState = GameState.menuScreen;
                        }
                    }
                    break;
                case GameState.gameScreen:
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.Back))
                        {
                            currentState = GameState.endScreen;
                        }
                        if (roundsCounter < 1)
                        {
                            currentState = GameState.endScreen;
                            hasPressed = true;
                            hasPressedTimer += 4.0f;
                        }
                    }
                    break;
                case GameState.endScreen:
                    {
                        // *** Reset Whole Game ***
                        if (gamePadOneState.Buttons.A == ButtonState.Pressed && hasPressedTimer < 0)
                        {
                            gameMusicActive = false;
                            newRound();
                            playerOne.points = 0;
                            playerTwo.points = 0;
                            playerThree.points = 0;
                            playerFour.points = 0;
                            highestPointsSet = false;
                            setRoundCounter = false;
                            doStandings = false;
                            startMusicActive = false;
                            currentState = GameState.menuScreen;
                            endMusicActive = false;
                            currentRound = 1;
                        }
                    }
                    break;
            }

            // **** timer press a ******
            if (hasPressed)
            {
                hasPressedTimer -= 1.3f * deltaTime;
                if (hasPressedTimer < 0)
                {
                    hasPressed = false;
                }
            }
            if (hasPressed == false && gamePadOneState.Buttons.A == ButtonState.Pressed)
            {
                hasPressedTimer = 1.0f;
                hasPressed = true;
            }


            // **** Music ****
            MediaPlayer.IsRepeating = true;
            if (startMusicActive == false)
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(menuMusic);
                startMusicActive = true;
            }
            if (currentState == GameState.endScreen)
            {
                if (endMusicActive == false)
                {
                    MediaPlayer.Stop();
                    MediaPlayer.Play(menuMusic);
                    endMusicActive = true;
                }
            }
            if (currentState == GameState.gameScreen)
            {
                if (gameMusicActive == false)
                {
                    MediaPlayer.Stop();
                    MediaPlayer.Play(gameMusic);
                    gameMusicActive = true;
                }
            }

            // **** soundeffect death *******
            if (playerOne.isDead == true && playerOne.playDeathSound == false)
            {
                deathSound.Play();
                playerOne.playDeathSound = true;
            }
            if (playerTwo.isDead == true && playerTwo.playDeathSound == false)
            {
                deathSound.Play();
                playerTwo.playDeathSound = true;
            }
            if (playerThree.isDead == true && playerThree.playDeathSound == false)
            {
                deathSound.Play();
                playerThree.playDeathSound = true;
            }
            if (playerFour.isDead == true && playerFour.playDeathSound == false)
            {
                deathSound.Play();
                playerFour.playDeathSound = true;
            }

            // **** Movement *** allowed if gameScreen & players alive **********
            if (currentState == GameState.gameScreen)
            {
                if (p1C.IsConnected && !playerOne.isDead && !playerOne.inGoal)
                {
                    MovePlayer(deltaTime, p1C, gamePadOneState, playerOne);
                }
                if (p2C.IsConnected && !playerTwo.isDead && !playerTwo.inGoal)
                {
                    MovePlayer(deltaTime, p2C, gamePadTwoState, playerTwo);
                }
                if (p3C.IsConnected && !playerThree.isDead && !playerThree.inGoal)
                {
                    MovePlayer(deltaTime, p3C, gamePadThreeState, playerThree);
                }
                if (p4C.IsConnected && !playerFour.isDead && !playerFour.inGoal)
                {
                    MovePlayer(deltaTime, p4C, gamePadFourState, playerFour);
                }
            }

            playerOne.rectCollider.X = (int)playerOne.position.X;
            playerOne.rectCollider.Y = (int)playerOne.position.Y;
            playerTwo.rectCollider.X = (int)playerTwo.position.X;
            playerTwo.rectCollider.Y = (int)playerTwo.position.Y;
            playerThree.rectCollider.X = (int)playerThree.position.X;
            playerThree.rectCollider.Y = (int)playerThree.position.Y;
            playerFour.rectCollider.X = (int)playerFour.position.X;
            playerFour.rectCollider.Y = (int)playerFour.position.Y;

            // ****** points *******
            InGoalChecker(playerOne, ref firstInGoal, ref positionInGoal);
            InGoalChecker(playerTwo, ref firstInGoal, ref positionInGoal);
            InGoalChecker(playerThree, ref firstInGoal, ref positionInGoal);
            InGoalChecker(playerFour, ref firstInGoal, ref positionInGoal);
            
            // ****** player to player collider ************
            if (!playerOne.isDead)
            {
                if (!playerTwo.isDead)
                {
                    PushPlayer(playerOne, playerTwo); // 1-2
                }
                if (!playerThree.isDead)
                {
                    PushPlayer(playerOne, playerThree); // 1-3
                }
                if (!playerFour.isDead)
                {
                    PushPlayer(playerOne, playerFour); // 1-4
                }
            }

            if (!playerTwo.isDead)
            {
                if (!playerOne.isDead)
                {
                    PushPlayer(playerTwo, playerOne); // 2-1
                }
                if (!playerThree.isDead)
                {
                    PushPlayer(playerTwo, playerThree); // 2-3
                }
                if (!playerFour.isDead)
                {
                    PushPlayer(playerTwo, playerFour); // 2-4
                }
            }

            if (!playerThree.isDead)
            {
                if (!playerOne.isDead)
                {
                    PushPlayer(playerThree, playerOne); // 3-1
                }
                if (!playerTwo.isDead)
                {
                    PushPlayer(playerThree, playerTwo); // 3-2
                }
                if (!playerFour.isDead)
                {
                    PushPlayer(playerThree, playerFour); // 3-4
                }
            }

            if (!playerFour.isDead)
            {
                if (!playerOne.isDead)
                {
                    PushPlayer(playerFour, playerOne); // 4-1
                }
                if (!playerTwo.isDead)
                {
                    PushPlayer(playerFour, playerTwo); // 4-2
                }
                if (!playerThree.isDead)
                {
                    PushPlayer(playerFour, playerThree); // 4-3
                }
            }

            void PushPlayer(Player p1, Player p2)
            {
                int tileSize = 32;
                const int collideDistance = 15; // prevent players from glitching through other players
                if (p1.rectCollider.Intersects(p2.rectCollider))
                {
                    if (p1.position.Y < p2.position.Y + tileSize
                        && p1.position.Y > p2.position.Y - tileSize
                        && p2.position.X - p1.position.X > collideDistance)
                    {
                        p2.position.X += p1.playerSpeed * deltaTime;
                    }
                    if (p1.position.Y < p2.position.Y + tileSize
                        && p1.position.Y > p2.position.Y - tileSize
                        && p1.position.X - p2.position.X > collideDistance)
                    {
                        p2.position.X -= p1.playerSpeed * deltaTime;
                    }
                    if (p1.position.X < p2.position.X + tileSize
                        && p1.position.X > p2.position.X - tileSize
                        && p2.position.Y - p1.position.Y > collideDistance)
                    {
                        p2.position.Y += p1.playerSpeed * deltaTime;
                    }
                    if (p1.position.X < p2.position.X + tileSize
                        && p1.position.X > p2.position.X - tileSize
                        && p1.position.Y - p2.position.Y > collideDistance)
                    {
                        p2.position.Y -= p1.playerSpeed * deltaTime;
                    }
                }
            }

            // ****** spielbereich collider *************
            CollisionFieldBorder(playerOne);
            CollisionFieldBorder(playerTwo);
            CollisionFieldBorder(playerThree);
            CollisionFieldBorder(playerFour);

            // ********* Player Collision with Cactus && slow down with quicksand ********
            int posX = 16;
            int posY = 108;

            for (int i = 0; i < levelOneArray.GetLength(0); i++, posY += tileSize)
            {
                for (int j = 0; j < levelOneArray.GetLength(1); j++, posX += tileSize)
                {
                    if (levelOneArray[i, j] == 3)
                    {
                        if (playerOne.position.X > posX - collideSize 
                            && playerOne.position.X < posX + collideSize
                            && playerOne.position.Y > posY - collideSize 
                            && playerOne.position.Y < posY + collideSize)
                        {
                            playerOne.isDead = true;
                        }
                        if (playerTwo.position.X > posX - collideSize 
                            && playerTwo.position.X < posX + collideSize
                            && playerTwo.position.Y > posY - collideSize 
                            && playerTwo.position.Y < posY + collideSize)
                        {
                            playerTwo.isDead = true;
                        }
                        if (playerThree.position.X > posX - collideSize 
                            && playerThree.position.X < posX + collideSize
                            && playerThree.position.Y > posY - collideSize 
                            && playerThree.position.Y < posY + collideSize)
                        {
                            playerThree.isDead = true;
                        }
                        if (playerFour.position.X > posX - collideSize 
                            && playerFour.position.X < posX + collideSize
                            && playerFour.position.Y > posY - collideSize 
                            && playerFour.position.Y < posY + collideSize)
                        {
                            playerFour.isDead = true;
                        }
                    }

                    if (levelOneArray[i, j] == 4)
                    {
                        if (playerOne.position.X > posX - collideSize 
                            && playerOne.position.X < posX + collideSize
                            && playerOne.position.Y > posY - collideSize 
                            && playerOne.position.Y < posY + collideSize)
                        {
                            playerOne.playerSpeed = slowSpeed;
                            playerOne.slowerX = posX;
                            playerOne.slowerY = posY;
                        }
                        if (playerOne.position.X < playerOne.slowerX - collideSize 
                            || playerOne.position.X > playerOne.slowerX + collideSize
                            || playerOne.position.Y < playerOne.slowerY - collideSize 
                            || playerOne.position.Y > playerOne.slowerY + collideSize)
                        {
                            playerOne.playerSpeed = fastSpeed;
                        }

                        if (playerTwo.position.X > posX - collideSize 
                            && playerTwo.position.X < posX + collideSize
                            && playerTwo.position.Y > posY - collideSize 
                            && playerTwo.position.Y < posY + collideSize)
                        {
                            playerTwo.playerSpeed = slowSpeed;
                            playerTwo.slowerX = posX;
                            playerTwo.slowerY = posY;
                        }
                        if (playerTwo.position.X < playerTwo.slowerX - collideSize 
                            || playerTwo.position.X > playerTwo.slowerX + collideSize
                            || playerTwo.position.Y < playerTwo.slowerY - collideSize 
                            || playerTwo.position.Y > playerTwo.slowerY + collideSize)
                        {
                            playerTwo.playerSpeed = fastSpeed;
                        }

                        if (playerThree.position.X > posX - collideSize 
                            && playerThree.position.X < posX + collideSize
                            && playerThree.position.Y > posY - collideSize 
                            && playerThree.position.Y < posY + collideSize)
                        {
                            playerThree.playerSpeed = slowSpeed;
                            playerThree.slowerX = posX;
                            playerThree.slowerY = posY;
                        }
                        if (playerThree.position.X < playerThree.slowerX - collideSize 
                            || playerThree.position.X > playerThree.slowerX + collideSize
                            || playerThree.position.Y < playerThree.slowerY - collideSize 
                            || playerThree.position.Y > playerThree.slowerY + collideSize)
                        {
                            playerThree.playerSpeed = fastSpeed;
                        }

                        if (playerFour.position.X > posX - collideSize 
                            && playerFour.position.X < posX + collideSize
                            && playerFour.position.Y > posY - collideSize 
                            && playerFour.position.Y < posY + collideSize)
                        {
                            playerFour.playerSpeed = slowSpeed;
                            playerFour.slowerX = posX;
                            playerFour.slowerY = posY;
                        }
                        if (playerFour.position.X < playerFour.slowerX - collideSize 
                            || playerFour.position.X > playerFour.slowerX + collideSize
                            || playerFour.position.Y < playerFour.slowerY - collideSize 
                            || playerFour.position.Y > playerFour.slowerY + collideSize)
                        {
                            playerFour.playerSpeed = fastSpeed;
                        }
                    }
                }
                posX = 16;
            }
            
            // ********* updating cactusghosts & collision & respawn *********
            if (currentState == GameState.gameScreen)
            {
                int projectileArrayCounter = 0;

                for (int i = 0; i < cactusProjectileArray.GetLength(0); i++)
                {
                    if (projectileArrayCounter == 0) // left movement
                    {
                        cactusProjectileArray[i, 0] -= fastSpeed * deltaTime;
                        if (cactusProjectileArray[i,0] < 176)
                        {
                            cactusProjectileArray[i, 0] = cactusProjectileStartArray[i, 0];
                        }
                    }
                    if (projectileArrayCounter == 1) // up movement
                    {
                        cactusProjectileArray[i, 1] -= fastSpeed * deltaTime;
                        if (cactusProjectileArray[i, 1] < 108)
                        {
                            cactusProjectileArray[i, 1] = cactusProjectileStartArray[i, 1];
                        }
                    }
                    if (projectileArrayCounter == 2) // right movement
                    {
                        cactusProjectileArray[i, 0] += fastSpeed * deltaTime;
                        if (cactusProjectileArray[i, 0] > 1712)
                        {
                            cactusProjectileArray[i, 0] = cactusProjectileStartArray[i, 0];
                        }
                    }
                    if (projectileArrayCounter == 3) // down movement
                    {
                        cactusProjectileArray[i, 1] += fastSpeed * deltaTime;
                        if (cactusProjectileArray[i, 1] > 940)
                        {
                            cactusProjectileArray[i, 1] = cactusProjectileStartArray[i, 1];
                        }
                    }

                    // checks collision with player and kills them if true
                    if (PlayerGhostCollider(playerOne, cactusProjectileArray, i))
                    {
                        playerOne.isDead = true;
                    }
                    if (PlayerGhostCollider(playerTwo, cactusProjectileArray, i))
                    {
                        playerTwo.isDead = true;
                    }
                    if (PlayerGhostCollider(playerThree, cactusProjectileArray, i))
                    {
                        playerThree.isDead = true;
                    }
                    if (PlayerGhostCollider(playerFour, cactusProjectileArray, i))
                    {
                        playerFour.isDead = true;
                    }

                    projectileArrayCounter++;
                    if (projectileArrayCounter == 4)
                    {
                        projectileArrayCounter = 0;
                    }
                }
            }

            // ***** vultures update & player collider *****
            if (currentState == GameState.gameScreen)
            {
                // vulture movement
                for (int i = 0; i < 4; i++)
                {
                    if (i == 0)
                    {
                        VultureBorderCollider(ref vultureOneSpeedX, ref vultureOneSpeedY, deltaTime, i);
                    }
                    if (i == 1)
                    {
                        VultureBorderCollider(ref vultureTwoSpeedX, ref vultureTwoSpeedY, deltaTime, i);
                    }
                    if (i == 2)
                    {
                        VultureBorderCollider(ref vultureThreeSpeedX, ref vultureThreeSpeedY, deltaTime, i);
                    }
                    if (i == 3)
                    {
                        VultureBorderCollider(ref vultureFourSpeedX, ref vultureFourSpeedY, deltaTime, i);
                    }

                    // checks collision with player and kills them if true
                    if (PlayerVultureCollider(playerOne,vultureArray,i))
                    {
                        playerOne.isDead = true;
                    }
                    if (PlayerVultureCollider(playerTwo, vultureArray, i))
                    {
                        playerTwo.isDead = true;
                    }
                    if (PlayerVultureCollider(playerThree, vultureArray, i))
                    {
                        playerThree.isDead = true;
                    }
                    if (PlayerVultureCollider(playerFour, vultureArray, i))
                    {
                        playerFour.isDead = true;
                    }
                }
            }

            // ***** coin ******
            if (currentState == GameState.gameScreen)
            {
                // set new coin position
                if (setCoin == false)
                {
                    int coinFieldSize = 720;
                    int coinChance = 1;
                    int coinposX = 16;
                    int coinposY = 108;
                    for (int i = 0; i < levelOneArray.GetLength(0); i++, coinposY += tileSize)
                    {
                        for (int j = 0; j < levelOneArray.GetLength(1); j++, coinposX += tileSize)
                        {
                            // reduce fieldsize for coin spawning
                            if (levelOneArray[i,j] == 0 && i > 2 && i < 24 && j > 10 && j < 48 && setCoin == false)
                            {
                                if (Leveleditor.GetChance(coinFieldSize, coinChance))
                                {
                                    setCoin = true;
                                    coinPosition.X = coinposX;
                                    coinPosition.Y = coinposY;
                                }
                            }
                        }
                        coinposX = 16;
                    }
                }
                // collision with players
                if (gotCoin == false)
                {
                    if (playerOne.position.X > coinPosition.X - collideSize 
                        && playerOne.position.X < coinPosition.X + collideSize
                        && playerOne.position.Y > coinPosition.Y - collideSize 
                        && playerOne.position.Y < coinPosition.Y + collideSize)
                    {
                        playerOne.points++;
                        gotCoin = true;
                    }
                    if (playerTwo.position.X > coinPosition.X - collideSize 
                        && playerTwo.position.X < coinPosition.X + collideSize
                        && playerTwo.position.Y > coinPosition.Y - collideSize 
                        && playerTwo.position.Y < coinPosition.Y + collideSize)
                    {
                        playerTwo.points++;
                        gotCoin = true;
                    }
                    if (playerThree.position.X > coinPosition.X - collideSize 
                        && playerThree.position.X < coinPosition.X + collideSize
                        && playerThree.position.Y > coinPosition.Y - collideSize 
                        && playerThree.position.Y < coinPosition.Y + collideSize)
                    {
                        playerThree.points++;
                        gotCoin = true;
                    }
                    if (playerFour.position.X > coinPosition.X - collideSize 
                        && playerFour.position.X < coinPosition.X + collideSize
                        && playerFour.position.Y > coinPosition.Y - collideSize 
                        && playerFour.position.Y < coinPosition.Y + collideSize)
                    {
                        playerFour.points++;
                        gotCoin = true;
                    }
                }
            }

            // ******** spielablauf && Counter *********
            showStartCountdown = (int)startCountdown;
            showPlayCountdown = (int)playCountdown;

            if (currentState == GameState.gameScreen)
            {
                if (gameHasStarted == false)
                {
                    startCountdown -= deltaTime;
                    if (startCountdown < 0)
                    {
                        gameHasStarted = true;
                    }
                }
                if (gameHasStarted == true)
                {
                    playCountdown -= deltaTime;
                }
                if (playCountdown < 0)
                {
                    endTimer -= deltaTime;

                    if (playerOne.inGoal == false)
                    {
                        playerOne.isDead = true;
                    }
                    if (playerTwo.inGoal == false)
                    {
                        playerTwo.isDead = true;
                    }
                    if (playerThree.inGoal == false)
                    {
                        playerThree.isDead = true;
                    }
                    if (playerFour.inGoal == false)
                    {
                        playerFour.isDead = true;
                    }
                }

                if ((playerOne.isDead == true || playerOne.inGoal == true)
                    && (playerTwo.isDead == true || playerTwo.inGoal == true)
                    && (playerThree.isDead == true || playerThree.inGoal == true)
                    && (playerFour.isDead == true || playerFour.inGoal == true)
                    && playCountdown > 2
                    && hasEndedEarly == false)
                {
                    playCountdown = 1;
                    hasEndedEarly = true;
                }
            }

            // ********* menü ************
            int maxRounds = 10;
            if (roundsWannaPlay <= 1)
            {
                roundsWannaPlay = 1;
            }
            if (roundsWannaPlay > maxRounds+1)
            {
                roundsWannaPlay = maxRounds;
            }
            roundsToPlay = (int)roundsWannaPlay;

            if (menuNav >= 4)
            {
                menuNav = 0;
            }
            if (menuNav < 0)
            {
                menuNav = 3.9f;
            }
            menuState = (int)menuNav;

            if (currentState == GameState.menuScreen)
            {
                MenuPlayer(deltaTime, p1C, gamePadOneState);
            }

            // ****** initialize round for game *******
            if (currentState == GameState.gameScreen)
            {
                if (setRoundCounter == false)
                {
                    setRoundCounter = true;
                    roundsCounter = roundsToPlay;
                }
            }

            // ******** NEW ROUND **********
            if (currentState == GameState.gameScreen
                && (playerOne.isDead == true || playerOne.inGoal == true)
                && (playerTwo.isDead == true || playerTwo.inGoal == true)
                && (playerThree.isDead == true || playerThree.inGoal == true)
                && (playerFour.isDead == true || playerFour.inGoal == true)
                && endTimer <= 0
                && roundsCounter > 0)
            {
                newRound();
                roundsCounter--;
                currentRound++;
            }

            void newRound()
            {
                // returns players to start position
                startingPositions = new int[4];
                HelperMethods.FillArrayRandomSingleOccurrence(startingPositions);
                HelperMethods.StartPositionY(startingPositions);
                playerOne.position = new Vector2(startPlayerXCord, startingPositions[0]);
                playerTwo.position = new Vector2(startPlayerXCord, startingPositions[1]);
                playerThree.position = new Vector2(startPlayerXCord, startingPositions[2]);
                playerFour.position = new Vector2(startPlayerXCord, startingPositions[3]);
                // gets players back to life
                playerOne.isDead = false;
                playerTwo.isDead = false;
                playerThree.isDead = false;
                playerFour.isDead = false;
                playerOne.inGoal = false;
                playerTwo.inGoal = false;
                playerThree.inGoal = false;
                playerFour.inGoal = false;
                hasEndedEarly = false;
                // new level
                levelOneArray = new int[27, 59];
                Leveleditor.NewLevelOneArray(levelOneArray);
                // new projectiles
                cactusProjectileArray = CactusProjectiles.NewProjectileArray(levelOneArray);
                cactusProjectileStartArray = CactusProjectiles.NewProjectileArray(levelOneArray);
                // reset times and states
                gameHasStarted = false;
                firstInGoal = false;
                startCountdown = 6.0f;
                playCountdown = 20.0f;
                endTimer = 3.0f;
                // resets death animations
                playerOne.animationIndexDeath = 0;
                playerTwo.animationIndexDeath = 0;
                playerThree.animationIndexDeath = 0;
                playerFour.animationIndexDeath = 0;
                // resets player animations
                playerOne.animationType = 0;
                playerTwo.animationType = 0;
                playerThree.animationType = 0;
                playerFour.animationType = 0;
                // resets in goal positions
                positionInGoal = 0;
                // resets coin
                gotCoin = false;
                setCoin = false;
                // resets vultures
                vultureArray = Vultures.NewVultureArray();
                // resets soundeffects
                playerOne.playDeathSound = false;
                playerTwo.playDeathSound = false;
                playerThree.playDeathSound = false;
                playerFour.playDeathSound = false;
            }

            // ****** End Screen *******
            if (currentState == GameState.endScreen)
            {
                if (doStandings == false)
                {
                    int[,] standings = {
                        {1,playerOne.points,0},
                        {2,playerTwo.points,0},
                        {3,playerThree.points,0},
                        {4,playerFour.points,0}
                    };

                    for (int i = 0; i < 4; i++)
                    {
                        int currentMaximum = 0;
                        int player = 0;
                        for (int j = 0; j < 4; j++)
                        {
                            if (standings[j,1] >= currentMaximum)
                            {
                                currentMaximum = standings[j, 1];
                                player = standings[j, 0];
                            }
                        }
                        if (highestPointsSet == false)
                        {
                            highestPoints = currentMaximum;
                            highestPointsSet = true;
                        }
                        sortedStandings[i, 0] = standings[player - 1, 0];
                        sortedStandings[i, 1] = standings[player - 1, 1];
                        standings[player - 1, 1] = -1;
                    }
                    doStandings = true;
                }
            }
            

            base.Update(gameTime);
        }

        // ********* player - Goal & points Checker ***************
        private void InGoalChecker(Player P, ref bool first, ref int goalPosition)
        {
            int goalXPos = 1808;
            int goalYPos = 380+94*goalPosition;
            int finishLine = 1712;

            if (!P.inGoal)
            {
                if (P.position.X > finishLine)
                {
                    P.inGoal = true;
                    if (first == false)
                    {
                        first = true;
                        P.points += 2;
                    }
                    else
                    {
                        P.points++;
                    }
                    P.position.X = goalXPos;
                    P.position.Y = goalYPos;
                    P.animationType = 0;
                    goalPosition++;
                }
            }
        }

        // ********* player cactusghosts collision ********
        private bool PlayerGhostCollider(Player p,float[,] cactus, int i)
        {
            if (p.position.X > cactus[i, 0] - collideSize 
                && p.position.X < cactus[i, 0] + collideSize
                && p.position.Y > cactus[i, 1] - collideSize 
                && p.position.Y < cactus[i, 1] + collideSize)
            {
                return true;
            }
            return false;
        }

        // ******** player vultures collision **********
        private bool PlayerVultureCollider(Player p, float[,] vulture, int i)
        {
            if (p.position.X > vulture[i, 0] - collideSize 
                && p.position.X < vulture[i, 0] + collideSize
                && p.position.Y > vulture[i, 1] - collideSize 
                && p.position.Y < vulture[i, 1] + collideSize)
            {
                return true;
            }
            return false;
        }

        // *******  vulture border change direction ******
        private void VultureBorderCollider(ref float xSpeed,ref float ySpeed, float deltaTime, int i)
        {
            vultureArray[i, 0] += xSpeed * deltaTime;
            vultureArray[i, 1] += ySpeed * deltaTime;
            if (vultureArray[i, 0] < 177 || vultureArray[i, 0] > 1711) // border left, right
            {
                xSpeed *= -1;
            }
            if (vultureArray[i, 1] < 109 || vultureArray[i, 1] > 939) // border up, down
            {
                ySpeed *= -1;
            }
        }

        // ********* collision with border game *************
        private void CollisionFieldBorder(Player P)
        {
            int leftBorder = 16;
            int rightBorder = 1872;
            int upperBorder = 108;
            int lowerBorder = 940;
            int startBorder = 128;
            if (P.position.X < leftBorder)
            {
                P.position.X = leftBorder;
            }
            if (P.position.X > rightBorder)
            {
                P.position.X = rightBorder;
            }
            if (P.position.Y < upperBorder)
            {
                P.position.Y = upperBorder;
            }
            if (P.position.Y > lowerBorder)
            {
                P.position.Y = lowerBorder;
            }
            if (gameHasStarted == false)
            {
                if (P.position.X > startBorder)
                {
                    P.position.X = startBorder;
                }
            }
        }

        // ******* Move ***********
        private void MovePlayer(float deltaTime,GamePadCapabilities gPC, GamePadState gPS, Player p)
        {
            float tempX;
            float tempY;
            if (gPC.HasLeftXThumbStick)
            {
                tempX = gPS.ThumbSticks.Left.X * p.playerSpeed * deltaTime;
                tempY = gPS.ThumbSticks.Left.Y * p.playerSpeed * deltaTime;
                if (tempX > 0)
                {
                    p.position.X += tempX;
                    p.animationType = 3;
                }
                if (tempX < 0)
                {
                    p.position.X += tempX;
                    p.animationType = 2;
                }
                if (tempY > 0)
                {
                    p.position.Y -= tempY;
                    p.animationType = 1;
                }
                if (tempY < 0)
                {
                    p.position.Y -= tempY;
                    p.animationType = 0;
                }
            }
        }

        private void MenuPlayer(float deltaTime, GamePadCapabilities gPC, GamePadState gPS)
        {
            if (gPC.HasLeftXThumbStick)
            {
                menuNav -= gPS.ThumbSticks.Left.Y * 0.1f;
                if (menuState == 1)
                { 
                    roundsWannaPlay += gPS.ThumbSticks.Left.X * 0.1f;
                }
                
            }
        }

        // ******************************************************************************************************************************************
        // ****************************************************            DRAW            **********************************************************
        // ******************************************************************************************************************************************
        /// <param name="gameTime">Provides a snapshot of timing values.</param>

        protected override void Draw(GameTime gameTime)
        {

            // **************************************************** DRAW INTRO *******************************************************************
            if (currentState == GameState.introScreen)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(backgroundIntro, backgroundPosition, Color.White);
                spriteBatch.DrawString(bodytext, "MultiMediaTechnology / FHS", new Vector2(16, 880), GameColor.specialWhite);
                spriteBatch.DrawString(bodytext, "MultiMediaProjekt 1", new Vector2(16, 930), GameColor.specialWhite);
                spriteBatch.DrawString(bodytext, "Programming, Art & Music - Hofer Thomas", new Vector2(16, 980), GameColor.specialWhite);
                spriteBatch.DrawString(bodytext, "Co - Music Producer - Veltman Bob", new Vector2(16, 1030), GameColor.specialWhite);
                spriteBatch.End();
            }

            // **************************************************** DRAW MENU *******************************************************************
            if (currentState == GameState.menuScreen)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(backgroundMenu, backgroundPosition, Color.White);

                if (menuState == 0)
                {
                    spriteBatch.DrawString(menutext, "Play", new Vector2(880, 200), GameColor.specialPink);
                    spriteBatch.DrawString(menutext, "Rounds " + roundsToPlay, new Vector2(800, 350), GameColor.specialWhite);
                    spriteBatch.DrawString(menutext, "Options", new Vector2(825, 500), GameColor.specialWhite);
                    spriteBatch.DrawString(menutext, "Quit", new Vector2(880, 650), GameColor.specialWhite);
                }
                else if (menuState == 1)
                {
                    spriteBatch.DrawString(menutext, "Play", new Vector2(880, 200), GameColor.specialWhite);
                    spriteBatch.DrawString(menutext, "Rounds " + roundsToPlay, new Vector2(800, 350), GameColor.specialPink);
                    spriteBatch.DrawString(menutext, "Options", new Vector2(825, 500), GameColor.specialWhite);
                    spriteBatch.DrawString(menutext, "Quit", new Vector2(880, 650), GameColor.specialWhite);
                }
                else if (menuState == 2)
                {
                    spriteBatch.DrawString(menutext, "Play", new Vector2(880, 200), GameColor.specialWhite);
                    spriteBatch.DrawString(menutext, "Rounds " + roundsToPlay, new Vector2(800, 350), GameColor.specialWhite);
                    spriteBatch.DrawString(menutext, "Options", new Vector2(825, 500), GameColor.specialPink);
                    spriteBatch.DrawString(menutext, "Quit", new Vector2(880, 650), GameColor.specialWhite);
                }
                else
                {
                    spriteBatch.DrawString(menutext, "Play", new Vector2(880, 200), GameColor.specialWhite);
                    spriteBatch.DrawString(menutext, "Rounds " + roundsToPlay, new Vector2(800, 350), GameColor.specialWhite);
                    spriteBatch.DrawString(menutext, "Options", new Vector2(825, 500), GameColor.specialWhite);
                    spriteBatch.DrawString(menutext, "Quit", new Vector2(880, 650), GameColor.specialPink);
                }
                spriteBatch.DrawString(bodytext, "(sry no Options atm)", new Vector2(790, 780), GameColor.specialWhite);

                spriteBatch.End();
            }

            // *************************************************** DRAW LEARN *************************************************************
            if (currentState == GameState.learnScreen)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(backgroundLearn, backgroundPosition, Color.White);
                spriteBatch.DrawString(menutext, "= dead", new Vector2(1050, 280), GameColor.specialPink);
                spriteBatch.DrawString(menutext, "= slow", new Vector2(1050, 490), GameColor.specialPink);
                spriteBatch.DrawString(menutext, "= points", new Vector2(1050, 700), GameColor.specialPink);
                spriteBatch.End();
            }

            // **************************************************** DRAW GAME *******************************************************************
            if (currentState == GameState.gameScreen)
            {

                // ********** Player Animations Alive ************
                DoAnimationPlayer(playerOne);
                DoAnimationPlayer(playerTwo);
                DoAnimationPlayer(playerThree);
                DoAnimationPlayer(playerFour);

                // ********** Player Animations Dead *************
                if (playerOne.isDead)
                {
                    DoAnimationDeath(playerOne);
                }
                if (playerTwo.isDead)
                {
                    DoAnimationDeath(playerTwo);
                }
                if (playerThree.isDead)
                {
                    DoAnimationDeath(playerThree);
                }
                if (playerFour.isDead)
                {
                    DoAnimationDeath(playerFour);
                }

                // *********** Quicksand Animation *************
                DoAnimationQuicksand(ref animationIndexQuicksand, ref quicksandAnimationRect);

                // *********** Cactus Animation **************
                DoAnimationCactus(ref animationIndexCactus, ref cactusAnimationRect);

                // *********** Cactusghost Animation *********
                DoAnimationCactusghost(ref animationIndexCactusghost, ref cactusghostAnimationRect);

                // *********** Coin Animation ***********
                DoAnimationCoin(ref animationIndexCoin, ref coinAnimationRect);

                // *********** Vulture Animation **********
                DoAnimationVulture(ref animationIndexVulture, ref vultureAnimationRect);


                // ************** DRAW ******************
                spriteBatch.Begin();
                if (gameHasStarted == false)
                {
                    spriteBatch.Draw(backgroundTemplateGameRoundStart, backgroundPosition, Color.White);
                }
                else
                {
                    spriteBatch.Draw(backgroundTemplateGameRound, backgroundPosition, Color.White);
                }

                // ************* Standing *****************
                spriteBatch.DrawString(menutext, "" + playerOne.points, new Vector2(692, 10), GameColor.specialWhite);
                spriteBatch.DrawString(menutext, "" + playerTwo.points, new Vector2(902, 10), GameColor.specialWhite);
                spriteBatch.DrawString(menutext, "" + playerThree.points, new Vector2(1112, 10), GameColor.specialWhite);
                spriteBatch.DrawString(menutext, "" + playerFour.points, new Vector2(1322, 10), GameColor.specialWhite);

                if (showStartCountdown > 0)
                {
                    spriteBatch.DrawString(menutext, "... " + showStartCountdown, new Vector2(1730, 14), GameColor.specialPink);
                }
                if (gameHasStarted == true && playCountdown > 0)
                {
                    spriteBatch.DrawString(menutext, "... " + showPlayCountdown, new Vector2(1730, 14), GameColor.specialPink);
                }

                spriteBatch.DrawString(menutext, "Points", new Vector2(16, 10), GameColor.specialBlue);
                spriteBatch.DrawString(menutext, "Round:  " + currentRound, new Vector2(16, 990), GameColor.specialBlue);

                // ** Draw Level **
                int posX = 16;
                int posY = 108;
                for (int i = 0; i < levelOneArray.GetLength(0); i++, posY += tileSize)
                {
                    for (int j = 0; j < levelOneArray.GetLength(1); j++, posX += tileSize)
                    {
                        Vector2 currentPosition = new Vector2(posX, posY);
                        if (levelOneArray[i,j] == 0)
                        {
                            // sand
                            spriteBatch.Draw(sandTexture, currentPosition, Color.White);
                        }
                        else if (levelOneArray[i,j] == 4)
                        {
                            // quicksand
                            spriteBatch.Draw(quicksandTexture, currentPosition, quicksandAnimationRect, Color.White);
                        }
                        else if (levelOneArray[i,j] == 3)
                        {
                            // cactus
                            spriteBatch.Draw(cactusTexture, currentPosition, cactusAnimationRect, Color.White);
                        }
                    }
                    posX = 16;
                }

                // *********** cactusghosts/"projectiles" *****************
                for (int i = 0; i < cactusProjectileArray.GetLength(0); i++)
                {
                    Vector2 projectilePosition = new Vector2(cactusProjectileArray[i,0], cactusProjectileArray[i,1]);
                    spriteBatch.Draw(cactusghostTexture, projectilePosition, cactusghostAnimationRect, Color.White);    
                }

                // ******* coin *********
                if (gotCoin == false)
                {
                    spriteBatch.Draw(coinTexture, coinPosition, coinAnimationRect, Color.White);
                }

                // ** Dead Players **
                if (playerOne.isDead)
                {
                    spriteBatch.Draw(deathTexture, playerOne.position, playerOne.deathAnimationRect, Color.White);
                }
                if (playerTwo.isDead)
                {
                    spriteBatch.Draw(deathTexture, playerTwo.position, playerTwo.deathAnimationRect, Color.White);
                }
                if (playerThree.isDead)
                {
                    spriteBatch.Draw(deathTexture, playerThree.position, playerThree.deathAnimationRect, Color.White);
                }
                if (playerFour.isDead)
                {
                    spriteBatch.Draw(deathTexture, playerFour.position, playerFour.deathAnimationRect, Color.White);
                }

                // ** Alive Players **
                if (!playerOne.isDead)
                {
                    spriteBatch.Draw(playerOne.texture, playerOne.position, playerOne.animationRect, Color.White);
                }
                if (!playerTwo.isDead)
                {
                    spriteBatch.Draw(playerTwo.texture, playerTwo.position, playerTwo.animationRect, Color.White);
                }
                if (!playerThree.isDead)
                {
                    spriteBatch.Draw(playerThree.texture, playerThree.position, playerThree.animationRect, Color.White);
                }
                if (!playerFour.isDead)
                {
                    spriteBatch.Draw(playerFour.texture, playerFour.position, playerFour.animationRect, Color.White);
                }

                // ******* vultures *******
                for (int i = 0; i < 4; i++)
                {
                    Vector2 currentVulturePosition = new Vector2(vultureArray[i, 0], vultureArray[i, 1]);
                    spriteBatch.Draw(vultureTexture, currentVulturePosition, vultureAnimationRect, Color.White);
                }

                // ******* countdown closeup *******
                if (startCountdown < 4 && startCountdown > 0)
                {
                    if (startCountdown < 1)
                    {
                        spriteBatch.DrawString(menutext, "GO!!!", new Vector2(870, 480), GameColor.specialPink);
                    }
                    else
                    {
                        spriteBatch.DrawString(menutext, "" + showStartCountdown, new Vector2(920, 480), GameColor.specialPink);
                    }
                }

                spriteBatch.End();
            }

            // **************************************************** DRAW END *******************************************************************
            if (currentState == GameState.endScreen)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(backgroundEnd, backgroundPosition, Color.White);
                spriteBatch.DrawString(menutext, "STANDINGS", new Vector2(755, 120), GameColor.specialWhite);

                int yStandingStartCord = 220;
                int yStandingOffset = 140;
                int xStandingPictures = 840;
                int xStandingPoints = xStandingPictures + 160;
                int yStandingPoints = 235;

                for (int i = 0; i < sortedStandings.GetLength(0); i++)
                {
                    if (sortedStandings[i,0] == 1)
                    {
                        spriteBatch.Draw(beani, new Vector2(xStandingPictures, yStandingStartCord), Color.White);
                        yStandingStartCord += yStandingOffset;
                    }
                    else if (sortedStandings[i, 0] == 2)
                    {
                        spriteBatch.Draw(kelpi, new Vector2(xStandingPictures, yStandingStartCord), Color.White);
                        yStandingStartCord += yStandingOffset;
                    }
                    else if (sortedStandings[i, 0] == 3)
                    {
                        spriteBatch.Draw(natscho, new Vector2(xStandingPictures, yStandingStartCord), Color.White);
                        yStandingStartCord += yStandingOffset;
                    }
                    else if (sortedStandings[i, 0] == 4)
                    {
                        spriteBatch.Draw(salsa, new Vector2(xStandingPictures, yStandingStartCord), Color.White);
                        yStandingStartCord += yStandingOffset;
                    }
                }

                spriteBatch.DrawString(menutext, "" + sortedStandings[0, 1], new Vector2(xStandingPoints, yStandingPoints),
                    (sortedStandings[0, 1] == highestPoints ? GameColor.specialPink : GameColor.specialBlue));
                spriteBatch.DrawString(menutext, "" + sortedStandings[1, 1], new Vector2(xStandingPoints, yStandingPoints + yStandingOffset * 1), 
                    (sortedStandings[1, 1] == highestPoints ? GameColor.specialPink : GameColor.specialBlue) );
                spriteBatch.DrawString(menutext, "" + sortedStandings[2, 1], new Vector2(xStandingPoints, yStandingPoints + yStandingOffset * 2), 
                    (sortedStandings[2, 1] == highestPoints ? GameColor.specialPink : GameColor.specialBlue) );
                spriteBatch.DrawString(menutext, "" + sortedStandings[3, 1], new Vector2(xStandingPoints, yStandingPoints + yStandingOffset * 3), 
                    (sortedStandings[3, 1] == highestPoints ? GameColor.specialPink : GameColor.specialBlue) );
                
                spriteBatch.DrawString(bodytext, "Press A to Reset", new Vector2(805, 780), GameColor.specialWhite);
                spriteBatch.End();
            }

            base.Draw(gameTime);
        }

        // ******** Animations *********
        private void DoAnimationPlayer(Player p)
        {
            p.animationIndex += animationSpeed;

            if (p.animationIndex >= 2)
            {
                p.animationIndex = 0;
            }

            p.animationRect = new Rectangle((int)p.animationIndex * p.animationRect.Width, p.animationType * p.animationRect.Height,
                p.animationRect.Width, p.animationRect.Height);
        }

        private void DoAnimationDeath(Player p)
        {
            if (p.animationIndexDeath > 3)
            {
            }
            else
            {
                p.animationIndexDeath += animationSpeed;
                p.deathAnimationRect = new Rectangle((int)p.animationIndexDeath * p.deathAnimationRect.Width, 0,
                    p.deathAnimationRect.Width, p.deathAnimationRect.Height);
            }
        }

        private void DoAnimationQuicksand(ref float index, ref Rectangle rect)
        {
            index += animationSpeed;
            if (index >=4)
            {
                index = 0;
            }
            rect = new Rectangle((int)index * rect.Width, 0, rect.Width, rect.Height);
        }

        private void DoAnimationCactus(ref float index, ref Rectangle rect)
        {
            index += animationSpeed;
            if (index >= 2)
            {
                index = 0;
            }
            rect = new Rectangle((int)index * rect.Width, 0, rect.Width, rect.Height);
        }

        private void DoAnimationCactusghost(ref float index, ref Rectangle rect)
        {
            index += animationSpeed;
            if (index >= 2)
            {
                index = 0;
            }
            rect = new Rectangle((int)index * rect.Width, 0, rect.Width, rect.Height);
        }

        private void DoAnimationCoin(ref float index, ref Rectangle rect)
        {
            index += animationSpeed;
            if (index >= 2)
            {
                index = 0;
            }
            rect = new Rectangle((int)index * rect.Width, 0, rect.Width, rect.Height);
        }

        private void DoAnimationVulture(ref float index, ref Rectangle rect)
        {
            index += animationSpeed;
            if (index >= 2)
            {
                index = 0;
            }
            rect = new Rectangle((int)index * rect.Width, 0, rect.Width, rect.Height);
        }
    }
}
