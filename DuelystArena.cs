// Author: Jason Wong
// File Name: DuelystArena
// Project Name: Street Brawlers
// Creation Date: 5/17/2016
// Modified Date: 6/13/2016
// Description: This program will allow two users to play a 2D fighting game
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Duelyst
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class DuelystArena : Microsoft.Xna.Framework.Game
    {
        Random rng = new Random();

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Collect Keyboard Inputs from the User
        KeyboardState kb;
        KeyboardState kkb;

        //Set Font for Menu Screen
        SpriteFont MenuFont;

        //Audio------------------------------------------------

        //Sound Effects
        SoundEffect BackM;
        SoundEffectInstance BackMInstance;
        SoundEffect BlockM;
        SoundEffectInstance BlockMInstance;
        SoundEffect MageM;
        SoundEffectInstance MageMInstance;
        SoundEffect HeavyM;
        SoundEffectInstance HeavyMInstance;
        SoundEffect SwiftM;
        SoundEffectInstance SwiftMInstance;
        SoundEffect WinM;
        SoundEffectInstance WinMinstance;

        //NonLoop
        bool VictoryLoop;

        //Sprite Sheets-----------------------------------------

        //Sprite Sheet Names
        Texture2D[] PlayerFighter = new Texture2D[2];
        Texture2D HeavyFighter;
        Texture2D SwiftFighter;
        Texture2D MagicFighter;
        Texture2D PlayerArrows;
        Texture2D Background;
        Texture2D Healthbar;
        Texture2D CollisionBox;
        Texture2D Victory;
        Texture2D Platform;
        Texture2D RangeAttack;
        Texture2D ManaBar;
        Texture2D PowerUps;
        Texture2D MenuBar;
        Texture2D FighterStats;
        Texture2D CircleParticle;
        Texture2D SuddenDeath;

        //Sprite Dimensions-------------------------------------

        //Background Sprite
        Rectangle BackgroundRec;
        Rectangle BackgroundSpriteSrc;

        //Particle Sprite
        Rectangle[] RangeParticle = new Rectangle[20];
        Color FillColor;
        Color[] RangeColor = new Color[6];

        //Platform Sprite
        Rectangle PlatformSpriteSrc;
        int PlatformDrawW;
        int PlatformDrawH;

        //Ranged Attack Sprite
        int RangeW;
        int RangeDrawW;
        int RangeDrawH;
        Rectangle[] RangeBound = new Rectangle[2];
        Rectangle[] RangeSpriteSrc = new Rectangle[2];

        //Arrow Sprite
        int ArrowW;
        int ArrowDrawW;
        int ArrowDrawH;
        Rectangle[] ArrowRec = new Rectangle[2];
        Rectangle[] ArrowSpriteSrc = new Rectangle[2];
        int[] PlayerHead = new int[2];

        //Health Bar Sprite
        int HealthW;
        int HealthDrawH;
        Rectangle[] HealthRec= new Rectangle[2];
        Rectangle[] HealthBackground = new Rectangle[2];
        Rectangle[] HealthFont = new Rectangle[2];
        Rectangle[] HealthSpriteSrc = new Rectangle[3];

        //Mana Bar Sprite
        int ManaW;
        int ManaDrawH;
        Rectangle[] ManaRec = new Rectangle[2];
        Rectangle[] ManaBackground = new Rectangle[2];
        Rectangle[] ManaCage = new Rectangle[2];
        Rectangle[] ManaSpriteSrc = new Rectangle[3];

        //Player Sprite
        const int PlayerW = 80;
        const int PlayerH = 80;
        const int PlayerDrawW = 240;
        const int PlayerDrawH = 240;
        Rectangle[] PlayerRec = new Rectangle[2];
        Rectangle[] PlayerSpriteSrc = new Rectangle[2];
        int[] PlayerFrameWide = new int[2];
        int[] PlayerFrameHeight = new int[2];

        //Victory Sprite
        int VictoryDrawW;
        int VictoryDrawH;
        Rectangle VictoryRec;
        Rectangle VictorySpriteSrc;

        //Sudden Death
        int SDDrawW;
        int SDDrawH;
        Rectangle SDRec;

        //Power Up Sprite
        int PowerUpW;
        int PowerUpH;
        int PowerUpDrawW;
        int PowerUpDrawH;
        Rectangle PowerUpRec;
        Rectangle PowerUpSpriteSrc;
        int PowerUpNumber;

        //Fighter Stat Sprite
        int StatW;
        int StatDrawW;
        int StatDrawH;
        Rectangle[] StatRec = new Rectangle[2];
        Rectangle[] StatSpriteSrc = new Rectangle[2];

        //Screen Dimensions
        int ScreenW;
        int ScreenH;

        //Animation---------------------------------------------

        //Player Input
        int[] PlayerCommand = new int[2];

        //Casting Animation
        bool[] CastAnimation = new bool[2];

        //Source X, Y
        int[] NewScrX = new int[2];
        int[] NewScrY = new int[2];
        
        //Idle Animation
        int[] IdleSmoothness = new int[2];
        int[] IdleTicker = new int[2];
        int[] IdleFrameNumber = new int[2];
        int FrameNumber;

        //Block Animation
        bool[] Block = new bool[2];
        int[] BlockSmoothess = new int[2];
        int[] BlockTicker = new int[2];
        const int BlockFrameNumber = 3;
        int[] BlockFrame = new int[2];

        //Fast Attack Animation
        bool[] Weak = new bool[2];
        int[] WeakAttackSmoothness = new int[2];
        int[] WeakTicker = new int[2];
        int[] WeakFrameNumber = new int[2];
        int[] WeakFrame = new int[2];
        int[] WeakCooldown = new int[2];

        //Strong Attack Animation
        bool[] Strong = new bool[2];
        int[] StrongAttackSmoothness = new int[2];
        int[] StrongTicker = new int[2];
        int[] StrongFrameNumber = new int[2];
        int[] StrongFrame = new int[2];
        int[] StrongCooldown = new int[2];

        //Ranged Attack Aniamtion
        bool[] Range = new bool[2];
        int[] RangeAttackSmoothness = new int[2];
        int[] RangeTicker = new int[2];
        int[] RangeFrameNumber = new int[2];
        int[] RangeFrame = new int[2];
        int[] RangeCooldown = new int[2];

        int[] FacingDirection = new int[2];

        //Death Animation
        bool[] Death = new bool[2];
        int[] DeathSmoothness = new int[2];
        int[] DeathTicker = new int[2];
        int[] DeathFrameNumber = new int[2];
        int[] DeathFrame = new int[2];

        //PowerUp Animation
        int PowerUpSmoothness;
        int PowerUpTicker;
        int PowerUpFrameNumber;
        int PowerUpFrame;
        int NewPowerUpX;
        int NewPowerUpY;

        //Collision---------------------------------------------

        //Collision Boxes
        Rectangle[] PlayerBounds = new Rectangle[2];
        Rectangle[] FloorBounds = new Rectangle[2];
        Rectangle[] RangedBound = new Rectangle[2];
        Rectangle[] PlayerBoundSrc = new Rectangle[2];
        Rectangle[] AttackBound = new Rectangle[2];
        Rectangle CircularBounds;

        //Collision Scenarios
        const int NoCollision = 1;
        const int HitCollision = 2;

        //Player States
        bool[] BlockedOne = new bool[2];
        bool[] BlockedTwo = new bool[2];

        //Physics-------------------------------------

        //Jump
        bool[] Jump = new bool[2];
        int[] JumpVelocity = new int[2];
        const int MaxVelocity = 20;
        int[] JumpCooldown = new int[2];
        bool[] WasOnPlatform = new bool[2];

        //Speed Up/Slow Down
        int PDuration;
        int ActiveP;
        int PlayerP;

        //In-Game Counter---------------------------------------

        //Loop Counter
        int i;
        int j;

        bool EndGame;

        bool Graphic;
        bool Sound;

        //Power Up Duration
        int RateCounter;

        //Projectile--------------------------------------------

        //Range Projectile
        int ProjectileSpeed = 16;
        int[] Direction = new int[2];

        //Stats-------------------------------------------------

        //Damage Amount
        int[] WeakDamage = new int[2];
        int[] StrongDamage = new int[2];
        int[] RangeDamage = new int[2];

        //Health Indicator
        int[] HealthDrawW = new int[2];
        //HealthDrawW is used in sprites as well
        int[] HealthCap = new int[2];

        //Mana Indicator
        int[] ManaDrawW = new int[2];
        //ManaDrawW is used in sprites as well
        int[] ManaCap = new int[2];

        //Movement Speed Indicator
        int[] MovementSpeed = new int[2];
        int[] MoveCap = new int[2];

        //Main Menu---------------------------------------------

        bool Menu;

        //User Selection
        Rectangle[] MenuOptions = new Rectangle[3];
        int MenuPage;
        int SelectedOption;

        //Testing-----------------------------------------------

        //Visible 1f, Hidden 0f
        float Testing = 0f;

        public DuelystArena()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //16:9 Ratio @ Home
            this.graphics.PreferredBackBufferWidth = 1280;
            this.graphics.PreferredBackBufferHeight = 720;

            this.graphics.ApplyChanges();

            //Store dimensions as integers
            ScreenH = graphics.GraphicsDevice.Viewport.Height;
            ScreenW = graphics.GraphicsDevice.Viewport.Width;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Load all sprites sheets and fonts --------------------
            MenuFont = Content.Load<SpriteFont>("Fonts/MenuFont");

            SwiftFighter = Content.Load<Texture2D>(@"Sprite\Fighters\SwiftSprite");
            HeavyFighter = Content.Load<Texture2D>(@"Sprite\Fighters\HeavySprite");
            PlayerArrows = Content.Load<Texture2D>(@"Sprite\Visual Aids\PlayerArrow");
            MagicFighter = Content.Load<Texture2D>(@"Sprite\Fighters\MageSprite");
            CollisionBox = Content.Load<Texture2D>(@"Sprite\Hitbox");
            Background = Content.Load<Texture2D>(@"Sprite\Map Design\Wallpaper");
            Healthbar = Content.Load<Texture2D>(@"Sprite\Visual Aids\HealthBar");
            Victory = Content.Load<Texture2D>(@"Sprite\Visual Aids\Victory");
            Platform = Content.Load<Texture2D>(@"Sprite\Map Design\Platform");
            RangeAttack = Content.Load<Texture2D>(@"Sprite\Fighters\RangeAttacks");
            PowerUps = Content.Load<Texture2D>(@"Sprite\Map Design\PowerUps");
            ManaBar = Content.Load<Texture2D>(@"Sprite\Visual Aids\ManaBar");
            MenuBar = Content.Load<Texture2D>(@"Sprite\Visual Aids\MenuBars");
            FighterStats = Content.Load<Texture2D>(@"Sprite\Visual Aids\StatsAll");
            CircleParticle = Content.Load<Texture2D>(@"Sprite\Particle\Cicular");
            SuddenDeath = Content.Load<Texture2D>(@"Sprite\Visual Aids\SuddenDeath");

            //Preset player state and game state--------------------
            for(i = 0; i < 2; i++)            
            {
                CharacterStats(0, i);
             
                CastAnimation[i] = false;

                PlayerHead[i] = 85;

                BlockSmoothess[i] = 8;

                Jump[i] = false;
                JumpVelocity[i] = MaxVelocity;

                PlayerCommand[i] = 10;
            }

            EndGame = false;
            Menu = true;
            MenuPage = 1;
            SelectedOption = 1;

            Graphic = true;
            Sound = true;
            VictoryLoop = true;

            //Create boundaries for the sprite sheets---------------

            PowerUpW = PowerUps.Width / 19;
            PowerUpH = PowerUps.Height / 5;
            PowerUpDrawW = PowerUpW;
            PowerUpDrawH = PowerUpH;
            PowerUpSmoothness = 4;

            RangeW = RangeAttack.Width / 3;
            RangeDrawW = RangeW + 75;
            RangeDrawH = RangeAttack.Height + 75;

            HealthW = Healthbar.Width / 4;
            HealthDrawH = Healthbar.Height * 5;

            ManaW = ManaBar.Width / 3;
            ManaDrawH = ManaBar.Height;

            StatW = FighterStats.Width / 3;
            StatDrawW = StatW;
            StatDrawH = FighterStats.Height;

            ArrowW = PlayerArrows.Width / 2;
            ArrowDrawW = ArrowW + ArrowW/2;
            ArrowDrawH = PlayerArrows.Height + PlayerArrows.Height/2;

            BackgroundRec = new Rectangle(0, 0, ScreenW + 25, ScreenH);
            BackgroundSpriteSrc = new Rectangle(0, 0, Background.Width, Background.Height);

            PlatformDrawW = Platform.Width;
            PlatformDrawH = Platform.Height/2;
            PlatformSpriteSrc = new Rectangle(0, 0, PlatformDrawW, PlatformDrawH);

            VictoryDrawW = Victory.Width/2 + 200;
            VictoryDrawH = Victory.Height/2 + 200;
            VictoryRec = new Rectangle(230, ScreenH/2 - 300, VictoryDrawW, VictoryDrawH);
            VictorySpriteSrc = new Rectangle(0, 0, Victory.Width, Victory.Height);

            SDDrawW = SuddenDeath.Width - 50;
            SDDrawH = SuddenDeath.Height - 50;
            SDRec = new Rectangle(-300, -300, SDDrawW, SDDrawH);

            //Loading Array Contents--------------------------------

            //Range Colors
            RangeColor[0] = new Color (254,36,66);
            RangeColor[1] = new Color (252,33,68);
            RangeColor[2] = new Color (0,193,154);
            RangeColor[3] = new Color (204,255,245);
            RangeColor[4] = new Color (255,84,0);
            RangeColor[5] = new Color (255,153,0);

            for (i = 0; i < 2; i++)
            {
                //Sets player hitboxes
                PlayerRec[i] = new Rectangle(50 + 940 * (i), ScreenH - 500, PlayerDrawW, PlayerDrawH);
                PlayerSpriteSrc[i] = new Rectangle(0, 0, PlayerW, PlayerH);
                PlayerBoundSrc[i] = new Rectangle(0, 0, CollisionBox.Width, CollisionBox.Height);
                PlayerBounds[i] = new Rectangle(115 + 950 * (i), 290, 100, 120);
                AttackBound[i] = new Rectangle(215 + 775 * (i), 290, 75, 120);

                //Sets health bar display
                HealthBackground[i] = new Rectangle(50 + i * 660, 50, HealthDrawW[i], HealthDrawH);
                HealthFont[i] = new Rectangle(55 + i * 660, 55, HealthDrawW[i], HealthDrawH);
                HealthRec[i] = new Rectangle(105 + i * 660, 50, HealthDrawW[i], HealthDrawH);

                //Sets mana bar display
                ManaCage[i] = new Rectangle(50 + i * 670, 120, ManaDrawW[i], ManaDrawH);
                ManaBackground[i] = new Rectangle(50 + i * 670, 120, ManaDrawW[i], ManaDrawH);
                ManaRec[i] = new Rectangle(50 + i * 670, 120, ManaDrawW[i], ManaDrawH);

                //Sets the ground where the game takes place
                FloorBounds[i] = new Rectangle(0 + i * 265, 640 + i * -165, 1300 + i * -550, 25 + i * -10);

                //Spawns range hitboxes off screen ready to be used
                RangeBound[i] = new Rectangle(-100, -100, RangeDrawW, RangeDrawH);
                RangeSpriteSrc[i] = new Rectangle(0, 0, RangeW, RangeAttack.Height);

                //Spawns power-up hitboxes off screen ready to be used
                PowerUpRec = new Rectangle(-100, -100, PowerUpDrawW, PowerUpDrawH);
                PowerUpSpriteSrc = new Rectangle(0, 0, PowerUpW, PowerUpH);
                PowerUpFrameNumber = 1;

                //Set the staring point of player arrows which follow the characters moving
                ArrowSpriteSrc[i] = new Rectangle(i * (PlayerArrows.Width / 2), 0, ArrowW, PlayerArrows.Height);

                //Sets the location of the pentagon stat chart
                StatRec[i] = new Rectangle(25 + i * 935, 425, StatDrawW, StatDrawH);
                StatSpriteSrc[i] = new Rectangle(0, 0, StatW, FighterStats.Height);
            }

            for (i = 0; i < 3; i++)
            {
                //Cut sprite sheet into its appropriate parts
                HealthSpriteSrc[i] = new Rectangle((HealthW * i), 0, HealthW, Healthbar.Height);
                ManaSpriteSrc[i] = new Rectangle((ManaW * i), 0, ManaW, ManaBar.Height);

                //Spawn the three menu rectangles which the player can interact with
                MenuOptions[i] = new Rectangle(540, 250 + i * 100, 200, 75);
            }

            //Sets circular hitboxes for the range attacks
            Vector2 PlayerCentre = new Vector2(PlayerBounds[0].Center.X, PlayerBounds[0].Center.Y);
            float PlayerRadius = Vector2.Distance(PlayerCentre, new Vector2(PlayerBounds[0].X, PlayerBounds[1].Y));
            CircularBounds = new Rectangle((int)(PlayerCentre.X - PlayerRadius), (int)(PlayerCentre.Y - PlayerRadius), (int)PlayerRadius * 2, (int)PlayerRadius * 2);

            //Music-------------------------------------------------

            //Load in all music clips
            BackM = Content.Load<SoundEffect>(@"Audio\Final_Destination_-_Super_Smash_Bros");
            BackMInstance = BackM.CreateInstance();
            MageM = Content.Load<SoundEffect>(@"Audio\move700ms_02");
            MageMInstance = MageM.CreateInstance();
            BlockM = Content.Load<SoundEffect>(@"Audio\block_L_05");
            BlockMInstance = BlockM.CreateInstance();
            SwiftM = Content.Load<SoundEffect>(@"Audio\spin_200ms_01_1_");
            SwiftMInstance = SwiftM.CreateInstance();
            HeavyM = Content.Load<SoundEffect>(@"Audio\punch_M_10");
            HeavyMInstance = HeavyM.CreateInstance();
            WinM = Content.Load<SoundEffect>(@"Audio\Victory");
            WinMinstance = WinM.CreateInstance();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            kb = Keyboard.GetState();

            // Allows the game to exit
            if (kb.IsKeyDown(Keys.Escape))
                this.Exit();

            //Checks which subprograms can run via the state of the game
            if (Menu == true)
            {
                //Figures out which request is being asked from the user at the menu
                if (kb.IsKeyDown(Keys.Enter) && kkb.IsKeyUp(Keys.Enter))
                {
                    MenuScreen(SelectedOption, MenuPage);
                }
                else if (kb.IsKeyDown(Keys.Back))
                {
                    MenuPage = MenuPage - 1;

                    if (MenuPage == 0)
                    {
                        MenuPage = 1;
                    }
                }

                //Allow players to choose a fighter
                SwitchCharacters();

                //Allow player to cycle through menu options
                MenuSettings();
            }
            else
            {
                if (Sound == true)
                {
                    //Plays blackground music
                    BackMInstance.Volume = 0.2f;
                    BackMInstance.Play();
                }

                //Waits for user to click "Play" before starting the match
                PlayerOneMovements(CastAnimation[0]);
                PlayerTwoMovements(CastAnimation[1]);

                //Apply game rules twice, once for each player
                for (i = 0; i < 2; i++)
                {
                    Projectile(i);

                    FallGravity(i);

                    PowerUpGravity();

                    ManaUsage(i);

                    ArrowRec[i] = new Rectangle(PlayerRec[i].X + PlayerHead[i], PlayerRec[i].Y - 10, ArrowDrawW, ArrowDrawH);
                }

                //Enable/disable game features depending on if the game is over
                if (EndGame != true)
                {
                    SpawnRate();

                    ActivePowerUp();

                    RangeCheck();
                }
                else
                {
                    if (Sound == true)
                    {
                        //Stops background sound
                        BackMInstance.Stop();

                        if (VictoryLoop == true)
                        {
                            WinMinstance.Play();
                        }

                        VictoryLoop = false;
                    }

                    //Allows user to restart game
                    if (kb.IsKeyDown(Keys.R))
                    {
                        this.Reset();
                    }
                }

                Barriers();
                }

            //Used for button clicks
            kkb = Keyboard.GetState();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);

            //Only draws these when the menu is closed
            if (Menu == false)
            {
                spriteBatch.Draw(Background, BackgroundRec, BackgroundSpriteSrc, Color.White * 0.9f);

                spriteBatch.Draw(Platform, FloorBounds[1], PlatformSpriteSrc, Color.White);

                for (i = 0; i < 2; i++)
                {
                    //Display visual aids twice, once for each fighter
                    spriteBatch.Draw(Healthbar, HealthBackground[i], HealthSpriteSrc[0], Color.White);

                    spriteBatch.Draw(Healthbar, HealthRec[i], HealthSpriteSrc[1], Color.White);

                    spriteBatch.Draw(Healthbar, HealthFont[i], HealthSpriteSrc[2], Color.White);

                    spriteBatch.Draw(ManaBar, ManaBackground[i], ManaSpriteSrc[2], Color.White);

                    spriteBatch.Draw(ManaBar, ManaRec[i], ManaSpriteSrc[1], Color.White);

                    spriteBatch.Draw(ManaBar, ManaCage[i], ManaSpriteSrc[0], Color.White);

                    spriteBatch.Draw(RangeAttack, RangeBound[i], RangeSpriteSrc[i], Color.White);

                    spriteBatch.Draw(PowerUps, PowerUpRec, PowerUpSpriteSrc, Color.White);

                    //Testing Purposes
                    spriteBatch.Draw(CollisionBox, FloorBounds[i], PlayerBoundSrc[i], Color.White * Testing);

                    spriteBatch.Draw(CollisionBox, PlayerBounds[i], PlayerBoundSrc[i], Color.White * Testing);

                    spriteBatch.Draw(CollisionBox, AttackBound[i], PlayerBoundSrc[i], Color.White * Testing);

                    spriteBatch.Draw(CollisionBox, PowerUpRec, Color.White * Testing);

                    spriteBatch.Draw(CollisionBox, RangeBound[i], Color.White * Testing);
                }
            }
            else
            {
                //Fades away background allowing player to foucs on the menu
                spriteBatch.Draw(Background, BackgroundRec, BackgroundSpriteSrc, Color.White * 0.4f);

                MenuDraw();
            }

            spriteBatch.Draw(SuddenDeath, SDRec, Color.White);

            MirrorPlayer();

            AnimationDefeat(HealthDrawW[0], HealthDrawW[1]);

            //"Freezes" features if the game ends
            if (EndGame != true)
            {
                //Checks if the user wants additional graphics
                if (Graphic == true)
                {
                    ParticleSpawn();
                    AnimationPowerUp();
                }
            }
            else
            {
                spriteBatch.DrawString(MenuFont, "Press R to restart", new Vector2(520, 600), Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        //Animation---------------------------------------------

        //Pre: Player one's animation state
        //Post: Plays appropriate animation
        //Desc: Uses switch statements to filter out which animations will be played
        private void AnimationOne(int State)
        {
            //Fliters which animation state player one is on
            switch (State)
            {
                //Block animation
                case (0):
                    BlockTicker[0]++;

                    if (BlockTicker[0] % BlockSmoothess[0] == 0)
                    {
                        //Fliter which fighter the player has selected
                        switch (PlayerFrameWide[0])
                        {
                            case (13):
                                NewScrX[0] = (PlayerW * 8) + (PlayerW * BlockFrame[0]);
                                NewScrY[0] = PlayerH * 2;
                                break;
                            case (15):
                                NewScrX[0] = PlayerW * BlockFrame[0];
                                NewScrY[0] = PlayerH * 3;
                                break;
                            case (16):
                                NewScrX[0] = (PlayerW * 9) + (PlayerW * BlockFrame[0]);
                                NewScrY[0] = PlayerH * 2;
                                break;
                        }
                        PlayerSpriteSrc[0] = new Rectangle(NewScrX[0], NewScrY[0], PlayerW, PlayerH);

                        BlockFrame[0]++;

                        BlockFrame[0] = BlockFrame[0] % BlockFrameNumber;
                    }
                    break;
                //Weak attack animation
                case (1):
                    WeakTicker[0]++;

                    if (WeakTicker[0] % WeakAttackSmoothness[0] == 0)
                    {
                        //Fliter which fighter the player has selected
                        switch (PlayerFrameWide[0])
                        {
                            case (13):
                                NewScrX[0] = (PlayerW * WeakFrame[0]);
                                NewScrY[0] = PlayerH * 1;

                                if (Sound == true)
                                {
                                    HeavyMInstance.Volume = 0.2f;
                                    HeavyMInstance.Play();
                                }
                                break;
                            case (15):
                                NewScrX[0] = PlayerW * WeakFrame[0];
                                NewScrY[0] = PlayerH * 1;

                                if (Sound == true)
                                {
                                    SwiftMInstance.Volume = 0.2f;
                                    SwiftMInstance.Play();
                                }
                                break;
                            case (16):
                                if (WeakFrame[0] <= 5)
                                {
                                    NewScrX[0] = PlayerW * WeakFrame[0];
                                    NewScrY[0] = PlayerH * 1;
                                }
                                else if (WeakFrame[0] > 5 && WeakFrame[0] <= 13)
                                {
                                    NewScrX[0] = PlayerW * (WeakFrame[0] - 5);
                                    NewScrY[0] = PlayerH * 2;
                                }

                                if (Sound == true)
                                {
                                    MageMInstance.Volume = 0.2f;
                                    MageMInstance.Play();
                                }
                                break;
                        }

                        PlayerSpriteSrc[0] = new Rectangle(NewScrX[0], NewScrY[0], PlayerW, PlayerH);

                        WeakFrame[0]++;

                        WeakFrame[0] = WeakFrame[0] % WeakFrameNumber[0];

                        if (WeakFrame[0] == 0)
                        {
                            if (PlayerOnPlayerCollision(PlayerBounds[0], PlayerBounds[1]) != HitCollision)
                            {
                                if (FacingDirection[0] == 1)
                                {
                                    PlayerRec[0].X = PlayerRec[0].X + 15;
                                    PlayerBounds[0].X = PlayerBounds[0].X + 15;
                                    AttackBound[0].X = AttackBound[0].X + 15;
                                }
                                else
                                {
                                    PlayerRec[0].X = PlayerRec[0].X - 15;
                                    PlayerBounds[0].X = PlayerBounds[0].X - 15;
                                    AttackBound[0].X = AttackBound[0].X - 15;
                                }
                            }

                            DamageCheck(0, 1, WeakDamage[0]);

                            Weak[0] = false;
                            WeakCooldown[0] = 15;
                        }
                    }
                    break;
                //Strong attack animation
                case (2):
                    StrongTicker[0]++;

                    if (StrongTicker[0] % StrongAttackSmoothness[0] == 0)
                    {
                        //Fliter which fighter the player has selected
                        switch (PlayerFrameWide[0])
                        {
                            case (13):
                                NewScrX[0] = (PlayerW * StrongFrame[0]);
                                NewScrY[0] = PlayerH * 2;

                                if (Sound == true)
                                {
                                    HeavyMInstance.Volume = 1.0f;
                                    HeavyMInstance.Play();
                                }
                                break;
                            case (15):
                                if (StrongFrame[0] <= 3)
                                {
                                    NewScrX[0] = PlayerW * 9;
                                }
                                else
                                {
                                    NewScrX[0] = (PlayerW * 10) + (PlayerW * (StrongFrame[0] - 4));
                                }

                                NewScrY[0] = PlayerH * 1;

                                if (Sound == true)
                                {
                                    SwiftMInstance.Volume = 1.0f;
                                    SwiftMInstance.Play();
                                }
                                break;
                            case (16):
                                NewScrX[0] = PlayerW * StrongFrame[0];
                                NewScrY[0] = PlayerH * 1;

                                if (Sound == true)
                                {
                                    MageMInstance.Volume = 1.0f;
                                    MageMInstance.Play();
                                }
                                break;
                        }
                        PlayerSpriteSrc[0] = new Rectangle(NewScrX[0], NewScrY[0], PlayerW, PlayerH);

                        StrongFrame[0]++;

                        StrongFrame[0] = StrongFrame[0] % StrongFrameNumber[0];

                        if (StrongFrame[0] == 0)
                        {
                            if (PlayerOnPlayerCollision(PlayerBounds[0], PlayerBounds[1]) != HitCollision)
                            {
                                if (FacingDirection[0] == 1)
                                {
                                    PlayerRec[0].X = PlayerRec[0].X + 25;
                                    PlayerBounds[0].X = PlayerBounds[0].X + 25;
                                    AttackBound[0].X = AttackBound[0].X + 25;
                                }
                                else
                                {
                                    PlayerRec[0].X = PlayerRec[0].X - 25;
                                    PlayerBounds[0].X = PlayerBounds[0].X - 25;
                                    AttackBound[0].X = AttackBound[0].X - 25;
                                }
                            }

                            DamageCheck(0, 1, StrongDamage[0]);

                            Strong[0] = false;
                            StrongCooldown[0] = 15;
                        }
                    }
                    break;
                //Range attack animation
                case (3):
                    RangeTicker[0]++;

                    if (RangeTicker[0] % RangeAttackSmoothness[0] == 0)
                    {
                        //Fliter which fighter the player has selected
                        switch (PlayerFrameWide[0])
                        {
                            case (13):
                                if (RangeFrame[0] >= 4)
                                {
                                    NewScrX[0] = PlayerW * 12;
                                }
                                else
                                {
                                    NewScrX[0] = (PlayerW * 9) + (PlayerW * RangeFrame[0]);
                                }

                                NewScrY[0] = PlayerH * 1;
                                break;
                            case (15):
                                if (RangeFrame[0] >= 4)
                                {
                                    NewScrX[0] = PlayerW * 6;
                                }
                                else
                                {
                                    NewScrX[0] = (PlayerW * 3) + (PlayerW * RangeFrame[0]);
                                }

                                NewScrY[0] = PlayerH * 3;
                                break;
                            case (16):
                                if (RangeFrame[0] >= 2)
                                {
                                    NewScrX[0] = PlayerW * 3;
                                }
                                else
                                {
                                    NewScrX[0] = (PlayerW * 6) + (PlayerW * RangeFrame[0]);
                                }

                                NewScrY[0] = PlayerH * 2;
                                break;
                        }
                        PlayerSpriteSrc[0] = new Rectangle(NewScrX[0], NewScrY[0], PlayerW, PlayerH);

                        RangeFrame[0]++;

                        RangeFrame[0] = RangeFrame[0] % RangeFrameNumber[0];

                        if (RangeFrame[0] == 0)
                        {
                            Direction[0] = FacingDirection[0];

                            RangeBound[0] = new Rectangle(AttackBound[0].X, AttackBound[0].Y + 10, RangeDrawW, RangeDrawH);

                            Range[0] = false;
                            RangeCooldown[0] = 15;
                        }
                    }
                    break;
                //Idle animation
                default:
                    IdleTicker[0]++;

                    if (IdleTicker[0] % IdleSmoothness[0] == 0)
                    {
                        NewScrX[0] = PlayerW * FrameNumber;

                        PlayerSpriteSrc[0] = new Rectangle(NewScrX[0], 0, PlayerW, PlayerH);

                        FrameNumber++;

                        FrameNumber = FrameNumber % IdleFrameNumber[0];
                    }
                    break;
            }
        }

        //Pre: Player two's animation state
        //Post: Plays appropriate animation
        //Desc: Uses switch statements to filter out which animations will be played
        private void AnimationTwo(int State)
        {
            //Same commenting as AnimationOne
            switch (State)
            {
                case (0):
                    BlockTicker[1]++;

                    if (BlockTicker[1] % BlockSmoothess[1] == 0)
                    {
                        switch (PlayerFrameWide[1])
                        {
                            case(13):
                                NewScrX[1] = (PlayerW * 8) + (PlayerW * BlockFrame[0]);
                                NewScrY[1] = PlayerH * 2;
                                break;
                            case(15):
                                NewScrX[1] = PlayerW * BlockFrame[0];
                                NewScrY[1] = PlayerH * 3;
                                break;
                            case(16):
                                NewScrX[1] = (PlayerW * 9) + (PlayerW * BlockFrame[0]);
                                NewScrY[1] = PlayerH * 2;
                                break;
                        }
                        PlayerSpriteSrc[1] = new Rectangle(NewScrX[1], NewScrY[1], PlayerW, PlayerH);

                        BlockFrame[1]++;

                        BlockFrame[1] = BlockFrame[1] % BlockFrameNumber;
                    }
                    break;
                case (1):
                    WeakTicker[1]++;

                    if (WeakTicker[1] % WeakAttackSmoothness[1] == 0)
                    {
                        switch (PlayerFrameWide[1])
                        {
                            case (13):
                                NewScrX[1] = (PlayerW * WeakFrame[1]);
                                NewScrY[1] = PlayerH * 1;

                                if (Sound == true)
                                {
                                    HeavyMInstance.Volume = 0.2f;
                                    HeavyMInstance.Play();
                                }
                                break;
                            case (15):
                                NewScrX[1] = PlayerW * WeakFrame[1];
                                NewScrY[1] = PlayerH * 1;

                                if (Sound == true)
                                {
                                    SwiftMInstance.Volume = 0.2f;
                                    SwiftMInstance.Play();
                                }
                                break;
                            case (16):
                                if (WeakFrame[1] <= 5)
                                {
                                    NewScrX[1] = PlayerW * WeakFrame[1];
                                    NewScrY[1] = PlayerH * 1;
                                }
                                else if (WeakFrame[1] > 5 && WeakFrame[1] <= 13)
                                {
                                    NewScrX[1] = PlayerW * (WeakFrame[1] - 5);
                                    NewScrY[1] = PlayerH * 2;
                                }

                                if (Sound == true)
                                {
                                    MageMInstance.Volume = 0.2f;
                                    MageMInstance.Play();
                                }
                                break;
                        }

                        PlayerSpriteSrc[1] = new Rectangle(NewScrX[1], NewScrY[1], PlayerW, PlayerH);

                        WeakFrame[1]++;
                        
                        WeakFrame[1] = WeakFrame[1] % WeakFrameNumber[1];

                        if (WeakFrame[1] == 0)
                        {
                            if (PlayerOnPlayerCollision(PlayerBounds[0], PlayerBounds[1]) != HitCollision)
                            {
                                if (FacingDirection[1] == 1)
                                {
                                    PlayerRec[1].X = PlayerRec[1].X + 15;
                                    PlayerBounds[1].X = PlayerBounds[1].X + 15;
                                    AttackBound[1].X = AttackBound[1].X + 15;
                                }
                                else
                                {
                                    PlayerRec[1].X = PlayerRec[1].X - 15;
                                    PlayerBounds[1].X = PlayerBounds[1].X - 15;
                                    AttackBound[1].X = AttackBound[1].X - 15;
                                }
                            }

                            DamageCheck(1, 0, WeakDamage[1]);

                            Weak[1] = false;
                            WeakCooldown[1] = 15;
                        }
                    }
                    break;
                case (2):
                    StrongTicker[1]++;

                    if (StrongTicker[1] % StrongAttackSmoothness[1] == 0)
                    {
                        switch (PlayerFrameWide[1])
                        {
                            case (13):
                                NewScrX[1] = (PlayerW * StrongFrame[1]);
                                NewScrY[1] = PlayerH * 2;

                                if (Sound == true)
                                {
                                    HeavyMInstance.Volume = 1.0f;
                                    HeavyMInstance.Play();
                                }
                                break;
                            case (15):
                                if (StrongFrame[1] <= 3)
                                {
                                    NewScrX[1] = PlayerW * 9;
                                }
                                else
                                {
                                    NewScrX[1] = (PlayerW * 10) + (PlayerW * (StrongFrame[1] - 4));
                                }

                                NewScrY[1] = PlayerH * 1;

                                if (Sound == true)
                                {
                                    SwiftMInstance.Volume = 0.2f;
                                    SwiftMInstance.Play();
                                }
                                break;
                            case (16):
                                    NewScrX[1] = PlayerW * StrongFrame[1];
                                    NewScrY[1] = PlayerH * 1;

                                    if (Sound == true)
                                    {
                                        MageMInstance.Volume = 0.2f;
                                        MageMInstance.Play();
                                    }
                                break;
                        }
                        PlayerSpriteSrc[1] = new Rectangle(NewScrX[1], NewScrY[1], PlayerW, PlayerH);

                        StrongFrame[1]++;

                        StrongFrame[1] = StrongFrame[1] % StrongFrameNumber[1];

                        if (StrongFrame[1] == 0)
                        {
                            if (PlayerOnPlayerCollision(PlayerBounds[0], PlayerBounds[1]) != HitCollision)
                            {
                                if (FacingDirection[1] == 1)
                                {
                                    PlayerRec[1].X = PlayerRec[1].X + 25;
                                    PlayerBounds[1].X = PlayerBounds[1].X + 25;
                                    AttackBound[1].X = AttackBound[1].X + 25;
                                }
                                else
                                {
                                    PlayerRec[1].X = PlayerRec[1].X - 25;
                                    PlayerBounds[1].X = PlayerBounds[1].X - 25;
                                    AttackBound[1].X = AttackBound[1].X - 25;
                                }
                            }

                            DamageCheck(1, 0, StrongDamage[1]);

                            Strong[1] = false;
                            StrongCooldown[1] = 15;
                        }
                    }
                    break;
                case (3):
                    RangeTicker[1]++;

                    if (RangeTicker[1] % RangeAttackSmoothness[1] == 0)
                    {
                        switch (PlayerFrameWide[1])
                        {
                            case (13):
                                if (RangeFrame[1] >= 4)
                                {
                                    NewScrX[1] = PlayerW * 12;
                                }
                                else
                                {
                                    NewScrX[1] = (PlayerW * 9) + (PlayerW * RangeFrame[1]);
                                }

                                NewScrY[1] = PlayerH * 1;
                                break;
                            case (15):
                                if (RangeFrame[1] >= 4)
                                {
                                    NewScrX[1] = PlayerW * 6;
                                }
                                else
                                {
                                    NewScrX[1] = (PlayerW * 3) + (PlayerW * RangeFrame[1]);
                                }

                                NewScrY[1] = PlayerH * 3;
                                break;
                            case (16):
                                if (RangeFrame[1] >= 2)
                                {
                                    NewScrX[1] = PlayerW * 3;
                                }
                                else
                                {
                                    NewScrX[1] = (PlayerW * 6) + (PlayerW * RangeFrame[1]);           
                                }

                                NewScrY[1] = PlayerH * 2;
                                break;
                        }
                        PlayerSpriteSrc[1] = new Rectangle(NewScrX[1], NewScrY[1], PlayerW, PlayerH);

                        RangeFrame[1]++;

                        RangeFrame[1] = RangeFrame[1] % RangeFrameNumber[1];

                        if (RangeFrame[1] == 0)
                        {
                            Direction[1] = FacingDirection[1];

                            RangeBound[1] = new Rectangle(AttackBound[1].X, AttackBound[1].Y + 10, RangeDrawW, RangeDrawH);

                            Range[1] = false;
                            RangeCooldown[1] = 15;
                        }
                    }
                    break;
                default:
                    IdleTicker[1]++;

                    if (IdleTicker[1] % IdleSmoothness[1] == 0)
                    {
                        NewScrX[1] = PlayerW * FrameNumber;

                        PlayerSpriteSrc[1] = new Rectangle(NewScrX[1], 0, PlayerW, PlayerH);

                        FrameNumber++;

                        FrameNumber = FrameNumber % IdleFrameNumber[1];
                    }
                    break;
            }
        }

        //Pre: Both players health remaining
        //Post: Queue's death animation and game over state when required
        //Desc: Checks which player has no health remaining
        private void AnimationDefeat(int HealthOne, int HealthTwo)
        {
            //Checks if player two won
            if (HealthOne <= 0 && HealthTwo > 0)
            {
                //Freezes game
                EndGame = true;

                CastAnimation[0] = true;
                CastAnimation[1] = true;

                //Remove Sudden Death image and show victory image
                SDRec = new Rectangle(-300, -300, SDDrawW, SDDrawH);

                spriteBatch.Draw(Victory, VictoryRec, VictorySpriteSrc, Color.White);

                //Queues death animation
                DeathTicker[0]++;

                if (DeathTicker[0] % DeathSmoothness[0] == 0)
                {
                    switch (PlayerFrameWide[0])
                    {
                        case (13):
                            NewScrX[0] = PlayerW * DeathFrame[0];
                            NewScrY[0] = PlayerH * 3;
                            break;
                        case (15):
                            NewScrX[0] = PlayerW * DeathFrame[0];
                            NewScrY[0] = PlayerH * 2;
                            break;
                        case (16):
                            NewScrX[0] = PlayerW * DeathFrame[0];
                            NewScrY[0] = PlayerH * 3;
                            break;
                    }

                    PlayerSpriteSrc[0] = new Rectangle(NewScrX[0], NewScrY[0], PlayerW, PlayerH);
                    PlayerSpriteSrc[1] = new Rectangle(0, 0, PlayerW, PlayerH);

                    DeathFrame[0]++;

                    if (DeathFrame[0] == DeathFrameNumber[0])
                    {
                        DeathFrame[0] = DeathFrame[0] - 1;
                    }
                }
            }
            //Same commenting as above
            else if (HealthTwo <= 0 && HealthOne > 0)
            {
                EndGame = true;

                CastAnimation[1] = true;
                CastAnimation[0] = true;

                SDRec = new Rectangle(-300, -300, SDDrawW, SDDrawH);

                spriteBatch.Draw(Victory, VictoryRec, VictorySpriteSrc, Color.White);

                DeathTicker[1]++;

                if (DeathTicker[1] % DeathSmoothness[1] == 0)
                {
                    switch (PlayerFrameWide[1])
                    {
                        case (13):
                            NewScrX[1] = PlayerW * DeathFrame[1];
                            NewScrY[1] = PlayerH * 3;
                            break;
                        case (15):
                            NewScrX[1] = PlayerW * DeathFrame[1];
                            NewScrY[1] = PlayerH * 2;
                            break;
                        case (16):
                            NewScrX[1] = PlayerW * DeathFrame[1];
                            NewScrY[1] = PlayerH * 3;
                            break;
                    }

                    PlayerSpriteSrc[1] = new Rectangle(NewScrX[1], NewScrY[1], PlayerW, PlayerH);
                    PlayerSpriteSrc[0] = new Rectangle(0, 0, PlayerW, PlayerH);

                    DeathFrame[1]++;

                    if (DeathFrame[1] == DeathFrameNumber[1])
                    {
                        DeathFrame[1] = DeathFrame[1] - 1;
                    }
                }
            }
            //Checks if both players die at the same time
            else if (HealthTwo <= 0 && HealthOne <= 0)
            {
                //Spawn both players back at their starting points
                HealthDrawW[0] = 10;
                HealthDrawW[1] = 10;

                for (i = 0; i < 2; i++)
                {
                    PlayerRec[i] = new Rectangle(50 + 940 * (i), ScreenH - 270, PlayerDrawW, PlayerDrawH);
                    PlayerBounds[i] = new Rectangle(115 + 950 * (i), 520, 100, 120);
                    AttackBound[i] = new Rectangle(215 + 775 * (i), 520, 75, 120);
                }

                SDRec = new Rectangle(400, 160, SDDrawW, SDDrawH);
            }
            else
            {
                //Allow players to play game if no one is dead
                AnimationOne(PlayerCommand[0]);

                AnimationTwo(PlayerCommand[1]);
            }
        }

        //Pre: Which power up is on screen
        //Post: Plays animation loop for the specific power up
        //Desc: Changes frame everytime the timer is meet
        private void AnimationPowerUp()
        {
            //Plays power up animation
            PowerUpTicker++;

            if (PowerUpTicker % PowerUpSmoothness == 0)
            {
                NewPowerUpX = PowerUpW * PowerUpFrame;

                PowerUpSpriteSrc = new Rectangle(NewPowerUpX, NewPowerUpY, PowerUpW, PowerUpH);

                PowerUpFrame++;

                PowerUpFrame = PowerUpFrame % PowerUpFrameNumber;
            }
        }

        //User Input--------------------------------------------

        //Pre: Player one's keyboard input
        //Post: Allows animation if all restriction are meet
        //Desc: Checks if only a single command is being inputed
        private void PlayerOneMovements(bool Cast)
        {
            if (Cast == false)
            {
                //Left movement
                if (kb.IsKeyDown(Keys.D) && BlockedOne[0] == false)
                {
                    PlayerRec[0].X = PlayerRec[0].X + MovementSpeed[0];
                    PlayerBounds[0].X = PlayerBounds[0].X + MovementSpeed[0];
                    AttackBound[0].X = AttackBound[0].X + MovementSpeed[0];
                }
                //Right movement
                else if (kb.IsKeyDown(Keys.A) && BlockedOne[1] == false)
                {
                    PlayerRec[0].X = PlayerRec[0].X - MovementSpeed[0];
                    PlayerBounds[0].X = PlayerBounds[0].X - MovementSpeed[0];
                    AttackBound[0].X = AttackBound[0].X - MovementSpeed[0];
                }

                //Makes sure every restriction is meet before allowing player to request an action
                if (kb.IsKeyDown(Keys.F) && WeakCooldown[0] <= 0 && Block[0] != true && Strong[0] != true && Range[0] != true)
                {
                    Weak[0] = true;
                }
                else if (kb.IsKeyDown(Keys.S) && Weak[0] != true && Strong[0] != true && Range[0] != true && ManaDrawW[0] > 0)
                {
                    Block[0] = true;
                }
                else if (kb.IsKeyDown(Keys.G) && StrongCooldown[0] <= 0 && Block[0] != true && Weak[0] != true && Range[0] != true)
                {
                    Strong[0] = true;
                }
                else if (kb.IsKeyDown(Keys.H) && RangeCooldown[0] <= 0 && RangeBound[0].Y == -50 && Block[0] != true && Weak[0] != true && Strong[0] != true)
                {
                    Range[0] = true;
                }
                else if (kb.IsKeyDown(Keys.W) && JumpCooldown[0] <= 0)
                {
                    Jump[0] = true;
                }
            }

            JumpGravity(0);

            Cooldowns(0);

            //Fliters which animation will be played
            if (Block[0] == true)
            {
                CastAnimation[0] = true;
                PlayerCommand[0] = 0;
            }
            else if (Weak[0] == true)
            {
                CastAnimation[0] = true;
                PlayerCommand[0] = 1;
            }
            else if (Strong[0] == true)
            {
                CastAnimation[0] = true;
                PlayerCommand[0] = 2;
            }
            else if (Range[0] == true)
            {
                CastAnimation[0] = true;
                PlayerCommand[0] = 3;
            }
            else
            {
                CastAnimation[0] = false;
                PlayerCommand[0] = 10;
            }
        }

        //Pre: Player two's keyboard input
        //Post: Allows animation if all restriction are meet
        //Desc: Checks if only a single command is being inputed
        private void PlayerTwoMovements(bool Cast)
        {
            //Same commenting as PlayerOneMovements
            if (Cast == false)
            {
                if (kb.IsKeyDown(Keys.Right) && BlockedTwo[0] == false)
                {
                    PlayerRec[1].X = PlayerRec[1].X + MovementSpeed[1];
                    PlayerBounds[1].X = PlayerBounds[1].X + MovementSpeed[1];
                    AttackBound[1].X = AttackBound[1].X + MovementSpeed[1];
                }
                else if (kb.IsKeyDown(Keys.Left) && BlockedTwo[1] == false)
                {
                    PlayerRec[1].X = PlayerRec[1].X - MovementSpeed[1];
                    PlayerBounds[1].X = PlayerBounds[1].X - MovementSpeed[1];
                    AttackBound[1].X = AttackBound[1].X - MovementSpeed[1];
                }

                if (kb.IsKeyDown(Keys.NumPad1) && WeakCooldown[1] <= 0 && Block[1] != true && Strong[1] != true && Range[1] != true)
                {
                    Weak[1] = true;
                }
                else if (kb.IsKeyDown(Keys.Down) && Weak[1] != true && Strong[1] != true && Range[1] != true && ManaDrawW[1] >= 0)
                {
                    Block[1] = true;
                }
                else if (kb.IsKeyDown(Keys.NumPad2) && StrongCooldown[1] <= 0 && Block[1] != true && Weak[1] != true && Range[1] != true)
                {
                    Strong[1] = true;
                }
                else if (kb.IsKeyDown(Keys.NumPad3) && RangeCooldown[1] <= 0 && RangeBound[1].Y == -50 && Block[1] != true && Weak[1] != true && Strong[1] != true)
                {
                    Range[1] = true;
                }
                else if (kb.IsKeyDown(Keys.Up) && JumpCooldown[1] <= 0)
                {
                    Jump[1] = true;
                }
            }

            JumpGravity(1);

            Cooldowns(1);

            if (Block[1] == true)
            {
                CastAnimation[1] = true;
                PlayerCommand[1] = 0;
            }
            else if (Weak[1] == true)
            {
                CastAnimation[1] = true;
                PlayerCommand[1] = 1;
            }
            else if (Strong[1] == true)
            {
                CastAnimation[1] = true;
                PlayerCommand[1] = 2;
            }
            else if (Range[1] == true)
            {
                CastAnimation[1] = true;
                PlayerCommand[1] = 3;
            }
            else
            {
                CastAnimation[1] = false;
                PlayerCommand[1] = 10;
            }
        }

        //Pre: Either user input
        //Post: Allows users to switch characters before starting the game
        //Desc: Use if statment to swap characters and stats
        private void SwitchCharacters()
        {
            //Calls subprogram upon request 
            if (kb.IsKeyDown(Keys.D1))
            {
                CharacterStats(0, 0);
            }
            else if (kb.IsKeyDown(Keys.D2))
            {
                CharacterStats(1, 0);
            }
            else if (kb.IsKeyDown(Keys.D3))
            {
                CharacterStats(2, 0);
            }
             else if (kb.IsKeyDown(Keys.D4))
            {
                CharacterStats(0, 1);
            }
            else if (kb.IsKeyDown(Keys.D5))
            {
                CharacterStats(1, 1);
            }
            else if (kb.IsKeyDown(Keys.D6))
            {
                CharacterStats(2, 1);
            }    
        }

        //Pre: Either user input
        //Post: Allows users to cycle through menu options
        //Desc: Use if statment to check for single clicks
        private void MenuSettings()
        {
            //Cycles through all options
            if (kkb.IsKeyDown(Keys.Up) && kb.IsKeyUp(Keys.Up))
            {
                switch (SelectedOption)
                {
                    case (1):
                        SelectedOption = 3;
                        break;
                    case (2):
                        SelectedOption = 1;
                        break;
                    case (3):
                        SelectedOption = 2;
                        break;
                }
            }
            else if (kkb.IsKeyDown(Keys.Down) && kb.IsKeyUp(Keys.Down))
            {
                switch (SelectedOption)
                {
                    case (1):
                        SelectedOption = 2;
                        break;
                    case (2):
                        SelectedOption = 3;
                        break;
                    case (3):
                        SelectedOption = 1;
                        break;
                }
            }
        }

        //Visual Aids and Game Rules----------------------------

        //Pre: Either user input 
        //Post: Allows users to restart the game
        //Desc: Revalue player stats and sends users back to the menu screen
        private void Reset()
        {
            //Revalue important values so players may start again
            Menu = true;
            EndGame = false;
            VictoryLoop = true;

            for (i = 0; i < 2; i++)
            {
                HealthDrawW[i] = HealthCap[i];

                PlayerRec[i] = new Rectangle(50 + 940 * (i), ScreenH - 500, PlayerDrawW, PlayerDrawH);
                PlayerSpriteSrc[i] = new Rectangle(0, 0, PlayerW, PlayerH);
                PlayerBoundSrc[i] = new Rectangle(0, 0, CollisionBox.Width, CollisionBox.Height);
                PlayerBounds[i] = new Rectangle(115 + 950 * (i), 290, 100, 120);
                AttackBound[i] = new Rectangle(215 + 775 * (i), 290, 75, 120);

                Weak[i] = false;
                Strong[i] = false;
                Range[i] = false;
                Block[i] = false;
                Jump[i] = false;

                CastAnimation[i] = false;
                PlayerCommand[i] = 10;
            }
            PowerUpRec = new Rectangle(-50, -50, PowerUpDrawW, PowerUpDrawH);
            SDRec = new Rectangle(-300, -300, SDDrawW, SDDrawH);
        }

        //Pre: Location of both players
        //Post: Deny players to moves out of screen and move across eachother
        //Desc: Teleports player a pixel off border if a player is trying to get off screen and denies movement if both players are on top of eachother
        private void Barriers()
        {
            //Checks both players to see if they've reached the edge of the screen
            for (i = 0; i < 2; i++)
            {
                if (PlayerRec[i].X < 15)
                {
                    PlayerRec[i].X = 16;
                    PlayerBounds[i].X = 85;
                    AttackBound[i].X = 185;
                }
                else if(PlayerRec[i].X > ScreenW - 245)
                {
                    PlayerRec[i].X = ScreenW - 244;
                    PlayerBounds[i].X = ScreenW - 170;
                    AttackBound[i].X = ScreenW - 245;
                }
            }

            //Prevents walking pass eachother
            if (PlayerOnPlayerCollision(PlayerBounds[1], PlayerBounds[0]) == HitCollision)
            {
                if (FacingDirection[1] == 1)
                {
                    BlockedTwo[0] = true;
                    BlockedOne[1] = true;
                }
                else
                {
                    BlockedTwo[1] = true;
                    BlockedOne[0] = true;
                }
            }
            else
            {
                BlockedTwo[0] = false;
                BlockedTwo[1] = false;
                BlockedOne[0] = false;
                BlockedOne[1] = false;
            }
        }

        //Pre: Direction both players are facing
        //Post: Flips direction players are facing
        //Desc: Keeps track of the direction both players are facing
        private void MirrorPlayer()
        {
            if (Menu == false)
            {
                for (i = 0; i < 2; i++)
                {
                    spriteBatch.Draw(PlayerArrows, ArrowRec[i], ArrowSpriteSrc[i], Color.White);
                }
            }

            //Switches direction the player is facing after only if they are idle
            if (PlayerCommand[0] == 10)
            {
                if (PlayerRec[0].X < PlayerRec[1].X)
                {
                    spriteBatch.Draw(PlayerFighter[0], PlayerRec[0], PlayerSpriteSrc[0], Color.White);
                    AttackBound[0].X = PlayerBounds[0].X + 100;

                    FacingDirection[0] = 1;

                    switch (PlayerFrameWide[0])
                    {
                        case (13):
                            PlayerHead[0] = 85;
                            break;
                        case (15):
                            PlayerHead[0] = 110;
                            break;
                        case (16):
                            PlayerHead[0] = 60;
                            break;
                    }
                }
                else
                {
                    spriteBatch.Draw(PlayerFighter[0], PlayerRec[0], PlayerSpriteSrc[0], Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                    AttackBound[0].X = PlayerBounds[0].X - 75;

                    FacingDirection[0] = 2;

                    switch (PlayerFrameWide[0])
                    {
                        case (13):
                            PlayerHead[0] = 85;
                            break;
                        case (15):
                            PlayerHead[0] = 60;
                            break;
                        case (16):
                            PlayerHead[0] = 110;
                            break;
                    }
                }
            }
            else
            {
                if (FacingDirection[0] == 1)
                {
                    spriteBatch.Draw(PlayerFighter[0], PlayerRec[0], PlayerSpriteSrc[0], Color.White);
                }
                else
                {
                    spriteBatch.Draw(PlayerFighter[0], PlayerRec[0], PlayerSpriteSrc[0], Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                }
            }

            if (PlayerCommand[1] == 10)
            {
                if (PlayerRec[0].X < PlayerRec[1].X)
                {
                    spriteBatch.Draw(PlayerFighter[1], PlayerRec[1], PlayerSpriteSrc[1], Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                    AttackBound[1].X = PlayerBounds[1].X - 75;

                    FacingDirection[1] = 2;

                    switch (PlayerFrameWide[1])
                    {
                        case (13):
                            PlayerHead[1] = 85;
                            break;
                        case (15):
                            PlayerHead[1] = 60;
                            break;
                        case (16):
                            PlayerHead[1] = 110;
                            break;
                    }
                }
                else
                {
                    spriteBatch.Draw(PlayerFighter[1], PlayerRec[1], PlayerSpriteSrc[1], Color.White);
                    AttackBound[1].X = PlayerBounds[1].X + 100;

                    FacingDirection[1] = 1;

                    switch (PlayerFrameWide[1])
                    {
                        case (13):
                            PlayerHead[1] = 85;
                            break;
                        case (15):
                            PlayerHead[1] = 110;
                            break;
                        case (16):
                            PlayerHead[1] = 60;
                            break;
                    }
                }
            }
            else
            {
                if (FacingDirection[1] == 1)
                {
                    spriteBatch.Draw(PlayerFighter[1], PlayerRec[1], PlayerSpriteSrc[1], Color.White);
                }
                else
                {
                    spriteBatch.Draw(PlayerFighter[1], PlayerRec[1], PlayerSpriteSrc[1], Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                }
            }


        }

        //Pre: Either player's cooldowns
        //Post: Lowers cooldowns
        //Desc: Continuously take away intergers from cooldowns until it hits zero
        private void Cooldowns(int Player)
        {
            //Prevents playters from spamming abilities
            if (Weak[Player] == false)
            {
                WeakCooldown[Player] = WeakCooldown[Player] - 1;
            }

            if (Strong[Player] == false)
            {
                StrongCooldown[Player] = StrongCooldown[Player] - 1;
            }

            if (Range[Player] == false)
            {
                RangeCooldown[Player] = RangeCooldown[Player] - 1;
            }

            //Undo block
            switch (Player)
            {
                case (0):
                    if (kb.IsKeyUp(Keys.S))
                    {
                        Block[0] = false;
                    }
                    break;
                case (1):
                    if(kb.IsKeyUp(Keys.Down))
                    {
                        Block[1] = false;
                    }
                    break;

            }

        }

        //Pre: User and the selected fighter
        //Post: Revalues character stats
        //Desc: Use switch statement of figure out which fighter was selected
        private void CharacterStats(int Fighter, int Player)
        {
            //Revalue stats base on which fighter is being used
            switch (Fighter)
            {
                case (0):
                    PlayerFighter[Player] = HeavyFighter;
                    PlayerFrameWide[Player] = 13;
                    PlayerFrameHeight[Player] = 4;
                    IdleFrameNumber[Player] = 10;
                    IdleSmoothness[Player] = 4;
                    WeakFrameNumber[Player] = 9;
                    WeakAttackSmoothness[Player] = 1;
                    StrongFrameNumber[Player] = 8;
                    StrongAttackSmoothness[Player] = 4;
                    RangeFrameNumber[Player] = 7;
                    RangeAttackSmoothness[Player] = 3;
                    RangeSpriteSrc[Player] = new Rectangle(RangeW * 0, 0, RangeW, RangeAttack.Height);
                    DeathFrameNumber[Player] = 13;
                    DeathSmoothness[Player] = 4;
                    WeakDamage[Player] = 24;
                    StrongDamage[Player] = 74;
                    RangeDamage[Player] = 30;
                    MovementSpeed[Player] = 9;
                    MoveCap[Player] = MovementSpeed[Player];
                    HealthDrawW[Player] = Healthbar.Width - 200;
                    HealthBackground[Player] = new Rectangle(50 + Player * 660, 50, HealthDrawW[Player], HealthDrawH);
                    ManaDrawW[Player] = ManaBar.Width - 500;
                    ManaBackground[Player] = new Rectangle(50 + Player * 670, 120, ManaDrawW[Player], ManaDrawH);
                    StatSpriteSrc[Player] = new Rectangle(0, 0, StatW, FighterStats.Height);
                    ManaCap[Player] = ManaBar.Width - 500;
                    ManaCage[Player] = new Rectangle(50 + Player * 670, 120, ManaDrawW[Player], ManaDrawH);
                    HealthCap[Player] = Healthbar.Width - 200;
                    break;
                case (1):
                    PlayerFighter[Player] = SwiftFighter;
                    PlayerFrameWide[Player] = 15;
                    IdleFrameNumber[Player] = 11;
                    PlayerFrameHeight[Player] = 4;
                    IdleSmoothness[Player] = 6;
                    WeakFrameNumber[Player] = 9;
                    WeakAttackSmoothness[Player] = 1;
                    StrongFrameNumber[Player] = 8;
                    StrongAttackSmoothness[Player] = 4;
                    RangeFrameNumber[Player] = 7;
                    RangeAttackSmoothness[Player] = 3;
                    RangeSpriteSrc[Player] = new Rectangle(RangeW * 2, 0, RangeW, RangeAttack.Height);
                    DeathFrameNumber[Player] = 13;
                    DeathSmoothness[Player] = 4;
                    WeakDamage[Player] = 38;
                    StrongDamage[Player] = 50;
                    RangeDamage[Player] = 30;
                    MovementSpeed[Player] = 9;
                    MoveCap[Player] = MovementSpeed[Player];
                    HealthDrawW[Player] = Healthbar.Width - 300;
                    HealthBackground[Player] = new Rectangle(50 + Player * 660, 50, HealthDrawW[Player], HealthDrawH);
                    ManaDrawW[Player] = ManaBar.Width - 500;
                    StatSpriteSrc[Player] = new Rectangle(StatW * 1, 0, StatW, FighterStats.Height);
                    ManaCap[Player] = ManaBar.Width - 500;
                    ManaCage[Player] = new Rectangle(50 + Player * 670, 120, ManaDrawW[Player], ManaDrawH);
                    ManaBackground[Player] = new Rectangle(50 + Player * 670, 120, ManaDrawW[Player], ManaDrawH);
                    HealthCap[Player] = Healthbar.Width - 300;
                    break;
                case (2):
                    PlayerFighter[Player] = MagicFighter;
                    PlayerFrameWide[Player] = 16;
                    PlayerFrameHeight[Player] = 4;
                    IdleFrameNumber[Player] = 14;
                    IdleSmoothness[Player] = 8;
                    WeakFrameNumber[Player] = 10;
                    WeakAttackSmoothness[Player] = 2;
                    StrongFrameNumber[Player] = 13;
                    StrongAttackSmoothness[Player] = 3;
                    RangeFrameNumber[Player] = 6;
                    RangeAttackSmoothness[Player] = 3;
                    RangeSpriteSrc[Player] = new Rectangle(RangeW * 1, 0, RangeW, RangeAttack.Height);
                    DeathFrameNumber[Player] = 15;
                    DeathSmoothness[Player] = 4;
                    WeakDamage[Player] = 24;
                    StrongDamage[Player] = 50;
                    RangeDamage[Player] = 46 ;
                    MovementSpeed[Player] = 8;
                    MoveCap[Player] = MovementSpeed[Player];
                    HealthDrawW[Player] = Healthbar.Width - 250;
                    HealthBackground[Player] = new Rectangle(50 + Player * 660, 50, HealthDrawW[Player], HealthDrawH);
                    ManaDrawW[Player] = ManaBar.Width - 400;
                    StatSpriteSrc[Player] = new Rectangle(StatW * 2, 0, StatW, FighterStats.Height);
                    ManaCap[Player] = ManaBar.Width - 400;
                    ManaCage[Player] = new Rectangle(50 + Player * 670, 120, ManaDrawW[Player], ManaDrawH);
                    ManaBackground[Player] = new Rectangle(50 + Player * 670, 120, ManaDrawW[Player], ManaDrawH);
                    HealthCap[Player] = Healthbar.Width - 250;
                    break;
            }
        }

        //Pre: Whichever player is blocking
        //Post: Lowers/adds mama
        //Desc: Continuously take away intergers from mana bars as well as regenerate
        private void ManaUsage(int Player)
        {
            //Drains mana if block if true
            if (Block[Player] == true)
            {
                ManaDrawW[Player] = ManaDrawW[Player] - 5;

                ManaRec[Player] = new Rectangle(50 + Player * 670, 120, ManaDrawW[Player], ManaDrawH);

                //Punish player if they deplete their mana
                if (ManaDrawW[Player] <= 0)
                {
                    ManaDrawW[Player] = -150;
                    Block[Player] = false;
                }
            }
            else
            {
                //Prevents getting more mana than their cap
                if (ManaDrawW[Player] <= ManaCap[Player])
                {
                    ManaDrawW[Player] = ManaDrawW[Player] + 1;
                    ManaRec[Player] = new Rectangle(50 + Player * 670, 120, ManaDrawW[Player], ManaDrawH);
                }
            }
        }

        //Pre: Menu page and whichever option the users are on
        //Post: Activates requested command 
        //Desc: Use switch statement to figure out which option the users are on
        private void MenuScreen(int BarNumber, int MenuWindow)
        {
            switch (MenuWindow)
            {
                case (1):
                    switch (BarNumber)
                    {
                        //Starts game
                        case (1):
                            if (Menu == true)
                            {
                                for (i = 0; i < 2; i++)
                                {
                                    PlayerRec[i] = new Rectangle(50 + 940 * (i), ScreenH - 270, PlayerDrawW, PlayerDrawH);
                                    PlayerBounds[i] = new Rectangle(115 + 950 * (i), 520, 100, 120);
                                    AttackBound[i] = new Rectangle(215 + 775 * (i), 520, 75, 120);
                                }
                            }
                            Menu = false;
                            break;
                        //Moves to page two
                        case (2):
                            MenuPage = 2;
                            break;
                        //Moves to page three
                        case (3):
                            MenuPage = 3;
                            break;
                    }
                    break;
                case (2):
                    switch (BarNumber)
                    {
                        //Toggle sound
                        case (1):
                            if (Sound == true)
                            {
                                Sound = false;
                            }
                            else
                            {
                                Sound = true;
                            }
                            break;
                        //Toggle graphic
                        case (2):
                            if (Graphic == true)
                            {
                                Graphic = false;
                            }
                            else
                            {
                                Graphic = true;
                            }
                            break;
                        //Toggle fullscreen
                        case (3):
                            if (this.graphics.IsFullScreen == false)
                            {
                                this.graphics.IsFullScreen = true;
                            }
                            else
                            {
                                this.graphics.IsFullScreen = false;
                            }

                            graphics.ApplyChanges();
                            break;
                    }
                    break;
            }
        }

        //Pre: Menu page and whichever option the users are on
        //Post: Change color and text of menu options base on user actions
        //Desc: Tracks what the user is requesting
        private void MenuDraw()
        {
            if (MenuPage != 3)
            {
                //Highlight the rectangle the players are on
                switch (SelectedOption)
                {
                    case (1):
                        spriteBatch.Draw(MenuBar, MenuOptions[0], Color.CadetBlue);
                        spriteBatch.Draw(MenuBar, MenuOptions[1], Color.White);
                        spriteBatch.Draw(MenuBar, MenuOptions[2], Color.White);
                        break;
                    case (2):
                        spriteBatch.Draw(MenuBar, MenuOptions[1], Color.CadetBlue);
                        spriteBatch.Draw(MenuBar, MenuOptions[0], Color.White);
                        spriteBatch.Draw(MenuBar, MenuOptions[2], Color.White);
                        break;
                    case (3):
                        spriteBatch.Draw(MenuBar, MenuOptions[2], Color.CadetBlue);
                        spriteBatch.Draw(MenuBar, MenuOptions[0], Color.White);
                        spriteBatch.Draw(MenuBar, MenuOptions[1], Color.White);
                        break;
                    default:
                        spriteBatch.Draw(MenuBar, MenuOptions[0], Color.White);
                        spriteBatch.Draw(MenuBar, MenuOptions[1], Color.White);
                        spriteBatch.Draw(MenuBar, MenuOptions[2], Color.White);
                        break;
                }
            }
            else
            {
                spriteBatch.Draw(MenuBar, MenuOptions[0], Color.White * 0f);
                spriteBatch.Draw(MenuBar, MenuOptions[1], Color.White * 0f);
                spriteBatch.Draw(MenuBar, MenuOptions[2], Color.White * 0f);
            }

            //Filters what information should appear on screen
            switch (MenuPage)
            {
                case (1):
                    spriteBatch.DrawString(MenuFont, "Play", new Vector2(MenuOptions[0].X + MenuBar.Width / 2, MenuOptions[0].Y + MenuBar.Height / 2), Color.White);
                    spriteBatch.DrawString(MenuFont, "Option", new Vector2(MenuOptions[1].X + MenuBar.Width / 2 - 25, MenuOptions[1].Y + MenuBar.Height / 2), Color.White);
                    spriteBatch.DrawString(MenuFont, "Instructions", new Vector2(MenuOptions[2].X + MenuBar.Width / 2 - 50, MenuOptions[2].Y + MenuBar.Height / 2), Color.White);
                    spriteBatch.Draw(FighterStats, StatRec[0], StatSpriteSrc[0], Color.White);
                    spriteBatch.Draw(FighterStats, StatRec[1], StatSpriteSrc[1], Color.White);
                    spriteBatch.DrawString(MenuFont, "Use 1,2,3 to select fighter", new Vector2(50, 200), Color.White);
                    spriteBatch.DrawString(MenuFont, "Use 4,5,6 to select fighter", new Vector2(900, 200), Color.White);
                    break;
                case (2):
                    spriteBatch.DrawString(MenuFont, "Sounds", new Vector2(MenuOptions[0].X + MenuBar.Width / 2 - 25, MenuOptions[0].Y + MenuBar.Height / 2), Color.White);
                    if (Sound == true)
                    {
                        spriteBatch.DrawString(MenuFont, "ON", new Vector2(MenuOptions[0].X + MenuBar.Width / 2 - 25 + 160, MenuOptions[0].Y + MenuBar.Height / 2), Color.White);
                    }
                    else
                    {
                        spriteBatch.DrawString(MenuFont, "OFF", new Vector2(MenuOptions[0].X + MenuBar.Width / 2 - 25 + 160, MenuOptions[0].Y + MenuBar.Height / 2), Color.White);
                    }
                    spriteBatch.DrawString(MenuFont, "Graphics", new Vector2(MenuOptions[1].X + MenuBar.Width / 2 - 25, MenuOptions[1].Y + MenuBar.Height / 2), Color.White);
                    if (Graphic == true)
                    {
                        spriteBatch.DrawString(MenuFont, "ON", new Vector2(MenuOptions[1].X + MenuBar.Width / 2 - 25 + 160, MenuOptions[1].Y + MenuBar.Height / 2), Color.White);
                    }
                    else
                    {
                        spriteBatch.DrawString(MenuFont, "OFF", new Vector2(MenuOptions[1].X + MenuBar.Width / 2 - 25 + 160, MenuOptions[1].Y + MenuBar.Height / 2), Color.White);
                    }
                    spriteBatch.DrawString(MenuFont, "Fullscreen", new Vector2(MenuOptions[2].X + MenuBar.Width / 2 - 50, MenuOptions[2].Y + MenuBar.Height / 2), Color.White);
                    if (this.graphics.IsFullScreen == true)
                    {
                        spriteBatch.DrawString(MenuFont, "ON", new Vector2(MenuOptions[2].X + MenuBar.Width / 2 - 25 + 160, MenuOptions[2].Y + MenuBar.Height / 2), Color.White);
                    }
                    else
                    {
                        spriteBatch.DrawString(MenuFont, "OFF", new Vector2(MenuOptions[2].X + MenuBar.Width / 2 - 25 + 160, MenuOptions[2].Y + MenuBar.Height / 2), Color.White);
                    }
                    break;
                case (3):
                    spriteBatch.DrawString(MenuFont, "Player Two               Player One\nMovement: L/R Arrows     Movement: AD\nQuick Attack: NumPad 1   Quick Attack: F\nStrong Attack: NumPad 2  Strong Attack: G\nRange Attack: NumPad 3   Range Attack: H\nBlock: Down Arrow        Block: S\nJump: Up Arrow           Jump: W", new Vector2(380, 200), Color.Black);
                    break;
            }
        }

        //Physics-----------------------------------------------

        //Pre: Whichever player requested a jump
        //Post: Moves player vertically as well as reseting jump values
        //Desc: Tracks collision with both floors and simulates a jump movement
        private void JumpGravity(int Player)
        {
            //Simulated jumping
            if (Jump[Player] == true)
            {
                PlayerRec[Player].Y = PlayerRec[Player].Y - JumpVelocity[Player];
                PlayerBounds[Player].Y = PlayerBounds[Player].Y - JumpVelocity[Player];
                AttackBound[Player].Y = AttackBound[Player].Y - JumpVelocity[Player];

                //Moves player in a parabola shape
                JumpVelocity[Player] = JumpVelocity[Player] - 1;

                //Cancles jump once they land on a surface
                if (GroundCollision(PlayerBounds[Player]) == HitCollision)
                {
                    Jump[Player] = false; 
                    JumpVelocity[Player] = MaxVelocity;
                    JumpCooldown[Player] = 15;
                }
                else if (PlayerBounds[Player].Bottom <= FloorBounds[1].Top  && JumpVelocity[Player] <= 0)              
                {
                    if (PlatformCollision(PlayerBounds[Player]) == HitCollision)
                    {
                        Jump[Player] = false;
                        JumpVelocity[Player] = MaxVelocity;
                        JumpCooldown[Player] = 15;
                        WasOnPlatform[Player] = true;
                    }
                }
            }
            else
            {
                JumpCooldown[Player] = JumpCooldown[Player] - 1;

                if (PlatformCollision(PlayerBounds[Player]) != HitCollision)
                {
                    WasOnPlatform[Player] = false;
                }
            }
        }

        //Pre: Whichever player is falling
        //Post: Slowly drops player
        //Desc: Simulates gravity until the player hits a ground
        private void FallGravity(int Player)
        {
            //Force players to fall off platform if they walk off
            if (Jump[Player] == false && WasOnPlatform[Player] != true)
            {
                PlayerRec[Player].Y = PlayerRec[Player].Y + 10;
                PlayerBounds[Player].Y = PlayerBounds[Player].Y + 10;
                AttackBound[Player].Y = AttackBound[Player].Y + 10;
            }
            
            //Make sure they are on ground level
            if (GroundCollision(PlayerBounds[Player]) == HitCollision)
            {
                PlayerBounds[Player].Y = 520;
                PlayerRec[Player].Y = ScreenH - 270;
                AttackBound[Player].Y = ScreenH - 200;
            }
        }

        //Pre: Whichever projectile is on screen
        //Post: Sends projectile flying across the screen
        //Desc: Moves the projectile in a specific
        private void Projectile(int Player)
        {
            //Checks if the projectile is on screen
            if (RangeBound[Player].X > 0 && RangeBound[Player].X < ScreenW)
            {
                //Filters out which direction it's flying
                if (Direction[Player] == 1)
                {
                    RangeBound[Player].X = RangeBound[Player].X + ProjectileSpeed;
                }
                else
                {
                    RangeBound[Player].X = RangeBound[Player].X - ProjectileSpeed;
                }
            }
            else
            {
                //Teleport it off screen for later use
                RangeBound[Player] = new Rectangle(-50, -50, RangeW, RangeAttack.Height);
            }
        }

        //Pre: Whichever power up is falling down
        //Post: Slowly drops power ups which spawn
        //Desc: Simulates gravity for power ups
        private void PowerUpGravity()
        {
            //Slowly let power ups fall
            if (PowerUpRec.Y >= 0 && GroundCollision(PowerUpRec) != HitCollision && PlatformCollision(PowerUpRec) != HitCollision)
            {
                PowerUpRec.Y = PowerUpRec.Y + 5;
            }
        }

        //Collision---------------------------------------------

        //Pre: Either player and the ground
        //Post: Checks whether players are collided with the floor or not
        //Desc: Calculate to see if any of rectangles are touching
        private int GroundCollision(Rectangle Player)
        {
            //Only the top of the ground level needs to be checked
            if (Player.Bottom < FloorBounds[0].Top)
            {
                return NoCollision;
            }
            else
            {
                return HitCollision;
            }     
        }

        //Pre: Either player and the platform
        //Post: Checks whether players are collided with the platform or not
        //Desc: Calculate to see if any of rectangles are touching
        private int PlatformCollision(Rectangle Player)
        {
            //Platforms bottom doesn't need to be checked as players can jump through them
            if (Player.Bottom < FloorBounds[1].Top ||
                Player.Left > FloorBounds[1].Right ||
                Player.Right < FloorBounds[1].Left)
            {
                return NoCollision;
            }
            else
            {
                return HitCollision;
            }
        }

        //Pre: Both player hitboxes
        //Post: Checks if both players are touching or not
        //Desc: Calculate to see if both player rectangles are touching
        private int PlayerOnPlayerCollision(Rectangle PlayerOne, Rectangle PlayerTwo)
        {
            //Must check all sides
            if (PlayerOne.Right < PlayerTwo.Left ||
                PlayerOne.Left > PlayerTwo.Right ||
                PlayerOne.Bottom < PlayerTwo.Top ||
                PlayerOne.Top > PlayerTwo.Bottom)
            {
                return NoCollision;
            }
            else
            {
                return HitCollision;
            }

        }

        //Pre: Attack hitbox of a player and the hitbox of another
        //Post: Checks if the attack hitbox is colliding with the other players hitbox
        //Desc: Calculates to see if both rectangles are touching
        private int AttackCollision(Rectangle Attacker, Rectangle Receiver)
        {
            //Same as PlayerOnPlayerCollision, diffrent purposes
            if (Attacker.Right < Receiver.Left ||
                Attacker.Left > Receiver.Right ||
                Attacker.Top > Receiver.Bottom ||
                Attacker.Bottom < Receiver.Top)
            {
                return NoCollision;
            }
            else
            {
                return HitCollision;
            }          
        }

        //Pre: Both players as well as the possible damage 
        //Post: Calculates and displays damage taken by melee attack
        //Desc: Fliter out which player is taking damage and how much
        private void DamageCheck(int PlayerOne, int PlayerTwo, int Damage)
        {
            //Checks if player two was hit
            if (AttackCollision(AttackBound[PlayerOne], PlayerBounds[PlayerTwo]) == HitCollision)
            {
                //Checks if they were blocking beofre inflicting damage
                switch (PlayerTwo)
                {
                    case(0):
                        if (Block[0] == true)
                        {
                            Damage = Damage / 2;

                            if (Sound == true)
                            {
                                BlockMInstance.Play();
                            }
                        }
                        break;
                    case (1):
                        if (Block[1] == true)
                        {
                            Damage = Damage / 2;

                            if (Sound == true)
                            {
                                BlockMInstance.Play();
                            }
                        }
                        break;
                }

                HealthDrawW[PlayerTwo] = HealthDrawW[PlayerTwo] - Damage;

                switch (PlayerTwo)
                {
                    case (1):
                        HealthRec[PlayerTwo] = new Rectangle(765, 50, HealthDrawW[PlayerTwo], HealthDrawH);
                        break;
                    case(0):
                        HealthRec[PlayerTwo] = new Rectangle(105, 50, HealthDrawW[PlayerTwo], HealthDrawH);
                        break;
                }
            }
        }

        //Pre: Projectilce hitboxes
        //Post: Calculates and display damage taken by ranged attack
        //Desc: Fliter out which player is taking damage and how much
        private void RangeCheck() 
        {
            //Only fires projectile if it is off screen
            if (RangeBound[0].X != -50)
            {
                //Teleports it back 
                if (RangeCollision(PlayerBounds[1], new Vector2(RangeBound[0].Center.X, RangeBound[0].Center.Y), RangeBound[0].Width / 2) == HitCollision)
                {
                    RangeBound[0] = new Rectangle(-50, -50, RangeW, RangeAttack.Height);

                    //Checks if the player was blocking beofre inflicting damage
                    if (Block[1] == true)
                    {
                        HealthDrawW[1] = HealthDrawW[1] - (RangeDamage[0]/2);

                        if (Sound == true)
                        {
                            BlockMInstance.Play();
                        }
                    }
                    else
                    {
                        HealthDrawW[1] = HealthDrawW[1] - RangeDamage[0];
                    }
                }
            }

            if (RangeBound[1].X != -50)
            {
                if (RangeCollision(PlayerBounds[0], new Vector2(RangeBound[1].Center.X, RangeBound[1].Center.Y), RangeBound[1].Width / 2) == HitCollision)
                {
                    RangeBound[1] = new Rectangle(-50, -50, RangeW, RangeAttack.Height);

                    if (Block[0] == true)
                    {
                        HealthDrawW[0] = HealthDrawW[0] - (RangeDamage[1]/2);

                        if (Sound == true)
                        {
                            BlockMInstance.Play();
                        }
                    }
                    else
                    {
                        HealthDrawW[0] = HealthDrawW[0] - RangeDamage[1];
                    }
                }
            }

            HealthRec[1] = new Rectangle(776, 50, HealthDrawW[1], HealthDrawH);

            HealthRec[0] = new Rectangle(105, 50, HealthDrawW[0], HealthDrawH);

        }

        //Pre: Either player hitbox as well as the others ranged projectile dimensions
        //Post: Checks if the circular hitbox collides with the opponents hitbox
        //Desc: Calculates to see if the rectangle and circle are touching
        private int RangeCollision(Rectangle Player, Vector2 RangeCenter, int RangeRadius)
        {
            int Result = NoCollision;

            //Define radius
            Vector2 PlayerCenter = new Vector2(Player.Center.X, Player.Center.Y);
            float RangeR = Vector2.Distance(PlayerCenter, new Vector2(Player.X, Player.Y));

            //Checks if the radius of the circle if colliding with the rectangle of the player
            if ((Player.Left - RangeRadius) <= RangeCenter.X && (Player.Right + RangeRadius) >= RangeCenter.X &&
                (Player.Top - RangeRadius) <= RangeCenter.Y && (Player.Bottom + RangeRadius) >= RangeCenter.Y)
            {
                Result = HitCollision;
            }
            else
            {
                if (Vector2.Distance(new Vector2(Player.X, Player.Y), RangeCenter) <= RangeRadius ||
                    Vector2.Distance(new Vector2(Player.X + Player.Width, Player.Y), RangeCenter) <= RangeRadius ||
                    Vector2.Distance(new Vector2(Player.X + Player.Width, Player.Y + Player.Height), RangeCenter) <= RangeRadius ||
                    Vector2.Distance(new Vector2(Player.X, Player.Y + Player.Height), RangeCenter) <= RangeRadius)
                    {
                        Result = HitCollision;
                    }    
            }

            return Result;
        }

        //Power Up---------------------------------------------

        //Pre: Statue of the power up
        //Post: Spawns power up at a random X location
        //Desc: Everytime the timer is meet, checks if there is a power up on the screen, if not it spawns one
        private void SpawnRate()
        {
            RateCounter++;

            //Checks about every 15 secs
            if (RateCounter % 900 == 0)
            {
                if (PowerUpRec.Y < 0)
                {
                    //Selects a random power up if none are on screen
                    PowerUpNumber = rng.Next(1, 6);

                    switch (PowerUpNumber)
                    {
                        case (1):
                            PowerUpFrameNumber = 9;
                            NewPowerUpY = PowerUpH * 0;
                            break;
                        case (2):
                            PowerUpFrameNumber = 14;
                            NewPowerUpY = PowerUpH * 1;
                            break;
                        case (3):
                            PowerUpFrameNumber = 18;
                            NewPowerUpY = PowerUpH * 2;
                            break;
                        case (4):
                            PowerUpFrameNumber = 12;
                            NewPowerUpY = PowerUpH * 3;
                            break;
                        default:
                            PowerUpFrameNumber = 15;
                            NewPowerUpY = PowerUpH * 4;
                            break;
                    }

                    //Teleport it so it is ready to fall
                    PowerUpRec = new Rectangle(10 + rng.Next(20, 1100), 0, PowerUpW, PowerUpH);
                }
            }
        }

        //Pre: Player to "grab" the power up
        //Post: Activates the power up which was picked up
        //Desc: Fliters out which power up was picked up
        private void ActivePowerUp()
        {
            //Checks which player touches the power up first
            for (i = 0; i < 2; i++)
            {
                if (PlayerOnPlayerCollision(PlayerBounds[i], PowerUpRec) == HitCollision)
                {
                    RateCounter = 0;
                    PowerUpRec.Y = PowerUpRec.Y = -100;

                    //Fliters out whihc power up was picked up
                    switch (PowerUpNumber)
                    {
                        //Removes health
                        case(1):
                            HealthDrawW[i] = HealthDrawW[i] - 100;
                            break;
                        //Heals
                        case(2):
                            HealthDrawW[i] = HealthDrawW[i] + 100;

                            if (HealthDrawW[i] > HealthCap[i])
                            {
                                HealthDrawW[i] = HealthCap[i];
                            }
                            break;
                        //Restore mana
                        case(3):
                            ManaDrawW[i] = ManaDrawW[i] + 200;

                            if (ManaDrawW[i] > ManaCap[i])
                            {
                                ManaDrawW[i] = ManaCap[i];
                            }
                            break;
                        //Sets variables for another part of the subprogram
                        case(4):
                            ActiveP = 1;
                            PlayerP = i;
                            PDuration = 0;
                            break;
                        //Sets variables for another part of the subprogram
                        case(5):
                            ActiveP = 2;
                            PlayerP = i;
                            PDuration = 0;
                            break;
                    }
                }
            }

            //Revalue the movespeed
            if (ActiveP == 0)
            {
                MovementSpeed[PlayerP] = MoveCap[PlayerP];
            }
            else
            {
                PDuration++;

                //About 4 secs
                if (PDuration < 175)
                {
                    //Slows player down
                    if (ActiveP == 1)
                    {
                        MovementSpeed[PlayerP] = 4;
                    }
                    //Speeds player up
                    else if (ActiveP == 2)
                    {
                        MovementSpeed[PlayerP] = 12;
                    }
                }
                else
                {
                    //Revalue the power up ready for next use
                    ActiveP = 0;
                }
            }
        }
        
        //Particle System--------------------------------------

        //Pre: Whichever projectile is on screen
        //Post: Spawn random particles which tails projectiles
        //Desc: Uses if statement to check the direction of the range attack and spawns particles accordingly
        private void ParticleSpawn()
        {
            for (i = 0; i < 2; i++)
            {
                if (RangeBound[i].Y > 0)
                {
                    //Spawns 10 particles for each projectile
                    for (j = 0 + i * 10; j < 10 * (i + 1); j++)
                    {
                        //Checks for the direction of the projectile
                        if (Direction[i] == 1)
                        {
                            //Creates an arrow like tail
                            RangeParticle[j].X = RangeBound[i].X - rng.Next(1, 100);

                            if (RangeParticle[j].X < RangeBound[i].X - 50)
                            {
                                RangeParticle[j].Y = RangeBound[i].Y + rng.Next(30, 60);
                            }
                            else
                            {
                                RangeParticle[j].Y = RangeBound[i].Y + rng.Next(5, 90);
                            }

                            RangeParticle[j] = new Rectangle(RangeParticle[j].X, RangeParticle[j].Y, CircleParticle.Width, CircleParticle.Height/2);
                        }
                        else
                        {
                            //Creates an arrrow like tall
                            RangeParticle[j].X = RangeBound[i].X + 170 - rng.Next(1, 100);

                            if (RangeParticle[j].X < RangeBound[i].X + 120)
                            {
                                RangeParticle[j].Y = RangeBound[i].Y + rng.Next(5, 90);
                            }
                            else
                            {
                                RangeParticle[j].Y = RangeBound[i].Y + rng.Next(30, 60);
                            }

                            RangeParticle[j] = new Rectangle(RangeParticle[j].X, RangeParticle[j].Y, CircleParticle.Width, CircleParticle.Height/2);
                        }
                        //Fliter which color scheme should be used
                        switch (PlayerFrameWide[i])
                        {
                            case (13):
                                FillColor = RangeColor[rng.Next(0, 2)];
                                break;
                            case(15):
                                FillColor = RangeColor[rng.Next(4, 6)];
                                break;
                            default:
                                FillColor = RangeColor[rng.Next(2, 4)];
                                break;
                        }

                        spriteBatch.Draw(CircleParticle, RangeParticle[j], FillColor);
                    }
                }
            }
        }
    }
}