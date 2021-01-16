using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveSystem
{
 
    public static void saveGame(GameManager gm)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.txt";

        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(gm);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void saveGame(GameData gameData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.txt";

        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, gameData);
        stream.Close();

    }


    public static GameData LoadGame()
    {
        string path = Application.persistentDataPath + "/player.txt";
        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);

            GameData playerData = binaryFormatter.Deserialize(fileStream) as GameData;
            fileStream.Close();
            return playerData;

        }
        else
        {
            Debug.LogError("Could not load");
            return null;
        }

    }

}
