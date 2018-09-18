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
    class Tile
    {

        protected Rectangle boundbox;
        protected bool isSolid;
        protected bool isInteractable;

        public Tile()
        {
            this.boundbox = new Rectangle(0, 0, 10, 10);
            this.isSolid = false;
            this.isInteractable = false;
        }
        public Tile(Rectangle boundbox, bool isSolid, bool isInteractable)
        {
            this.boundbox = boundbox;
            this.isSolid = isSolid;
            this.isInteractable = isInteractable;
        }
        public Tile(int x, int y, int size, bool isSolid, bool isInteractable)
        {
            this.boundbox = new Rectangle(x, y, size, size);
            this.isSolid = isSolid;
            this.isInteractable = isInteractable;
        }

    }
}
