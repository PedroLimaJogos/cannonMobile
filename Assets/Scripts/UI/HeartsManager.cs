using UnityEngine;
using UnityEngine.UI;

public class HeartsManager : MonoBehaviour
{
    public Image heartsPng;
    
    public void UpdateHearts(int currentLives)
    {
        //Vê quantos tem
        int currentHeartCount = transform.childCount;

        // Adiciona os que falta
        if (currentHeartCount < currentLives)
        {
            
            int heartsToAdd = currentLives - currentHeartCount;
            for (int i = 0; i < heartsToAdd; i++)
            {
                Instantiate(heartsPng, transform);
            }
        }
        //Retira os adicionais
        else if (currentHeartCount > currentLives)
        {
            int heartsToRemove = currentHeartCount - currentLives;
            for (int i = 0; i < heartsToRemove; i++)
            {
                Image heartObject = GetComponentInChildren<Image>();
                Destroy(heartObject.gameObject);
            }
        }
    }

}
