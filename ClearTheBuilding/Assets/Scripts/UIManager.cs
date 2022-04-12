using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image healtFill;
    public Image ammoFill;
    private Attack playerAmmo;
    private Target playerHealt;


    private void Awake()
    {
        playerAmmo = GameObject.FindGameObjectWithTag("Player").GetComponent<Attack>();         //Attack scripti içinde playerin bulunduðu attack scripti önemlidir.
        playerHealt = playerAmmo.GetComponent<Target>();                                        //Attack scripti sayesinde playerhealti kýsa yoldan bulduk.
    }

    void Update()
    {
        UptadeAmmoFill();
        UptadeHealtFill();
    }

    private void UptadeHealtFill()
    {
        ammoFill.fillAmount =(float) playerAmmo.GetAmmo / playerAmmo.GetClipSize;
    }

    private void UptadeAmmoFill()
    {
        healtFill.fillAmount =(float) playerHealt.GetHealt / playerHealt.GetMaksHealt;
    }
}
