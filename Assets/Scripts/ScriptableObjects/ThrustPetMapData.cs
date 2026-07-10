using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using System.Collections.Generic;
using Digx7.Zygote;

namespace Digx7.ThrustPets
{
    [CreateAssetMenu(fileName = "NewThrustPetMapData", menuName = "ScriptableObjects/Data/ThrustPetMapData", order = 1)]
    public class ThrustPetMapData: ScriptableObject
    {
        [SerializeField] string _displayName;
        public string DisplayName => _displayName;
        [SerializeField] Sprite _menuIcon;
        public Sprite MenuIcon => _menuIcon;
        [TextArea(3, 10)]
        [SerializeField] string _menuDescription;
        public string MenuDescription => _menuDescription;
        [SerializeField] AudioResource _menuVO;
        public AudioResource MenuVO => _menuVO;
        [SerializeField] SceneData _sceneData;
        public SceneData sceneData => _sceneData;
    }
}