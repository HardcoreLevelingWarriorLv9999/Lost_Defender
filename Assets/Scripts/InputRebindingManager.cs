using UnityEngine;
using UnityEngine.InputSystem;

public class InputRebindingManager : MonoBehaviour
{
    public static InputRebindingManager Instance { get; private set; }
    public InputActionAsset actions;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Đảm bảo rằng đối tượng không bị hủy khi chuyển scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        LoadRebinds();
    }

    private void OnDisable()
    {
        SaveRebinds();
    }

    public void SaveRebinds()
    {
        var rebinds = actions.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("rebinds", rebinds);
    }

    public void LoadRebinds()
    {
        var rebinds = PlayerPrefs.GetString("rebinds");
        if (!string.IsNullOrEmpty(rebinds))
        {
            actions.LoadBindingOverridesFromJson(rebinds);
        }
    }
}
