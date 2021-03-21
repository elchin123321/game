using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{

    public static string path;
    [SerializeField] Hero hero;

    public void Awake()
    {


        path = Application.persistentDataPath + "/GameData.sm";

        LoadGame();

    }

    public void SaveGame()
    {

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(path, FileMode.Create);

        Save save = new Save();

        save.positionX = hero.transform.position.x;
        save.positionY = hero.transform.position.y;

        bf.Serialize(fs, save);

        fs.Close();

        PlayerPrefs.SetString("Save", path);

    }

    public void LoadGame()
    {

        if (!File.Exists(path))
            return;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(PlayerPrefs.GetString("Save"), FileMode.Open);

        Save save = (Save)bf.Deserialize(fs);
        fs.Close();

        hero.transform.position=new Vector3(save.positionX, save.positionY);
        

    }

}

[Serializable]
public class Save
{
    public float positionX;
    public float positionY;



}
