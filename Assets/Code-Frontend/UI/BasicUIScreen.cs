using UnityEngine;

public abstract class BasicUIScreen : MonoBehaviour
{

    public abstract void FillText();

    void Awake()
    {
        FillText();
    }
}
