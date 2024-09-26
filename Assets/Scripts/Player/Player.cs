using System.Collections;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    private GameManager gameManager;

    [Header("Movimentação")]
    private bool isDragging = false;
    private Rigidbody rb;
    public float moveSpeed = 2;
    public GameObject visualPlayer;

    [Header("Combate")]
    public float shootWait = 0.5f;
    public GameObject bulletPrefab;
    public Transform shootPosition;
    public int lives;
    private Coroutine shootingCoroutine;
    private HeartsManager heartsManager;
    private bool isInvencible = false;

    [Header("Blink Coroutine")]
    public float blinkTime = 0.2f;
    public int blinkRepetition = 3;
    

    [Header("Pontuação")]
    public int coins = 0;
    public TextMeshProUGUI textMeshProUGUI;

    [Header("Restart")]
    private Vector3 initialPosition;

    void Start()
    {
        //Inicio de variáveis
        initialPosition = transform.position;
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody>();
        heartsManager = FindObjectOfType<HeartsManager>();

        //Adiciona os corações
        heartsManager.UpdateHearts(lives);
    }

    void Update()
    {
        CharacterMovement();
    }

    private void CharacterMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Clicou
            isDragging = true;
            shootingCoroutine = StartCoroutine(ShootBullets());
        }
        else if (Input.GetMouseButton(0))
        {
            //Movimenta usando camera
            Vector2 touchPos = Input.mousePosition;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, Camera.main.transform.position.y - rb.position.y));

            //Velocidade pra acompanhar o dedo
            float adjustedX = worldPos.x * 2f;
            Vector3 newPosition = new Vector3(adjustedX, rb.position.y, rb.position.z);

            rb.MovePosition(newPosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            //Soltou
            isDragging = false;
            if (shootingCoroutine != null)
            {
                StopCoroutine(shootingCoroutine);
                shootingCoroutine = null;
            }
        }
    }

    private IEnumerator ShootBullets()
    {
        while (isDragging)
        {
            Instantiate(bulletPrefab, shootPosition.position, Quaternion.identity);
            yield return new WaitForSeconds(shootWait);
        }
    }

    private void OnTriggerEnter(Collider other) {
        //Tocou em um objeto
        if (other.transform.tag == "Coin")
        {
            //Pega moeda
            SumCoins(1);
            Destroy(other.gameObject);
        }
        else if (other.transform.tag == "Ball")
        {
            //Toma dano
            TakeDamage();
        }
    }
    public void TakeDamage()
    {
        if(!isInvencible)
        {
            lives -= 1;
            heartsManager.UpdateHearts(lives);
            if(lives > 0)
            {
                StartCoroutine(BlinkPlayer(blinkTime, blinkRepetition));
            }
            else
            {
                gameManager.FinishGame(false);
            }
        }
    }
    

    private IEnumerator BlinkPlayer(float blinkDuration, int blinkCount) 
    {
        isInvencible = true;
        for (int i = 0; i < blinkCount; i++) 
        {
            visualPlayer.SetActive(false); 
            yield return new WaitForSeconds(blinkDuration);
            visualPlayer.SetActive(true);
            yield return new WaitForSeconds(blinkDuration);
        }
        isInvencible = false;
    }

    public void SumCoins(int value)
    {
        coins += value;
        textMeshProUGUI.text = coins.ToString("D2");
    }

    public void RestartPlayer(){
        transform.position = initialPosition;
        lives = 4;
        heartsManager.UpdateHearts(lives);
        coins = 0;
        SumCoins(coins);
    }
}
