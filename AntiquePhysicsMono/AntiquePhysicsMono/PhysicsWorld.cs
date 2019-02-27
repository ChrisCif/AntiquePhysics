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
    class PhysicsWorld
    {

        protected float gravity = 2.0f; // Default
        protected float airResistance = 0.9f;   // Default
        
        // Do colision detection on these
        protected List<Body> solidBodies = new List<Body>();
        public List<Body> SolidBodies
        {
            get
            {
                return solidBodies;
            }
        }

        // Enact forces on these
        protected List<Body> rigidBodies = new List<Body>();
        public List<Body> RigidBodies
        {
            get
            {
                return rigidBodies;
            }
        }

        // Keep track of these no matter what
        protected List<Body> masterBodies = new List<Body>();
        public List<Body> MasterBodies
        {
            get
            {
                return this.masterBodies;
            }
        }

        private CollisionManager collisionManager = new CollisionManager();

        #region Constructor

        public PhysicsWorld(float gravity)
        {
            this.gravity = gravity;
        }
        public PhysicsWorld(float gravity, float airResistance)
        {
            this.gravity = gravity;
            this.airResistance = airResistance;
        }

        #endregion

        public void WorldForces(Body body)
        {

            // Gravity
            body.EnactForce(new Vector2(0.0f, gravity));

            // TODO: Wind?
            
        }

        #region Add Bodies

        public void AddSolidBody(Body body)
        {
            solidBodies.Add(body);
            masterBodies.Add(body);
        }
        public void AddRigidBody(Body body)
        {
            rigidBodies.Add(body);
            masterBodies.Add(body);
        }
        public void AddGhostBody(Body body)
        {
            masterBodies.Add(body);
        }
        public void AddSolidRigidBody(Body body)
        {
            solidBodies.Add(body);
            rigidBodies.Add(body);
            masterBodies.Add(body);
        }
        // TODO: Maybe some more body adds

        #endregion

        // Updates
        public void Update()
        {

            // Enact forces on bodies
            foreach(Body body in rigidBodies)
            {

                WorldForces(body);

            }
            
            // Update all of the bodies after everything else
            foreach(Body body in masterBodies)
            {

                body.Update(airResistance);

            }

            // Collisions
            collisionManager.RectCheckCollisions(solidBodies);

        }

    }
}
