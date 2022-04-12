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
    private void Awake()                //Atamalar�n oyun ba�lamadan �nce yap�lmas� gerekti�i i�in awake i�erisinde kulland�k.
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
            rigidbody.velocity = new Vector3(rigidbody.velocity.x,Mathf.Clamp((jumpPower * 100) * Time.deltaTime,0f,12f), 0f); //Z�plama i�in birden fazla kes basma durumuna kar�� s�n�r konuldu.
            
            
        } 

        if (Input.GetKey(KeyCode.A))
        {
            rigidbody.velocity = new Vector3(Mathf.Clamp((speed * 100) * Time.deltaTime,0f,10f), rigidbody.velocity.y, 0f); // Sa�a giderken anl�k buglar� �nlemek i�in h�za s�n�r konuldu. 
            //transform.rotation = Quaternion.Euler(0f, -180f, 0f); // -90 derece y�n�n� �evirir.
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, -180.001f, 0), rotationTime * Time.deltaTime);
            
        }else if (Input.GetKey(KeyCode.D))
        {
            rigidbody.velocity = new Vector3(Mathf.Clamp((-speed * 100) * Time.deltaTime,-10f, 0f), rigidbody.velocity.y, 0f) ;
            //transform.rotation = Quaternion.Euler(0f, 0f, 0f);                         //Karakter zaten sa�a bakt��� i�in ayn� �ekilde kal�yor.
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0.001f, 0f), rotationTime * Time.deltaTime);  // �stedi�imiz rotasyona giderken rotasyon s�resi vererek anl�k rotasyonundan hedef rotasyona ka� saniyede ge�mesi gerekti�ini s�ylemi� oluyoruz.
            
        }else
        {
            rigidbody.velocity = new Vector3(0f, rigidbody.velocity.y, 0f); // Karakter tu�a basmad��� durumlarda ivmeden kaynakl� hareket olmamas� i�in 0 land�.
        }
    }
    private bool OnGroundCheck()        // Bu metod sayesinde sadece bir yere yakla�t���m�zda �al��mas�n� istedi�imiz �zellikleri ayarlayabiliyoruz.
    {
        bool hit = false;
        for (int i = 0; i < rayStartPoint.Length; i++)
        {
            hit = Physics.Raycast(rayStartPoint[i].position, -rayStartPoint[i].transform.up, 0.5f);         // rayStartPointin pozisyonundan a�a�� do�ru 0.5f mesafe aral�kta �al��.
            Debug.DrawRay(rayStartPoint[i].position, -rayStartPoint[i].transform.up * 0.5f, Color.red);          // Oyunda raycastin g�z�kmesini sa�lar.
            
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
