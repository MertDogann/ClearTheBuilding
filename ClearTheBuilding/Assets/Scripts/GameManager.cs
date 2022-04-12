using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject levelFinishParent;
    private bool levelFinished;
    private Target target;

    public bool GetLevelFinished
    {
        get
        {
            return levelFinished;
        }
    }
    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Target>();
    }


    void Update()
    {
        int enemyCount = FindObjectsOfType<Enemy>().Length;                //Oyunun bitiþi tüm enemylerin öldüðü zaman olduðu için oyun baþladýðýnda findobjectSoftype ile tüm enemy scriptini içeren objelerin sayýsýný tutmuþ oluyoruz.

        if (enemyCount <=0 || target.GetHealt <= 0)
        {
            levelFinishParent.gameObject.SetActive(true);
            levelFinished = true;
        }else
        {
            levelFinishParent.gameObject.SetActive(false);
            levelFinished = false;
        }


    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
}
