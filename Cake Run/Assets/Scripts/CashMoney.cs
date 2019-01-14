using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CashMoney : PlayerControl
{
    //Confirm-ikkunat
    public GameObject confirm50;
    public GameObject confirm100;
    //Coinien määrä
    public Text coins;

	void Start ()
    {
        //Asetetaan Confirm-ikkunat pois käytöstä alussa.
        confirm50.SetActive(false);
        confirm100.SetActive(false);
        coins.text = "Coins:" + totalCoins.ToString();
    }
	
	new void Update ()
    {
        Debug.Log(totalCoins);
        coins.text = "Coins:" + totalCoins.ToString();
    }

    public void MinusHundredCoins()
    {
        totalCoins = totalCoins - 100;
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
        confirm100.SetActive(false);
    }
    public void MinusFiftyCoins()
    {
        totalCoins = totalCoins - 50;
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
        confirm50.SetActive(false);
    }
    public void ConfirmHundred()
    {
        confirm100.SetActive(true);
    }

    public void ConfirmFifty()
    {
        confirm50.SetActive(true);
    }

    public void CloseConfirmWindow()
    {
        confirm50.SetActive(false);
        confirm100.SetActive(false);
    }
}
