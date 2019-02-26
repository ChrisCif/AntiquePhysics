using System;
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
        VectorControl debugVector;
        Texture2D testBodTex;
        //RigidBody testBlock;
        Body testBlock;
        Texture2D testBlockTex;
        Texture2D debugT;
        SpriteFont font;

        CollisionManager testCollision = new CollisionManager();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {

            //myWorld = new AntiqueWorld(0.25f, 0.0f);
            myWorld = new PhysicsWorld(0.0f);

            // Character
            //testBod = new RigidBody(new Rectangle(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferWidth / 2, 40, 40), false, false);
            testBod = new Body(new Rectangle(400, 200, 40, 40), 1.0f);
            myWorld.AddRigidForceBody(testBod);

            // Debug Point
            debugVector = new VectorControl(testBod.GetBox());

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
            
            // Character
            var movementSpeed = 0.85f;

            // Up
            if(ButtonHeld(PlayerIndex.One, Buttons.DPadUp)  || KeyHeld(Keys.Up))
            {
                testBod.EnactForce(new Vector2(0.0f, -movementSpeed));
            }

            // Down
            if (ButtonHeld(PlayerIndex.One, Buttons.DPadDown) || KeyHeld(Keys.Down))
            {
                testBod.EnactForce(new Vector2(0.0f, movementSpeed));
            }

            // Right
            if (ButtonHeld(PlayerIndex.One, Buttons.DPadRight) || KeyHeld(Keys.Right))
            {
                testBod.EnactForce(new Vector2(movementSpeed, 0.0f));
            }

            // Left
            if (ButtonHeld(PlayerIndex.One, Buttons.DPadLeft) || KeyHeld(Keys.Left))
            {
                testBod.EnactForce(new Vector2(-movementSpeed, 0.0f));
            }

            // Debug Vector
            // Up
            if (KeyHeld(Keys.W))
            {
                debugVector.MovePoint(new Vector2(0.0f, -3.0f));
            }

            // Down
            if (KeyHeld(Keys.S))
            {
                debugVector.MovePoint(new Vector2(0.0f, 3.0f));
            }

            // Right
            if (KeyHeld(Keys.D))
            {
                debugVector.MovePoint(new Vector2(3.0f, 0.0f));
            }

            // Left
            if (KeyHeld(Keys.A))
            {
                debugVector.MovePoint(new Vector2(-3.0f, 0.0f));
            }

            // Update
            myWorld.Update();
            testCollision.RectCheckCollisions(testBod, testBlock);

            debugVector.Update();

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            
            // Draw Block
            spriteBatch.Draw(testBlockTex, testBlock.GetBox(), Color.White);

            // Draw Character
            spriteBatch.Draw(testBodTex, testBod.GetBox(), Color.White);

            // Debug
            var vecPosition = new Vector2(testBod.GetBox().Center.X, testBod.GetBox().Center.Y);

            spriteBatch.DrawString(font, "Body Velocity: " + testBod.GetVelocity(), Vector2.Zero, Color.Yellow);
            aSpriteBatch.DrawVector(vecPosition, testBod.GetVelocity(), (testBod.GetVelocity().Length() * 5), Color.Red);   // Draw velocity
            
            var distance = (new Vector2(testBlock.GetBox().Center.X, testBlock.GetBox().Center.Y) - vecPosition);
            var perp = Vector2.Normalize(new Vector2(distance.Y, -distance.X)) * 100;
            var testVec = debugVector.GetPoint();
            aSpriteBatch.DrawVector(vecPosition, new Vector2(testBlock.GetBox().Center.X, testBlock.GetBox().Center.Y), Color.Green); // Draw distance
            //aSpriteBatch.DrawVector((Vector2.Zero + vecPosition), (vecPosition + testVec), Color.Yellow); // Draw test vector
            aSpriteBatch.DrawVector((Vector2.Zero + vecPosition), (vecPosition + perp), Color.Black);  // Draw perpendicular line
            aSpriteBatch.DrawProjection(testBod.GetVelocity()/*testVec*/, distance/*projBase*/, new Vector2(testBod.GetBox().Center.X, testBod.GetBox().Center.Y), Color.White); // Draw projection
            
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
