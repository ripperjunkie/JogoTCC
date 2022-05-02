using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class SaveSystem
{
    public PlayerMaster playerMasterRef;


    //create save game object
    public SaveData CreateSaveGameObject()
    {
        SaveData saveData = new SaveData();
        saveData.checkpointData = new CheckpointData(playerMasterRef.checkpointLocation.x,
            playerMasterRef.checkpointLocation.y, playerMasterRef.checkpointLocation.z);
        Debug.Log("CreateSaveGameObject");
        return saveData;
    }

    public void SaveCheckpoint()
    {
        SaveData checkpoint = CreateSaveGameObject();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, checkpoint);
        file.Close();
        Debug.Log(Application.persistentDataPath.ToString());
    }

    public void LoadCheckpoint()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            SaveData saveData = (SaveData)bf.Deserialize(file);
            file.Close();

            playerMasterRef.transform.SetPositionAndRotation(new Vector3(saveData.checkpointData.x, 
                saveData.checkpointData.y, saveData.checkpointData.z), playerMasterRef.transform.rotation);
            return;
        }
        Debug.Log("Não tem nenhum save para carregar!");
    }

    public void DeleteSave()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
             File.Delete(Application.persistentDataPath + "/gamesave.save");
        }
    }
}

[System.Serializable]
public class SaveData
{
    public CheckpointData checkpointData;
}

[System.Serializable]
public class CheckpointData
{
    public float x, y, z;
    public CheckpointData(float _x, float _y, float _z)
    {
        x = _x;
        y = _y;
        z = _z;
    }
}