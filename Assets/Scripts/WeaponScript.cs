using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapons/Weapon")]
public class Weapon : ScriptableObject
{
    public string weaponName;         // Name of the weapon
    public string bulletType;         // Bullet type (tag for ObjectPool)
    public float bulletForce;         // Force applied to the bullet
    public float fireRate;            // Rate of fire (bullets per second)
    public Sprite weaponSprite;       // Sprite for the weapon in UI or as a visual
    public Sprite gunSprite;          // Sprite for the gun (used to change the gun's appearance)
}
