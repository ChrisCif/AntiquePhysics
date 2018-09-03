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

        private List<Body> bodies { get; }
        private List<RigidBody> rbodies { get; }
        private Rectangle map { get; }
        private Vector2 gravity { get; set; }
        private Vector2 wind { get; set; }
        
        public AntiqueWorld()
        {
            bodies = new List<Body>();
            rbodies = new List<RigidBody>();
            map = new Rectangle(0, 0, 800, 480);
            gravity = new Vector2(0f, 2.0f);
            wind = new Vector2(0f, 0f);
        }
        public AntiqueWorld(float gravForce, float windForce)
        {
            bodies = new List<Body>();
            rbodies = new List<RigidBody>();
            map = new Rectangle(0, 0, 800, 480);
            gravity = new Vector2(0f, gravForce);
            wind = new Vector2(windForce, 0.0f);
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

            });

        }

    }
}
