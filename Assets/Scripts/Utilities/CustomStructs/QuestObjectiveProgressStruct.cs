using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    [System.Serializable]
    public struct QuestObjectiveProgress : IEquatable<QuestObjectiveProgress>
    {
        #region Variables ============================
        public string objectiveName;
        public int addedAmount;
        #endregion

        #region Equality Methods ============================

        // Implement IEquatable<T>.Equals(T other) for type-safe, efficient comparison
        public bool Equals(QuestObjectiveProgress other)
        {
            return objectiveName == other.objectiveName && addedAmount == other.addedAmount;
        }

        // Override Object.Equals(object obj) to call the type-specific Equals
        public override bool Equals(object obj)
        {
            return obj is QuestObjectiveProgress other && Equals(other);
        }

        // Override Object.GetHashCode() so that equal objects have the same hash code
        public override int GetHashCode()
        {
            return HashCode.Combine(objectiveName, addedAmount);
        }

        // Overload the == and != operators for intuitive syntax
        public static bool operator ==(QuestObjectiveProgress left, QuestObjectiveProgress right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(QuestObjectiveProgress left, QuestObjectiveProgress right)
        {
            return !(left == right);
        }
        #endregion
    }
}