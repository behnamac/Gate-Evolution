using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] private Particles[] particlesList;
    private Dictionary<string, Particles> particleDic;

    private void Awake()
    {
        particleDic = new Dictionary<string, Particles>();
        for (int i = 0; i < particlesList.Length; i++)
        {
            particleDic.Add(particlesList[i].Name, particlesList[i]);
        }
    }

    public void PlayParticle(string particleName)
    {

    }


    [System.Serializable]
    public class Particles
    {
        public string Name;
        public ParticleSystem particle;
    }
}
