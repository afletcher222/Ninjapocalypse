using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class NinjaController : MonoBehaviour, ISpeed, IComparable<ISpeed>
{
    public int health;
    public int maxHealth;

    public int meleeDamage;

    public int speed { get { return turnSpeed; } set { turnSpeed = value; } }
    public int turnSpeed;
    public int turnRate;
    public int maxSpeed;

    public int skill2Cooldown;

    public float moveSpeed;
    public float returnSpeed;
    public float animTime;

    public Animator anim;
    public SpriteRenderer spriteRender;

    public GameObject projectile;

    public GameController gameController;

    public GameObject arrow;

    public Slider healthSlider;
    public Slider speedSlider;

    public CapsuleCollider2D capsuleCollider;

    public GameObject floatingText;


    // Start is called before the first frame update
    void Awake()
    {
        gameController.turnOrder.Add(this);
        arrow.SetActive(false);
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRender = GetComponent<SpriteRenderer>();
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
    }

    public void FirstAttackButtonPress(Transform targetPosition)
    {
        StartCoroutine(MoveToTarget(targetPosition));
    }

    public void MeleeAttack(Vector2 target)
    {
        anim.SetBool("IsInMeleeRange", true);
        StartCoroutine(MeleeAttackEnd(target));
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 3f);
        if(hit.collider.tag == "Zombie")
        {
            hit.collider.gameObject.GetComponent<ZombieController>().TakeDamage(meleeDamage);
        }
        else if (hit.collider.tag == "ZombieTwo")
        {
            hit.collider.gameObject.GetComponent<ZombieController>().TakeDamage(meleeDamage);
        }
        else if (hit.collider.tag == "ZombieThree")
        {
            hit.collider.gameObject.GetComponent<ZombieController>().TakeDamage(meleeDamage);
        }
    }

    IEnumerator MeleeAttackEnd(Vector2 target)
    {
        yield return new WaitForSeconds(animTime);
        anim.SetBool("IsInMeleeRange", false);
        spriteRender.flipX = true;
        StartCoroutine(ReturnToStart(target));
        capsuleCollider.enabled = true;
    }

    public void RangedAttack(Vector2 targetPos)
    {
        anim.SetBool("IsThrowing", true);
        skill2Cooldown = 4;
        Vector3 scale = transform.localScale;
        GameObject Shot = Instantiate(projectile, transform.position, projectile.transform.rotation);
        Shot.GetComponent<Projectile>().target = targetPos;
        Shot.GetComponent<Projectile>().shouldLerp = true;
        if (scale.x >= 0)
        {
            Shot.GetComponent<Projectile>().isGoingLeft = true;
        }
        StartCoroutine(RangedAttackEnd());
    }

    IEnumerator RangedAttackEnd()
    {
        yield return new WaitForSeconds(animTime);
        anim.SetBool("IsThrowing", false);
        StartCoroutine(GoToNextTurn());
    }


    IEnumerator MoveToTarget(Transform target)
    {
        capsuleCollider.enabled = false;
        anim.SetBool("IsMoving", true);
        Vector2 initialPos = transform.position;
        float i = 0.0f;

        while (i < 2.0)
        {
            yield return new WaitForEndOfFrame();
            float step = moveSpeed * Time.deltaTime;
            i += Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target.position, step);

            if (Vector2.Distance(transform.position, target.position) < 1f)
            {
                MeleeAttack(initialPos);
                break;
            }
        }
    }



    IEnumerator ReturnToStart(Vector2 target)
    {
        float i = 0.0f;

        while (i < 1.0)
        {
            yield return new WaitForEndOfFrame();
            float step = returnSpeed * Time.deltaTime;
            i += Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target, step);
            if (Vector2.Distance(transform.position, target) < 0.001f)
            {
                anim.SetBool("IsMoving", false);
                spriteRender.flipX = false;
                gameController.CheckNextTurn();
                break;
            }
        }
    }

    public void SetTarget()
    {

    }

    public int CompareTo(ISpeed other)
    {
        return speed.CompareTo(other.speed);
    }

    IEnumerator GoToNextTurn()
    {
        yield return new WaitForSeconds(3f);
        gameController.CheckNextTurn();
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        healthSlider.value = health;

        GameObject FloatText = Instantiate(floatingText, (transform.position + new Vector3(0.5f, 1.1f, 0)), Quaternion.identity, transform);
        FloatText.GetComponent<MeshRenderer>().sortingOrder = 2;
        FloatText.GetComponent<TextMeshPro>().text = damage.ToString();

        if (health <= 0)
        {
            anim.SetBool("IsDead", true);
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(0.5f);
        gameController.numberOfHeroesDefeated++;
        gameController.ScoreUpdate();
        this.gameObject.SetActive(false);
    }

}
