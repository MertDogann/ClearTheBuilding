using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private int maxHealt = 2;
    private int currentHealt;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private GameObject dieEffect;

    public int GetHealt         //Burada bi proposition olu�turuyoruz. healt de�erini oku veya healt de�erini set ile de�i�tir gibi �eyleri d�zenlemi� oluyoruz.
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

    private void Awake()            //Atamalar�n oyun ba�lamadan �nce yap�lmas� gerekti�i i�in awakenin i�erisinde kulland�k.
    {
        currentHealt = maxHealt;
    }


    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Bullet bullet = other.gameObject.GetComponent<Bullet>();
        if (bullet) //Bize �arpan objenin i�erisinde bullet scripti varsa buray� �al��t�r.
        {
            if (bullet.owner != gameObject)     //E�er bullet scriptimizin i�erisindeki owner GameObjesi bizim targetimizin ba�l� oldu�u gameobjecte e�it de�ilse bu i�lemleri yap. E�itse zaten mermi bizden ��kt��� i�in bize zarar vermemesi i�in ifin i�erisini �al��t�rma diyoruz.
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
