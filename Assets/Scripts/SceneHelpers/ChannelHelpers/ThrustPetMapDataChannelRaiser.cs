using UnityEngine;
using UnityEngine.Events;

namespace Digx7.ThrustPets
{
    public class ThrustPetMapDataChannelRaiser : MonoBehaviour
    {
        #region Variables ==============================================
        [SerializeField] private ThrustPetMapDataChannel channelToRaise;
        [SerializeField] private ThrustPetMapData _data;
        #endregion

        #region Setup ==============================================

        #endregion

        #region Channel Response Functions ==============================================

        #endregion

        #region Main Functions ==============================================

        public void Raise(ThrustPetMapData data)
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
