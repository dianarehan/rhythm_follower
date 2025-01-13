using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color hoverColor = new Color(251, 255,113);

    private TextMeshProUGUI textMeshPro;
    private float sizeMultiplier = 1.1f;

    private void Awake()
    {
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        textMeshPro.color = hoverColor;
        textMeshPro.gameObject.transform.localScale *= sizeMultiplier;
        gameObject.transform.localScale *= sizeMultiplier;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textMeshPro.color = normalColor;
        textMeshPro.gameObject.transform.localScale /= sizeMultiplier;
        gameObject.transform.localScale /= sizeMultiplier;
    }
}