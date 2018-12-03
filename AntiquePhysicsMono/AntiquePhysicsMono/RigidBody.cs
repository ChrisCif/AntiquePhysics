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
    class RigidBody// : Body
    {

        protected Vector2 masterForce;
        protected const float MAX_FORCE = 10.0f;

        public RigidBody(Rectangle box, bool isSolid, bool interactable)// : base(box, isSolid, interactable)
        {
            masterForce = Vector2.Zero;
        }
        public RigidBody(int x, int y, int width, int height, bool isSolid, bool interactable)// : base(x, y, width, height, isSolid, interactable)
        {
            masterForce = Vector2.Zero;
        }

        public Vector2 GetMasterForce() { return masterForce; }

        public void EnactForce(Vector2 externalForce)
        {

            masterForce += externalForce;

            if (Math.Abs(masterForce.X) > MAX_FORCE)
                masterForce.X = MAX_FORCE * (masterForce.X / Math.Abs(masterForce.X));
            if (Math.Abs(masterForce.Y) > MAX_FORCE)
                masterForce.Y = MAX_FORCE * (masterForce.Y / Math.Abs(masterForce.Y));

        }

        public void Collide(AntiqueWorld world)
        {

            /*
            var intersectedTiles = BoundTiles(world);

            foreach(Tile t in intersectedTiles)
            {

                if (t.GetContent() != null)
                {
                    if (t.GetContent().IsCollidable())
                    {

                        var distance = Minkowski(t);
                        masterForce.Normalize();
                        masterForce = Vector2.Multiply(masterForce, (float)distance);

                    }
                }
                
            }
            */

        }
        /*
        public Rectangle BoundRectangle()
        {

            // Get this location box
            var nowBox = GetBox();

            // Get next location box
            var thenBox = new Rectangle(nowBox.X + (int)GetMasterForce().X, nowBox.Y + (int)GetMasterForce().Y, nowBox.Width, nowBox.Height);

            // Get full movement box
            var boundLeft = Math.Min(nowBox.Left, thenBox.Left);
            var boundTop = Math.Min(nowBox.Top, thenBox.Top);
            var boundRight = Math.Max(nowBox.Right, thenBox.Right);
            var boundBottom = Math.Max(nowBox.Bottom, thenBox.Bottom);
            var boundBox = new Rectangle(boundLeft, boundTop, boundRight - boundLeft, boundBottom - boundTop);

            return boundBox;

        }
        
        private Tile[,] BoundTiles(AntiqueWorld world)
        {

            // Get this location box
            var nowBox = GetBox();

            // Get next location box
            var thenBox = new Rectangle(nowBox.X + (int)GetMasterForce().X, nowBox.Y + (int)GetMasterForce().Y, nowBox.Width, nowBox.Height);

            // Get full movement box
            var boundLeft = Math.Min(nowBox.Left, thenBox.Left);
            var boundTop = Math.Min(nowBox.Top, thenBox.Top);
            var boundRight = Math.Max(nowBox.Right, thenBox.Right);
            var boundBottom = Math.Max(nowBox.Bottom, thenBox.Bottom);
            var boundBox = new Rectangle(boundLeft, boundTop, boundRight - boundLeft, boundBottom - boundTop);

            // Get intersected tiles
            return world.GetIntrsctTiles(boundBox);

        }
        private int Minkowski(Tile tile)
        {

            int distance = 0;

            // Get the AABB
            var tileBox = tile.GetBox();
            var heightExtend = GetBox().Height / 2;
            var widthExtend = GetBox().Width / 2;
            var aabb = new Rectangle(tileBox.X - widthExtend, tileBox.Y - heightExtend, tileBox.Width + 2 * (widthExtend), tileBox.Height + 2 * (heightExtend));

            // Get the points
            var bodyPoint = GetBox().Center;
            var tilePoint = tile.GetBox().Center;

            // Get the Major Axis
            var mAxis = MajorAxis(new Vector2(bodyPoint.X - tilePoint.X, bodyPoint.Y - bodyPoint.Y));
            distance = (int)mAxis.Length();

            // Change velocity if necessary
            //  Project vel onto MA
            //      Get dot product (vel * MA)
            var dot = Vector2.Dot(masterForce, mAxis);
            
            //      Multiply calculation to MA as a scalar
            var projScalar = (dot / Math.Pow(mAxis.Length(), 2));
            var proj = Vector2.Multiply(mAxis, (float)projScalar);

            //  Vel is now the lesser between the proj vector length and the vel
            if (proj.Length() > distance)
                return distance;

            return (int)masterForce.Length();

        }
        private Vector2 MajorAxis(Vector2 vec)
        {

            if (Math.Abs(vec.X) > Math.Abs(vec.Y))
                return new Vector2(vec.X, 0f);
            else
                return new Vector2(0f, vec.Y);

        }
        */

        /*
        public override void Update()
        {

            Move(masterForce);
            
        }
        */

    }
}
