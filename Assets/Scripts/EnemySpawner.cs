using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    public GameObject[] enemys;
    public float spawnTime;
    public GameObject boss;
    public bool spawnBoss;

    GameObject[] enemy0;
    GameObject[] enemy1;
    GameObject[] enemy2;
    GameObject[] enemy3;

    // Start is called before the first frame update
    void Awake()
    {
        spawnTime = 1f;
        enemy0 = new GameObject[40];
        enemy1 = new GameObject[30];
        enemy2 = new GameObject[30];
        enemy3 = new GameObject[20];
        spawnBoss = false;
        Generate(enemy0, enemys[0]);
        Generate(enemy1, enemys[1]);
        Generate(enemy2, enemys[2]);
        Generate(enemy3, enemys[3]);
        boss.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float score = GameManager.instance.score;
        if(score <= 1000)
        {
            StartCoroutine(MakeObject(enemy0));
        }
        else if(score <= 2000)
        {
            StartCoroutine(MakeObject(enemy0));
            StartCoroutine(MakeObject(enemy1));
        }
        else if (score <= 3000)
        {
            StartCoroutine(MakeObject(enemy0));
            StartCoroutine(MakeObject(enemy1));
            StartCoroutine(MakeObject(enemy2));
        }
        else if(score <= 10000)
        {
            StartCoroutine(MakeObject(enemy0));
            StartCoroutine(MakeObject(enemy1));
            StartCoroutine(MakeObject(enemy2));
            StartCoroutine(MakeObject(enemy3));
        }
        else
        {
            StopCoroutine(MakeObject(enemy0));
            StopCoroutine(MakeObject(enemy1));
            StopCoroutine(MakeObject(enemy2));
            StopCoroutine(MakeObject(enemy3));
            if (!spawnBoss && !boss.activeSelf)
            {
                spawnBoss = true;
                boss.transform.position = RandomPos();
                boss.SetActive(true);
            }

            if(spawnBoss && !boss.activeSelf)
            {
                GameManager.instance.WinGame();
                
            }
        }
    }


    void Generate(GameObject[] arr, GameObject preFab)
    {
        for(int i = 0; i < arr.Length; i++)
        {
            arr[i] = Instantiate(preFab);
            arr[i].SetActive(false);
        }
    }
   
    IEnumerator MakeObject(GameObject[] targetPool)
    {
        for (int i = 0; i < targetPool.Length; i++)
        {
            if (!targetPool[i].activeSelf)
            {
                targetPool[i].transform.position = RandomPos();
                targetPool[i].SetActive(true);
            }
            yield return new WaitForSeconds(spawnTime);
        }
    }

    public Vector2 RandomPos()
    {
        Player player = GameManager.instance.player;
        float ranX = 0;
        float ranY = 0;

        while(Mathf.Abs(ranX) < 10 && Mathf.Abs(ranY) < 10)
        {
            ranX = Random.Range(-20f, 20f);
            ranY = Random.Range(-20f, 20f);
        }

        Vector2 randomPos = new Vector2(ranX + player.transform.position.x, ranY + player.transform.position.y);

        return randomPos;
    }

}
