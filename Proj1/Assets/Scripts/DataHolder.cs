using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataHolder : object
{
    private static string heroName;
    public static string HeroName
    {
        get
        {
            return heroName;
        }
        set
        {
            heroName = value;
        }
    }
}
