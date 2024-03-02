using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Timers;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Vector3 = UnityEngine.Vector3;

public class SpikeHeadTrap : EnemyDamage
{
    [Header("SpikeHead Attributes:")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;

    private float checkTimer;
    private Vector3 destination;
    private bool attacking;
    private Vector3[] directions = new Vector3[4];

    private void Start()
    {
        //Right
        directions[0] = transform.right * range; 
        //Left
        directions[1] = -transform.right * range;
        //Up
        directions[2] = transform.up * range;
        //Down
        directions[3] = -transform.up * range;
        
        
    }

    private void Update()
    {
        if (attacking)
        {
            transform.Translate(destination*Time.deltaTime*speed);
        }
        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
            {
                CheckForPlayer();
            }
        }
    }

    private void CheckForPlayer()
    {
        //Check if spikehead sees player
        foreach (Vector3 dir in directions)
        {
            Debug.DrawRay(transform.position, dir, Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, range, playerLayer);
            if (hit.collider != null && !attacking)
            {
                Debug.Log("Tocado:" + hit.collider.name);
                attacking = true;
                destination = dir;
                checkTimer = 0.0f;
            }
            
        }
    }

    private void Stop()
    {
        destination = transform.position;
        attacking = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("El spikeHead ha tocado algo");
        base.OnTriggerEnter2D(other);
        Stop();
    }
}
