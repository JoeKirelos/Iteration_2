using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemyBPrefab;
    public Transform spawnPoint;
    GameObject currentEnemy;
    public float shootingIntervalEnemyA;
    public float randomizerEnemyAInt;
    public float bounceEnemyInt;
    public float enemyInitialShoot;
    public float EnemyBshootingCooldown;
    public float enemyBAccuracy;
    public Vector3 movement = new Vector3(1, 1,0);
    public float speed;
    public GameObject self;
    public int sniperThreshold;
    public Transform corner;
    public int waveCounter;
    public int waveMax = 10;
    public float spawnRate;
    // Start is called before the first frame update
    void Start()
    {
        spawnRate = 5;
        StartCoroutine(Spawn());
        StartCoroutine(OneTwo());

    }

    // Update is called once per frame
    void Update()
    {
        self.transform.position += (movement * speed * Time.deltaTime);

    }

    IEnumerator WaveControl()
    {
        yield return new WaitForSeconds(10);
        if (waveCounter >= waveMax)
        {
            waveCounter = 0;
            waveMax += 2;
            if(spawnRate > 1)
            {
                spawnRate -= 0.5f;
            }
        }        
    }
   IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            if ( currentEnemy == null && waveCounter < waveMax)
            {
                waveCounter++;
                int percent = Random.Range(0, 10);
                if (percent > sniperThreshold)
                {
                    currentEnemy = (Instantiate(enemyBPrefab, spawnPoint.position, spawnPoint.rotation));
                    currentEnemy.GetComponent<enemyB>().trackingUpdate = enemyBAccuracy;
                    currentEnemy.GetComponent<enemyB>().tracker = EnemyBshootingCooldown;
                    currentEnemy.GetComponent<enemyB>().target = corner;
                }
                else
                {
                    currentEnemy = (Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation));
                    currentEnemy.GetComponent<enemyA>().initialShoot = enemyInitialShoot;
                    currentEnemy.GetComponent<enemyA>().shootInterval = shootingIntervalEnemyA;
                    currentEnemy.GetComponent<enemyA>().randomizerInt = randomizerEnemyAInt;
                    currentEnemy.GetComponent<enemyA>().bounceInt = bounceEnemyInt;
                }

            }else if( waveCounter >= waveMax)
            {
                StartCoroutine(WaveControl());
            }

        }

        

    }
    IEnumerator OneTwo()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            speed = -speed;
        }
    }
}
