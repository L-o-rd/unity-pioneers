using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
    private bool isDead = false;
    private bool trapImmune = false;
    private float health;

    [SerializeField]
    private float totalCoins = 0;
	[SerializeField]
	private int permanentCoins = 0;

	[SerializeField]
	public int defenceLevel = 0;
    [SerializeField]
	public int maxHealthLevel = 0;
	[SerializeField]
    public int movementSpeedLevel = 0;
	[SerializeField]
    public int bonusDamageLevel = 0;
	private InGameTextUI inGameTextUI;
	private RageMeter rageMeter;

	private int defence;
    private float maxHealth;
	private float movementSpeed;
	private int playerDamage;

    public float getTotalCoins(){
        return totalCoins;
    }
    public void addCoins(float coins){
        totalCoins += coins;
    }

	public float getMovementSpeed()
	{
		return (float)(5.5 + 2.5 / 6 * movementSpeedLevel);
	}

	public int setPlayerDamage(int damage)
	{
		playerDamage = damage;
		return playerDamage;
	}

	public int getPlayerDamage()
	{
		return playerDamage;
	}
	public int getBonusDamage()
	{
		return bonusDamageLevel * 2;
	}

	public float getMaxHealth()
	{
		return 100 + maxHealthLevel * 10;
    }

	public int getDefence()
	{
		return defenceLevel * 2;
	}

    public bool isTrapImmune(){
        return trapImmune;
    }

    public void setTrapImmune(bool value){
        trapImmune = value;
    }

    public void Heal(float heal) {
        if (isDead) return;
		
        if (health + heal > maxHealth) {
			heal = maxHealth - health;
        }
        health += heal;
		inGameTextUI.ShowWorldFeedback("+"+heal+"HP",Color.green);
		if (rageMeter != null)
		{
			rageMeter.AddRage(Mathf.Floor(-heal / 5));
		}
        Debug.Log(string.Format("Health remaining: {0}.", health));
    }

    private void Start()
	{
		//TODO read stats levels from savefile

		maxHealth = getMaxHealth();

		health = maxHealth;

		rageMeter = GetComponent<RageMeter>();

		if (rageMeter==null){
			Debug.LogWarning("RageMeter not found");
		}
			
		inGameTextUI = FindObjectOfType<InGameTextUI>();
	}

	private void Update()
	{
		if (isDead)
		{
			return;
		}
		inGameTextUI.UpdateCoinText(totalCoins);
		inGameTextUI.UpdateHPText(health);
		// Debug: reduce health with the Space key
		if (Input.GetKey(KeyCode.Space))
		{
			TakeDamage(1);
		}

		if (health <= 0)
		{
			isDead = true;
			Die();
		}
	}

	public void TakeDamage(float amount)
	{
		if (isDead)
		{
			return;
		}

		health -= (amount - getDefence());
		inGameTextUI.ShowWorldFeedback("-"+amount+"HP",Color.red);
		if (rageMeter != null)
		{
			rageMeter.AddRage(Mathf.Floor(amount / 4));
		}
		Debug.Log($"Health remaining: {health}");

		if (health <= 0)
		{
			isDead = true;
			Die();
		}
	}

	private void Die()
	{
		Debug.Log("Player is dead!");
	}

	public void UpgradeMaxHealth()
	{
		if (maxHealthLevel == 6)
			return;
		int price = 1 + maxHealthLevel * 2;
		if (price <= permanentCoins)
		{
			permanentCoins -= price;
			maxHealthLevel += 1;
			health = getMaxHealth();
		}
	}

	public void UpgradeMovementSpeed()
	{
        if (movementSpeedLevel == 6)
            return;
        int price = 1 + movementSpeedLevel * 2;
        if (price <= permanentCoins)
        {
            permanentCoins -= price;
            movementSpeedLevel += 1;
        }
    }

	public void UpgradeBonusDamage()
	{
        if (bonusDamageLevel == 6)
            return;
        int price = 1 + bonusDamageLevel * 2;
        if (price <= permanentCoins)
        {
            permanentCoins -= price;
            bonusDamageLevel += 1;
        }
    }

	public void UpgradeDefence()
	{
        if (defenceLevel == 6)
            return;
        int price = 1 + defenceLevel * 2;
        if (price <= permanentCoins)
        {
            permanentCoins -= price;
            defenceLevel += 1;
        }
    }
	public int GetPermanentCoins()
	{
		return this.permanentCoins;
	}
}
