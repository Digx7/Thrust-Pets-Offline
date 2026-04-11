using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System;

namespace Digx7.Zygote
{
    [System.Serializable]
    public struct InstantiatedObject : IEquatable<InstantiatedObject>
    {
        #region Variables ============================
        public GameObject gameObject;
        public Vector3 instantiatedPosition;
        public Quaternion instantiatedRotation;

        #endregion

        #region Equality Methods ============================

        // Implement IEquatable<T>.Equals(T other) for type-safe, efficient comparison
        public bool Equals(InstantiatedObject other)
        {
            return gameObject == other.gameObject && instantiatedPosition == other.instantiatedPosition && instantiatedRotation == other.instantiatedRotation;
        }

        // Override Object.Equals(object obj) to call the type-specific Equals
        public override bool Equals(object obj)
        {
            return obj is InstantiatedObject other && Equals(other);
        }

        // Override Object.GetHashCode() so that equal objects have the same hash code
        public override int GetHashCode()
        {
            return HashCode.Combine(gameObject, instantiatedPosition, instantiatedRotation);
        }

        // Overload the == and != operators for intuitive syntax
        public static bool operator ==(InstantiatedObject left, InstantiatedObject right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(InstantiatedObject left, InstantiatedObject right)
        {
            return !(left == right);
        }

        #endregion
    }
}