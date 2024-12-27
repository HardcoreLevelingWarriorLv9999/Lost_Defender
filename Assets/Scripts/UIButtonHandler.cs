using UnityEngine;
using UnityEngine.UI;

public class UIButtonHandler : MonoBehaviour
{
    public void OnButtonClicked(int weaponIndex)
    {
        // Logic để chuyển đổi vũ khí ở đây
        Debug.Log("Button clicked! Switching to weapon: " + weaponIndex);
        // Gọi hàm SwitchWeapon từ script WeaponSwitcher
        FindObjectOfType<WeaponSwitcher>().SwitchWeapon(weaponIndex);
    }
}
