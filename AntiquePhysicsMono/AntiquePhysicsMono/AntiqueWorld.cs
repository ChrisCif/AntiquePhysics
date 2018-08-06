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
    class AntiqueWorld
    {

        private List<Body> bodies { get; }
        private Rectangle map { get; }
        private Vector2 gravity { get; set; }
        private Vector2 wind { get; set; }
        
        public AntiqueWorld()
        {
            bodies = new List<Body>();
            map = new Rectangle(0, 0, 800, 480);
            gravity = new Vector2(0f, 2.0f);
            wind = new Vector2(0f, 0f);
        }

        public void Update()
        {

            Parallel.ForEach(bodies, (bod) => {

                bod.Update();

            });

        }

        public void WorldForces()
        {

            Parallel.ForEach(bodies, (bod) => {

                if(bod is RigidBody)
                {
                    bod.EnactForce(gravity);
                    bod.EnactForce(wind);
                }

            });

        }

    }
}
