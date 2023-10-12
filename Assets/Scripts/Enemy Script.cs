using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{



    public float moveSpeed = 3f; // Speed at which the enemy moves
    public float detectionRange = 10f; // The range within which players are detected
    private float checkInterval = 3f; // The interval to check for new players
    private float lastCheckTime = 0f;
    private Transform targetPlayer;
    public float dmg;
    bool touchingplayer= false;
    float Cooldown;

    private void Update()
    {
        if (Time.time - lastCheckTime >= checkInterval)
        {
            lastCheckTime = Time.time;
            FindClosestPlayer();
        }

        if (targetPlayer != null && touchingplayer == false)
        {
            Vector3 direction = (targetPlayer.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }

    private void FindClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        float closestDistance = detectionRange;
        targetPlayer = null;

        foreach (GameObject player in players)
        {
            if (player != null)
            {
                float distance = Vector3.Distance(transform.position, player.transform.position);
                if (distance <= detectionRange && distance < closestDistance)
                {
                    closestDistance = distance;
                    targetPlayer = player.transform;
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if(collision.gameObject.tag == "Player")
        {
            if (Cooldown<= 1)
            {
                Cooldown += Time.deltaTime;
            }
            else
            {
                Cooldown = 0;
                HitManager hit = collision.gameObject.GetComponent<HitManager>();
                hit.Hit(dmg);
            }
            touchingplayer = true;
        }
    }
}
