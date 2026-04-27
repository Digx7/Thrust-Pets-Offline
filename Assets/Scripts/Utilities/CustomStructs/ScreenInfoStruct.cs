using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System;
using Digx7.Utils;

namespace Digx7.Zygote
{
    [System.Serializable]
    public struct ScreenInfo : IEquatable<ScreenInfo>
    {
        #region Variables ============================
        public int width;
        public int height;

        public Vector2Int Resolution => new Vector2Int(width, height);
        #endregion

        #region Main Methods ===============================

        public Vector2 GetAspectRatio()
        {
            return MathUtils.GetRatio(width, height);
        }

        #endregion

        #region Equality Methods ============================

        // Implement IEquatable<T>.Equals(T other) for type-safe, efficient comparison
        public bool Equals(ScreenInfo other)
        {
            return width == other.width && height == other.height;
        }

        // Override Object.Equals(object obj) to call the type-specific Equals
        public override bool Equals(object obj)
        {
            return obj is ScreenInfo other && Equals(other);
        }

        // Override Object.GetHashCode() so that equal objects have the same hash code
        public override int GetHashCode()
        {
            return HashCode.Combine(width, height);
        }

        // Overload the == and != operators for intuitive syntax
        public static bool operator ==(ScreenInfo left, ScreenInfo right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ScreenInfo left, ScreenInfo right)
        {
            return !(left == right);
        }

        #endregion
    }
}