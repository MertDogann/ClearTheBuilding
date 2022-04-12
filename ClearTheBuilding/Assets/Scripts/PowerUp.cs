using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [Header("Healt Settings")]
    public bool healtPowerUp = false;
    public int healtAmount = 1;
    [Header("Ammo Settings")]
    public bool ammoPowerUp = false;
    public int ammoAmount = 3;
    [Header("ScaleSettings")]
    [SerializeField] private float period = 2f;
    [SerializeField] private Vector3 scaleVector;
    private float scaleFactor;
    private Vector3 startScale;

    [SerializeField] Vector3 turnSpeed = new Vector3(0,0,0);


    private void Awake()                //Atamalar�n oyun ba�lamadan kullan�lmas� i�in awake i�erisinde kullanmam�z gerekiyor.
    {
        startScale = transform.localScale;      //Ba�lang��taki scalemizi startscaleye e�itledik.
    }
    void Start()
    {

        if (healtPowerUp && ammoPowerUp)
        {
            healtPowerUp = false;
            ammoPowerUp = false;
        }else if (healtPowerUp)
        {
            ammoPowerUp = false;
        }else if (ammoPowerUp)
        {
            healtPowerUp = false;
        }

        
    }


    void Update()
    {
        transform.Rotate(turnSpeed);
        SinusWawe();
    }

    private void SinusWawe()                                    // Bir objeyi scale etmek istersek kullan�lacak yer.
    {
        if (period <= 0f)
        {
            period = 0.1f;
        }

        float cycles = Time.timeSinceLevelLoad / period;        //Scale i�lemi s�reklilik istedi�i i�in s�rekli olarak oyunda de�i�en �ey s�re oldu�u i�in s�reye b�ld�k.   
        const float piX2 = Mathf.PI * 2;                        // Burada sin a��s� 2pi ile -2pi aras�nda d�nd��� i�in 2yle �arpt�k.
        float sinusWawe = Mathf.Sin(cycles * piX2);

        scaleFactor = sinusWawe / 2 + 0.5f;                     // Scalemizin eksiyte d��memesi i�in +0.5f ekleyerek sa�lam�� olduk.
        Vector3 offset = scaleFactor * scaleVector;             // Objeye ne kadar scale de�eri ekleyecei�imizi bir de�i�kene atarak g�stermi� olduk.
        transform.localScale = startScale + offset;             //Objenin scalesini ba�lang�� scalesine offset scalesi ekleyerek devam ettir.
    }

        private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")       //E�er objenin triggerlendi�i obje player de�ilse a�a��daki kodlar� okuma return yani bo� d�n playerse a�a��daki kodlar� okumaya devam et.
        {
            return;
        }
        if (ammoPowerUp)
        {
            other.gameObject.GetComponent<Attack>().GetAmmo += ammoAmount;
        }else if (healtPowerUp)
        {
            other.gameObject.GetComponent<Target>().GetHealt += healtAmount;
        }
        Destroy(gameObject);
    }
}
