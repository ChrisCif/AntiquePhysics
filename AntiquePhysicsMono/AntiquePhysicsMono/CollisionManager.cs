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

            // Move back out
            bodA.Move(-bodA.GetVelocity());

            // Shorten bodA to a point (pointA)
            var pointA = new Vector2(bodA.GetBox().Center.X, bodA.GetBox().Center.Y);

            // Expand bodB by extents of bodA (rectB)
            var rectB = new Rectangle(new Point(bodB.GetBox().Left - (bodA.GetBox().Width / 2), bodB.GetBox().Top - (bodA.GetBox().Height / 2)),
                new Point(bodB.GetBox().Width + bodA.GetBox().Width, bodB.GetBox().Height + bodA.GetBox().Height));

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
            var proj = minkowski.Project(bodA.GetVelocity());

            // Resolve velocity
            if(proj.Length() <= bodA.GetVelocity().Length())
            {

                // TODO: Can apply friction here
                var friction = 0.9f;    // Change to body's friction

                if (changeX)
                    bodA.SetVelocity(new Vector2(
                        proj.X,
                        bodA.GetVelocity().Y * friction
                    ));
                else
                    bodA.SetVelocity(new Vector2(
                        bodA.GetVelocity().X * friction,
                        proj.Y
                    ));

            }

            // Move body
            bodA.Move(bodA.GetVelocity());
            
        }

        public bool RectIsColliding(Rectangle rectA, Rectangle rectB)
        {

            return (rectA.Left < rectB.Right &&
                        rectA.Right > rectB.Left &&
                        rectA.Top < rectB.Bottom &&
                        rectA.Bottom > rectB.Top);

        }
        public bool RectIsColliding(Body bodA, Body bodB)
        {

            return RectIsColliding(bodA.GetBox(), bodB.GetBox());

        }

        public bool CircleIsColliding(Body bodA, Body bodB)
        {

            Circle circleA = bodA.GetCircle();
            Circle circleB = bodB.GetCircle();

            float diff = (circleA.GetCenter() - circleB.GetCenter()).Length();
            float sum = (circleA.GetRadius() + circleB.GetRadius());

            return (diff < sum);

        }

        #region Helper Functions

        public Vector2 GetDistance(Body bodA, Body bodB)
        {

            // bodA -> bodB

            Vector2 distance;

            var xDistance = bodA.GetBox().Center.X - bodB.GetBox().Center.X;
            var yDistance = bodA.GetBox().Center.Y - bodB.GetBox().Center.Y;

            distance = new Vector2(xDistance, yDistance);

            return distance;

        }

        #endregion


    }

}
