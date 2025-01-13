using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color hoverColor = new Color(251, 255,113);
    [SerializeField] private float sizeMultiplier = 1.1f;
    [SerializeField] private AudioClip clickSound;

    private TextMeshProUGUI textMeshPro;
    private AudioSource audioSource;

    private void Awake()
    {
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        audioSource = FindAnyObjectByType<AudioSource>();
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

    public void PlayClickSound() => audioSource.PlayOneShot(clickSound);
}