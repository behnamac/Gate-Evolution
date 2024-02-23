using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager instance;
    public ParticleData[] particleData;
    Dictionary<string, ParticleData> particleDic;
    private void Awake()
    {
        instance = this;
        particleDic = new Dictionary<string, ParticleData>();
        for (int i = 0; i < particleData.Length; i++)
        {
            particleDic.Add(particleData[i].name, particleData[i]);
        }
    }

    [System.Obsolete]
    public static void PlayParticle(string n, Vector3 pos, Quaternion Rot)
    {
        Instantiate(instance.particleDic[n].particleObj[Random.RandomRange(0, instance.particleDic[n].particleObj.Length - 1)], pos, Rot);
    }
}
[System.Serializable]
public class ParticleData 
{
    public string name;
    public Transform[] particleObj;
}
