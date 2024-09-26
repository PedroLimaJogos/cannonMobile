using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    Player player;
    BallManager ballManager;
    public GameObject startScreen;
    public TelaFinal lastScreen;
    Vector3 initialPosition = new Vector3(0, 0, 0);
    private bool gameIsGoing = false;

    void Start()
    {
        lastScreen.gameObject.SetActive(false);
        player= FindObjectOfType<Player>();
        ballManager = FindObjectOfType<BallManager>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(gameIsGoing == false){
                //Tocou, tira texto, inicia moedas, spawna uma bola nivel 4
                startScreen.SetActive(false);
                player.SumCoins(0);
                ballManager.InstantiateBall(4, initialPosition);

                gameIsGoing = true;
            }
        }
    }

    public void FinishGame(bool won)
    {
        lastScreen.gameObject.SetActive(true);
        lastScreen.ShowWin(won);
        if(won == false)
            player.gameObject.SetActive(false);
    }

    public void Restart()
    {
        foreach (Transform child in ballManager.transform)
        {
            Destroy(child.gameObject);
        }
        player.gameObject.SetActive(true);
        startScreen.SetActive(true);
        lastScreen.gameObject.SetActive(false);
        player.RestartPlayer();
        gameIsGoing = false;
    }
}
