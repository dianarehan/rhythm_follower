using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] ArrowType arrowType;
    [SerializeField] float speed = 5f;

    public ArrowType ArrowType { get { return arrowType; } }
    public float Speed { get { return speed; } set { speed = value; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -speed * Time.deltaTime, 0);
    }

    
}

public enum ArrowType
{
    Up,
    Down,
    Left,
    Right,
}