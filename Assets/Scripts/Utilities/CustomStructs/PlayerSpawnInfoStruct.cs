using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    [System.Serializable]
    public struct PlayerSpawnInfo : IEquatable<PlayerSpawnInfo>
    {
        #region Variables ============================
        public int ID;
        public Vector3 location;
        public Quaternion rotation;
        #endregion

        #region Equality Methods ============================

        // Implement IEquatable<T>.Equals(T other) for type-safe, efficient comparison
        public bool Equals(PlayerSpawnInfo other)
        {
            return ID == other.ID && location == other.location && rotation == other.rotation;
        }

        // Override Object.Equals(object obj) to call the type-specific Equals
        public override bool Equals(object obj)
        {
            return obj is PlayerSpawnInfo other && Equals(other);
        }

        // Override Object.GetHashCode() so that equal objects have the same hash code
        public override int GetHashCode()
        {
            return HashCode.Combine(ID, location, rotation);
        }

        // Overload the == and != operators for intuitive syntax
        public static bool operator ==(PlayerSpawnInfo left, PlayerSpawnInfo right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(PlayerSpawnInfo left, PlayerSpawnInfo right)
        {
            return !(left == right);
        }

        #endregion

    }
}