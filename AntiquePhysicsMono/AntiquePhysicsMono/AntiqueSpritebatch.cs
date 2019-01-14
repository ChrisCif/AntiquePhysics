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

    class AntiqueSpritebatch
    {

        SpriteBatch spriteBatch;
        Texture2D debugTex;
     
        public AntiqueSpritebatch(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
        }

        public void SetTexture(Texture2D debugTex)
        {

            this.debugTex = debugTex;

        }

        public void DrawVector(Vector2 vec, Vector2 origin, float length, Color color)
        {

            vec.Normalize();
            float angle = (float)Math.Atan2((vec * length).Y, (vec * length).X);

            spriteBatch.Draw(

                debugTex,
                new Rectangle(
                    (int)origin.X,
                    (int)origin.Y,
                    (int)length,
                    1
                    ),
                null,
                color,
                angle,
                Vector2.Zero,
                SpriteEffects.None,
                0
                );

        }
        public void DrawVector(Vector2 origin, Vector2 end, Color color)
        {

            Vector2 edge = end - origin;
            float angle = (float)Math.Atan2(edge.Y, edge.X);

            spriteBatch.Draw(

                debugTex,
                new Rectangle(
                    (int)origin.X,
                    (int)origin.Y,
                    (int)edge.Length(),
                    1
                    ),
                null,
                color,
                angle,
                Vector2.Zero,
                SpriteEffects.None,
                0
                );

        }

    }

}
