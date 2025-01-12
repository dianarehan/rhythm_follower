using AYellowpaper.SerializedCollections;
using UnityEngine;
using System.Collections;

public class ArrowsGenerator : MonoBehaviour
{
    [SerializeField]private Transform arrowsParent;
    [SerializeField,SerializedDictionary] private SerializedDictionary<ArrowType,Arrow> arrowKeyValue;
    [SerializeField] private float tempoBPM = 120f;

    private void Start()=> StartCoroutine(GenerateArrowsBasedOnTempo());

    public void GenerateArrow(ArrowType arrowType) => Instantiate(arrowKeyValue[arrowType], arrowsParent);

    private IEnumerator GenerateArrowsBasedOnTempo()
    {
        float interval = 60f/tempoBPM;
        while (true)
        {
            GenerateRandomArrow();
            yield return new WaitForSeconds(interval);
        }
    }

    private void GenerateRandomArrow()
    {
        ArrowType[] arrowTypes = (ArrowType[])System.Enum.GetValues(typeof(ArrowType));
        ArrowType randomArrowType = arrowTypes[Random.Range(0, arrowTypes.Length)];
        GenerateArrow(randomArrowType);
    }
}