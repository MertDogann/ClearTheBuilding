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

    private void Awake()                //Burada a�a��daki onEnable metodu ile start metodunun ayn� anda �al��mas�ndan kaynakl� olan sorunu gidermek ama�l� starttan �nce �al��an awakeyi kulland�k.
    {
        currentAmmoCount = clipSize;

    }

    private void OnEnable()         //Bu metodu olu�turmam�z�n amac� bu scriptin ba�l� oldu�u gameobjenin sadece setactivitesi atkif oldu�u zaman �al���r. Kapal�yken �al��maz.
    {
        if (attack != null)         //Attack scripti bo� de�ilse.
        {
            attack.GetFireTransform = fireTransform;
            attack.GetFireRate = fireRate;
            attack.GetClipSize = clipSize;
            attack.GetAmmo = currentAmmoCount;
        }
    }
}
