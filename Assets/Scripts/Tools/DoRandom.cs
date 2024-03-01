using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DoRandom
{
    public static int SetRandom(int a,int b)
    {
        return Random.Range(a,b);
    }

    public static float SetRandom(float a, float b)
    {
        return Random.Range(a, b);
    }

}
