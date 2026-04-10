using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System;

namespace Digx7.Zygote
{
    [System.Serializable]
    public struct GameEndResult : IEquatable<GameEndResult>
    {
        #region Variables ============================
        
        public GameEndCondition endCondition;
        public int levelReached;
        public int score;
        public int coins;

        #endregion

        #region Equality Methods ============================

        // Implement IEquatable<T>.Equals(T other) for type-safe, efficient comparison
        public bool Equals(GameEndResult other)
        {
            return endCondition == other.endCondition && levelReached == other.levelReached && score == other.score && coins == other.coins;
        }

        // Override Object.Equals(object obj) to call the type-specific Equals
        public override bool Equals(object obj)
        {
            return obj is GameEndResult other && Equals(other);
        }

        // Override Object.GetHashCode() so that equal objects have the same hash code
        public override int GetHashCode()
        {
            return HashCode.Combine(endCondition, levelReached, score, coins);
        }

        // Overload the == and != operators for intuitive syntax
        public static bool operator ==(GameEndResult left, GameEndResult right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(GameEndResult left, GameEndResult right)
        {
            return !(left == right);
        }

        #endregion
    }
}