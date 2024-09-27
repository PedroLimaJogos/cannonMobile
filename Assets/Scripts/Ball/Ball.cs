using UnityEngine;
using TMPro;
using DG.Tweening;

public class Ball : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 10f;
    public int bounce = 15;

    [Header("Base")]  
    public int level;     
    public float size;    
    public float health;
    public float maxHealth;

    [Header("Damage Animation")]  
    public float multiplierSize = 2f;
    public float duration = 0.2f;
    private Vector3 originalScale;

    [Header("Damage Color")]  
    private Color startColor;
    public Color finishColor;
    private Material bolaSkin;
    public Bullet bullet;


    private TextMeshPro textMeshPro;
    private BallManager ballManager;
    private Rigidbody rb;
    public GameObject coin_PFB;
    private Vector3 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ballManager = FindObjectOfType<BallManager>();

        //Starta os status
        BallBaseConfig();

        //Atualiza texto
        textMeshPro = GetComponentInChildren<TextMeshPro>();
        UpdateHealthDisplay();

        // Direção aleatória
        moveDirection = new Vector3(Random.Range(-1f, 1f), 0, 0);
        rb.velocity = moveDirection * speed;

        originalScale = transform.localScale;

        //Cor
        Renderer renderer = transform.GetComponentInChildren<Renderer>();
        bolaSkin = renderer.material;
        startColor = bolaSkin.color;
        
        UpdateColor();
    }

    void Update()
    {
        // Checar se a vida da bola chegou a 0
        if (health <= 0)
        {
            HandleBallDestruction();
        }
    }

    void BallBaseConfig()
    {
        //Maior o nível, maior o tamanho
        size *= level;
        transform.localScale = Vector3.one * size;

        //Quanto maior, mais vida. Mas um pouco aleatório
        health *= level;
        health = (int)Random.Range(health, health*2);
        maxHealth = health;

        //Quanto maior, mais lento
        speed -= (1 * level);

        //Quanto maior, maior o pulo
        bounce +=(1 * level);
    }

    void HandleBallDestruction()
    {
        if (level > 1)
        {
            // Nível acima de 1, Spawna 2
            ballManager.SpawnBalls(level - 1, transform.position);
        }
        else{
            //Nível 1, dropa moedas
            SpawnCoins(transform.position);
        }

        Destroy(gameObject);
        ballManager.CheckForGameOver();
    }

    // Recebe dano e reduz a vida da bola
    public void TakeDamage(float damage)
    {
        health -= damage;
        UpdateHealthDisplay();
        if (health > 0)
            transform.DOScale(originalScale * multiplierSize, duration).SetLoops(2, LoopType.Yoyo).SetEase(Ease.InOutSine);
            UpdateColor();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Bateu no chão, mantêm a velocidade horizontal, adiciona vertical
            Vector3 newVelocity = new Vector3((int)(moveDirection.x * speed), bounce, 0);
            rb.velocity = newVelocity;

            if (level > 3)
            {
                //Treme a câmera
                CameraShake cameraShake = Camera.main.GetComponent<CameraShake>();
                    if (cameraShake != null)
                    {
                        cameraShake.StartShake(0.5f, .1f);
                    }
            }
        }

         if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Ball"))
        {      
            //Bateu em uma parede ou outra bola, troca de lado.
            moveDirection.x = -moveDirection.x;

            // Adiciona vertical pra não quebrar a subida
            Vector3 currentVelocity = rb.velocity;
            currentVelocity.y += 1.4f;

            rb.velocity = new Vector3(moveDirection.x * speed, currentVelocity.y, 0); 
        }

        if (collision.gameObject.CompareTag("Roof"))
        {
            //Teto não pode retirar velocidade de X
            Vector3 currentVelocity = rb.velocity; 
            rb.velocity = new Vector3(currentVelocity.x, 0, 0); 
        }
    }

    private void UpdateHealthDisplay()
    {
        if (textMeshPro != null)
        {
            textMeshPro.text = health.ToString("F0");
        }
    }

    private void SpawnCoins(Vector3 position)
    {
        int amount = Random.Range(1, 5);
        for (int i = 0; i < amount; i++)
        {
            Vector3 coinPosition = position + new Vector3(i * 0.5f, 0, 0);
            Instantiate(coin_PFB, coinPosition, Quaternion.identity);
        }
    }

    void UpdateColor()
    {
        // Calcula o número de tiros necessários para destruir a bola
        float shotsToKill = health / bullet.damage;

        float shotsRange = Mathf.Clamp(shotsToKill, 1, 6);
        float colorPercentage = (shotsRange - 1) / 5f;

        Color newColor = Color.Lerp(finishColor, startColor, colorPercentage);
        bolaSkin.color = newColor;
    }
}
