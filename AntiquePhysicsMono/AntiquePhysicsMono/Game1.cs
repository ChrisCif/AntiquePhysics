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
        PhysicsWorld myWorld;
        Body testBod;
        Body testBlock;
        bool airborne = true;
        VectorControl debugVector;
        Texture2D testBodTex;
        Texture2D testBlockTex;
        Texture2D debugT;
        SpriteFont font;
        MapBuilder mapBuilder = new MapBuilder();

        //CollisionManager collisionManager = new CollisionManager();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {
            
            myWorld = new PhysicsWorld(0.0f);

            // Character
            testBod = new Body(new Rectangle(500, 200, 40, 40), 1.0f);
            myWorld.AddSolidRigidBody(testBod);

            // Test Block
            testBlock = new Body(new Rectangle(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2, 40, 40), 1.0f);
            myWorld.AddSolidBody(testBlock);

            // Debug Point
            debugVector = new VectorControl(testBod.Box);
            
            // Load Map
            /*
            var mapBodies = mapBuilder.BuildFromImage("D:/repo/AntiquePhysics/AntiquePhysicsMono/AntiquePhysicsMono/Content/maps/testMap.png");
            foreach (Body mapBod in mapBodies)
                myWorld.AddSolidBody(mapBod);
            */
            
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
            var jumpSpeed = 30.0f;
            var movementSpeed = 0.85f;

            // Up
            /*
            if(ButtonHeld(PlayerIndex.One, Buttons.DPadUp)  || KeyHeld(Keys.Up))
            {
                if (!airborne)
                {
                    testBod.EnactForce(new Vector2(0.0f, -(jumpSpeed)));
                    airborne = true;
                }
            }
            else
            {
                airborne = false;
            }
            */
            if (ButtonHeld(PlayerIndex.One, Buttons.DPadUp) || KeyHeld(Keys.Up))
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

            debugVector.Update();

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            
            // Draw Blocks
            //spriteBatch.Draw(testBlockTex, testBlock.Box, Color.White);    // TODO: This will draw a block under the character
            foreach(Body bod in myWorld.MasterBodies)
                spriteBatch.Draw(testBlockTex, bod.Box, Color.White);

            // Draw Character
            spriteBatch.Draw(testBlockTex, testBod.Box, Color.Black);

            // Debug
            var vecPosition = new Vector2(testBod.Box.Center.X, testBod.Box.Center.Y);

            spriteBatch.DrawString(font, "Body Velocity: " + testBod.Velocity, Vector2.Zero, Color.Yellow);
            aSpriteBatch.DrawVector(vecPosition, testBod.Velocity, (testBod.Velocity.Length() * 5), Color.Red);   // Draw velocity

            //var distance = (new Vector2(testBlock.Box.Center.X, testBlock.Box.Center.Y) - vecPosition);
            //var perp = Vector2.Normalize(new Vector2(distance.Y, -distance.X)) * 100;
            //var testVec = debugVector.GetPoint();
            //aSpriteBatch.DrawVector(vecPosition, new Vector2(testBlock.Box.Center.X, testBlock.Box.Center.Y), Color.Green); // Draw distance
            //aSpriteBatch.DrawVector((Vector2.Zero + vecPosition), (vecPosition + testVec), Color.Yellow); // Draw test vector
            //aSpriteBatch.DrawVector((Vector2.Zero + vecPosition), (vecPosition + perp), Color.Black);  // Draw perpendicular line
            //aSpriteBatch.DrawProjection(testBod.Velocity/*testVec*/, distance/*projBase*/, new Vector2(testBod.Box.Center.X, testBod.Box.Center.Y), Color.White); // Draw projection

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
