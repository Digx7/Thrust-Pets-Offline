using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    [System.Serializable]
    public struct QuestNodeOutcome : IEquatable<QuestNodeOutcome>
    {
        #region Variables ============================
        public QuestNodeOutcomeType outcomeType;
        public string data;
        #endregion

        #region Equality Methods ============================

        // Implement IEquatable<T>.Equals(T other) for type-safe, efficient comparison
        public bool Equals(QuestNodeOutcome other)
        {
            return outcomeType == other.outcomeType && data == other.data;
        }

        // Override Object.Equals(object obj) to call the type-specific Equals
        public override bool Equals(object obj)
        {
            return obj is QuestNodeOutcome other && Equals(other);
        }

        // Override Object.GetHashCode() so that equal objects have the same hash code
        public override int GetHashCode()
        {
            return HashCode.Combine(outcomeType, data);
        }

        // Overload the == and != operators for intuitive syntax
        public static bool operator ==(QuestNodeOutcome left, QuestNodeOutcome right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(QuestNodeOutcome left, QuestNodeOutcome right)
        {
            return !(left == right);
        }
        #endregion
    }
}