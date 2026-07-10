using UnityEngine;
using UnityEngine.Events;

namespace Digx7.ThrustPets
{
    public class ThrustPetGameModeDataChannelRaiser : MonoBehaviour
    {
        #region Variables ==============================================
        [SerializeField] private ThrustPetGameModeDataChannel channelToRaise;
        [SerializeField] private ThrustPetGameModeData _data;
        #endregion

        #region Setup ==============================================

        #endregion

        #region Channel Response Functions ==============================================

        #endregion

        #region Main Functions ==============================================

        public void Raise(ThrustPetGameModeData data)
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
