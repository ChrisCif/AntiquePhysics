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
        protected bool isSolid; // Whether or not collisions are calculated for this body
        protected bool interactable;    // Whether or not collisions are allowed with creatures

        public Body(Rectangle box, bool isSolid, bool interactable)
        {
            this.box = box;
            this.isSolid = isSolid;
            this.interactable = interactable;
        }
        public Body(int x, int y, int width, int height, bool isSolid, bool interactable)
        {
            this.box = new Rectangle(x, y, width, height);
            this.isSolid = isSolid;
            this.interactable = interactable;
        }

        public Rectangle GetBox() { return box; }

        public void Move(Vector2 trans)
        {

            box.Offset(trans);

        }

        public abstract void Update();

        public bool IsSolid() { return isSolid; }
        public bool IsInteractable() { return interactable; }
        public bool isCollidable() { return (isSolid && !interactable); }

    }
}
