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
    abstract class Body
    {

        protected Rectangle box;
        protected bool isSolid;

        public Body(Rectangle box)
        {
            this.box = box;
        }
        public Body(int x, int y, int width, int height)
        {
            this.box = new Rectangle(x, y, width, height);
        }

        public Rectangle GetBox() { return box; }

        public void Move(Vector2 trans)
        {

            box.Offset(trans);

        }

        public abstract void Update();

        public bool IsSolid() { return isSolid; }

    }
}
