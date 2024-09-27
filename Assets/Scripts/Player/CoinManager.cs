using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int coins = 0;
    private int priceUpgrade = 1;
    public TextMeshProUGUI coinTextMesh;
    public TextMeshProUGUI upgradeCostTextMesh;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        UpdateCoins();
        player = FindObjectOfType<Player>();
        UpdatePriceUpgrade();
    }

    public void SumCoins(int value)
    {
        //Sobe o placar
        coins += value;
        UpdateCoins();
    }

    public void UpgradePlayerLevel()
    {
        //Aumenta o nível e o preço, tira as moedas
        if(coins >= priceUpgrade)
        {
            player.Upgrade();
            coins -= priceUpgrade;
            priceUpgrade *= 2;
            UpdatePriceUpgrade();
            UpdateCoins();
        }
    }

    private void UpdatePriceUpgrade()
    {
        //Update do texto
        upgradeCostTextMesh.text = "Upgrade: "+ priceUpgrade.ToString();
    }
    private void UpdateCoins()
    {
        //Update do texto
        coinTextMesh.text = coins.ToString("D2");
    }
}
