using UnityEngine;

public class CharacterManagement : MonoBehaviour
{
    public GameObject character1, character2, character3, character4;
    [SerializeField] string map;
    void Awake()
    {
        Character();
    }
    // Start is called before the first frame update
    void Start()
    {
     
        AudioManager.Instance.PlayMusic(map);

    }

    // Update is called once per frame
    void Character()
    {
        SaveLoadManager.PlayerData data = SaveLoadManager.LoadData();

        if (data == null)
        {
            Debug.LogError("Không thể tải dữ liệu người chơi!");
            return;
        }

        int Characternumber = data.characternumber;
        Debug.Log("Mã nhân vật: " + Characternumber);

        switch (Characternumber)
        {
            case 1:
                character1.SetActive(true);
                character2.SetActive(false);
                character3.SetActive(false);
                character4.SetActive(false);
                Debug.LogError("Mã nhân vật hợp lệ: " + Characternumber);
                break;
            case 2:
                character1.SetActive(false);
                character2.SetActive(true);
                character3.SetActive(false);
                character4.SetActive(false);
                Debug.LogError("Mã nhân vật hợp lệ: " + Characternumber);
                break;
            case 3:
                character1.SetActive(false);
                character2.SetActive(false);
                character3.SetActive(true);
                character4.SetActive(false);
                Debug.LogError("Mã nhân vật hợp lệ: " + Characternumber);
                break;
            case 4:
                character1.SetActive(false);
                character2.SetActive(false);
                character3.SetActive(false);
                character4.SetActive(true);
                Debug.LogError("Mã nhân vật hợp lệ: " + Characternumber);
                break;
            default:
                Debug.LogError("Mã nhân vật không hợp lệ: " + Characternumber);
                break;
        }
    }
}
