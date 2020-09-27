using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;

    public float distance;
    public LayerMask mask;
    public bool isGoingLeft;

    public Vector2 target;

    public bool shouldLerp;
    public float lerpTime;

    public Vector2 start;

    public int rangedDamge;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimedDestroy());
        lerpTime = 0;
        start = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldLerp)
        {
            lerpTime += Time.deltaTime * speed;
            transform.position = Vector2.Lerp(start, target, lerpTime);
        }

    }

    IEnumerator TimedDestroy()
    {
        yield return new WaitForSeconds(6.0f);
        Destroy(this.gameObject);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Zombie")
        {
            other.gameObject.GetComponent<ZombieController>().TakeDamage(rangedDamge);
            Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "ZombieTwo")
        {
            other.gameObject.GetComponent<ZombieController>().TakeDamage(rangedDamge);
            Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "ZombieThree")
        {
            other.gameObject.GetComponent<ZombieController>().TakeDamage(rangedDamge);
            Destroy(this.gameObject);
        }
    }
}
