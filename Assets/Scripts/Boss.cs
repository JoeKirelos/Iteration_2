using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject miniPrefab;
    public GameObject rippleController;
    public Transform spawnPoint;
    public Transform spawnPoint2;
    public GameObject player;
    public Transform playerRB;
    Vector3 target;
    public GameObject self;
    public float speed;
    public bool moving = false;
    public int hitPoints = 40;
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
            transform.position -= target * speed * Time.deltaTime;
        }
        if (hitPoints <= 0)
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
        rippleController.GetComponent<BossRippleController>().animator.SetTrigger("Ripple");
        moving = false;
    }
    public void TakeDamage()
    {
        hitPoints -= 1;
    }
    public void DestroySelf()
    {
        Instantiate(miniPrefab, spawnPoint.position, spawnPoint.rotation);
        Instantiate(miniPrefab, spawnPoint2.position, spawnPoint.rotation);
        Destroy(self);
    }
}
