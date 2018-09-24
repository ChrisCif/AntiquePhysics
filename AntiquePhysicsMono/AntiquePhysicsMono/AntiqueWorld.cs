using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AntiquePhysicsMono
{
    class AntiqueWorld
    {

        private Rectangle map { get; }
        private int tileSize;
        private Tile[,] tiles;

        private List<Body> bodies { get; }
        private List<RigidBody> rbodies { get; }

        // Forces
        private Vector2 gravity { get; set; }
        private Vector2 wind { get; set; }
        
        public AntiqueWorld()
        {

            this.bodies = new List<Body>();
            this.rbodies = new List<RigidBody>();
            this.map = new Rectangle(0, 0, 800, 480);
            this.tileSize = 10;
            this.tiles = new Tile[800 / tileSize, 480 / tileSize];
            this.GenerateTiles();
            this.gravity = new Vector2(0.0f, 2.0f);
            this.wind = new Vector2(0.0f, 0.0f);

        }
        public AntiqueWorld(float gravForce, float windForce)
        {

            this.bodies = new List<Body>();
            this.rbodies = new List<RigidBody>();
            this.map = new Rectangle(0, 0, 800, 480);
            this.tileSize = 10;
            this.tiles = new Tile[800 / tileSize, 480 / tileSize];
            this.GenerateTiles();
            this.gravity = new Vector2(0f, gravForce);
            this.wind = new Vector2(windForce, 0.0f);

        }
        public AntiqueWorld(float gravForce, float windForce, int tileSize)
        {

            this.bodies = new List<Body>();
            this.rbodies = new List<RigidBody>();
            this.map = new Rectangle(0, 0, 800, 480);
            this.tileSize = tileSize;
            this.tiles = new Tile[800 / tileSize, 480 / tileSize];
            this.GenerateTiles();
            this.gravity = new Vector2(0f, gravForce);
            this.wind = new Vector2(windForce, 0.0f);

        }

        protected void GenerateTiles()
        {
            for(int c = 0; c < tiles.GetLength(0); c++)
            {
                for(int r = 0; r < tiles.GetLength(1); r++)
                {
                    tiles[c, r] = new Tile(c * tileSize, r * tileSize, tileSize, null);
                }
            }
        }

        public Tile[,] GetIntrsctTiles(Rectangle box)
        {

            var boundWidth = ((box.Right - box.Left) / tileSize) + 1;
            var boundHeight = ((box.Bottom - box.Top) / tileSize) + 1;
            Tile[,] intrsctTiles = new Tile[boundWidth, boundHeight];
            for(int c = 0; c < boundWidth; c++)
            {
                for(int r = 0; r < boundHeight; r++)
                {

                    // TODO: Index out of bounds
                    intrsctTiles[c, r] = tiles[(box.Left / tileSize) + c, (box.Top / tileSize) + r];
                    
                }
            }

            return intrsctTiles;

        }

        public void AddBody(Body bod)
        {

            bodies.Add(bod);
            var tile = tiles[bod.GetBox().Left / tileSize, bod.GetBox().Top / tileSize];
            tile.Fill(bod);

        }
        public void AddRigidBody(RigidBody rbod)
        {
            rbodies.Add(rbod);
            AddBody(rbod);
        }

        public void Update()
        {

            WorldForces();
            Collisions();

            Parallel.ForEach(bodies, (bod) => {

                bod.Update();

            });

        }

        public void WorldForces()
        {

            Parallel.ForEach(rbodies, (rbod) =>
            {

                rbod.EnactForce(gravity);
                rbod.EnactForce(wind);

            });

        }

        public void Collisions()
        {

            Parallel.ForEach(rbodies, (rbod) =>
            {

                // TODO: Collisions
                rbod.Collide(this);

            });

        }

    }
}
