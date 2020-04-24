using System;

[Serializable]
public class SaveFile
{
    private static SaveFile Instance;
    public GameMetaData MetaData { get; set; }

    private SaveFile()
    {
        MetaData = new GameMetaData();
    }
    public static void DeleteInstance()
    {
        Instance = null;
    }
    public static SaveFile LoadInstance()
    {
        if (Instance == null)
        {
            throw new InvalidOperationException();
        }
        return Instance;
    }
    public static SaveFile GetInstance()
    {
        if (Instance == null)
        {
            if (SaveManager.Instance.IsSaveFileCreated())
            {
                Instance = SaveManager.Instance.Load();
                SaveManager.Instance.Save(Instance);
            }
            else
            {
                Instance = SaveManager.Instance.Save(new SaveFile());
            }
        }
        return Instance;
    }
}
