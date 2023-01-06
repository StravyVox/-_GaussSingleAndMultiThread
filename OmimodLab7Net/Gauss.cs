using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmimodLab7Net
{
    internal static class Gauss
    {
        public static float func(double x)
        {
            return (float)Math.Abs(Math.Pow(x, 3) * Math.Cos(x));
        }
       public static float dfunc(double x)
        {
            return (float)(-Math.Pow(x, 3) * Math.Sin(x) + (3 * Math.Pow(x, 2)) * Math.Cos(x));
        }

        public static float f(float x) { return(float)(func(x) * Math.Sqrt(1 + Math.Pow(dfunc(x), 2))); }

        public static float g(float a, float b, float z)
        {
            float x = (b - a) / 2 * z + (b + a) / 2;
            return (f(x));
        }
        public static float Square(float x1, float x2)
        {
            float Square = (float)(2 * Math.PI * GaussIntegral(x1, x2));
            return Square;
        }
        public static  float GaussIntegral(float a, float b)
        {
            float I = (float)((b - a) / 2 * (1 * g(a, b, -1 / (float)Math.Sqrt(3)) + 1 * g(a, b, 1 / (float)Math.Sqrt(3))));
            return I;
        }
    }
}
