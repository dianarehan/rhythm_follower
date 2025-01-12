using UnityEngine;

public class ArrowsGenerator : MonoBehaviour
{
    [SerializeField] Transform arrowsParent;
    [SerializeField] GameObject UpArrow;
    [SerializeField] GameObject DownArrow;
    [SerializeField] GameObject LeftArrow;
    [SerializeField] GameObject RightArrow;


    public void GenerateUpArrow()
    {
        Instantiate(UpArrow, arrowsParent);
    }

    public void GenerateDownArrow()
    {
        Instantiate(DownArrow, arrowsParent);
    }

    public void GenerateLeftArrow()
    {
        Instantiate(LeftArrow, arrowsParent);
    }

    public void GenerateRightArrow()
    {
        Instantiate(RightArrow, arrowsParent);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
