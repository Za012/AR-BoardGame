using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionScreen : BasicUIScreen
{
    public Text connectText;
    public Text loadingAnimation;
    private int dotCount;
    public override void FillText()
    {
        connectText.text = LanguageManager.Instance.GetWord(connectText.name);
        InvokeRepeating("Animation", 0.4f,0.4f);
    }

    public override void HandleError(string errorName)
    {
        throw new System.NotImplementedException("Error Name: " + errorName + " has not been handled");
    }
    public void Animation()
    {
        loadingAnimation.text += ".";
        if (dotCount >= 10)
        {
            loadingAnimation.text = ".";
            dotCount = 0;
        }
        dotCount++;
    }
}
