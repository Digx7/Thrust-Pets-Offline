using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    [System.Serializable]
    public struct SceneContext : IEquatable<SceneContext>
    {
        #region Variables ============================
        public int SpawnPointID;
        public SceneCameraMode sceneCameraMode;
        public Vector3 cameraLocation;
        #endregion

        public void Clear()
        {
            SpawnPointID = 0;
            sceneCameraMode = SceneCameraMode.FollowPlayer;
        }

        #region Equality Methods ============================

        // Implement IEquatable<T>.Equals(T other) for type-safe, efficient comparison
        public bool Equals(SceneContext other)
        {
            return SpawnPointID == other.SpawnPointID && sceneCameraMode == other.sceneCameraMode && cameraLocation == other.cameraLocation;
        }

        // Override Object.Equals(object obj) to call the type-specific Equals
        public override bool Equals(object obj)
        {
            return obj is SceneContext other && Equals(other);
        }

        // Override Object.GetHashCode() so that equal objects have the same hash code
        public override int GetHashCode()
        {
            return HashCode.Combine(SpawnPointID, sceneCameraMode, cameraLocation);
        }

        // Overload the == and != operators for intuitive syntax
        public static bool operator ==(SceneContext left, SceneContext right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(SceneContext left, SceneContext right)
        {
            return !(left == right);
        }
        #endregion
    }
}