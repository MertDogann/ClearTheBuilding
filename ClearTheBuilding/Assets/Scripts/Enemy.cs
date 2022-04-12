using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    [SerializeField] private Transform[] patrol;
    [SerializeField] private float speed;
    [SerializeField] private float shootRange = 5f;
    [SerializeField] private LayerMask shootLayer;
    [SerializeField] private float reloadTime = 5f;
    private Transform aimTransform;
    private Attack attack;
    private bool isReloaded = false;
    

    private bool canMoveRight;

    private void Awake()            //Atamalar�n oyun ba�lamadan �nce yap�lmas� i�in awake i�erisinde kulland�k.
    {
        attack = GetComponent<Attack>();
        aimTransform = attack.GetFireTransform;
    }


    void Update()
    {
        EnemyAttack();
        CheckCanMoveRight();
        MoveTowards();
        Aim();
    }

    private void Reload()       //Enemy mermi toplayamad��� i�in mermisinin yenilemesi i�in kod yazd�k.
    {
        attack.GetAmmo = attack.GetClipSize;
        isReloaded = false;
    }
    private void EnemyAttack()      
    {
        if (attack.GetAmmo <= 0 && isReloaded == false)     //Ammo bittikten sonra s�rekli olarak ayn� komutu �a��rmamas� i�in isreloaded diye bir de�i�ken olu�turuyoruz.
        {
            Invoke("Reload", reloadTime);           // Reload konumutunu reloat time kadar sonra yap dedim.
            isReloaded = true;                      
        }

        if (attack.GetCurrentFireRate <= 0f && attack.GetAmmo > 0&& Aim())            //Enemy bize do�ru olan ate� etme alan�na girersek ve mermi 0 dan b�y�kse istedi�imi s�rede ate� et.
        {
            attack.FireEnemy();
        }
    }

    private bool Aim()          //Enemynin karakteri g�rd��� s�ra tetiklenmesi i�in olu�turdu�umuz kod blo�u.
    {
        if (aimTransform == null)
        {
            aimTransform = attack.GetFireTransform;         //Sahne tekrar yenilendi�i zaman aimtransformun firetransforumu bulmas�ndan kaynakl� olabilecek sorunu �nlemi� olduk.
        }
        bool hit = Physics.Raycast(aimTransform.position, transform.forward, shootRange, shootLayer);        //Raycast i�lemi ���n �izmek i�in raycast sistemi olu�turuldu.
        Debug.DrawLine(aimTransform.position, transform.forward * shootRange, Color.blue);                 //�izilen g�r�nmez ���n� scene i�erisinden g�r�n�r hale getirdik.
        return hit;

    }
    private void MoveTowards()
    {
        if (Aim()&& attack.GetAmmo >0)          //Enemynin ate� ederken ayn� anda hareket etmemesi i�in b�yle bi return d�nd�r�ld�.
        {
            return;
        }

        if (canMoveRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(patrol[1].position.x , transform.position.y , patrol[1].position.z), speed * Time.deltaTime);         //�stenilen noktaya oldu�umuz konumdan hangi h�zda gidece�imizi yaz�yoruz.
            LookAtTheTarget(patrol[1].position);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(patrol[0].position.x , transform.position.y , patrol[0].position.z), speed * Time.deltaTime);
            LookAtTheTarget(patrol[0].position);
        }
    }

    private void CheckCanMoveRight()            //Enemyi oyun i�erisindeki s�n�rlad���m�z yerlerde konumland�rmak i�in konumlara belirli mesafaeden fazla yakla��rlarsa bool de�i�keni ile di�er notkya do�ru gitmesini sa�l�yoruz.
    {
        if (Vector3.Distance(transform.position, patrol[0].position) <= 0.1f)
        {
            canMoveRight = true;
        }
        else if (Vector3.Distance(transform.position,patrol[1].position) <= 0.1f)
        {
            canMoveRight = false;
        }
    }

    private void LookAtTheTarget(Vector3 newTarget)             //Enemynin hareket ederken hedefe do�ru bakmas� i�in.
    {
        Vector3 newLookPosition = new Vector3(newTarget.x, transform.position.y, newTarget.z);
        Quaternion targetRotation = Quaternion.LookRotation(newTarget - transform.position);            //Burada sadece hedefin rotation de�erlerini baz ald�k.
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, speed * Time.deltaTime);          // Enemy targetin rotasyonunda olmas� gerekti�ini s�yledik.
    } 
}
