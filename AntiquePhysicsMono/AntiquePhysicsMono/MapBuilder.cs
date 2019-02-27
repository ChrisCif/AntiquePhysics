using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AntiquePhysicsMono
{

    class MapBuilder
    {
        
        public List<Body> BuildFromImage(string filePath)
        {

            List<Body> bodies = new List<Body>();
            var bodySize = 10;

            Bitmap bitmap = new Bitmap(filePath);
            
            for(int x = 0; x < bitmap.Width; x++)
            {
                for(int y = 0; y < bitmap.Height; y++)
                {

                    Color colorOption = bitmap.GetPixel(x, y);

                    if (colorOption.ToArgb() == Color.Black.ToArgb())
                    {

                        bodies.Add(
                            new Body(
                                new Microsoft.Xna.Framework.Rectangle(
                                    bodySize * x,
                                    bodySize * y,
                                    bodySize,
                                    bodySize
                                ),
                                1.0f
                            )
                        );

                    }

                }
            }

            return bodies;

        }

    }

}
