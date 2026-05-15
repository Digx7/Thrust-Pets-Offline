using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System;

namespace Digx7.Zygote
{
    [System.Serializable]
    public struct ObstaclesState : IEquatable<ObstaclesState>
    {
        #region Variables ============================
        public bool checkStartTimeActive;
        public float startTimeActive;
        public bool checkEndTimeActive;
        public float endTimeActive;

        public int minObstacleOffset;
        public int maxObstacleOffset;

        public int numberOfObstacles;
        public int numberOfCoins;
        #endregion

        #region Main Methods ============================

        public bool IsActive(float timeSinceLevelLoad)
        {
            if (checkStartTimeActive && timeSinceLevelLoad < startTimeActive)
            {
                return false;
            }
            if (checkEndTimeActive && timeSinceLevelLoad > endTimeActive)
            {
                return false;
            }
            return true;
        }

        public int GetRandomObstacleOffset()
        {
            return UnityEngine.Random.Range(minObstacleOffset, maxObstacleOffset);
        }

        #endregion

        #region Equality Methods ============================

        // Implement IEquatable<T>.Equals(T other) for type-safe, efficient comparison
        public bool Equals(ObstaclesState other)
        {
            return checkStartTimeActive == other.checkStartTimeActive &&
                   startTimeActive == other.startTimeActive &&
                   checkEndTimeActive == other.checkEndTimeActive &&
                   endTimeActive == other.endTimeActive &&
                   minObstacleOffset == other.minObstacleOffset &&
                   maxObstacleOffset == other.maxObstacleOffset &&
                   numberOfObstacles == other.numberOfObstacles;
        }

        // Override Object.Equals(object obj) to call the type-specific Equals
        public override bool Equals(object obj)
        {
            return obj is ObstaclesState other && Equals(other);
        }

        // Override Object.GetHashCode() so that equal objects have the same hash code
        public override int GetHashCode()
        {
            return HashCode.Combine(checkStartTimeActive, startTimeActive, checkEndTimeActive, endTimeActive, minObstacleOffset, maxObstacleOffset, numberOfObstacles);
        }

        // Overload the == and != operators for intuitive syntax
        public static bool operator ==(ObstaclesState left, ObstaclesState right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ObstaclesState left, ObstaclesState right)
        {
            return !(left == right);
        }

        #endregion
    }
}