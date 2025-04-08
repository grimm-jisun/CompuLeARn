using UnityEngine;
using UnityEngine.UI;

public class DropdownToggle : MonoBehaviour
{
    public Button dropdownButton;
    public GameObject dropdownMenu;

    void Start()
    {
        dropdownMenu.SetActive(false); // Ensure the menu is hidden initially
        dropdownButton.onClick.AddListener(ToggleDropdownMenu);
    }

    void ToggleDropdownMenu()
    {
        dropdownMenu.SetActive(!dropdownMenu.activeSelf);
    }
}
