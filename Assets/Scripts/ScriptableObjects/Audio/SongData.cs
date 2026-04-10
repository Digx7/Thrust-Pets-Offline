using UnityEngine;
using System;
using System.Collections.Generic;

namespace Digx7.Zygote
{
    [CreateAssetMenu(fileName = "NewSong", menuName = "ScriptableObjects/Data/SongData", order = 1)]
    public class SongData : ScriptableObject
    {
        #region Variables ==============================================
        public string Name;
        public string Artist;
        public bool shouldLoop = true;
        public List<AudioClip> layers;

        #endregion
    }
}
