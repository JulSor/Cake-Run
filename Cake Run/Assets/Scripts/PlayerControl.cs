using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public Rigidbody2D PlayerRb;
    public GameObject Player;

    public GameObject Coin;
    public static int CCount;
    public static int totalCoins;
    public Text CoinCount;
    public Text totalCoinsText;

    public GameObject GameOverPanel;
    public GameObject PausePanel;

    //ARROW BUTTONS
    public GameObject Up;
    public GameObject Down;
    public GameObject Right;
    public GameObject Left;

    //PAUSE BUTTON
    public GameObject Pause;

    //HEALTH BAR 
    public GameObject Health1;
    public GameObject Health2;
    public GameObject Health3;

    private int playerHealth = 3;
    private int maxHealth = 4;

    public Animator iceCream;
    
    //IS PLAYER ON ICE PLATFORM?
    public bool isOnIce = false;
    
    public bool facingUp;
    public bool facingDown;
    public bool facingLeft;
    public bool facingRight;
    
    //MOVEMENT VECTORS
    private Vector2 velocityUp;
    private Vector2 velocityDown;
    private Vector2 velocityRight;
    private Vector2 velocityLeft;

    //METER COUNTER
    private Vector2 startPoint;
    public Text metersText;
    public Text metersBest;
    private float meterCounter;

    //TELEPORTS
    private float portalSpeed = 6f;
    
    public bool BluePortal = false;
    public bool RedPortal = false;

    //RAYCAST
    private float moveDistance = 1;
    static int wallLayer = 8;
    static int iceCreamLayer = 11;
    public static int WallMask = 1 << wallLayer;
    public static int IceCreamMask = 1 << iceCreamLayer;

    void Start ()
    {

        playerHealth = 3;

        GameOverPanel.SetActive(false);

        iceCream = GetComponent<Animator>();

        startPoint = transform.position;

        meterCounter = 0;
        metersText.text = meterCounter.ToString("F0");

        velocityUp = new Vector2(0.0f, 1.0f);
        velocityDown = new Vector2(0.0f, -1.0f);
        velocityRight = new Vector2(1.0f, 0.0f);
        velocityLeft = new Vector2(-1.0f, 0.0f);

        PlayerRb = GetComponent<Rigidbody2D>();
        Player = GetComponent<GameObject>();

        PausePanel.SetActive(false);

        metersBest.text = "Best: \n" + PlayerPrefs.GetFloat("HighScore", 0).ToString("F0") + "m";
        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        totalCoinsText.text = "Total coins: " + totalCoins.ToString();

        Time.timeScale = 1;
	}
    public void Update()
    {
        Debug.Log(totalCoins + "totalcoins");

        meterCounter = (transform.position.y - startPoint.y);
        metersText.text = meterCounter.ToString("F0") + "m";
        metersBest.text = "Best: \n" + PlayerPrefs.GetFloat("HighScore", 0).ToString("F0") + "m";
        totalCoinsText.text = PlayerPrefs.GetInt("TotalCoins", 0).ToString();

        if (meterCounter > PlayerPrefs.GetFloat("HighScore", 0))
        {
            PlayerPrefs.SetFloat("HighScore", meterCounter);
        }
        if (totalCoins > PlayerPrefs.GetInt("TotalCoins", 0))
        {
            PlayerPrefs.SetInt("TotalCoins", totalCoins);
        }
        if (totalCoins < PlayerPrefs.GetInt("TotalCoins", 0))
        {
            PlayerPrefs.SetInt("TotalCoins", totalCoins);
        }
        if (BluePortal == true)
        {
            BluePortalMovement();
        }
        if (RedPortal == true)
        {
            RedPortalMovement();
        }
        //ASETETAAN HEALTH BARIN OSIA HEALTHIN MUKAAN
        if (playerHealth == 3)
        {
            Health3.SetActive(true);
            Health2.SetActive(true);
            Health1.SetActive(true);
        }
        else if (playerHealth == 2)
        {
            Health3.SetActive(false);
            Health2.SetActive(true);
            Health1.SetActive(true);
        }
        else if (playerHealth == 1)
        {
            Health3.SetActive(false);
            Health2.SetActive(false);
            Health1.SetActive(true);
        }
        else if (playerHealth == 0)
        {
            Health3.SetActive(false);
            Health2.SetActive(false);
            Health1.SetActive(false);
            GameOver();
        }
        /*if (playerHealth >= maxHealth) //ESTETÄÄN HEALTHIA NOUSEMASTA YLI KOLMEEN
        {
            playerHealth = 3;
        }*/
        if (Input.GetKeyDown("up"))     { MoveUp(); }
        if (Input.GetKeyDown("down"))   { MoveDown(); }
        if (Input.GetKeyDown("right"))  { MoveRight(); }
        if (Input.GetKeyDown("left"))   { MoveLeft(); }
        //LIIKKUU AUTOMAAGISESTI MENOSUUNTAANSA, JOS ON ICE PLATFORMILLA
        if (isOnIce == true && facingUp == true)     { MoveUp(); }
        if (isOnIce == true && facingRight == true)  { MoveRight(); }
        if (isOnIce == true && facingLeft == true)   { MoveLeft(); }
        if (isOnIce == true && facingDown == true)   { MoveDown(); }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Normal")
        {
            isOnIce = false;
        }
        if (collision.gameObject.tag == "Ice")
        {
            isOnIce = true;

            BluePortal = false;
            RedPortal = false;
        }
        if (collision.gameObject.tag == "CollPlat")
        {
            isOnIce = false;
        }
        if (collision.gameObject.tag == "Enemy")
        {
            playerHealth = playerHealth -1;
        }
        if (collision.gameObject.tag == "PortRed1")
        {
            if (BluePortal == false)
            {
                GameObject.FindGameObjectWithTag("PortRed1").SetActive(false);
                RedPortal = true;
                Invoke("RedPortDeactivate", 1f);
            }
        }
        if (collision.gameObject.tag == "PortBlue1")
        {
            if (RedPortal == false)
            {
                GameObject.FindGameObjectWithTag("PortBlue1").SetActive(false);
                BluePortal = true;
                Invoke("BluePortDeactivate", 1f);
            }
        }
        if (collision.gameObject.tag == "Coin")
        {
            totalCoins = totalCoins + 1;
            CCount += 1;
            CoinCounter();
        }
        if (collision.gameObject.tag == "Off")
        {
            if (BluePortal == false && RedPortal == false)
            {
                //GameOver();
                Invoke("GameOver", 0.5f);
            }
        }
    }
    public void BluePortDeactivate()
    {
        GameObject.FindGameObjectWithTag("PortBlue2").SetActive(false);
    }
    public void RedPortDeactivate()
    {
        GameObject.FindGameObjectWithTag("PortRed2").SetActive(false);
    }
    public void BluePortalMovement()
    {
        if (RedPortal == false)
        {
            float step = portalSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, GameObject.FindGameObjectWithTag("PortBlue2").transform.position + new Vector3(0.0f, 0.4f, 0.0f), step);
            Invoke("PortalOff", 1f);
        }
    }
    public void RedPortalMovement()
    {
        if (BluePortal == false)
        {
            float step = portalSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, GameObject.FindGameObjectWithTag("PortRed2").transform.position + new Vector3(0.0f, 0.4f, 0.0f), step);
            Invoke("PortalOff", 1f);
        }
    }
    public void PortalOff()
    {
        RedPortal = false;
        BluePortal = false;
    }
    public void MoveUp()
    {
        //RAYCASTATAAN MENOSUUNTAAN MOVEDISTANCEN VERRAN JA TSEKATAAN ONKO EDESSÄ SEINÄÄ, TAHI ESIM. JÄÄTÖLÖTÖTTÖRÖÖ
        //sama setti alemmissa liikkumissuunnissa
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, moveDistance, WallMask);
        RaycastHit2D hitCream = Physics2D.Raycast(transform.position, Vector2.up, moveDistance, IceCreamMask);
        if (hit.collider == null && hitCream.collider == null)
        {
            facingUp = true;
            facingDown = false;
            facingRight = false;
            facingLeft = false;
            PlayerRb.MovePosition(PlayerRb.position + velocityUp);
        }
        if (hitCream.collider.tag == "IceCream")
        {
            if (hitCream.transform.gameObject.GetComponent<Animator>().enabled == false)
            {
                isOnIce = false;
                hitCream.transform.gameObject.GetComponent<Animator>().enabled = true;
            }
            if (hitCream.transform.gameObject.GetComponent<Animator>().enabled == true && isOnIce == false)
            {
                if (Input.GetKeyDown("up"))
                {
                    isOnIce = true;
                }
            }
            if (hitCream.transform.gameObject.GetComponent<Animator>().enabled == true && isOnIce == true)
            {
                Destroy(hitCream.transform.gameObject);
            }
        }
    }
    public void MoveDown()
    {
        //tsek MoveUp-selitys
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, moveDistance, WallMask);
        RaycastHit2D hitCream = Physics2D.Raycast(transform.position, Vector2.down, moveDistance, IceCreamMask);
        if (hit.collider == null && hitCream.collider == null)
        {
            facingUp = false;
            facingDown = true;
            facingRight = false;
            facingLeft = false;
            PlayerRb.MovePosition(PlayerRb.position + velocityDown);
        }
        if (hitCream.collider.tag == "IceCream")
        {
            if (hitCream.transform.gameObject.GetComponent<Animator>().enabled == false)
            {
                isOnIce = false;
                hitCream.transform.gameObject.GetComponent<Animator>().enabled = true;
            }
            if (hitCream.transform.gameObject.GetComponent<Animator>().enabled == true && isOnIce == false)
            {
                if (Input.GetKeyDown("down"))
                {
                    isOnIce = true;
                }
            }
            if (hitCream.transform.gameObject.GetComponent<Animator>().enabled == true && isOnIce == true)
            {
                Destroy(hitCream.transform.gameObject);
            }
        }
    }
    public void MoveRight()
    {
        //tsek MoveUp-selitys
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, moveDistance, WallMask);
        RaycastHit2D hitCream = Physics2D.Raycast(transform.position, Vector2.right, moveDistance, IceCreamMask);
        if (hit.collider == null && hitCream.collider == null)
        {
            facingUp = false;
            facingDown = false;
            facingRight = true;
            facingLeft = false;
            PlayerRb.MovePosition(PlayerRb.position + velocityRight);
        }
        if (hitCream.collider.tag == "IceCream")
        {
            if (hitCream.transform.gameObject.GetComponent<Animator>().enabled == false)
            {
                isOnIce = false;
                hitCream.transform.gameObject.GetComponent<Animator>().enabled = true;
            }
            if (hitCream.transform.gameObject.GetComponent<Animator>().enabled == true && isOnIce == false)
            {
                if (Input.GetKeyDown("right"))
                {
                    isOnIce = true;
                }
            }
            if (hitCream.transform.gameObject.GetComponent<Animator>().enabled == true && isOnIce == true)
            {
                Destroy(hitCream.transform.gameObject);
            }
        }
    }
    public void MoveLeft()
    {
        //tsek MoveUp-selitys
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, moveDistance, WallMask);
        RaycastHit2D hitCream = Physics2D.Raycast(transform.position, Vector2.left, moveDistance, IceCreamMask);
        if (hit.collider == null && hitCream.collider == null)
        {
            facingUp = false;
            facingDown = false;
            facingRight = false;
            facingLeft = true;
            PlayerRb.MovePosition(PlayerRb.position + velocityLeft);
        }
        if (hitCream.collider.tag == "IceCream")
        {
            if (hitCream.transform.gameObject.GetComponent<Animator>().enabled == false)
            {
                isOnIce = false;
                hitCream.transform.gameObject.GetComponent<Animator>().enabled = true;
            }
            if (hitCream.transform.gameObject.GetComponent<Animator>().enabled == true && isOnIce == false)
            {
                if (Input.GetKeyDown("left"))
                {
                    isOnIce = true;
                }
            }
            if (hitCream.transform.gameObject.GetComponent<Animator>().enabled == true && isOnIce == true)
            {
                Destroy(hitCream.transform.gameObject);
            }
        }
    }
    public void CoinCounter()
    {
        CoinCount.text = CCount.ToString(); //Lasketaan ruudulle kerätyt kolikot
    }
    public void PauseToggle()
    {
        if (!PausePanel.activeInHierarchy)
        {
            PausePanelActive();
        }
        else
        {
            ClosePausePanel();
        }
    }
    public void PausePanelActive()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void ClosePausePanel()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void GameOver()
    {
        GameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }
}
