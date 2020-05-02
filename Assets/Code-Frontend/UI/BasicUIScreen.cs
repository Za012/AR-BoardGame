using UnityEngine;

public abstract class BasicUIScreen : MonoBehaviour
{
    public abstract void FillText();
    public abstract void HandleError(string errorName);

    void Awake()
    {
        FillText();
    }
    public void Update()
    {
        IsUserReturning();
    }
    public virtual void IsUserReturning()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            UIManager.Instance.ActivateScreen(UIManager.Instance.initScreen);
        }
    }
}
