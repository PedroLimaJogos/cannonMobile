using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
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
            //Detecta se está tocando em um botão
            if (Application.isMobilePlatform){
                Touch touch = Input.GetTouch(0);
                if(EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    return;
                }
            }
            else{
                if(EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }
            }
            if(gameIsGoing == false)
            {
                //Tocou, tira texto, inicia moedas, spawna uma bola nivel 4
                startScreen.SetActive(false);
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
