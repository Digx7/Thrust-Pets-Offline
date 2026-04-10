using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    [System.Serializable]
    public struct ConversationNode : IEquatable<ConversationNode>
    {
        #region Variables ============================
        public string speaker;
        
        [TextAreaAttribute]
        public string line;

        #endregion

        public void Print()
        {
            Debug.Log(speaker + ":\n" + line);
        }

        #region Equality Methods ============================

        // Implement IEquatable<T>.Equals(T other) for type-safe, efficient comparison
        public bool Equals(ConversationNode other)
        {
            return speaker == other.speaker && line == other.line;
        }

        // Override Object.Equals(object obj) to call the type-specific Equals
        public override bool Equals(object obj)
        {
            return obj is ConversationNode other && Equals(other);
        }

        // Override Object.GetHashCode() so that equal objects have the same hash code
        public override int GetHashCode()
        {
            return HashCode.Combine(speaker, line);
        }

        // Overload the == and != operators for intuitive syntax
        public static bool operator ==(ConversationNode left, ConversationNode right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ConversationNode left, ConversationNode right)
        {
            return !(left == right);
        }

        #endregion
    }
}