using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AntiquePhysicsMono
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        AntiqueWorld myWorld;
        RigidBody testBod;
        Texture2D testChar;
        RigidBody testBlock;
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
            
            myWorld = new AntiqueWorld(0.25f, 0.0f);

            // Character
            testBod = new RigidBody(new Rectangle(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferWidth / 2, 40, 40), false, false);
            myWorld.AddRigidBody(testBod);

            // Block
            testBlock = new RigidBody(new Rectangle(200, 200, 10, 10), true, false);
            myWorld.AddBody(testBlock);

            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            
            spriteBatch = new SpriteBatch(GraphicsDevice);

            testChar = Content.Load<Texture2D>("danny");
            testBlockTex = Content.Load<Texture2D>("smwBlock");
            debugT = Content.Load<Texture2D>("debugTexture");
            font = Content.Load<SpriteFont>("myFont");
            
        }
        
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        public bool ButtonHeld(PlayerIndex pindex, Buttons button)
        {
            return GamePad.GetState(pindex).IsButtonDown(button);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (ButtonHeld(PlayerIndex.One, Buttons.A)  ||  Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                testBod.EnactForce(new Vector2(0.0f, -1.0f));
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                testBod.EnactForce(new Vector2(-.5f, 0f));
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                testBod.EnactForce(new Vector2(0.5f, 0f));
            testBod.EnactForce(new Vector2(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X, 0.0f));

            myWorld.Update();

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            // Draw Character
            spriteBatch.Draw(testChar, testBod.GetBox(), Color.White);

            // Draw Tile
            var myColor = Color.White;
            if (testBlock.GetBox().Intersects(testBod.BoundRectangle()))
                myColor = Color.Red;

            spriteBatch.Draw(testBlockTex, testBlock.GetBox(), myColor);

            spriteBatch.DrawString(font, "" + testBod.GetMasterForce(), Vector2.Zero, Color.White);

            // Debug
            spriteBatch.Draw(debugT, testBod.BoundRectangle(), Color.White * 0.3f);
            spriteBatch.Draw(debugT, testBod.GetBox(), Color.Blue * 0.3f);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
