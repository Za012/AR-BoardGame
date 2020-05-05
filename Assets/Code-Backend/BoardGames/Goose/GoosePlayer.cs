using UnityEngine;

public class GoosePlayer : MonoBehaviour
{
    public int CurrentPosition { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Rotate(new Vector3(40,40,40) * Time.deltaTime);
    }
}
