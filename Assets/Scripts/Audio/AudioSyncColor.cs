using UnityEngine;
using System.Collections;

public class AudioSyncColor : AudioSyncer
{
    [SerializeField] private Color[] beatColors;
    [SerializeField] private Color restColor;

    private int m_randomIndx;


    private IEnumerator MoveToColor(Color target)
    {
        Color curr = GetComponent<Renderer>().material.color;
        Color initial = curr;
        float timer = 0;

        while (curr != target)
        {
            curr = Color.Lerp(initial, target, timer / timeToBeat);
            timer += Time.deltaTime;

            GetComponent<Renderer>().material.color = curr;

            yield return null;
        }

        isBeat = false;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (isBeat) return;

        GetComponent<Renderer>().material.color = Color.Lerp(GetComponent<Renderer>().material.color, restColor, restSmoothTime * Time.deltaTime);
    }

    public override void OnBeat()
    {
        base.OnBeat();

        m_randomIndx = Random.Range(0, beatColors.Length);
        StopCoroutine("MoveToColor");
        StartCoroutine("MoveToColor", beatColors[m_randomIndx]);
    }
}