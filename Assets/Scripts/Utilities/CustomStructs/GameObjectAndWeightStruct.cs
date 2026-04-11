using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System;

namespace Digx7.Zygote
{
    [System.Serializable]
    public struct GameObjectAndWeight : IEquatable<GameObjectAndWeight>
    {
        #region Variables ============================
        public GameObject gameObject;
        [Range(0f, 1f)]
        public float weight;
        #endregion

        #region Equality Methods ============================

        // Implement IEquatable<T>.Equals(T other) for type-safe, efficient comparison
        public bool Equals(GameObjectAndWeight other)
        {
            return gameObject == other.gameObject && weight == other.weight;
        }

        // Override Object.Equals(object obj) to call the type-specific Equals
        public override bool Equals(object obj)
        {
            return obj is GameObjectAndWeight other && Equals(other);
        }

        // Override Object.GetHashCode() so that equal objects have the same hash code
        public override int GetHashCode()
        {
            return HashCode.Combine(gameObject, weight);
        }

        // Overload the == and != operators for intuitive syntax
        public static bool operator ==(GameObjectAndWeight left, GameObjectAndWeight right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(GameObjectAndWeight left, GameObjectAndWeight right)
        {
            return !(left == right);
        }

        #endregion
    }
}