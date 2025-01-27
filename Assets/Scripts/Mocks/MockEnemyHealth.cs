using UnityEngine;
public class MockEnemyHealth : MonoBehaviour
{
    public float DamageTaken { get; private set; }
    public void TakeDamage(float damage) => DamageTaken += damage;
}