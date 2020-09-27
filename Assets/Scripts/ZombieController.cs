using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class ZombieController : MonoBehaviour, ISpeed, IComparable<ISpeed>
{


    public int speed { get { return turnSpeed; } set { turnSpeed = value; } }
    public GameController gameController;
    public int turnSpeed;
    public int turnRate;

    public int health;
    public int maxHealth;

    public int meleeDamage;

    public float moveSpeed;
    public float returnSpeed;
    public float animTime;
    public List<Transform> target = new List<Transform>();

    public NinjaController maleNinja;
    public NinjaController femaleNinja;
    public NinjaController maleNinjaTwo;

    public Animator anim;
    public SpriteRenderer spriteRender;

    public GameObject arrow;

    public Slider healthSlider;
    public Slider speedSlider;

    public CapsuleCollider2D capsuleCollider;

    public GameObject floatingText;


    void Awake()
    {
        gameController.turnOrder.Add(this);
        arrow.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRender = GetComponent<SpriteRenderer>();
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;

        target.Add(maleNinja.transform);
        target.Add(femaleNinja.transform);
        target.Add(maleNinjaTwo.transform);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthSlider.value = health;
        GameObject FloatText = Instantiate(floatingText, (transform.position + new Vector3(-0.5f, 1.1f, 0)), Quaternion.identity, transform);
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
        gameController.numberOfEnemiesDefeated++;
        gameController.ScoreUpdate();
        this.gameObject.SetActive(false);
    }

    public void MeleeAttackZombie()
    {
        int random = UnityEngine.Random.Range(0, target.Count);

        StartCoroutine(MoveToTarget(target[random]));
    }

    public void MeleeAttack(Vector2 returnPos)
    {
        anim.SetBool("IsInMeleeRange", true);
        StartCoroutine(MeleeAttackEnd(returnPos));
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, 3f);
        if (hit.collider.tag == "Player")
        {
            hit.collider.gameObject.GetComponent<NinjaController>().TakeDamage(meleeDamage);
        }
    }

    IEnumerator MeleeAttackEnd(Vector2 returnPos)
    {
        yield return new WaitForSeconds(animTime);
        anim.SetBool("IsInMeleeRange", false);
        spriteRender.flipX = false;
        StartCoroutine(ReturnToStart(returnPos));
        capsuleCollider.enabled = true;
    }

    IEnumerator MoveToTarget(Transform targetPos)
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
            transform.position = Vector2.MoveTowards(transform.position, targetPos.position, step);

            if (Vector2.Distance(transform.position, targetPos.position) < 1f)
            {
                MeleeAttack(initialPos);
                break;
            }
        }
    }


    IEnumerator ReturnToStart(Vector2 returnPos)
    {
        float i = 0.0f;

        while (i < 1.0)
        {
            yield return new WaitForEndOfFrame();
            float step = returnSpeed * Time.deltaTime;
            i += Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, returnPos, step);
            if (Vector2.Distance(transform.position, returnPos) < 0.001f)
            {
                anim.SetBool("IsMoving", false);
                spriteRender.flipX = true;
                gameController.CheckNextTurn();
                break;
            }
        }
    }

    public int CompareTo(ISpeed other)
    {
        return speed.CompareTo(other.speed);
    }
}
