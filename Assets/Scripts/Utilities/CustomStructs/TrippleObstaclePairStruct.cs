using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System;

namespace Digx7.Zygote
{
    [System.Serializable]
    public struct TrippleObstaclePair : IEquatable<TrippleObstaclePair>
    {
        #region Variables ============================
        public GameObject obstaclePrefab1;
        public GameObject obstaclePrefab2;
        public GameObject obstaclePrefab3;
        #endregion

        #region Equality Methods ============================

        // Implement IEquatable<T>.Equals(T other) for type-safe, efficient comparison
        public bool Equals(TrippleObstaclePair other)
        {
            return obstaclePrefab1 == other.obstaclePrefab1 &&
                   obstaclePrefab2 == other.obstaclePrefab2 &&
                   obstaclePrefab3 == other.obstaclePrefab3;
        }

        // Override Object.Equals(object obj) to call the type-specific Equals
        public override bool Equals(object obj)
        {
            return obj is TrippleObstaclePair other && Equals(other);
        }

        // Override Object.GetHashCode() so that equal objects have the same hash code
        public override int GetHashCode()
        {
            return HashCode.Combine(obstaclePrefab1, obstaclePrefab2, obstaclePrefab3);
        }

        // Overload the == and != operators for intuitive syntax
        public static bool operator ==(TrippleObstaclePair left, TrippleObstaclePair right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(TrippleObstaclePair left, TrippleObstaclePair right)
        {
            return !(left == right);
        }

        #endregion
    }
}