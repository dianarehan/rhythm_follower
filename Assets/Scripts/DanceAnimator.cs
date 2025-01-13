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
        //StartCoroutine(GetRandomDanceClip());
    }

    private IEnumerator GetRandomDanceClip()
    {
        // Wait for the current animation to finish
        yield return new WaitForSeconds(GetCurrentAnimationLength());

        // Pick a random dance clip
        int _newIdx = Random.Range(1, 11);
        animator.Play("Dance" + _newIdx.ToString());

        // Repeat the process
        StartCoroutine(GetRandomDanceClip());
    }

    private float GetCurrentAnimationLength()
    {
        // Get the current animation clip information from the animator
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // Return the length of the current animation
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