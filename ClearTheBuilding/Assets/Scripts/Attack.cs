using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private GameObject[] weapons;          //Deðiþtirilecek silahlar. 
    [SerializeField] private GameObject ammo;
    [SerializeField] private bool isPlayer = false;         //Burada farklý script yazmamak için playeri unity üzerinden true hale getirdik.
    
    private GameManager gameManager;


    private Transform fireTransform;
    private int maxAmmoCount = 8;                            //Mermimizin sayýsýný sýnýrladýk.
    private int ammoCount;                                  //Mevcut mermi
    private float fireRate = 0.5f;

    private float currentFireRate = 0f;

    public int GetAmmo                              //Burada bi proposition oluþturuyoruz.ammo deðerini oku veya ammo deðerini set ile deðiþtir gibi þeyleri düzenlemiþ oluyoruz.
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

    public float GetCurrentFireRate         // Enemynin bize ateþ etmesi için property haline getirdik.
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
    public int GetClipSize                 //Mermimizi yenilememiz için kullanýlan property.
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

    private void Awake()            //Atamalarýn oyun baþlamadan önce yapýlmasý gerektiði için awake kullandýk.
    {
        ammoCount = maxAmmoCount;       //Mevcut mermimizi max mermiye eþitle oyun baþýnda.
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
        if (isPlayer && !gameManager.GetLevelFinished )       //Player true ise çalýþtýr. Playeri de unity üzerinden aktif hale getirdik. Eðer oyun bitmediyse.
        {

            if (Input.GetMouseButtonDown(0) && currentFireRate <= 0f && ammoCount > 0)
            {
                Fire();
            }

            switch (Input.inputString)          //  Silah deðiþimi için switch sistemi kullanýldý. Ýften farký bulunmuyor.
            {
                case "1":
                    weapons[1].gameObject.GetComponent<Weapon>().GetCurrentAmmoCount = ammoCount;           //Mermilerimizi her silah deðiþtiðinde yenilenmemesi için mevcut ammomuzu kapanan silahta güncelliyoruz.
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
        // Buradaki olay karakterin rotasyonu sað yöne bakarken yani y açýsý 0,90 derece arasýnda mermimizin tam olarak 90dereceye gitmesi , sola bakarken de 90,180 derece arasýnda mermimizin tam olarak 180dereceye gitmesi için if yapýsý oluþturuldu.
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
        ammoCount--; //Her fire metodu çaðýrýldýðýnda mermi sayýmýzý azalt.
        currentFireRate += fireRate;
        GameObject bulletClone = Instantiate(ammo, fireTransform.position, Quaternion.Euler(0f, 0f, targetRotation)) ; // Mermi ateþ edilecek pozisyondan istediðimiz açýda gitmesi için yapýldý. Yeni yarattýðýmýz game objeyi bullet clone adýnda yeni bir gameobjeye atadýk
        bulletClone.GetComponent<Bullet>().owner = gameObject; //Oluþturduðumuz yeni clone bulletin içerisinden bullet scriptine git ve ordaki ownergameobjesini attackýn bulunduðu gameobjene dönüþtür.
    }

    public void FireEnemy()
    {
        // Buradaki olay karakterin rotasyonu sað yöne bakarken yani y açýsý 0,90 derece arasýnda mermimizin tam olarak 90dereceye gitmesi , sola bakarken de 90,180 derece arasýnda mermimizin tam olarak 180dereceye gitmesi için if yapýsý oluþturuldu.
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
        ammoCount--; //Her fire metodu çaðýrýldýðýnda mermi sayýmýzý azalt.
        currentFireRate += fireRate;
        GameObject bulletClone = Instantiate(ammo, fireTransform.position, Quaternion.Euler(0f, 0f, targetRotation)); // Mermi ateþ edilecek pozisyondan istediðimiz açýda gitmesi için yapýldý. Yeni yarattýðýmýz game objeyi bullet clone adýnda yeni bir gameobjeye atadýk
        bulletClone.GetComponent<Bullet>().owner = gameObject; //Oluþturduðumuz yeni clone bulletin içerisinden bullet scriptine git ve ordaki ownergameobjesini attackýn bulunduðu gameobjene dönüþtür.
    }
}
