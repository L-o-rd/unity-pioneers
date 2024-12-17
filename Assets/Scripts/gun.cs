using UnityEngine;

public class GunRotation : MonoBehaviour {
    [SerializeField]
    private Transform gun; // Referință către arma copil

    private void Update() {
        // Obține poziția mouse-ului în coordonatele lumii
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; // Setează Z la 0 pentru a evita problemele 2D

        // Calculează direcția de la player către mouse
        Vector3 direction = (mousePos - transform.position).normalized;

        // Calculează unghiul de rotație în grade
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Setează rotația player-ului
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Sincronizează rotația gun-ului
        if (gun != null) {
            gun.rotation = Quaternion.Euler(0, 0, angle); // Gun urmează aceeași rotație
        }
    }
}
