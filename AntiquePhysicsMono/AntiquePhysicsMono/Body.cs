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

        // Debug


        // Bounds
        public Rectangle Box { get; set; }
        public Circle Circle { get; set; }

        // Physics
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public float Mass { get; set; }

        private const float MAX_VELOCITY = 10.0f;

        //protected bool isSolid; // Whether or not collisions are calculated for this body
        //protected bool interactable;    // Whether or not collisions are allowed with creatures

        // Debug
        //public bool isIntr;

        public Body(Rectangle box/*, bool isSolid, bool interactable*/, float mass)
        {
            this.Box = box;
            /*
            this.isSolid = isSolid;
            this.interactable = interactable;
            */
            this.Mass = mass;
        }
        /*
        public Body(int x, int y, int width, int height, bool isSolid, bool interactable)
        {
            this.Box = new Rectangle(x, y, width, height);
            this.isSolid = isSolid;
            this.interactable = interactable;
        }
        */
        public Body(Circle circle, float mass)
        {
            this.Circle = circle;
            this.Mass = mass;
        }
        
        public void EnactForce(Vector2 force)
        {

            this.Acceleration += force / this.Mass;

        }
        public void Accelerate()
        {

            // Accelerate
            this.Velocity += this.Acceleration;

            // Clamp velocity
            this.Velocity = ClampVelocity(this.Velocity);

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
        public void Decelerate(float factor)
        {

            this.Velocity *= factor;

        }
        public void Move(Vector2 trans)
        {

            // Offset the bounding box
            this.Box.Offset(trans);

        }

        public void Update(float resistance)
        {
            
            // Accelerate
            Accelerate();
            
            // Move
            Move(this.Velocity);
            
            // Reset acceleration
            this.Acceleration = Vector2.Zero;

            // Resistance
            Decelerate(resistance);

        }

    }
}
