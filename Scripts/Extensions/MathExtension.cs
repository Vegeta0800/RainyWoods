using System;
using UnityEngine;

namespace RainyWoods.Extensions
{
    public static class MathExtension
    {
        /// <summary>
        /// Returns the normalized (0.0 - 1.0) sinus of t
        /// </summary>
        /// <param name="t">Radians to pass into sinus function</param>
        /// <returns></returns>
        public static double NormalizedSin(double t) => (Math.Sin(t) / 2) + 0.5;
        
        /// <summary>
        /// Returns a scaled sinus with the range from min to max
        /// </summary>
        /// <param name="t">Radians to pass into sinus function</param>
        /// <param name="min">Minimum value</param>
        /// <param name="max">Maximum value</param>
        /// <returns></returns>
        public static double ScaledSin(double t, double min, double max) => min + (NormalizedSin(t) * (max - min));

        /// <summary>
        /// Returns a scaled sinus with the range from min to max controlled by
        /// either Time.time or Time.unscaledTime (if unscaledTime = true)
        /// </summary>
        /// <param name="min">Minimum Value</param>
        /// <param name="max">Maximum Value</param>
        /// <param name="speed">The acceleration that should be applied (this also has effect if unscaledTime = true)</param>
        /// <param name="offset">The offset that should be applied</param>
        /// <param name="unscaledTime">If set to <code>true</code> Time.unscaledTime will be used instead of Time.time</param>
        /// <returns></returns>
        public static double TimeScaledSin(double min, double max, double speed = 1.0, double offset = 0.0, bool unscaledTime = false) => ScaledSin(DegToRad((((unscaledTime ? Time.unscaledTime : Time.time) * speed) + offset) % 360), min, max);

        /// <summary>
        /// Converts degrees to radians
        /// </summary>
        /// <param name="deg"></param>
        /// <returns></returns>
        public static double DegToRad(double deg) => (deg * Math.PI) / 180;

        /// <summary>
        /// Converts radians to degrees
        /// </summary>
        /// <param name="rad"></param>
        /// <returns></returns>
        public static double RadToDeg(double rad) => (rad * 180) / Math.PI;
    }
}
