
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private readonly KeyCode right = KeyCode.D;
    private readonly KeyCode left = KeyCode.A;
    private readonly KeyCode down = KeyCode.S;
    private readonly KeyCode up = KeyCode.W;

    private float horizontal = 0f;
    private float vertical = 0f;
    private Vector2 movement;
    private Rigidbody2D rb;
    private KeyCode lastKeyPressed = KeyCode.None;
    private float doubleTapThreshold = 0.3f;
    private float lastKeyPressedTime;
    private bool isDashing = false;
    private bool canDash = false;
    private bool immuneToSlow = false;
    private bool inMud = false; // to prevent multiple speed change calls and to fix dash bug
    private float maxSpeed;
    private Animator animator;

    [SerializeField]
    private GameObject direction;

    [SerializeField] 
    private float dashSpeedMultiplier = 2.5f;

    [SerializeField] 
    private float dashDuration = 0.2f;

    [SerializeField]
    private float dashCooldown = 5f;

    [SerializeField]
    private PlayerStats playerStats;

    private void Start() {
        rb = this.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
    }

    private void Update() {
        if (inMud){
            maxSpeed = playerStats.getMovementSpeed()/2;
        }
        else{
            maxSpeed = playerStats.getMovementSpeed();
        }
        horizontal = Input.GetKey(right) ? 1f : (Input.GetKey(left) ? -1f : 0f);
        vertical = Input.GetKey(up) ? 1f : (Input.GetKey(down) ? -1f : 0f);
        movement.x = horizontal;
        movement.y = vertical;
        movement.Normalize();

        if(movement.x == 0 && movement.y == 0)
        {
            animator.SetBool("isMoving", false);
        }
        else
        {
            animator.SetBool("isMoving", true);
        }

        if (DetectDoubleTap(right) || DetectDoubleTap(left) || 
            DetectDoubleTap(up) || DetectDoubleTap(down))
        {
            if (!isDashing && canDash)
            {
                StartCoroutine(Dash());
            }
        }

    }

    private void FixedUpdate() {
        if (!isDashing) {
            rb.MovePosition(rb.position + movement * maxSpeed * Time.fixedDeltaTime);
        }

        if (direction is not null)
        {
            if (movement != Vector2.zero)
            {
                direction.SetActive(true);
                float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
                direction.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
                direction.transform.position = rb.position + movement * 1.0f;
            }
            else direction.SetActive(false);
        }
    }

    public bool IsImmuneToSlow()
    {
        return immuneToSlow;
    }

    public void setInMud(bool value)
    {
        inMud = value;
    }
    public void SlowPlayerBy(float percentage)
    {

        if (percentage < 0 || percentage > 1) {
            Debug.LogWarning("Invalid percentage value");
            return;
        }

        playerStats.ScaleSpeed(percentage);
    }

    public void SpeedUpPlayerBy(float percentage)
    {

        if (percentage < 0 || percentage > 1){
            Debug.LogWarning("Invalid percentage value");
            return;
        }

        playerStats.ScaleSpeed(1.0f / percentage);
    }

    private bool DetectDoubleTap(KeyCode key) {
        if (Input.GetKeyDown(key)){
            // Debug.Log("Detecting double tap with pressed key: "+key);
            if (lastKeyPressed == key && Time.time - lastKeyPressedTime < doubleTapThreshold) {
                return true;
            }
            lastKeyPressed = key;
            lastKeyPressedTime = Time.time;
        }

        return false;
    }

    private IEnumerator Dash()
    {
        Debug.Log("Dashing");
        isDashing = true;
        canDash = false;
        immuneToSlow = true;
        Vector2 dashDirection = movement != Vector2.zero ? movement : Vector2.up;

        float dashSpeed = maxSpeed * dashSpeedMultiplier;
        float elapsedTime = 0f;

        while (elapsedTime < dashDuration)
        {
            rb.MovePosition(rb.position + dashDirection * dashSpeed * Time.fixedDeltaTime);
            elapsedTime += Time.fixedDeltaTime;
            yield return null;
        }
        immuneToSlow = false;
        isDashing = false;

        // Start cooldown
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    public void ActivateDashPower(){
        if (canDash){
            ReduceDashCooldown();
        }
        Debug.Log("Dash Power Activated");
        Debug.Log("Dash Cooldown: "+dashCooldown);
        canDash=true;
    }

    public void ReduceDashCooldown(){
        dashCooldown=Mathf.Max(dashCooldown-1,1);
    }
}


