using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class VRMenuSmall : MonoBehaviour
{
    private Canvas canvas;

    void Start()
    {
        CreateCanvas();
        CreateButton("Start Game", new Vector3(0, 0.1f, 0), Color.green, StartGame);
        CreateButton("Quit Game", new Vector3(0, -0.05f, 0), Color.red, QuitGame);
        CreateButton("Pause", new Vector3(0.15f, 0.2f, 0), Color.yellow, PauseGame);
    }

    void CreateCanvas()
    {
        GameObject canvasObj = new GameObject("VR_UI_Canvas");
        canvasObj.transform.SetParent(this.transform);
        canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;
        canvasObj.AddComponent<CanvasScaler>().scaleFactor = 1f;
        canvasObj.AddComponent<GraphicRaycaster>();
        canvasObj.transform.localScale = Vector3.one * 0.2f; // small UI
        // Place a bit in front of camera
        canvasObj.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 1.5f;
        canvasObj.transform.rotation = Quaternion.LookRotation(canvasObj.transform.position - Camera.main.transform.position);
    }

    void CreateButton(string text, Vector3 localPos, Color color, UnityEngine.Events.UnityAction action)
    {
        GameObject buttonObj = new GameObject(text + "_Button");
        buttonObj.transform.SetParent(canvas.transform);
        buttonObj.transform.localPosition = localPos;

        RectTransform rt = buttonObj.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(120, 40);

        Image img = buttonObj.AddComponent<Image>();
        img.color = color;

        Button btn = buttonObj.AddComponent<Button>();
        btn.onClick.AddListener(action);

        // Text
        GameObject txtObj = new GameObject("Text");
        txtObj.transform.SetParent(buttonObj.transform);
        RectTransform txtRt = txtObj.AddComponent<RectTransform>();
        txtRt.sizeDelta = rt.sizeDelta;
        txtRt.localPosition = Vector3.zero;

        TextMeshProUGUI tmp = txtObj.AddComponent<TextMeshProUGUI>();
        tmp.text = text;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.fontSize = 24;
        tmp.color = Color.white;
    }

    void StartGame()
    {
        Debug.Log("Start Game pressed");
        // SceneManager.LoadScene("YourSceneName"); // Replace with your main scene
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