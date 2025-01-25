using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RageMeter : MonoBehaviour
{
    [SerializeField] private float maxRage = 200f; // Maximum rage value
    [SerializeField] private float rageDecayRate = 40f; // Rage decrease per second when activated
    [SerializeField] private int damageBoost = 10; // Extra damage during rage
    [SerializeField] private Slider rageSlider; // Reference to UI slider for rage
    [SerializeField] private float currentRage = 0f;
    [SerializeField] private float shakeMagnitude = 4f; // Magnitude of the shake effect
    [SerializeField] private RectTransform rageMeterTransform; // The RageMeter UI's RectTransform

    [SerializeField] private AudioClip rageFullSound;
    [SerializeField] private AudioClip rageActivateSound;
    [SerializeField] private AudioClip rageStopSound;

    private PlayerStats playerStats;
    public bool isRageActive {get; private set;} = false;
    public bool testMode = false;
    public bool manualStop = false;
    private Vector3 originalPosition; // Original position for shaking effect

    public void TestStart()
    {
        if (rageSlider != null)
        {
            rageSlider.maxValue = maxRage;
            rageSlider.value = currentRage;
        }
        originalPosition = rageMeterTransform.localPosition;
    }
    void Start()
    {
        if (testMode)
        {
            TestStart();
            return;
        }
        if (rageSlider != null)
        {
            rageSlider.maxValue = maxRage;
            rageSlider.value = currentRage;
            playerStats=GameObject.Find("Player").GetComponent<PlayerStats>();
        }
        originalPosition = rageMeterTransform.localPosition;
    }
    public void TestUpdate()
    {
        Update();
    }   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && currentRage >= maxRage && !isRageActive)
        {
            Debug.Log("Rage activated!");
            StartCoroutine(ActivateRage());
        }
        if (rageSlider != null)
        {
            rageSlider.value = currentRage;
        }
    }

    public float getCurrentRage(){
        return currentRage;
    }

    public void setCurrentRage(float rage){
        currentRage = rage;
    }


    public void AddRage(float amount)
    {
        if (currentRage==maxRage){
            return;
        }
        if (!isRageActive)
        {
            currentRage = Mathf.Clamp(currentRage + amount, 0f, maxRage);
            if (currentRage == maxRage && !testMode) // Tested the Sound Manager playing sounds in Powerup Tests
            {
                SoundManager.Instance.PlaySound(rageFullSound);
                Debug.Log("Rage is full!");
            }
            Debug.Log("Rage: " + currentRage);
        }
    }

    public IEnumerator ActivateRage()
    {
        Debug.Log("In enumerator!");
        isRageActive = true;
        if (!testMode)
        {
            SoundManager.Instance.PlaySound(rageActivateSound);
            StartCoroutine(ShakeMeter());
        }

        if (testMode){
            FindObjectOfType<MockPlayerStats>().setPlayerDamage(FindObjectOfType<MockPlayerStats>().Damage+ damageBoost);
        }
        if (playerStats != null)
        {
            playerStats.setPlayerDamage(playerStats.getPlayerDamage()+ damageBoost); // Add the damage boost
        }

        float rageDuration = maxRage / rageDecayRate; // Calculate duration of rage
        float elapsed = 0f;

        while (elapsed < rageDuration)
        {
            elapsed += Time.deltaTime;

            if (testMode && elapsed >= 0.1f && manualStop)
            {
                yield break;
            }
            currentRage -= rageDecayRate * Time.deltaTime;
            yield return null;
        }
        if (testMode)
        {
            FindObjectOfType<MockPlayerStats>().setPlayerDamage(FindObjectOfType<MockPlayerStats>().Damage - damageBoost);
        }
        if (playerStats != null)
        {
            playerStats.setPlayerDamage(playerStats.getPlayerDamage() - damageBoost); // Remove the damage boost
        }

        isRageActive = false;
        if (!testMode)
            SoundManager.Instance.PlaySound(rageStopSound);
        currentRage = 0f;
    }

    public IEnumerator ShakeMeter()
    {
        float shakeDuration = maxRage/rageDecayRate; // Duration of the shake
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            elapsed += Time.deltaTime;
            Vector3 randomOffset = new Vector3(
                Random.Range(-shakeMagnitude, shakeMagnitude),
                Random.Range(-shakeMagnitude, shakeMagnitude),
                0f
            );

            rageMeterTransform.localPosition = originalPosition + randomOffset;
            yield return null;
        }

        // Reset to the original position
        rageMeterTransform.localPosition = originalPosition;
    }
}
