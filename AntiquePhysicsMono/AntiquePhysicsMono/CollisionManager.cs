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

        public void RectCheckCollisions(List<Body> bodies)
        {

            for(int i = 0; i < bodies.Count; i++)
            {
                for(int j = 0; j < bodies.Count; j++)
                {

                    if(i != j)
                    {
                        if(RectIsColliding(bodies.ElementAt(i), bodies.ElementAt(j)))
                        {

                            // TODO: Correct collisions

                        }
                    }
                    
                }
            }

        }

        public void RectCorrectCollision(Body bodA, Body bodB)
        {



        }

        public bool RectIsColliding(Body bodA, Body bodB)
        {

            Rectangle boxA = bodA.GetBox();
            Rectangle boxB = bodB.GetBox();

            return (boxA.Left < boxB.Right &&
                        boxA.Right > boxB.Left &&
                        boxA.Top < boxB.Bottom &&
                        boxA.Bottom > boxB.Top);

        }

        public bool CircleIsColliding(Body bodA, Body bodB)
        {

            Circle circleA = bodA.GetCircle();
            Circle circleB = bodB.GetCircle();

            float diff = (circleA.GetCenter() - circleB.GetCenter()).Length();
            float sum = (circleA.GetRadius() + circleB.GetRadius());

            return (diff < sum);

        }

    }

}
