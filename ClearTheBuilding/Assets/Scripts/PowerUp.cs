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


    private void Awake()                //Atamalarýn oyun baþlamadan kullanýlmasý için awake içerisinde kullanmamýz gerekiyor.
    {
        startScale = transform.localScale;      //Baþlangýçtaki scalemizi startscaleye eþitledik.
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

    private void SinusWawe()                                    // Bir objeyi scale etmek istersek kullanýlacak yer.
    {
        if (period <= 0f)
        {
            period = 0.1f;
        }

        float cycles = Time.timeSinceLevelLoad / period;        //Scale iþlemi süreklilik istediði için sürekli olarak oyunda deðiþen þey süre olduðu için süreye böldük.   
        const float piX2 = Mathf.PI * 2;                        // Burada sin açýsý 2pi ile -2pi arasýnda döndüðü için 2yle çarptýk.
        float sinusWawe = Mathf.Sin(cycles * piX2);

        scaleFactor = sinusWawe / 2 + 0.5f;                     // Scalemizin eksiyte düþmemesi için +0.5f ekleyerek saðlamýþ olduk.
        Vector3 offset = scaleFactor * scaleVector;             // Objeye ne kadar scale deðeri ekleyeceiðimizi bir deðiþkene atarak göstermiþ olduk.
        transform.localScale = startScale + offset;             //Objenin scalesini baþlangýç scalesine offset scalesi ekleyerek devam ettir.
    }

        private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")       //Eðer objenin triggerlendiði obje player deðilse aþaðýdaki kodlarý okuma return yani boþ dön playerse aþaðýdaki kodlarý okumaya devam et.
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
