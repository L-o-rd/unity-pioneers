using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Status
{
    public int permanentCoins = 0;
    public int defenceLevel = 0;
    public int maxHealthLevel = 0;
    public int movementSpeedLevel = 0;
    public int bonusDamageLevel = 0;
}

public class PlayerStats : MonoBehaviour {
    private bool isDead = false;
    private bool trapImmune = false;
    private float health;

	[SerializeField]
	public Status status = new Status();

    [SerializeField]
    private float totalCoins = 0;
	
	private InGameTextUI inGameTextUI;
	private RageMeter rageMeter;

	private float baseSpeed = 5.5f;
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
		return (float)(baseSpeed + 2.5 / 6 * status.movementSpeedLevel);
	}

    public void ScaleSpeed(float factor)
    {
		baseSpeed *= factor;
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
		return status.bonusDamageLevel * 2;
	}

	public float getMaxHealth()
	{
		return 100 + status.maxHealthLevel * 10;
    }

	public int getDefence()
	{
		return status.defenceLevel * 2;
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

	public void SaveStats()
	{
		Debug.Log(Application.persistentDataPath);
        FileStream file = File.Create(Application.persistentDataPath + "/playerStats.dat");
        BinaryFormatter bf = new BinaryFormatter();
		bf.Serialize(file, this.status);
        file.Close();
    }

	public void LoadStats()
	{
        if (File.Exists(Application.persistentDataPath + "/playerStats.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerStats.dat", FileMode.Open);
			this.status = (Status) bf.Deserialize(file);
            file.Close();
        }
    }

    private void Start()
	{
		this.LoadStats();
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
	}

	private void Die()
	{
		Debug.Log("Player is dead!");
		status.permanentCoins += RNGManager.Instance.diamonds;
        SaveStats();
        SceneManager.LoadScene("GameOver");
    }

	public void UpgradeMaxHealth()
	{
		if (status.maxHealthLevel == 6)
			return;
		int price = 1 + status.maxHealthLevel * 2;
		if (price <= status.permanentCoins)
		{
			status.permanentCoins -= price;
			status.maxHealthLevel += 1;
			health = getMaxHealth();
            this.SaveStats();
        }
	}

	public void UpgradeMovementSpeed()
	{
        if (status.movementSpeedLevel == 6)
            return;
        int price = 1 + status.movementSpeedLevel * 2;
        if (price <= status.permanentCoins)
        {
            status.permanentCoins -= price;
            status.movementSpeedLevel += 1;
            this.SaveStats();
        }
    }

	public void UpgradeBonusDamage()
	{
        if (status.bonusDamageLevel == 6)
            return;
        int price = 1 + status.bonusDamageLevel * 2;
        if (price <= status.permanentCoins)
        {
            status.permanentCoins -= price;
            status.bonusDamageLevel += 1;
            this.SaveStats();
        }
    }

	public void UpgradeDefence()
	{
        if (status.defenceLevel == 6)
            return;
        int price = 1 + status.defenceLevel * 2;
        if (price <= status.permanentCoins)
        {
            status.permanentCoins -= price;
            status.defenceLevel += 1;
            this.SaveStats();
        }
    }
	public int GetPermanentCoins()
	{
		return this.status.permanentCoins;
	}
}




 