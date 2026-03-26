using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class VRMenuCreator : MonoBehaviour
{
    private Canvas canvas;

    void Start()
    {
        CreateCanvas();
        CreatePanel();
        CreateTitle();
        CreateButton("Start Game", new Vector2(0, -50), Color.green, StartGame);
        CreateButton("Quit Game", new Vector2(0, -120), Color.red, QuitGame);
        CreateButton("Pause", new Vector2(150, 100), Color.yellow, PauseGame);
    }

    void CreateCanvas()
    {
        GameObject canvasObj = new GameObject("VR_UI_Canvas");
        canvasObj.transform.SetParent(this.transform);
        canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;
        canvasObj.AddComponent<CanvasScaler>().scaleFactor = 100f;
        canvasObj.AddComponent<GraphicRaycaster>();
        canvas.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 300);

        // Position in front of camera
        canvasObj.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2f;
        canvasObj.transform.rotation = Quaternion.LookRotation(canvasObj.transform.position - Camera.main.transform.position);
        canvasObj.transform.localScale = Vector3.one * 0.01f;
    }

    void CreatePanel()
    {
        GameObject panelObj = new GameObject("Background_Panel");
        panelObj.transform.SetParent(canvas.transform);
        RectTransform rt = panelObj.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(400, 300);
        panelObj.AddComponent<CanvasRenderer>();
        Image img = panelObj.AddComponent<Image>();
        img.color = new Color(0.2f, 0.6f, 0.9f, 0.8f); // soft blue
        rt.localPosition = Vector3.zero;
    }

    void CreateTitle()
    {
        GameObject titleObj = new GameObject("Title_Text");
        titleObj.transform.SetParent(canvas.transform);
        RectTransform rt = titleObj.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(400, 60);
        rt.localPosition = new Vector3(0, 100, 0);
        TextMeshProUGUI tmp = titleObj.AddComponent<TextMeshProUGUI>();
        tmp.text = "VR Escape Room Game";
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.fontSize = 50;
        tmp.color = Color.white;
    }

    void CreateButton(string text, Vector2 position, Color color, UnityEngine.Events.UnityAction action)
    {
        GameObject buttonObj = new GameObject(text + "_Button");
        buttonObj.transform.SetParent(canvas.transform);
        RectTransform rt = buttonObj.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(160, 50);
        rt.localPosition = new Vector3(position.x, position.y, 0);

        Button button = buttonObj.AddComponent<Button>();
        Image img = buttonObj.AddComponent<Image>();
        img.color = color;

        button.onClick.AddListener(action);

        // Add Text
        GameObject textObj = new GameObject("Text");
        textObj.transform.SetParent(buttonObj.transform);
        RectTransform textRt = textObj.AddComponent<RectTransform>();
        textRt.sizeDelta = rt.sizeDelta;
        textRt.localPosition = Vector3.zero;

        TextMeshProUGUI tmp = textObj.AddComponent<TextMeshProUGUI>();
        tmp.text = text;
        tmp.fontSize = 30;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.color = Color.white;
    }

    // Button Callbacks
    void StartGame()
    {
        Debug.Log("Start Game pressed");
        // SceneManager.LoadScene("YourSceneName"); // uncomment and set your scene name
    }

    void QuitGame()
    {
        Debug.Log("Quit Game pressed");
        Application.Quit();
    }

    void PauseGame()
    {
        Time.timeScale = (Time.timeScale == 0) ? 1 : 0;
        Debug.Log("Pause toggled. TimeScale = " + Time.timeScale);
    }
}