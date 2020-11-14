using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum AttackTypeEnum
    {
        meele,
        range
    }

    public CharacterController controller;
    public Transform target;
    public Animator anim;
    public AttackTypeEnum AttackType;

    public GameObject bloodEffect;
    public GameObject bullet;

    public Transform bulletSpawnPos;

    private bool dead = false;
    public float speed = 1f;
    public float gravity = 10;
    private Vector3 moveDirection;

    public float shootCoolDown = 2;
    private float lookAtTimer = 0;
    private float bulletTimer = 0;

    //TODO: will make bullet pool

    List<GameObject> bulletStorage = new List<GameObject>();

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        moveDirection = Vector3.zero;
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGameActive)
            return;

        DistanceControl();

        if (controller.isGrounded)
        {
            if (anim.GetInteger("State") == 1)
            { 
                //State == 1 == meele Enemy
                Vector3 direction = target.transform.position - transform.position;
                moveDirection = direction.normalized * speed;;
            }

            if (anim.GetInteger("State") == 2)
            {
                //State == 2 == range Enemy
                moveDirection = Vector3.zero;
                TryShoot();
            }
        }
        
        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);
        controller.Move(moveDirection * Time.deltaTime);
        
        // only Y-Axis
        Vector3 targetPos = new Vector3(target.position.x, transform.position.y, target.position.z);
        transform.LookAt(targetPos);
    }

    void TryShoot()
    {
        bulletTimer += Time.deltaTime;
        if (bulletTimer >= shootCoolDown)
        {
            Shoot();
            bulletTimer = 0;
        }
    }

    //TODO: will make better
    void Shoot()
    {
        Vector3 direction = target.transform.position - transform.position;
        direction = direction.normalized;
        GameObject bulletGameObject = Instantiate(bullet, bulletSpawnPos.position, transform.rotation);
        bulletGameObject.GetComponent<BulletController>().SetDirection(direction);
    }

    private void DistanceControl()
    {
        // state == 1 == melee | state == 2 == range
        if (!dead)
        {
            var attackAnim = AttackType == AttackTypeEnum.meele ? 1 : 2;

            var distance = Vector3.Distance(target.position, transform.position);
            if (distance < 23.0f)
                anim.SetInteger("State", attackAnim);
        }
    }

    private void Die()
    {
        // state == 3 == dead
        anim.SetInteger("State", 3);
        dead = true;
        GameObject effect = Instantiate(bloodEffect, transform.position + Vector3.up * 1.5f, transform.rotation);
        Destroy(effect, 0.7f);
        Destroy(gameObject, 0.7f);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("blade") && !dead)
        {
            Die();
        }
        else if (other.gameObject.CompareTag("PlayerCollison") && !dead)
            target.gameObject.GetComponent<PlayerController>().Die();
    }
}