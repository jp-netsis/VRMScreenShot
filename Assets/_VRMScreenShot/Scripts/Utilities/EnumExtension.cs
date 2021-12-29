using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRMScreenShot
{
    public static class EnumExtension
    {
        public static bool TryParse<T>(string s, out T temp) where T : struct
        {
            return Enum.TryParse(s, out temp) && Enum.IsDefined(typeof(T), temp);
        }
    }
}