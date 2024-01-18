using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public GameObject winPanel, winPanel1;
    public void OpenWinPanel(bool winpanel)
    {
        if(winpanel)
            winPanel.SetActive(true);
        else
            winPanel1.SetActive(true);
        AudioManager.instance.Play("Win");
    }

    public void CloseWinPanel()
    {
        winPanel.SetActive(false);
        winPanel1.SetActive(false);
    }
}
