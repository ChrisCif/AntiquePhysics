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

    static class Vector2Extensions
    {

        public static Vector2 Project(this Vector2 v, Vector2 onto)
        {

            var projVector = Vector2.Dot(v, onto) / onto.Length() * (onto / onto.Length());
            return projVector;

        }

    }

}
