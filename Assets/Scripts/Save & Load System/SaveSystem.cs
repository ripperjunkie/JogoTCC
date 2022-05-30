using System.Collections.Generic;
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

    public ConceptArtData CreateConceptArtGameObject()
    {
        ConceptArtData artData = new ConceptArtData();
        foreach(var item in playerMasterRef.gameProgress.conceptArtManager.unlockedConcepts)
        {
            artData.unlockedConcepts.Add(item.Key, item.Value);
        }
        return artData;
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

    public void SaveConceptArt()
    {
        ConceptArtData conceptArtData = CreateConceptArtGameObject();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/unlockables.save");
        bf.Serialize(file, conceptArtData);
        file.Close();
        Debug.Log(Application.persistentDataPath.ToString());

    }

    public ConceptArtData LoadConceptArt()
    {
        if (File.Exists(Application.persistentDataPath + "/unlockables.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/unlockables.save", FileMode.Open);
            ConceptArtData conceptArtData = (ConceptArtData)bf.Deserialize(file);
            file.Close();

            return conceptArtData;
        }
        return null;
    }

    public bool CheckSaveExist()
    {       
        return File.Exists(Application.persistentDataPath + "/gamesave.save");
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

[System.Serializable]
public class ConceptArtData
{ 
    public Dictionary<string, bool> unlockedConcepts = new Dictionary<string, bool>();
}