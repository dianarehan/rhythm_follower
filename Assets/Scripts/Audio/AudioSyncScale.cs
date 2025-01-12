using UnityEngine;
using System.Collections;

public class AudioSyncScale : AudioSyncer
{
    [SerializeField] private Vector3 beatScale;
    [SerializeField] private Vector3 restScale;

    private IEnumerator MoveToScale(Vector3 target)
    {
        Vector3 curr = transform.localScale;
        Vector3 initial = curr;
        float timer = 0;

        while (curr != target)
        {
            curr = Vector3.Lerp(initial, target, timer / timeToBeat);
            timer += Time.deltaTime;

            transform.localScale = curr;

            yield return null;
        }

        isBeat = false;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (isBeat) return;

        transform.localScale = Vector3.Lerp(transform.localScale, restScale, restSmoothTime * Time.deltaTime);
    }

    public override void OnBeat()
    {
        base.OnBeat();

        StopCoroutine("MoveToScale");
        StartCoroutine("MoveToScale", beatScale);
    }
}