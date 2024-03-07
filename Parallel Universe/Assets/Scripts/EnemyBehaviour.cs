using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public GameObject[] players;
    GameObject player;
    public float speed;
    public float nearDistance;

    private float distance;
    private float distance1;
    private float distance2;

    void Update()
    {
        distance1 = Vector2.Distance(transform.position, players[0].transform.position); 
        distance2 = Vector2.Distance(transform.position, players[1].transform.position);
        if (distance1 < distance2)
        {
            distance = distance1;
            player = players[0];
        } else
        {
            distance = distance2;
            player = players[1];
        }
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize(); 
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (distance > nearDistance)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }
}
