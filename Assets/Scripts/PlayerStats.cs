using UnityEngine;

public class PlayerStats : MonoBehaviour {
    private bool isDead = false;
    private bool trapImmune = false;
    private float health;

    [SerializeField]
    private float maxHealth = 100;
    private float totalCoins = 0;

    public float getTotalCoins(){
        return totalCoins;
    }
    public void addCoins(float coins){
        totalCoins += coins;
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
            health = maxHealth;
            return;
        }
        health += heal;
        Debug.Log(string.Format("Health remaining: {0}.", health));
    }

	private void Start()
	{
		health = maxHealth;
	}

	private void Update()
	{
		if (isDead)
		{
			return;
		}

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

		health -= amount;
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
}
