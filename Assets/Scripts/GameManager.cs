using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform arrowsParent;
    [SerializeField] float allowedDistance = 50;

    public UnityEvent OnArrowHit = new UnityEvent();
    public UnityEvent OnArrowMiss = new UnityEvent();

    List<Arrow> arrows = new List<Arrow>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            CheckIdxArrow(0, ArrowType.Up);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            CheckIdxArrow(1, ArrowType.Down);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            CheckIdxArrow(2, ArrowType.Left);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            CheckIdxArrow(3, ArrowType.Right);
        }
    }

    void GetAllArrows()
    {
        arrows.Clear();
        for (int i = 3; i < arrowsParent.childCount; i++)
        {
            arrows.Add(arrowsParent.GetChild(i).GetComponent<Arrow>());
        }
    }



    void CheckIdxArrow(int _idx, ArrowType _arrowType)
    {
        GetAllArrows();

        foreach (Arrow arrow in arrows)
        {
            if (arrow.ArrowType == _arrowType)
            {
                float _dis = arrowsParent.GetChild(_idx).position.y - arrow.transform.position.y;
                if (_dis < allowedDistance)
                {
                    OnArrowHit.Invoke();
                    Debug.Log("Hit");
                    return;
                }
            }
        }
        Debug.Log("Miss");
        OnArrowMiss.Invoke();
    }
}
