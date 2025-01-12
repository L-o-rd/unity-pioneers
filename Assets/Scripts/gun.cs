using Unity.VisualScripting;
using UnityEngine;

public class GunRotation : MonoBehaviour {
    [SerializeField]
    private Transform gun; // Referință către arma copil
    private Quaternion initq;

    private void Awake()
    {
        initq = gun.rotation;
    }

    private void Update() {
        if (PauseManager.Instance.isPaused) return;
        // Obține poziția mouse-ului în coordonatele lumii
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; // Setează Z la 0 pentru a evita problemele 2D

        // Calculează direcția de la player către mouse
        Vector3 direction = (mousePos - transform.position).normalized;

        // Calculează unghiul de rotație în grade
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Setează rotația player-ului
        gun.rotation = initq;
        gun.RotateAround(transform.position, Vector3.forward, angle);
        gun.position = transform.position + direction * 1.2f;
    }
}
