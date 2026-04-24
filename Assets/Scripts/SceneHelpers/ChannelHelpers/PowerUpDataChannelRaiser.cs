using UnityEngine;
using UnityEngine.Events;

namespace Digx7.ThrustPets
{
    public class PowerUpDataChannelRaiser : MonoBehaviour
    {
        #region Variables ==============================================
        [SerializeField] private PowerUpDataChannel channelToRaise;
        [SerializeField] private PowerUpData _data;
        #endregion

        #region Setup ==============================================

        #endregion

        #region Channel Response Functions ==============================================

        #endregion

        #region Main Functions ==============================================

        public void Raise(PowerUpData data)
        {
            channelToRaise.Raise(data);
        }

        public void Raise()
        {
            channelToRaise.Raise(_data);
        }

        public void SetData(PowerUpData newData)
        {
            _data = newData;
        }

        #endregion
    }
}
