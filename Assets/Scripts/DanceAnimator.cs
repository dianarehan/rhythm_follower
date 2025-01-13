using System.Collections;
using UnityEngine;

public class DanceAnimator : MonoBehaviour
{
    private Animator animator;

    private float curSpeed;
    private float changePerHit = 0.05f;

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(GetRandomDanceClip());
    }

    private IEnumerator GetRandomDanceClip()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);

        int _newIdx = Random.Range(1, 11);
        animator.Play("Dance" + _newIdx.ToString());
        GetRandomDanceClip();
    }

    public void OnArrowHit()
    {
        curSpeed += changePerHit;
        curSpeed = Mathf.Clamp(curSpeed, 0f, 2f);
        animator.speed += curSpeed;
    }

    public void OnArrowMiss()
    {
        curSpeed -= changePerHit;
        curSpeed = Mathf.Clamp(curSpeed, 0f, 2f);
        animator.speed = curSpeed;
    }
}