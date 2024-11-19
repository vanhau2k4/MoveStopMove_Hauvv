using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public enum WeaponType 
{
    Hammer, HammerRed, HammerGreen, HammerBlue,
}
public class Weapon : MonoBehaviour
{
    public WeaponType wpType;
    public List<GameObject> weapons = new List<GameObject>();
    public Transform firePoint;
    // Start is called before the first frame update
    private GameObject currentWeapon;
    public void Shoot(CharactedBase characted)
    {
        currentWeapon = Instantiate(weapons[(int)wpType], firePoint.position, firePoint.rotation);
        Bullet bullet = currentWeapon.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Initialize(characted.target.Value, characted); // Gán chủ sở hữu cho đạn
        }
    }
}
