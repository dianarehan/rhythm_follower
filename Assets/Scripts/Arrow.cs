using UnityEngine;

[System.Serializable]
public class Arrow : MonoBehaviour
{
    [SerializeField] private ArrowType arrowType;
    [SerializeField] private float speed = 5f;

    public ArrowType ArrowType { get { return arrowType; } }
    public float Speed { get { return speed; } set { speed = value; } }

    void Update()
    {
        transform.Translate(0, -speed * Time.deltaTime, 0);
        if (transform.position.y < -10)
            Destroy(gameObject); 
    }
}