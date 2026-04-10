using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    [System.Serializable]
    public struct SceneData : IEquatable<SceneData>
    {
        #region Variables ============================
        public string sceneName;
        public SceneContext context;
        #endregion

        public void Clear()
        {
            sceneName = "";
            context.Clear();
        }

        #region Equality Methods ============================

        // Implement IEquatable<T>.Equals(T other) for type-safe, efficient comparison
        public bool Equals(SceneData other)
        {
            return sceneName == other.sceneName && context == other.context;
        }

        // Override Object.Equals(object obj) to call the type-specific Equals
        public override bool Equals(object obj)
        {
            return obj is SceneData other && Equals(other);
        }

        // Override Object.GetHashCode() so that equal objects have the same hash code
        public override int GetHashCode()
        {
            return HashCode.Combine(sceneName, context);
        }

        // Overload the == and != operators for intuitive syntax
        public static bool operator ==(SceneData left, SceneData right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(SceneData left, SceneData right)
        {
            return !(left == right);
        }
        #endregion
    }
}