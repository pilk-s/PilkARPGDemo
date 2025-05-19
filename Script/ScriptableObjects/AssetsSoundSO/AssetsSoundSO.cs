using System.Collections.Generic;
using UnityEngine;

namespace piiilk_ARPGDemo.Assets
{
    [CreateAssetMenu(fileName = "Sound", menuName = "Create/Assets/Sound", order = 0)]
    public class AssetsSoundSO : ScriptableObject
    {
        [System.Serializable]
        private class Sounds
        {
            public SoundType SoundType;
            public AudioClip[] AudioClips;
        }

        [SerializeField] private List<Sounds> _configSounds = new List<Sounds>();

        public AudioClip GetAudioClip(SoundType soundType)
        {
            if (_configSounds.Count==0) return null;
            switch (soundType)
            {
                case SoundType.FOOT:
                    return _configSounds[0].AudioClips[Random.Range(0, _configSounds[0].AudioClips.Length)];
                    break;
                case SoundType.ATK:
                    return _configSounds[1].AudioClips[Random.Range(0, _configSounds[1].AudioClips.Length)];
                    break;
                // case SoundType.HIT:
                //     return _configSounds[1].AudioClips[Random.Range(0, _configSounds[1].AudioClips.Length)];
                //     break;
                // case SoundType.BLOCK:
                //     return _configSounds[2].AudioClips[Random.Range(0, _configSounds[2].AudioClips.Length)];
                //     break;
               
                default:
                    break;
            }
            return null;
        }
    }
    
}