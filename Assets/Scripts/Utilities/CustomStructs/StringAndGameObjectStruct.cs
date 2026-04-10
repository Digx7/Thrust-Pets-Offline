using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    [System.Serializable]
    public struct StringAndGameObject : IEquatable<StringAndGameObject>
    {
        #region Variables ============================
        public string name;
        public GameObject obj;
        #endregion

        #region Equality Methods ============================

        // Implement IEquatable<T>.Equals(T other) for type-safe, efficient comparison
        public bool Equals(StringAndGameObject other)
        {
            return name == other.name && obj == other.obj;
        }

        // Override Object.Equals(object obj) to call the type-specific Equals
        public override bool Equals(object obj)
        {
            return obj is StringAndGameObject other && Equals(other);
        }

        // Override Object.GetHashCode() so that equal objects have the same hash code
        public override int GetHashCode()
        {
            return HashCode.Combine(name, obj);
        }

        // Overload the == and != operators for intuitive syntax
        public static bool operator ==(StringAndGameObject left, StringAndGameObject right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(StringAndGameObject left, StringAndGameObject right)
        {
            return !(left == right);
        }

        #endregion
    }
}