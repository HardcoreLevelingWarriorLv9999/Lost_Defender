using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class WeaponSwitcher : MonoBehaviour
{
    public int numberOfWeapons = 5;
    private int currentWeaponIndex = 0;

    // Tham chiếu tới các nút trên UI và Animator
    public Button[] weaponButtons;
    private Animator[] animators;
    private bool[] isSelected;

    // Danh sách các GameObject cha chứa các Item Icon (Image components)
    public GameObject[] parentGameObjects; // Các GameObject cha chứa các Item Icon
    public Image[] buttonIcons; // Các Image của các nút bấm để gán icon

    private List<Image> itemIcons; // Danh sách chứa các Image (Item Icons)

    private Sprite[] previousSprites; // Lưu trữ sprite trước đó để kiểm tra thay đổi

    void Start()
    {
        animators = new Animator[weaponButtons.Length];
        isSelected = new bool[weaponButtons.Length];
        itemIcons = new List<Image>();
        previousSprites = new Sprite[parentGameObjects.Length]; // Mảng để lưu sprite cũ

        // Lấy tất cả các Animator từ các Button
        for (int i = 0; i < weaponButtons.Length; i++)
        {
            animators[i] = weaponButtons[i].GetComponent<Animator>();
        }

        // Lấy hình ảnh từ các GameObject con có tên "Item Icon" trong các parentGameObjects
        foreach (GameObject parent in parentGameObjects)
        {
            Transform itemIconTransform = parent.transform.Find("Item Icon"); // Tìm "Item Icon" trong GameObject cha
            if (itemIconTransform != null)
            {
                Image itemIconImage = itemIconTransform.GetComponent<Image>(); // Lấy component Image
                if (itemIconImage != null)
                {
                    itemIcons.Add(itemIconImage); // Thêm Image vào danh sách itemIcons
                }
            }
        }

        // Gắn các hình ảnh vào các nút bấm (Icon) theo thứ tự
        for (int i = 0; i < buttonIcons.Length && i < itemIcons.Count; i++)
        {
            buttonIcons[i].sprite = itemIcons[i].sprite; // Gán hình ảnh (Sprite) từ Image vào Icon của nút bấm
            previousSprites[i] = itemIcons[i].sprite; // Lưu trữ sprite ban đầu
        }
    }

    void Update()
    {
        // Kiểm tra các phím từ 1 đến số lượng vũ khí để chuyển đổi vũ khí
        for (int i = 1; i <= numberOfWeapons; i++)
        {
            if (Input.GetKeyDown(i.ToString()))
            {
                SwitchWeapon(i - 1); // Chuyển đổi vũ khí
            }
        }

        // Kiểm tra xem hình ảnh có thay đổi trong các parentGameObjects không
        for (int i = 0; i < parentGameObjects.Length; i++)
        {
            Transform itemIconTransform = parentGameObjects[i].transform.Find("Item Icon");
            if (itemIconTransform != null)
            {
                Image itemIconImage = itemIconTransform.GetComponent<Image>();
                if (itemIconImage != null && itemIconImage.sprite != previousSprites[i])
                {
                    // Nếu sprite thay đổi, cập nhật lại sprite của buttonIcon tương ứng
                    buttonIcons[i].sprite = itemIconImage.sprite;

                    // Lưu sprite mới để so sánh lần sau
                    previousSprites[i] = itemIconImage.sprite;
                }
            }
        }
    }

    public void SwitchWeapon(int weaponIndex)
    {
        if (weaponIndex >= 0 && weaponIndex < numberOfWeapons)
        {
            // Tắt trạng thái "Selected" cho nút hiện tại
            if (currentWeaponIndex >= 0 && currentWeaponIndex < numberOfWeapons)
            {
                animators[currentWeaponIndex].SetBool("Selected", false);
                animators[currentWeaponIndex].SetTrigger("Normal");
                isSelected[currentWeaponIndex] = false;
            }

            // Chuyển đổi vũ khí
            currentWeaponIndex = weaponIndex;
            Debug.Log("Switched to weapon: " + weaponIndex);

            // Bật trạng thái "Selected" cho nút mới
            animators[currentWeaponIndex].SetBool("Selected", true);
            animators[currentWeaponIndex].SetTrigger("Pressed");
            isSelected[currentWeaponIndex] = true;
        }
        else
        {
            Debug.LogWarning("Weapon index out of range: " + weaponIndex);
        }
    }
}
