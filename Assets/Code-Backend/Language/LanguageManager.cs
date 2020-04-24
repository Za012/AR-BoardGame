using System;
using System.Xml;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    private XmlNodeList library;

    #region Singleton
    private LanguageManager()
    {

    }
    public static LanguageManager Instance;
    private void Awake()
    {
        Instance = this;
        LoadLanguageArray();
    }
    #endregion

    void LoadLanguageArray()
    {
        XmlDocument xmlDoc = new XmlDocument();
        TextAsset textAsset = (TextAsset)Resources.Load("library");
        xmlDoc.LoadXml(textAsset.text);
        library = xmlDoc.GetElementsByTagName("Language");
    }

    public void SetLanguage(LanguageEnum language)
    {
        SaveFile.GetInstance().MetaData.language = language;
    }

    public LanguageEnum GetLanguage()
    {
        return SaveFile.GetInstance().MetaData.language;
    }


    public string GetWord(string id)
    {
        foreach (XmlNode language in library)
        {
            if(language["Name"].InnerText == SaveFile.GetInstance().MetaData.language.ToString())
            {
                XmlNodeList dictionary = language.ChildNodes;
                foreach (XmlNode word in dictionary)
                {
                    if(word.Name == id)
                    {
                        return word.InnerText;
                    }
                }
            }
        }
        throw new NotImplementedException("ID: " + id + " Does not have a word implemented in the language " + SaveFile.GetInstance().MetaData.language.ToString());
    }
}
