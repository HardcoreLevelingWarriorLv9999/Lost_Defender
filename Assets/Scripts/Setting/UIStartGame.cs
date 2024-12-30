using TMPro; // Thêm thư viện TextMeshPro
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Thêm thư viện SceneManagement
using System.IO;

public class UIStartGame : MonoBehaviour
{
    public TMP_InputField saveNameInputField;
    public GameObject start;
    public GameObject exit;
    public GameObject game1;
    public GameObject game2;
    public GameObject game3;
    public GameObject clean1;
    public GameObject clean2;
    public GameObject clean3;
    public TextMeshProUGUI warning;
    public TextMeshProUGUI textgame1;
    public TextMeshProUGUI textgame2;
    public TextMeshProUGUI textgame3;
    public Texture2D customCursorTexture;
    public Vector2 hotSpot = Vector2.zero;

    private bool updateEnabled = true; // Biến cờ để điều khiển hàm Update

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(customCursorTexture, hotSpot, CursorMode.Auto);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        start.SetActive(false);
        exit.SetActive(true);
        game1.SetActive(true);
        game2.SetActive(true);
        game3.SetActive(true);
        warning.gameObject.SetActive(false);
    }

    public void Game1()
    {
        AudioManager.Instance.PlaySFX("ClickButton");
        string gameData = PlayerPrefs.GetString("game1");
        if (string.IsNullOrEmpty(gameData))
        {
          
            exit.SetActive(false);
            game1.SetActive(false);
            game2.SetActive(false);
            clean1.SetActive(false);
            clean2.SetActive(false);
            clean3.SetActive(false);
            game3.SetActive(false);
            start.SetActive(true);
            PlayerPrefs.SetInt("input", 1);
            updateEnabled = false; // Dừng hàm Update
        }
        else
        {
            PlayerPrefs.SetString("FileGame", gameData);
            SceneManager.LoadScene("MenuScene");
        }
    }

    public void Game2()
    {
        AudioManager.Instance.PlaySFX("ClickButton");
        string gameData = PlayerPrefs.GetString("game2");
        if (string.IsNullOrEmpty(gameData))
        {
            exit.SetActive(false);
            game1.SetActive(false);
            game2.SetActive(false);
            clean1.SetActive(false);
            clean2.SetActive(false);
            clean3.SetActive(false);
            game3.SetActive(false);
            start.SetActive(true);
            PlayerPrefs.SetInt("input", 2);
            updateEnabled = false; // Dừng hàm Update
        }
        else
        {
            PlayerPrefs.SetString("FileGame", gameData);
            SceneManager.LoadScene("MenuScene");
        }
    }

    public void Game3()
    {
        AudioManager.Instance.PlaySFX("ClickButton");
        string gameData = PlayerPrefs.GetString("game3");
        if (string.IsNullOrEmpty(gameData))
        {
            exit.SetActive(false);
            game1.SetActive(false);
            game2.SetActive(false);
            clean1.SetActive(false);
            clean2.SetActive(false);
            clean3.SetActive(false);
            game3.SetActive(false);
            start.SetActive(true);
            PlayerPrefs.SetInt("input", 3);
            updateEnabled = false; // Dừng hàm Update
        }
        else
        {
            PlayerPrefs.SetString("FileGame", gameData);
            SceneManager.LoadScene("MenuScene");
        }
    }

    public void Clean1()
    {
        AudioManager.Instance.PlaySFX("ClickButton");
        string filegame = PlayerPrefs.GetString("game1");
        string path = Application.persistentDataPath + "/" + filegame + ".dat";
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("File deleted: " + path);
        }
        PlayerPrefs.SetString("game1", "");
        PlayerPrefs.Save();
    }

    public void Clean2()
    {
        AudioManager.Instance.PlaySFX("ClickButton");
        string filegame = PlayerPrefs.GetString("game2");
        string path = Application.persistentDataPath + "/" + filegame + ".dat";
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("File deleted: " + path);
        }
        PlayerPrefs.SetString("game2", "");
        PlayerPrefs.Save();
    }

    public void Clean3()
    {
        AudioManager.Instance.PlaySFX("ClickButton");
        string filegame = PlayerPrefs.GetString("game3");
        string path = Application.persistentDataPath + "/" + filegame + ".dat";
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("File deleted: " + path);
        }
        PlayerPrefs.SetString("game3", "");
        PlayerPrefs.Save();
    }

    public void SaveGameData()
    {
        AudioManager.Instance.PlaySFX("ClickButton");
        string inputText = saveNameInputField.text.Trim();
        if (string.IsNullOrEmpty(inputText))
        {
            ShowWarningMessage("Please don't leave it blank, please re-enter");
            return;
        }
        // Kiểm tra nếu dữ liệu trùng với game1, game2 hoặc game3
        if (inputText == PlayerPrefs.GetString("game1") || inputText == PlayerPrefs.GetString("game2") || inputText == PlayerPrefs.GetString("game3"))
        {
            ShowWarningMessage("The name already exists. Please choose another name.");
            return;
        }
        int input = PlayerPrefs.GetInt("input");
        if (input == 1)
        {
            PlayerPrefs.SetString("game1", inputText);
            PlayerPrefs.SetString("FileGame", inputText);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Newcharacter");
            Debug.Log("Saved text to game1: " + inputText);
        }
        else if (input == 2)
        {
            PlayerPrefs.SetString("game2", inputText);
            PlayerPrefs.SetString("FileGame", inputText);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Newcharacter");
            Debug.Log("Saved text to game2: " + inputText);
        }
        else if (input == 3)
        {
            PlayerPrefs.SetString("game3", inputText);
            PlayerPrefs.SetString("FileGame", inputText);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Newcharacter");
            Debug.Log("Saved text to game3: " + inputText);
        }
    }

    void ShowWarningMessage(string message)
    {
        warning.text = message;
        warning.gameObject.SetActive(true);
        Invoke("HideWarningMessage", 3f);
        // Tắt thông báo sau 3 giây
    }

    void HideWarningMessage()
    {
        warning.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!updateEnabled) return; // Dừng hàm Update nếu updateEnabled là false

        string gamedata1 = PlayerPrefs.GetString("game1");
        string gamedata2 = PlayerPrefs.GetString("game2");
        string gamedata3 = PlayerPrefs.GetString("game3");
        if (string.IsNullOrEmpty(gamedata1))
        {
            textgame1.text = "NEW GAME";
            clean1.SetActive(false);
        }
        else
        {
            textgame1.text = gamedata1;
            clean1.SetActive(true);
        }
        if (string.IsNullOrEmpty(gamedata2))
        {
            textgame2.text = "NEW GAME";
            clean2.SetActive(false);
        }
        else
        {
            textgame2.text = gamedata2;
            clean2.SetActive(true);
        }
        if (string.IsNullOrEmpty(gamedata3))
        {
            textgame3.text = "NEW GAME";
            clean3.SetActive(false);
        }
        else
        {
            textgame3.text = gamedata3;
            clean3.SetActive(true);
        }
    }
    public void ExitGame()
    {
        AudioManager.Instance.PlaySFX("ClickButton");
        Application.Quit();
    }
}

