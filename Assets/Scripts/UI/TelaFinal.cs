using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TelaFinal : MonoBehaviour
{
    public TextMeshProUGUI textoWin;
    public TextMeshProUGUI textoLose;

    public void ShowWin(bool won)
    {
        if (won)
        {
            textoWin.gameObject.SetActive(true);
            textoLose.gameObject.SetActive(false);
        }
        else
        {
            textoWin.gameObject.SetActive(false);
            textoLose.gameObject.SetActive(true);
        }
    }
}
