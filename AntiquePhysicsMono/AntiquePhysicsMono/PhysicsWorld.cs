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
        protected float airResistance = 0.75f;   // Default

        // Do colision detection on these
        protected List<Body> rigidBodies = new List<Body>();

        // Enact forces on these
        protected List<Body> forceBodies = new List<Body>();

        // Keep track of these, but don't do forces or check their collisions
        protected List<Body> ghostBodies = new List<Body>();
        
        // Constructor
        public PhysicsWorld(float gravity, float airResistance)
        {
            this.gravity = gravity;
            this.airResistance = airResistance;
        }

        public void WorldForces(Body body)
        {

            // Gravity
            body.EnactForce(new Vector2(0.0f, gravity));

        }

        // If bodies are using rectangle collisions
        public bool RectCollides(Body bodA, Body bodB)
        {

            Rectangle boxA = bodA.GetBox();
            Rectangle boxB = bodB.GetBox();

            return (boxA.Left < boxB.Right &&
                        boxA.Right > boxB.Left &&
                        boxA.Top < boxB.Bottom &&
                        boxA.Bottom > boxB.Top);

        }

        // If bodies are using circular collisions
        public bool CircleCollides(Body bodA, Body bodB)
        {

            Circle circleA = bodA.GetCircle();
            Circle circleB = bodB.GetCircle();

            float diff = (circleA.GetCenter() - circleB.GetCenter()).Length();
            float sum = (circleA.GetRadius() + circleB.GetRadius());

            return (diff < sum);

        }

    }
}
