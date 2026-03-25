using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ProMenuWithSound : MonoBehaviour
{
    private AudioSource audioSource;

    void Awake()
    {
        CreateEverything();
        CreateAudio();
    }

    void CreateAudio()
    {
        GameObject audioObj = new GameObject("Audio");
        audioSource = audioObj.AddComponent<AudioSource>();
    }

    void CreateEverything()
    {
        // EVENT SYSTEM
        if (FindObjectOfType<EventSystem>() == null)
        {
            GameObject es = new GameObject("EventSystem");
            es.AddComponent<EventSystem>();
            es.AddComponent<StandaloneInputModule>();
        }

        // CANVAS
        GameObject canvasObj = new GameObject("Canvas");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        canvasObj.AddComponent<CanvasScaler>();
        canvasObj.AddComponent<GraphicRaycaster>();

        // BACKGROUND
        GameObject bg = new GameObject("Background");
        bg.transform.SetParent(canvasObj.transform);

        Image bgImg = bg.AddComponent<Image>();
        bgImg.color = new Color(0.05f, 0.05f, 0.08f, 1f);

        RectTransform bgRT = bg.GetComponent<RectTransform>();
        bgRT.anchorMin = Vector2.zero;
        bgRT.anchorMax = Vector2.one;
        bgRT.offsetMin = Vector2.zero;
        bgRT.offsetMax = Vector2.zero;

        // TITLE
        CreateText(canvasObj.transform, "MY GAME", new Vector2(0, 180), 42, Color.white);

        // BUTTONS
        CreateButton(canvasObj.transform, "START", new Vector2(0, 30), new Color(0.2f, 0.8f, 0.3f), StartGame);
        CreateButton(canvasObj.transform, "QUIT", new Vector2(0, -50), new Color(0.9f, 0.2f, 0.2f), QuitGame);
    }

    void CreateButton(Transform parent, string text, Vector2 position, Color color, UnityEngine.Events.UnityAction action)
    {
        GameObject btn = new GameObject(text);
        btn.transform.SetParent(parent);

        Image img = btn.AddComponent<Image>();
        img.color = color;

        Button button = btn.AddComponent<Button>();

        RectTransform rt = btn.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(260, 70);
        rt.anchoredPosition = position;

        // TEXT
        CreateText(btn.transform, text, Vector2.zero, 24, Color.white);

        // CLICK SOUND + ACTION
        button.onClick.AddListener(() => PlayClickSound());
        button.onClick.AddListener(action);
    }

    void CreateText(Transform parent, string text, Vector2 position, int size, Color color)
    {
        GameObject tObj = new GameObject("Text");
        tObj.transform.SetParent(parent);

        Text t = tObj.AddComponent<Text>();
        t.text = text;
        t.fontSize = size;
        t.color = color;
        t.alignment = TextAnchor.MiddleCenter;
        t.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");

        RectTransform rt = tObj.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(300, 80);
        rt.anchoredPosition = position;
    }

    void PlayClickSound()
    {
        if (audioSource == null) return;

        // Simple built-in beep (no external file needed)
        audioSource.PlayOneShot(CreateBeep());
    }

    AudioClip CreateBeep()
    {
        int frequency = 1000;
        int sampleRate = 44100;
        float duration = 0.1f;

        AudioClip clip = AudioClip.Create("beep", (int)(sampleRate * duration), 1, sampleRate, false);
        float[] samples = new float[(int)(sampleRate * duration)];

        for (int i = 0; i < samples.Length; i++)
        {
            samples[i] = Mathf.Sin(2 * Mathf.PI * frequency * i / sampleRate) * 0.3f;
        }

        clip.SetData(samples, 0);
        return clip;
    }

    void StartGame()
    {
        Debug.Log("START CLICKED");
        SceneManager.LoadScene("GameScene");
    }

    void QuitGame()
    {
        Debug.Log("QUIT CLICKED");
        Application.Quit();
    }
}