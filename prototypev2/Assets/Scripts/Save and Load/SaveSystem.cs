using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void Save(GameManager gameManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        //saves the data in the same file by searching a "constant" path
        string path = Application.persistentDataPath + "/worlddata.txt";
        FileStream stream = new FileStream(path, FileMode.Create);

        WorldData data = new WorldData(gameManager);
        //turns the data into binary so that it cannot be edited easily (prevents players from editing their progress in the game)
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static WorldData Load()
    {
        //loads the data in the same place that it saved it in
        string path = Application.persistentDataPath + "/worlddata.txt";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            //turns the binary text back into a WorldData instance so it can be reloaded
            WorldData data = formatter.Deserialize(stream) as WorldData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}

