using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.TextCore.Text;

public static class SaveLoadManager
{
    [System.Serializable]
    public class PlayerData
    {
        public int level;
        public int exp;
        public string playerName;
        public int characternumber;
        public int money;
        public int energystone;
        public int gem;
        public int openMap1;
        public int openMap2;
        public int openMap3;
        public int openMap4;
    }

    public static void SaveData(PlayerData data)
    {
        string filegame = PlayerPrefs.GetString("FileGame");
        string path = Application.persistentDataPath + "/" + filegame + ".dat";

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadData()
    {
        string filegame = PlayerPrefs.GetString("FileGame");
        string path = Application.persistentDataPath + "/" + filegame + ".dat";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
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

