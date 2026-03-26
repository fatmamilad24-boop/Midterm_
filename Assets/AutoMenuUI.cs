using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AutoMenuUI : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        CreateUI();
    }

    void CreateUI()
    {
        // Canvas
        GameObject canvasObj = new GameObject("Canvas");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObj.AddComponent<CanvasScaler>();
        canvasObj.AddComponent<GraphicRaycaster>();

        // Panel
        GameObject panelObj = new GameObject("Panel");
        panelObj.transform.SetParent(canvas.transform);
        Image panel = panelObj.AddComponent<Image>();
        panel.color = new Color(0, 0, 0, 0.7f);
        RectTransform panelRect = panelObj.GetComponent<RectTransform>();
        panelRect.sizeDelta = new Vector2(300, 200);
        panelRect.anchoredPosition = Vector2.zero;

        // Start Button
        CreateButton(panelObj.transform, "Start Game", new Vector2(0, 30), StartGame);

        // Quit Button
        CreateButton(panelObj.transform, "Quit", new Vector2(0, -40), QuitGame);
    }

    void CreateButton(Transform parent, string text, Vector2 pos, UnityEngine.Events.UnityAction action)
    {
        GameObject btnObj = new GameObject(text);
        btnObj.transform.SetParent(parent);

        Image img = btnObj.AddComponent<Image>();
        img.color = Color.gray;

        Button btn = btnObj.AddComponent<Button>();
        btn.onClick.AddListener(action);

        RectTransform rect = btnObj.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(180, 40);
        rect.anchoredPosition = pos;

        // Text
        GameObject txtObj = new GameObject("Text");
        txtObj.transform.SetParent(btnObj.transform);

        Text txt = txtObj.AddComponent<Text>();
        txt.text = text;
        txt.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf"); // <-- FIXED
        txt.alignment = TextAnchor.MiddleCenter;
        txt.color = Color.white;
        txt.fontSize = 16;

        RectTransform txtRect = txtObj.GetComponent<RectTransform>();
        txtRect.sizeDelta = new Vector2(180, 40);
        txtRect.anchoredPosition = Vector2.zero;
    }

    void StartGame()
    {
        SceneManager.LoadScene("Demo 2 - Office Set 1"); // Make sure scene is in Build Settings
    }

    void QuitGame()
    {
        Application.Quit();
    }
}