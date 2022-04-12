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

    private void Awake()            //Atamalarýn oyun baþlamadan önce yapýlmasý için awake içerisinde kullandýk.
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

    private void Reload()       //Enemy mermi toplayamadýðý için mermisinin yenilemesi için kod yazdýk.
    {
        attack.GetAmmo = attack.GetClipSize;
        isReloaded = false;
    }
    private void EnemyAttack()      
    {
        if (attack.GetAmmo <= 0 && isReloaded == false)     //Ammo bittikten sonra sürekli olarak ayný komutu çaðýrmamasý için isreloaded diye bir deðiþken oluþturuyoruz.
        {
            Invoke("Reload", reloadTime);           // Reload konumutunu reloat time kadar sonra yap dedim.
            isReloaded = true;                      
        }

        if (attack.GetCurrentFireRate <= 0f && attack.GetAmmo > 0&& Aim())            //Enemy bize doðru olan ateþ etme alanýna girersek ve mermi 0 dan büyükse istediðimi sürede ateþ et.
        {
            attack.FireEnemy();
        }
    }

    private bool Aim()          //Enemynin karakteri gördüðü sýra tetiklenmesi için oluþturduðumuz kod bloðu.
    {
        if (aimTransform == null)
        {
            aimTransform = attack.GetFireTransform;         //Sahne tekrar yenilendiði zaman aimtransformun firetransforumu bulmasýndan kaynaklý olabilecek sorunu önlemiþ olduk.
        }
        bool hit = Physics.Raycast(aimTransform.position, transform.forward, shootRange, shootLayer);        //Raycast iþlemi ýþýn çizmek için raycast sistemi oluþturuldu.
        Debug.DrawLine(aimTransform.position, transform.forward * shootRange, Color.blue);                 //Çizilen görünmez ýþýný scene içerisinden görünür hale getirdik.
        return hit;

    }
    private void MoveTowards()
    {
        if (Aim()&& attack.GetAmmo >0)          //Enemynin ateþ ederken ayný anda hareket etmemesi için böyle bi return döndürüldü.
        {
            return;
        }

        if (canMoveRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(patrol[1].position.x , transform.position.y , patrol[1].position.z), speed * Time.deltaTime);         //Ýstenilen noktaya olduðumuz konumdan hangi hýzda gideceðimizi yazýyoruz.
            LookAtTheTarget(patrol[1].position);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(patrol[0].position.x , transform.position.y , patrol[0].position.z), speed * Time.deltaTime);
            LookAtTheTarget(patrol[0].position);
        }
    }

    private void CheckCanMoveRight()            //Enemyi oyun içerisindeki sýnýrladýðýmýz yerlerde konumlandýrmak için konumlara belirli mesafaeden fazla yaklaþýrlarsa bool deðiþkeni ile diðer notkya doðru gitmesini saðlýyoruz.
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

    private void LookAtTheTarget(Vector3 newTarget)             //Enemynin hareket ederken hedefe doðru bakmasý için.
    {
        Vector3 newLookPosition = new Vector3(newTarget.x, transform.position.y, newTarget.z);
        Quaternion targetRotation = Quaternion.LookRotation(newTarget - transform.position);            //Burada sadece hedefin rotation deðerlerini baz aldýk.
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, speed * Time.deltaTime);          // Enemy targetin rotasyonunda olmasý gerektiðini söyledik.
    } 
}
