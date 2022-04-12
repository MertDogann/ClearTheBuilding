using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    public GameObject owner;

    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime; //Bullet instantiate edildiðinde local olarak y yönünde hareket etsin.
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.GetComponent<Target>() == false) //Triggerlendiðimiz gameobjenin içerisinde eðer target scripti bulunmuyorsa çalýþtýr.
        {
            Destroy(gameObject);
        }
    }

}
