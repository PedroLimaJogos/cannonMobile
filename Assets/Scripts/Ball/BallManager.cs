using UnityEngine;

public class BallManager : MonoBehaviour
{
    //Manager instancia as bolas e verifica se o jogo acabou
    public GameObject ballPrefab;
    GameManager gameManager;
    private void Start() {
        gameManager = FindObjectOfType<GameManager>();
    }
    public void SpawnBalls(int newLevel, Vector3 position)
    {
        //Distancia pra não prenderem uma na outra
        Vector3 distance = new Vector3(1f, 0, 0); 
        InstantiateBall(newLevel, position + distance);
        InstantiateBall(newLevel, position - distance);
    }

    public void InstantiateBall(int level, Vector3 position)
    {
        //Instancia na posição que recebeu
        GameObject newBall = Instantiate(ballPrefab, position, Quaternion.identity);
        newBall.transform.SetParent(transform);

        Ball ballScript = newBall.GetComponent<Ball>();
        ballScript.level = level;
    }
    public void CheckForGameOver()
    {
        //Verifica a cada destroy
        if (transform.childCount == 1)
        {
            gameManager.FinishGame(true);
        }
    }
}
