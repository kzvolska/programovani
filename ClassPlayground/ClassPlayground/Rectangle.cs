using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassPlayground
{
    internal class Rectangle
    {
        private float width;
        private float height;

        public Rectangle(float width, float height)
        {
            SetWidth(width);
            SetHeight(height);
        }

        public void SetWidth(float value)
        {
            width = value;
            if (width < 0)
            {
                width = -width;
            }
            else if (width == 0)
            {
                Console.WriteLine("Rectangle doesn't exist");
            } 
        }

        public float GetWidth()
        {
            return width;
        }

        public void SetHeight(float value)
        {
            height = value;
            if (height < 0)
            {
                height = -height;
            }
            else if (height == 0)
            {
                Console.WriteLine("Rectangle doesn't exist");
            }
        }

        public float GetHeight()
        {
            return height;
        }

        public float CalculateArea()
        {
            return width * height;
        }

        public float CalculateAspectRatio ()
        {
            return width / height;
        }

        public bool ContainsPoint(float x, float y)
        {
            return x >= 0 && y >= 0 && x < width && y < height; //vrat mi tu logickou hodnotu, ktera vyleze z tohoto vyrazu
        }

    }
}
