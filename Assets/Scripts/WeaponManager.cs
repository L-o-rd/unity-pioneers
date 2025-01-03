using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public List<Weapon> availableWeapons; // List of available weapons
    public PlayerShooting playerShooting; // Reference to the player's Shooting script
    private int currentWeaponIndex;

    void Start()
    {
        LoadWeaponSelection(); // Load the last saved weapon index
        EquipWeapon(currentWeaponIndex); // Equip the loaded or default weapon
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) // Switch weapon with the Q key
        {
            Debug.Log("Q pressed, switching weapon...");
            SwitchWeapon();
        }
    }

    public void EquipWeapon(int index)
    {
        if (index >= 0 && index < availableWeapons.Count)
        {
            currentWeaponIndex = index;
            playerShooting.EquipWeapon(availableWeapons[currentWeaponIndex]);
            SaveWeaponSelection(); // Save the weapon index after equipping
            // Debug.Log($"Equipped weapon: {availableWeapons[currentWeaponIndex].weaponName}");
        }
        else
        {
            Debug.LogWarning("Invalid weapon index!");
        }
    }

    public void SwitchWeapon()
    {
        currentWeaponIndex = (currentWeaponIndex + 1) % availableWeapons.Count;
        EquipWeapon(currentWeaponIndex);
    }

    private void SaveWeaponSelection()
    {
        PlayerPrefs.SetInt("SelectedWeaponIndex", currentWeaponIndex);
        PlayerPrefs.Save();
        // Debug.Log($"Weapon index {currentWeaponIndex} saved.");
    }

    private void LoadWeaponSelection()
    {
        // Default to 0 if no value has been saved before
        currentWeaponIndex = PlayerPrefs.GetInt("SelectedWeaponIndex", 0);

        // Validate the loaded index
        if (currentWeaponIndex < 0 || currentWeaponIndex >= availableWeapons.Count)
        {
            currentWeaponIndex = 0;
        }

        // Debug.Log($"Loaded weapon index: {currentWeaponIndex}");
    }
}
