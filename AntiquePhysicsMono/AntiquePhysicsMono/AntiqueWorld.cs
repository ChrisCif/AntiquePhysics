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
        private Tile[,] tiles;
        private int tileSize;

        private List<Body> bodies { get; }
        private List<RigidBody> rbodies { get; }

        // Forces
        private Vector2 gravity { get; set; }
        private Vector2 wind { get; set; }
        
        public AntiqueWorld()
        {
            bodies = new List<Body>();
            rbodies = new List<RigidBody>();
            map = new Rectangle(0, 0, 800, 480);
            tiles = new Tile[800 / tileSize, 480 / tileSize];
            PopuateTiles();
            gravity = new Vector2(0f, 2.0f);
            wind = new Vector2(0f, 0f);
        }
        public AntiqueWorld(float gravForce, float windForce)
        {
            bodies = new List<Body>();
            rbodies = new List<RigidBody>();
            map = new Rectangle(0, 0, 800, 480);
            PopuateTiles();
            gravity = new Vector2(0f, gravForce);
            wind = new Vector2(windForce, 0.0f);
        }

        private void PopuateTiles()
        {
            for(int c = 0; c < tiles.GetLength(0); c++)
            {
                for(int r = 0; r < tiles.GetLength(1); r++)
                {
                    tiles[c, r] = new Tile(c * tileSize, r * tileSize, tileSize, false, false);
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

                    intrsctTiles[c, r] = tiles[(box.Left / tileSize) + c, (box.Top / tileSize) + r];
                    
                }
            }

            return intrsctTiles;

        }

        public void AddBody(Body bod)
        {
            bodies.Add(bod);
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
                rbod.Collide();

            });

        }

    }
}
