using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    [CreateAssetMenu(fileName = "NewScreenBreakPointData", menuName = "ScriptableObjects/Data/ScreenBreakPointData", order = 1)]
    public class ScreenBreakPointData: ScriptableObject
    {
        [Header("Screen Width")]
        public float minScreenWidth;
        public float maxScreenWidth;
    }
}