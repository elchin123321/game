using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoad
{
   static public void SaveJSON(object obj)
    {
        File.WriteAllText(Hero.savePath, JsonUtility.ToJson(obj));
    }

    static public T LoadJson<T>()
    {
        if (File.Exists(Hero.savePath))
        {
            return JsonUtility.FromJson<T>(File.ReadAllText(Hero.savePath));
        }
        else return default(T);
    }
}
