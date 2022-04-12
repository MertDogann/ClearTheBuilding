using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private GameObject[] weapons;          //De�i�tirilecek silahlar. 
    [SerializeField] private GameObject ammo;
    [SerializeField] private bool isPlayer = false;         //Burada farkl� script yazmamak i�in playeri unity �zerinden true hale getirdik.
    
    private GameManager gameManager;


    private Transform fireTransform;
    private int maxAmmoCount = 8;                            //Mermimizin say�s�n� s�n�rlad�k.
    private int ammoCount;                                  //Mevcut mermi
    private float fireRate = 0.5f;

    private float currentFireRate = 0f;

    public int GetAmmo                              //Burada bi proposition olu�turuyoruz.ammo de�erini oku veya ammo de�erini set ile de�i�tir gibi �eyleri d�zenlemi� oluyoruz.
    {
        get
        {
            return ammoCount;
        }
        set
        {
            ammoCount = value;
            if (ammoCount > maxAmmoCount)
            {
                ammoCount = maxAmmoCount;
            }
        }
    }

    public float GetCurrentFireRate         // Enemynin bize ate� etmesi i�in property haline getirdik.
    {
        get
        {
            return currentFireRate ;
        }
        set
        {
            currentFireRate = value;
        }
    }
    public int GetClipSize                 //Mermimizi yenilememiz i�in kullan�lan property.
    {
        get 
        {
            return maxAmmoCount;
        }
        set
        {
            maxAmmoCount = value;
        }
    }       

    public Transform GetFireTransform
    {
        get
        {
            return fireTransform;
        }
        set
        {
            fireTransform = value;
        }
    }
    public float GetFireRate
    {
        get
        {
            return fireRate;
        }
        set
        {
            fireRate = value;
        }
    }

    private void Awake()            //Atamalar�n oyun ba�lamadan �nce yap�lmas� gerekti�i i�in awake kulland�k.
    {
        ammoCount = maxAmmoCount;       //Mevcut mermimizi max mermiye e�itle oyun ba��nda.
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {

        if (currentFireRate > 0)
        {
            currentFireRate -= Time.deltaTime;
        }
        PlayerInput();

    }

    private void PlayerInput()
    {
        if (isPlayer && !gameManager.GetLevelFinished )       //Player true ise �al��t�r. Playeri de unity �zerinden aktif hale getirdik. E�er oyun bitmediyse.
        {

            if (Input.GetMouseButtonDown(0) && currentFireRate <= 0f && ammoCount > 0)
            {
                Fire();
            }

            switch (Input.inputString)          //  Silah de�i�imi i�in switch sistemi kullan�ld�. �ften fark� bulunmuyor.
            {
                case "1":
                    weapons[1].gameObject.GetComponent<Weapon>().GetCurrentAmmoCount = ammoCount;           //Mermilerimizi her silah de�i�ti�inde yenilenmemesi i�in mevcut ammomuzu kapanan silahta g�ncelliyoruz.
                    weapons[0].gameObject.SetActive(true);
                    weapons[1].gameObject.SetActive(false);
                    break;
                case "2":
                    weapons[0].gameObject.GetComponent<Weapon>().GetCurrentAmmoCount = ammoCount;
                    weapons[0].gameObject.SetActive(false);
                    weapons[1].gameObject.SetActive(true);
                    break;
            }
        }
    }

    public void Fire()
    {
        // Buradaki olay karakterin rotasyonu sa� y�ne bakarken yani y a��s� 0,90 derece aras�nda mermimizin tam olarak 90dereceye gitmesi , sola bakarken de 90,180 derece aras�nda mermimizin tam olarak 180dereceye gitmesi i�in if yap�s� olu�turuldu.
        float difference = 180f - transform.eulerAngles.y;
        float targetRotation = 90f;
        if (difference >= 90f)
        {
            targetRotation = 90f;
        }
        else if (difference <= 90f)
        {
            targetRotation = -90f;
        }
        ammoCount--; //Her fire metodu �a��r�ld���nda mermi say�m�z� azalt.
        currentFireRate += fireRate;
        GameObject bulletClone = Instantiate(ammo, fireTransform.position, Quaternion.Euler(0f, 0f, targetRotation)) ; // Mermi ate� edilecek pozisyondan istedi�imiz a��da gitmesi i�in yap�ld�. Yeni yaratt���m�z game objeyi bullet clone ad�nda yeni bir gameobjeye atad�k
        bulletClone.GetComponent<Bullet>().owner = gameObject; //Olu�turdu�umuz yeni clone bulletin i�erisinden bullet scriptine git ve ordaki ownergameobjesini attack�n bulundu�u gameobjene d�n��t�r.
    }

    public void FireEnemy()
    {
        // Buradaki olay karakterin rotasyonu sa� y�ne bakarken yani y a��s� 0,90 derece aras�nda mermimizin tam olarak 90dereceye gitmesi , sola bakarken de 90,180 derece aras�nda mermimizin tam olarak 180dereceye gitmesi i�in if yap�s� olu�turuldu.
        float difference = 180f - transform.eulerAngles.y;
        float targetRotation = 90f;
        if (difference <= 90f)
        {
            targetRotation = 90f;
        }
        else if (difference >= 90f)
        {
            targetRotation = -90f;
        }
        ammoCount--; //Her fire metodu �a��r�ld���nda mermi say�m�z� azalt.
        currentFireRate += fireRate;
        GameObject bulletClone = Instantiate(ammo, fireTransform.position, Quaternion.Euler(0f, 0f, targetRotation)); // Mermi ate� edilecek pozisyondan istedi�imiz a��da gitmesi i�in yap�ld�. Yeni yaratt���m�z game objeyi bullet clone ad�nda yeni bir gameobjeye atad�k
        bulletClone.GetComponent<Bullet>().owner = gameObject; //Olu�turdu�umuz yeni clone bulletin i�erisinden bullet scriptine git ve ordaki ownergameobjesini attack�n bulundu�u gameobjene d�n��t�r.
    }
}
