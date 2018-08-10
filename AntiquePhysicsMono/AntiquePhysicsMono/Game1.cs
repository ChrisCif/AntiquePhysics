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
        SpriteFont font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {

            myWorld = new AntiqueWorld(0.5f, 0.0f);
            testBod = new RigidBody(new Rectangle(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferWidth / 2, 40, 40));
            myWorld.AddBody(testBod);

            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            
            spriteBatch = new SpriteBatch(GraphicsDevice);

            testChar = Content.Load<Texture2D>("devito_emoji");
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

            if (ButtonHeld(PlayerIndex.One, Buttons.A))
            {
                testBod.EnactForce(new Vector2(0.0f, -3.0f));
            }
            testBod.EnactForce(new Vector2(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X, 0.0f));

            myWorld.Update();

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            spriteBatch.Draw(testChar, testBod.GetBox(), Color.White);

            spriteBatch.DrawString(font, "" + testBod.GetMasterForce(), Vector2.Zero, Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
