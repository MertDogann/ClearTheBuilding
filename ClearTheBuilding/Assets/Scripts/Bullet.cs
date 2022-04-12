using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    public GameObject owner;

    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime; //Bullet instantiate edildi�inde local olarak y y�n�nde hareket etsin.
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.GetComponent<Target>() == false) //Triggerlendi�imiz gameobjenin i�erisinde e�er target scripti bulunmuyorsa �al��t�r.
        {
            Destroy(gameObject);
        }
    }

}
