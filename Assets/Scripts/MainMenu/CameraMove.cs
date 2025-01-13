using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform[] targets;
    [SerializeField] private float lerpSpeed = 0.01f;

    private int currentTargetIndex = 0;
    private float lerpTime = 0.0f;

    void Start()
    {
        if (targets.Length > 0)
        {
            transform.position = targets[0].position;
            transform.rotation = targets[0].rotation;
        }
    }

    void Update()
    {
        if (targets.Length > 1)
        {
            lerpTime += Time.deltaTime * lerpSpeed;
            transform.position = Vector3.Lerp(transform.position, targets[currentTargetIndex].position, lerpTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, targets[currentTargetIndex].rotation, lerpTime);

            if (lerpTime >= 1.0f)
            {
                lerpTime = 0.0f;
                currentTargetIndex = (currentTargetIndex + 1) % targets.Length;
            }
        }
    }
}