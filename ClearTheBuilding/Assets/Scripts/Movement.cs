using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rigidbody;
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float rotationTime = 0.5f;
    [SerializeField] private Transform[] rayStartPoint;

    private GameManager gameManager;
    private void Awake()                //Atamalarýn oyun baþlamadan önce yapýlmasý gerektiði için awake içerisinde kullandýk.
    {
        rigidbody = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
    }


    void Update()
    {
        if (!gameManager.GetLevelFinished)
        {
            TakeInput();
        }
        
        
    }
    private void TakeInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && OnGroundCheck())
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x,Mathf.Clamp((jumpPower * 100) * Time.deltaTime,0f,12f), 0f); //Zýplama için birden fazla kes basma durumuna karþý sýnýr konuldu.
            
            
        } 

        if (Input.GetKey(KeyCode.A))
        {
            rigidbody.velocity = new Vector3(Mathf.Clamp((speed * 100) * Time.deltaTime,0f,10f), rigidbody.velocity.y, 0f); // Saða giderken anlýk buglarý önlemek için hýza sýnýr konuldu. 
            //transform.rotation = Quaternion.Euler(0f, -180f, 0f); // -90 derece yönünü çevirir.
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, -180.001f, 0), rotationTime * Time.deltaTime);
            
        }else if (Input.GetKey(KeyCode.D))
        {
            rigidbody.velocity = new Vector3(Mathf.Clamp((-speed * 100) * Time.deltaTime,-10f, 0f), rigidbody.velocity.y, 0f) ;
            //transform.rotation = Quaternion.Euler(0f, 0f, 0f);                         //Karakter zaten saða baktýðý için ayný þekilde kalýyor.
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0.001f, 0f), rotationTime * Time.deltaTime);  // Ýstediðimiz rotasyona giderken rotasyon süresi vererek anlýk rotasyonundan hedef rotasyona kaç saniyede geçmesi gerektiðini söylemiþ oluyoruz.
            
        }else
        {
            rigidbody.velocity = new Vector3(0f, rigidbody.velocity.y, 0f); // Karakter tuþa basmadýðý durumlarda ivmeden kaynaklý hareket olmamasý için 0 landý.
        }
    }
    private bool OnGroundCheck()        // Bu metod sayesinde sadece bir yere yaklaþtýðýmýzda çalýþmasýný istediðimiz özellikleri ayarlayabiliyoruz.
    {
        bool hit = false;
        for (int i = 0; i < rayStartPoint.Length; i++)
        {
            hit = Physics.Raycast(rayStartPoint[i].position, -rayStartPoint[i].transform.up, 0.5f);         // rayStartPointin pozisyonundan aþaðý doðru 0.5f mesafe aralýkta çalýþ.
            Debug.DrawRay(rayStartPoint[i].position, -rayStartPoint[i].transform.up * 0.5f, Color.red);          // Oyunda raycastin gözükmesini saðlar.
            
        }
        if (hit)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
