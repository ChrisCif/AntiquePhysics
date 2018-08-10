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
        
        public override void Update()
        {

            Move(masterForce);

        }

    }
}
