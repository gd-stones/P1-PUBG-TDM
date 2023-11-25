using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Enemy health & damage")]
    public float giveDamage = 5f;
    public float enemySpeed;

    [Header("Enemy things")]
    public NavMeshAgent enemyAgent;
    public Transform lookPoint;
    public GameObject shootingRaycastArea;
    public Transform playerBody;
    public LayerMask playerLayer;

    [Header("Enemy shooting var")]
    public float timebtwShoot;
    bool previouslyShoot;

    [Header("Enemy states")]
    public float visionRadius;
    public float shootingRadius;
    public bool playerInvisionRadius;
    public bool playerInshootingRadius;
    public bool isPlayer = false;

    private void Awake()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInvisionRadius = Physics.CheckSphere(transform.position, visionRadius, playerLayer); // doc docs ham nay xem co phai dung raycast, tim cach toi uu hon
        playerInshootingRadius = Physics.CheckSphere(transform.position, shootingRadius, playerLayer);

        if (playerInvisionRadius && !playerInshootingRadius) PursuePlayer();
        if (playerInvisionRadius && playerInshootingRadius) ShootPlayer();
    }

    private void PursuePlayer()
    {
        if (enemyAgent.SetDestination(playerBody.position))
        {
            // animation
        }
    }

    private void ShootPlayer()
    {
        enemyAgent.SetDestination(transform.position);
        transform.LookAt(lookPoint);

        if (!previouslyShoot)
        {
            RaycastHit hit;
            if (Physics.Raycast(shootingRaycastArea.transform.position, shootingRaycastArea.transform.forward, out hit, shootingRadius))
            {
                Debug.Log("<color=red>Shooting</color> " + hit.transform.name);
            }
        }

        previouslyShoot = true;
        Invoke(nameof(ActiveShooting), timebtwShoot);
    }

    private void ActiveShooting()
    {
        previouslyShoot = false;
    }
}
