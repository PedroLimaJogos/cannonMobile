using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 5;
    public float timer = 4f;

    void Update()
    {
        //Bala sobe até o timer acabar
        transform.position += Vector3.up * speed * Time.deltaTime;

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        //Bateu numa bola, dá dano e destrói
        if (other.transform.tag == "Ball")
        {
            Ball ball = other.GetComponent<Ball>();
            if (ball != null)
            {
                ball.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}
