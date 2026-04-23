using UnityEngine;
using UnityEngine.Events;

namespace Digx7.ThrustPets
{
    public class PlayerSkinDataChannelRaiser : MonoBehaviour
    {
        #region Variables ==============================================
        [SerializeField] private PlayerSkinDataChannel channelToRaise;
        [SerializeField] private PlayerSkinData _data;
        #endregion

        #region Setup ==============================================

        #endregion

        #region Channel Response Functions ==============================================

        #endregion

        #region Main Functions ==============================================

        public void Raise(PlayerSkinData data)
        {
            channelToRaise.Raise(data);
        }

        public void Raise()
        {
            channelToRaise.Raise(_data);
        }

        public void SetData(PlayerSkinData newData)
        {
            _data = newData;
        }

        #endregion
    }
}
