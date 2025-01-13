using System.Collections;
using UnityEngine;

public class DanceAnimator : MonoBehaviour
{
    private Animator animator;

    private float curSpeed = 1f;
    private float changePerHit = 0.05f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private IEnumerator GetRandomDanceClip()
    {
        yield return new WaitForSeconds(GetCurrentAnimationLength());

        int _newIdx = Random.Range(1, 11);
        animator.Play("Dance" + _newIdx.ToString());

        StartCoroutine(GetRandomDanceClip());
    }

    private float GetCurrentAnimationLength()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        return stateInfo.length - stateInfo.normalizedTime % 1 * stateInfo.length;
    }

    public void OnArrowHit()
    {
        curSpeed += changePerHit;
        curSpeed = Mathf.Clamp(curSpeed, 0.1f, 1.5f);
        animator.speed = curSpeed;
    }

    public void OnArrowMiss()
    {
        curSpeed -= changePerHit;
        curSpeed = Mathf.Clamp(curSpeed, 0.1f, 1.5f);
        animator.speed = curSpeed;
    }
}