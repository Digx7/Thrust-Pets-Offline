using UnityEngine;
using TMPro;

namespace Digx7.Zygote
{
    public class BuildNumberHelper : MonoBehaviour
    {
        #region Variables ================================

        [Header("Variables")]
        [SerializeField] private TextMeshProUGUI textMeshProUGUI;

        #endregion

        #region Setup ================================

        private void Start()
        {
            textMeshProUGUI.text = Application.version;
        }

        #endregion
    }
}
