using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;



public class SaveManager : MonoBehaviour
{
    // Singleton
    public static SaveManager Instance { get; private set; }

    // Fields
    public SaveState save;
    private const string saveFileName = "data.ss";
    private BinaryFormatter formatter;

    // Actions
    public event Action<SaveState> OnLoad; 
    public event Action<SaveState> OnSave; 


    private void Awake()
    {
        Instance = this;
        formatter = new BinaryFormatter();

        // Try and load the previous state
        Load();
    }

    public void Load()
    {
        try
        {
            var file = new FileStream(saveFileName, FileMode.Open, FileAccess.Read);
            save = formatter.Deserialize(file) as SaveState;
            file.Close();

            OnLoad?.Invoke(save);
        }
        catch
        {
            Debug.Log("Save file not found");
            Save();
        }
    }

    public void Save()
    {
        // If there's no previous state found, create a new one
        if (save == null)
        {
            save = new SaveState();
        }

        // Set the time at which we tried saving
        save.LastSaveTime = DateTime.Now;

        // Open a file on our system and write to it
        var file = new FileStream(saveFileName, FileMode.OpenOrCreate, FileAccess.Write);
        formatter.Serialize(file, save);
        file.Close();

        OnSave?.Invoke(save);
    }
}
