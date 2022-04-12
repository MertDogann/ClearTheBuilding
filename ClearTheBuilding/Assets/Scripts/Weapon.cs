using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Attack attack;
    [SerializeField] private Transform fireTransform;
    [SerializeField] private float fireRate;
    [SerializeField] private int clipSize;
    private int currentAmmoCount;

    public int GetCurrentAmmoCount
    {
        get
        {
            return currentAmmoCount;
        }
        set
        {
            currentAmmoCount = value;
        }
    }

    private void Awake()                //Burada aþaðýdaki onEnable metodu ile start metodunun ayný anda çalýþmasýndan kaynaklý olan sorunu gidermek amaçlý starttan önce çalýþan awakeyi kullandýk.
    {
        currentAmmoCount = clipSize;

    }

    private void OnEnable()         //Bu metodu oluþturmamýzýn amacý bu scriptin baðlý olduðu gameobjenin sadece setactivitesi atkif olduðu zaman çalýþýr. Kapalýyken çalýþmaz.
    {
        if (attack != null)         //Attack scripti boþ deðilse.
        {
            attack.GetFireTransform = fireTransform;
            attack.GetFireRate = fireRate;
            attack.GetClipSize = clipSize;
            attack.GetAmmo = currentAmmoCount;
        }
    }
}
