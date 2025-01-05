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
    private PlayerStats playerStats;
    private bool isRageActive = false;
    private Vector3 originalPosition; // Original position for shaking effect


    void Start()
    {
        if (rageSlider != null)
        {
            rageSlider.maxValue = maxRage;
            rageSlider.value = currentRage;
            playerStats=GameObject.Find("Player").GetComponent<PlayerStats>();
        }
        originalPosition = rageMeterTransform.localPosition;
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

    public void AddRage(float amount)
    {
        if (!isRageActive)
        {
            currentRage = Mathf.Clamp(currentRage + amount, 0f, maxRage);
            Debug.Log("Rage: " + currentRage);
        }
    }

    private IEnumerator ActivateRage()
    {
        Debug.Log("In enumerator!");
        isRageActive = true;
        StartCoroutine(ShakeMeter());
        if (playerStats != null)
        {
            playerStats.setPlayerDamage(playerStats.getPlayerDamage()+ damageBoost); // Add the damage boost
        }

        float rageDuration = maxRage / rageDecayRate; // Calculate duration of rage
        float elapsed = 0f;

        while (elapsed < rageDuration)
        {
            elapsed += Time.deltaTime;
            currentRage -= rageDecayRate * Time.deltaTime;
            yield return null;
        }

        if (playerStats != null)
        {
            playerStats.setPlayerDamage(playerStats.getPlayerDamage() - damageBoost); // Remove the damage boost
        }

        isRageActive = false;
        currentRage = 0f;
    }

    private IEnumerator ShakeMeter()
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
