using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AntiquePhysicsMono
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        AntiqueSpritebatch aSpriteBatch;
        //AntiqueWorld myWorld;
        PhysicsWorld myWorld;
        //RigidBody testBod;
        Body testBod;
        Texture2D testBodTex;
        //RigidBody testBlock;
        Body testBlock;
        Texture2D testBlockTex;
        Texture2D debugT;
        SpriteFont font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {

            //myWorld = new AntiqueWorld(0.25f, 0.0f);
            myWorld = new PhysicsWorld(0.8f);

            // Character
            //testBod = new RigidBody(new Rectangle(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferWidth / 2, 40, 40), false, false);
            testBod = new Body(new Rectangle(400, 200, 40, 40), 1.0f);
            myWorld.AddRigidForceBody(testBod);

            // Block
            //testBlock = new RigidBody(new Rectangle(200, 200, 10, 10), true, false);
            testBlock = new Body(new Rectangle(200, 200, 10, 10), 1.0f);
            myWorld.AddRigidBody(testBlock);

            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            
            spriteBatch = new SpriteBatch(GraphicsDevice);
            aSpriteBatch = new AntiqueSpritebatch(spriteBatch);

            testBodTex = Content.Load<Texture2D>("danny");
            testBlockTex = Content.Load<Texture2D>("smwBlock");
            debugT = Content.Load<Texture2D>("debugTexture");
            font = Content.Load<SpriteFont>("myFont");

            aSpriteBatch.SetTexture(debugT);

        }
        
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        public bool ButtonHeld(PlayerIndex pindex, Buttons button)
        {
            return GamePad.GetState(pindex).IsButtonDown(button);
        }
        public bool KeyHeld(Keys key)
        {
            return Keyboard.GetState().IsKeyDown(key);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            /*
            if (ButtonHeld(PlayerIndex.One, Buttons.A)  ||  Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                testBod.EnactForce(new Vector2(0.0f, -1.0f));
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                testBod.EnactForce(new Vector2(-.5f, 0f));
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                testBod.EnactForce(new Vector2(0.5f, 0f));
            testBod.EnactForce(new Vector2(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X, 0.0f));
            */

            // Up
            if(ButtonHeld(PlayerIndex.One, Buttons.DPadUp)  || KeyHeld(Keys.Up))
            {
                testBod.EnactForce(new Vector2(0.0f, -1.75f));
            }

            // Down
            if (ButtonHeld(PlayerIndex.One, Buttons.DPadDown) || KeyHeld(Keys.Down))
            {
                testBod.EnactForce(new Vector2(0.0f, 1.25f));
            }

            // Right
            if (ButtonHeld(PlayerIndex.One, Buttons.DPadRight) || KeyHeld(Keys.Right))
            {
                testBod.EnactForce(new Vector2(1.25f, 0.0f));
            }

            // Left
            if (ButtonHeld(PlayerIndex.One, Buttons.DPadLeft) || KeyHeld(Keys.Left))
            {
                testBod.EnactForce(new Vector2(-1.25f, 0.0f));
            }

            myWorld.Update();

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            // Draw Character
            spriteBatch.Draw(testBodTex, testBod.GetBox(), Color.White);

            // Draw Block
            spriteBatch.Draw(testBlockTex, testBlock.GetBox(), Color.White);

            // Debug
            spriteBatch.DrawString(font, "Body Velocity: " + testBod.GetVelocity(), Vector2.Zero, Color.Yellow);
            aSpriteBatch.DrawVector(testBod.GetVelocity(), new Vector2(testBod.GetBox().Center.X, testBod.GetBox().Center.Y), testBod.GetVelocity().Length(), Color.Red);   // Draw velocity
            aSpriteBatch.DrawVector(new Vector2(testBod.GetBox().Center.X, testBod.GetBox().Center.Y), new Vector2(testBlock.GetBox().Center.X, testBlock.GetBox().Center.Y), Color.Green); // Draw distance

            /*
            // Draw Tile
            var myColor = Color.White;
            if (testBlock.GetBox().Intersects(testBod.BoundRectangle()))
                myColor = Color.Red;

            spriteBatch.Draw(testBlockTex, testBlock.GetBox(), myColor);

            spriteBatch.DrawString(font, "" + testBod.GetMasterForce(), Vector2.Zero, Color.White);

            // Debug
            spriteBatch.Draw(debugT, testBod.BoundRectangle(), Color.White * 0.3f);
            spriteBatch.Draw(debugT, testBod.GetBox(), Color.Blue * 0.3f);
            */


            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
