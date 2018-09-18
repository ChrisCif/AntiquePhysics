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
    class RigidBody : Body
    {

        protected Vector2 masterForce;
        protected const float MAX_FORCE = 10.0f;

        public RigidBody(Rectangle box) : base(box)
        {
            masterForce = Vector2.Zero;
        }
        public RigidBody(int x, int y, int width, int height) : base(x, y, width, height)
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
            Tile[,] checkTiles = world.GetIntrsctTiles(boundBox);

        }

        public override void Update()
        {

            Move(masterForce);
            
        }

    }
}
