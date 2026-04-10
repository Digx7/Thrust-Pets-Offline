using UnityEngine;
using UnityEngine.Events;
using System;

namespace Digx7.Zygote
{
    [System.Serializable]
    public class GenericBlackBoardEntry 
    {
        #region Variables ==============================================
        public object value;
        #endregion

        #region Main Functions ==============================================
        public GenericBlackBoardEntry()
        {
            value = new object();
        }

        public virtual T GetEntryValue<T>()
        {
            return (T)value;
        }

        public virtual void SetEntryValue<T>(T newValue)
        {
            value = newValue;
        }

        public override string ToString()
        {
            return ("" + value);
        }

        #endregion
    }
}