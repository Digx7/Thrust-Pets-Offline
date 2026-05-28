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

        [Range(0f, 1f)]
        public float chanceOfDoubleObstacles;

        [Range(0f, 1f)]
        public float chanceOfTrippleObstacles;

        [Range(0f, 1f)]
        public float chanceOfSpecialBlock;

        public int minCoinLineOffset;
        public int maxCoinLineOffset;
        public int numberOfCoinLines;
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

        public int GetNumberOfObstaclesToSpawnAtOneTime()
        {
            int output = 1;

            if (ShouldSpawnTrippleObstacles())
            {
                output = 3;
            }
            else if (ShouldSpawnDoubleObstacles())
            {
                output = 2;
            }

            return output;
        }

        public bool ShouldSpawnDoubleObstacles()
        {
            return UnityEngine.Random.Range(0f, 1f) <= chanceOfDoubleObstacles;
        }

        public bool ShouldSpawnTrippleObstacles()
        {
            return UnityEngine.Random.Range(0f, 1f) <= chanceOfTrippleObstacles;
        }

        public bool ShouldSpawnSpecialBlock()
        {
            return UnityEngine.Random.Range(0f, 1f) <= chanceOfSpecialBlock;
        }

        public int GetRandomObstacleOffset()
        {
            return UnityEngine.Random.Range(minObstacleOffset, maxObstacleOffset);
        }

        public int GetRandomCoinLineOffset()
        {
            return UnityEngine.Random.Range(minCoinLineOffset, maxCoinLineOffset);
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