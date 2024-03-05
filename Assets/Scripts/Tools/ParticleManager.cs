using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    public class ParticleManager : MonoBehaviour
    {
        public static ParticleManager instance;
        public ParticleData[] particleData;
        Dictionary<string, ParticleData> _particleDic;
        private void Awake()
        {
            instance = this;
            _particleDic = new Dictionary<string, ParticleData>();
            for (int i = 0; i < particleData.Length; i++)
            {
                _particleDic.Add(particleData[i].name, particleData[i]);
            }
        }

        public static void PlayParticle(string n, Vector3 pos, Quaternion rot)
        {
            Instantiate(instance._particleDic[n].particleObj[Random.Range(0, instance._particleDic[n].particleObj.Length - 1)], pos, rot);
        }
    }
    [System.Serializable]
    public class ParticleData 
    {
        public string name;
        public Transform[] particleObj;
    }
}