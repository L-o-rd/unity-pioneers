using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public abstract class PowerupManager : MonoBehaviour
{
    [SerializeField] 
    protected string itemDescription;
    protected bool isPlayerNearby = false;
    protected PlayerMovement playerMovement;
    protected PlayerStats playerStats;
    protected PlayerShooting playerShooting;
    public bool isPurchasable = false;
    private float price;

    private TextMeshProUGUI priceText;
    private GameObject canvas;


    void TryBuyPowerUp()
    {
        if (playerStats.getTotalCoins() >= price)
        {
            playerStats.addCoins(-price);
            ActivatePowerUp();
            if (priceText != null)
            {
                Destroy(priceText.gameObject); // Remove the price text after purchase
            }
        }
        else
        {
            Debug.Log("Not enough coins. Current coins: " + playerStats.getTotalCoins() + ", Required coins: " + price);
        }
    }

    private void CreatePriceText()
    {

        canvas = new GameObject("PowerUpCanvas");
        Canvas canvasComponent = canvas.AddComponent<Canvas>();
        canvasComponent.renderMode = RenderMode.WorldSpace;
        canvas.AddComponent<CanvasScaler>();
        canvas.AddComponent<GraphicRaycaster>();

        // Set the canvas as a child of the power-up
        canvas.transform.SetParent(transform);
        canvas.transform.localPosition = new Vector3(0, 1.5f, 0); // Position above the power-up
        // Resize canvas to fit the text
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        canvasRect.sizeDelta = new Vector2(2, 2);

        GameObject textObject = new GameObject("PriceText");
        textObject.transform.SetParent(canvas.transform);

        // Add TextMeshPro component
        TextMeshProUGUI priceTextTMP = textObject.AddComponent<TextMeshProUGUI>();
        priceTextTMP.text = "Price: " + price.ToString("F0");
        priceTextTMP.fontSize = 0.5f;
        priceTextTMP.color = Color.yellow;
        priceTextTMP.alignment = TextAlignmentOptions.Center;
        // Adjust the RectTransform of the text
        RectTransform textRect = priceTextTMP.GetComponent<RectTransform>();
        textRect.localPosition = Vector3.zero; // Position at (0, 0) relative to the canvas
        textRect.sizeDelta = new Vector2(20, 20); // Set width and height to 20x20

        TMP_FontAsset defaultFont = Resources.Load<TMP_FontAsset>("Fonts & Materials/LiberationSans SDF"); // Replace with your SDF font asset path
        if (defaultFont != null)
        {
            priceTextTMP.font = defaultFont;
        }
        else
        {
            Debug.LogWarning("SDF Font not found! Ensure you have a valid SDF font assigned.");
        }

        textObject.SetActive(false);

        priceText = priceTextTMP;
    }


    void Start()
    {
        if (isPurchasable)
        {
            price = Random.Range(50, 70)*GameObject.Find("RoomManager").GetComponent<RoomManager>().GetDifficulty();
            CreatePriceText();
        }
    }

    void Update()
    {
        if (isPurchasable && isPlayerNearby && Input.GetKeyDown(KeyCode.Space))
        {
            TryBuyPowerUp();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerMovement = other.GetComponent<PlayerMovement>();
            playerStats = other.GetComponent<PlayerStats>();
            playerShooting = other.GetComponent<PlayerShooting>();
            isPlayerNearby = true;

            if (!isPurchasable) // Direct pickup if not purchasable
            {
                ActivatePowerUp();
            }
            else if (priceText != null)
            {
                priceText.gameObject.SetActive(true); // Show the price when the player is nearby
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerMovement = null;
            isPlayerNearby = false;
        }
        if (priceText != null)
        {
            priceText.gameObject.SetActive(false); // Hide price when leaving
        }
    }

    protected abstract void ActivatePowerUp();
}
