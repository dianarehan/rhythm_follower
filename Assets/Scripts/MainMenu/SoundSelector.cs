using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundSelector : MonoBehaviour
{
    [SerializeField] private SoundData[] sounds;
    [SerializeField] private TextMeshProUGUI soundNameText;
    [SerializeField] private Button left;
    [SerializeField] private Button right;

    private int currentSoundIndex = 0;
    public SoundData CurrentSound => sounds[currentSoundIndex];
    public static SoundSelector Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
        
    }

    private void Start()
    {
        UpdateSoundName();
        left.onClick.AddListener(OnLeftClick);
        right.onClick.AddListener(OnRightClick);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnLeftClick()
    {
        currentSoundIndex--;
        if (currentSoundIndex < 0)
        {
            currentSoundIndex = sounds.Length - 1;
        }
        UpdateSoundName();
    }

    public void OnRightClick() {
        currentSoundIndex++;
        if (currentSoundIndex >= sounds.Length)
        {
            currentSoundIndex = 0;
        }
        UpdateSoundName();
    }

    private void UpdateSoundName()=>
        soundNameText.text = sounds[currentSoundIndex].Clip.name;
    

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(SceneManager.GetActiveScene().name == "GamePlay")
        {
            AudioSource audioSource = FindAnyObjectByType<AudioSource>();
            audioSource.clip = CurrentSound.Clip;
            Debug.Log("SoundSelector: " + CurrentSound.Clip.name);
        }  
        left = GameObject.Find("LeftButton")?.GetComponent<Button>();
        right = GameObject.Find("RightButton")?.GetComponent<Button>();
        soundNameText = GameObject.Find("SoundName")?.GetComponent<TextMeshProUGUI>();
        left?.onClick.AddListener(OnLeftClick);
        right?.onClick.AddListener(OnRightClick);
        soundNameText?.SetText(CurrentSound.Clip.name);
    }
}