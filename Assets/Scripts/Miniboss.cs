using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miniboss : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject rippleController;
    GameObject currentA;
    GameObject currentB;
    GameObject currentC;
    GameObject currentD;
    public Transform spawnPoint;
    public Transform spawnPoint2;
    public Transform spawnPoint3;
    public Transform spawnPoint4;
    public GameObject player;
    public Transform playerRB;
    Vector3 target;
    public GameObject self;
    public float speed;
    public bool moving = false;
    public int hitPoints = 10;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerRB = player.transform;
        target = (transform.position - playerRB.position).normalized;
        StartCoroutine(FindPlayer());
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            transform.position -= target  * speed * Time.deltaTime;
        }
        if(hitPoints <= 0)
        {
            DestroySelf();
        }
    }

    IEnumerator FindPlayer()
    {
        while (true)
        {
            target = (transform.position - playerRB.position).normalized;
            yield return null;
        }
    }
    void Move()
    {
        moving = true;
    }

    void Stop()
    {
        rippleController.GetComponent<MiniRippleController>().animator.SetTrigger("Ripple");
        moving = false;
    }
    public void TakeDamage()
    {
        hitPoints -= 1;
    }
    public void DestroySelf()
    {
        currentA = (Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation));
        currentA.GetComponent<enemyA>().initialShoot =0.2f;
        currentA.GetComponent<enemyA>().shootInterval = 1f;
        currentA.GetComponent<enemyA>().randomizerInt = 1.5f;
        currentA.GetComponent<enemyA>().bounceInt = 0.5f;
        currentB = (Instantiate(enemyPrefab, spawnPoint2.position, spawnPoint2.rotation));
        currentB.GetComponent<enemyA>().initialShoot = 0.2f;
        currentB.GetComponent<enemyA>().shootInterval = 0.5f;
        currentB.GetComponent<enemyA>().randomizerInt = 0.5f;
        currentB.GetComponent<enemyA>().bounceInt = 2f;
        currentC = (Instantiate(enemyPrefab, spawnPoint3.position, spawnPoint3.rotation));
        currentC.GetComponent<enemyA>().initialShoot = 0.1f;
        currentC.GetComponent<enemyA>().shootInterval = 1f;
        currentC.GetComponent<enemyA>().randomizerInt = 1.5f;
        currentC.GetComponent<enemyA>().bounceInt = 0.5f;
        currentD = (Instantiate(enemyPrefab, spawnPoint4.position, spawnPoint4.rotation));
        currentD.GetComponent<enemyA>().initialShoot = 0.1f;
        currentD.GetComponent<enemyA>().shootInterval = 0.5f;
        currentD.GetComponent<enemyA>().randomizerInt = 0.5f;
        currentD.GetComponent<enemyA>().bounceInt = 2f;
        Destroy(self);
    }
}
