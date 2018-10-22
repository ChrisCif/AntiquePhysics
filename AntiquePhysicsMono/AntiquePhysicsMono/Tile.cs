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
        protected Body content;
        

        public Tile()
        {
            this.boundbox = new Rectangle(0, 0, 10, 10);
        }
        public Tile(Rectangle boundbox)
        {

            this.boundbox = boundbox;
            this.content = null;

        }
        public Tile(Rectangle boundbox, Body content)
        {

            this.boundbox = boundbox;
            this.content = content;

        }
        public Tile(int x, int y, int size)
        {

            this.boundbox = new Rectangle(x, y, size, size);
            this.content = null;

        }
        public Tile(int x, int y, int size, Body content)
        {

            this.boundbox = new Rectangle(x, y, size, size);
            this.content = content;

        }

        public void Fill(Body content)
        {
            this.content = content;
        }

        public Rectangle GetBox() { return boundbox; }
        public Body GetContent() { return content; }
        

    }
}
