using UnityEngine;

public class ChangeColorCamera : MonoBehaviour
{
    [SerializeField] Color color;
    [SerializeField] private AnimationCurve speedOverTime;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Camera.main.backgroundColor = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.backgroundColor = color;
    }
}
