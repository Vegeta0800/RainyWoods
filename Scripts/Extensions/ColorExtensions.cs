using UnityEngine;
using UnityEngine.UI;

namespace RainyWoods.Extensions
{
    public static class ColorExtension
    {
        public static Color SetAlpha(this Color c, float a)
        {
            c.a = a;
            return c;
        }

        public static Color SetRed(this Color c, float r)
        {
            c.r = r;
            return c;
        }

        public static Color SetGreen(this Color c, float g)
        {
            c.g = g;
            return c;
        }

        public static Color SetBlue(this Color c, float b)
        {
            c.b = b;
            return c;
        }

        public static void CrossFadeAlphaFixed(this Graphic img, float alpha, float duration, bool ignoreTimeScale)
        {
            //Make the alpha 1
            Color fixedColor = img.color;
            fixedColor.a = 1;
            img.color = fixedColor;

            //Set the 0 to zero then duration to 0
            img.CrossFadeAlpha(0f, 0f, true);

            //Finally perform CrossFadeAlpha
            img.CrossFadeAlpha(alpha, duration, ignoreTimeScale);
        }
    }
}