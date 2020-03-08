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

    class CollisionManager
    {

        public void RectCheckCollisions(Body bodA, Body bodB)
        {

            if (RectIsColliding(bodA, bodB))
                RectCorrectCollision(bodA, bodB);

        }
        public void RectCheckCollisions(List<Body> bodies)
        {

            for(int i = 0; i < bodies.Count; i++)
            {
                for(int j = 0; j < bodies.Count; j++)
                {

                    if(i != j)
                    {

                        var bodA = bodies.ElementAt(i);
                        var bodB = bodies.ElementAt(j);
                        
                        // Check and correct collisions
                        RectCheckCollisions(bodA, bodB);

                    }
                    
                }
            }

        }

        public void RectCorrectCollision(Body bodA, Body bodB)
        {

            bool oneStationary = (bodA.Velocity.Length() != bodB.Velocity.Length());
            bool bothStationary = (bodA.Velocity.Length() == 0 && bodA.Velocity.Length() == 0);

            if (oneStationary)
            {

                // Get moving velocity
                Body moving = bodA.Velocity.Length() != 0 ? bodA : bodB;
                Body stationary = bodA.Velocity.Length() != 0 ? bodB : bodA;
                Vector2 originalV = moving.Velocity;

                // Get overlapping rectangle
                Rectangle overlap = Rectangle.Intersect(bodA.Box, bodB.Box);

                // Get distance vector
                Vector2 distance = new Vector2((moving.Box.Center.X - stationary.Box.Center.X), (moving.Box.Center.Y - stationary.Box.Center.Y));

                // Get overlap slope
                float ovSlope = Math.Abs(overlap.Height / overlap.Width);

                // Compare slopes
                float correctionX;
                float correctionY;
                if (Math.Abs(originalV.Y / originalV.X) > ovSlope)
                {

                    // Push Vertically
                    correctionY = overlap.Height * (stationary.Box.Center.Y > moving.Box.Center.Y ? -1 : 1);
                    correctionX = 0f;
                    moving.Velocity = new Vector2(originalV.X, 0.0f);

                }
                else
                {

                    // Push Horizontally
                    correctionX = overlap.Width * (stationary.Box.Center.X > moving.Box.Center.X ? -1 : 1);
                    correctionY = 0f;
                    moving.Velocity = new Vector2(0.0f, originalV.Y);

                }
                
                moving.Move(new Vector2(correctionX, correctionY));

            }
            else if (bothStationary)
            {
                return;
            }

        }

        /*
        public void RectCorrectCollision(Body bodA, Body bodB)
        {

            // Move back out
            bodA.Move(-bodA.Velocity);

            // Shorten bodA to a point (pointA)
            var pointA = new Vector2(bodA.Box.Center.X, bodA.Box.Center.Y);

            // Expand bodB by extents of bodA (rectB)
            var rectB = new Rectangle(new Point(bodB.Box.Left - (bodA.Box.Width / 2), bodB.Box.Top - (bodA.Box.Height / 2)),
                new Point(bodB.Box.Width + bodA.Box.Width, bodB.Box.Height + bodA.Box.Height));

            // Get minkowski/maDistance
            Vector2 minkowski;
            float maDistance;
            var xAxis = GetDistance(bodB, bodA).X;
            var yAxis = GetDistance(bodB, bodA).Y;
            bool changeX;
            if (Math.Abs(xAxis) > Math.Abs(yAxis))
            {

                maDistance = xAxis;
                maDistance -= (rectB.Width / 2) * (maDistance / Math.Abs(maDistance));

                minkowski = new Vector2(maDistance, 0.0f);
                changeX = true;
                
            }
            else
            {

                maDistance = yAxis;
                maDistance -= (rectB.Height / 2) * (maDistance / Math.Abs(maDistance));

                minkowski = new Vector2(0.0f, maDistance);
                changeX = false;
                
            }

            // Project onto velocity
            var proj = minkowski.Project(bodA.Velocity);

            // Resolve velocity
            if(proj.Length() <= bodA.Velocity.Length())
            {

                // TODO: Can apply friction here
                var friction = 0.9f;    // Change to body's friction

                if (changeX)
                    bodA.Velocity = new Vector2(proj.X, bodA.Velocity.Y * friction);
                else
                    bodA.Velocity = new Vector2(bodA.Velocity.X * friction, proj.Y);

            }

            // Move body
            bodA.Move(bodA.Velocity);
            
        }
        */

        public bool RectIsColliding(Rectangle rectA, Rectangle rectB)
        {

            return (rectA.Left < rectB.Right &&
                        rectA.Right > rectB.Left &&
                        rectA.Top < rectB.Bottom &&
                        rectA.Bottom > rectB.Top);

        }
        public bool RectIsColliding(Body bodA, Body bodB)
        {

            return RectIsColliding(bodA.Box, bodB.Box);

        }

        public bool CircleIsColliding(Body bodA, Body bodB)
        {

            Circle circleA = bodA.Circle;
            Circle circleB = bodB.Circle;

            float diff = (circleA.GetCenter() - circleB.GetCenter()).Length();
            float sum = (circleA.GetRadius() + circleB.GetRadius());

            return (diff < sum);

        }

        #region Helper Functions

        public Vector2 GetDistance(Body bodA, Body bodB)
        {

            // bodA -> bodB

            Vector2 distance;

            var xDistance = bodA.Box.Center.X - bodB.Box.Center.X;
            var yDistance = bodA.Box.Center.Y - bodB.Box.Center.Y;

            distance = new Vector2(xDistance, yDistance);

            return distance;

        }

        #endregion


    }

}
