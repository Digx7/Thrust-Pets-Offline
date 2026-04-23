using UnityEngine;
using UnityEngine.Events;

namespace Digx7.ThrustPets
{
    public class ItemUnlockTypeChannelRaiser : MonoBehaviour
    {
        #region Variables ==============================================
        [SerializeField] private ItemUnlockTypeChannel channelToRaise;
        [SerializeField] private ItemUnlockType _data;
        #endregion

        #region Setup ==============================================

        #endregion

        #region Channel Response Functions ==============================================

        #endregion

        #region Main Functions ==============================================

        public void Raise(ItemUnlockType data)
        {
            channelToRaise.Raise(data);
        }

        public void Raise()
        {
            channelToRaise.Raise(_data);
        }

        #endregion
    }
}
