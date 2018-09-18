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

        public Tile()
        {
            this.boundbox = new Rectangle(0, 0, 10, 10);
            this.isSolid = false;
        }
        public Tile(Rectangle boundbox, bool isSolid)
        {
            this.boundbox = boundbox;
            this.isSolid = isSolid;
        }
        public Tile(int x, int y, int size, bool isSolid)
        {
            this.boundbox = new Rectangle(x, y, size, size);
            this.isSolid = isSolid;
        }

    }
}
