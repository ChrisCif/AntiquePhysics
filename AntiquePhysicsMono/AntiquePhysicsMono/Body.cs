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

    public struct Circle
    {
        private Vector2 center;
        private float radius;
        
        public Circle(Vector2 center, float radius)
        {
            this.center = center;
            this.radius = radius;
        }

        public Vector2 GetCenter() { return center; }
        public float GetRadius() { return radius; }

    }

    /*abstract*/ class Body
    {

        // Bounds
        protected Rectangle box;
        protected Circle circle;

        // Physics
        private Vector2 velocity;
        private Vector2 acceleration;
        private float mass;

        private const float MAX_VELOCITY = 10.0f;

        //protected bool isSolid; // Whether or not collisions are calculated for this body
        //protected bool interactable;    // Whether or not collisions are allowed with creatures

        // Debug
        //public bool isIntr;

        public Body(Rectangle box/*, bool isSolid, bool interactable*/, float mass)
        {
            this.box = box;
            /*
            this.isSolid = isSolid;
            this.interactable = interactable;
            */
            this.mass = mass;
        }
        /*
        public Body(int x, int y, int width, int height, bool isSolid, bool interactable)
        {
            this.box = new Rectangle(x, y, width, height);
            this.isSolid = isSolid;
            this.interactable = interactable;
        }
        */
        public Body(Circle circle, float mass)
        {
            this.circle = circle;
            this.mass = mass;
        }

        // Accessors
        public Rectangle GetBox() { return box; }
        public Circle GetCircle() { return circle; }
        public Vector2 GetVelocity() { return velocity; }
        public Vector2 GetAcceleration() { return acceleration; }
        public float GetMass() { return mass; }
        
        public void EnactForce(Vector2 force)
        {

            this.acceleration += force / mass;

        }
        public void Accelerate()
        {

            // Accelerate
            this.velocity += acceleration;

            // Clamp velocity
            this.velocity = ClampVelocity(this.velocity);

        }
        public Vector2 ClampVelocity(Vector2 velocity)
        {

            if (velocity.Length() > MAX_VELOCITY)
            {
                velocity.Normalize();
                velocity *= MAX_VELOCITY;
            }
            
            return velocity;

        }
        public void Move(Vector2 trans)
        {

            // Offset the bounding box
            box.Offset(trans);

        }

        public /*abstract*/ void Update()
        {

            // Accelerate
            Accelerate();

            // Move
            Move(this.velocity);

            // Reset acceleration
            acceleration = Vector2.Zero;

        }

        /*
        public bool IsSolid() { return isSolid; }
        public bool IsInteractable() { return interactable; }
        public bool IsCollidable() { return (isSolid && !interactable); }
        */

    }
}
