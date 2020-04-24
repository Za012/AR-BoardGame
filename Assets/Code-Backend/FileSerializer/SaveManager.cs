using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    private SaveManager() { }

    private void Awake()
    {
        Instance = GameObject.Find("SaveManager").GetComponent<SaveManager>();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Debug.Log("Application Paused");
            Save(SaveFile.GetInstance());
        }
    }
    private void OnApplicationQuit()
    {
        Debug.Log("Application Quit");
        Save(SaveFile.GetInstance());
    }
    public bool IsSaveFileCreated()
    {
        Debug.Log("Checking path: " + Application.persistentDataPath + "/save.dat");
        bool isFileCreated = File.Exists(Application.persistentDataPath + "/save.dat");
        if (isFileCreated)
        {
            Debug.Log("SaveFile Exists");
        }
        else
        {
            Debug.Log("SaveFile Missing");
        }
        return isFileCreated;
    }

    public SaveFile Load()
    {
        SaveFile saveFile;
        Debug.Log("Loading Savefile");
        using (FileStream fileStream = File.Open(Application.persistentDataPath + "/save.dat", FileMode.Open))
        {
            BinaryFormatter bf = new BinaryFormatter();
            saveFile = bf.Deserialize(fileStream) as SaveFile;
            fileStream.Close();
        }
        Debug.Log("Loading Complete");
        return saveFile;
    }

    public SaveFile Save(SaveFile saveFile)
    {
        Debug.Log("Saving Savefile");
        using (FileStream fileStream = File.Create(Application.persistentDataPath + "/save.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fileStream, saveFile);
            fileStream.Close();
        }
        Debug.Log("Saving Complete");
        return saveFile;
    }

    private void DeleteSave()
    {
        File.Delete(Application.persistentDataPath + "/save.dat");
    }
}
