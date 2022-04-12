using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private int maxHealt = 2;
    private int currentHealt;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private GameObject dieEffect;

    public int GetHealt         //Burada bi proposition oluþturuyoruz. healt deðerini oku veya healt deðerini set ile deðiþtir gibi þeyleri düzenlemiþ oluyoruz.
    {
        get
        {
            return currentHealt;
        }
        set
        {
            currentHealt = value;
            if (currentHealt> maxHealt)
            {
                currentHealt = maxHealt;
            }
        }
    }
    public int GetMaksHealt
    {
        get
        {
            return maxHealt;
        }
        set
        {
            maxHealt = value;
        }
    }

    private void Awake()            //Atamalarýn oyun baþlamadan önce yapýlmasý gerektiði için awakenin içerisinde kullandýk.
    {
        currentHealt = maxHealt;
    }


    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Bullet bullet = other.gameObject.GetComponent<Bullet>();
        if (bullet) //Bize çarpan objenin içerisinde bullet scripti varsa burayý çalýþtýr.
        {
            if (bullet.owner != gameObject)     //Eðer bullet scriptimizin içerisindeki owner GameObjesi bizim targetimizin baðlý olduðu gameobjecte eþit deðilse bu iþlemleri yap. Eþitse zaten mermi bizden çýktýðý için bize zarar vermemesi için ifin içerisini çalýþtýrma diyoruz.
            {
                if (hitEffect != null)
                {
                    Instantiate(hitEffect, transform.position, Quaternion.identity);
                }
                currentHealt--;
                if (currentHealt <= 0)
                {
                    Die();
                }
                Destroy(other.gameObject);
            }
        }
    }
    private void Die()
    {
        if (dieEffect != null)
        {
            Instantiate(dieEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
