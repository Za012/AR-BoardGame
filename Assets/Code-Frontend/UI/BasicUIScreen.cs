using UnityEngine;

public abstract class BasicUIScreen : MonoBehaviour
{
    public abstract void FillText();
    public abstract void HandleError(string errorName);

    void Awake()
    {
        FillText();
    }
}
