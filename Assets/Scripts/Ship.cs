using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Ship : MonoBehaviour {

    Rigidbody rb;
    AudioSource audioSource;

    [HideInInspector]
    public Quaternion rotation;

    public GameObject warningPanel;
    public Material thrusterMaterial;

    float speed = 0f;
    float rollSpeed = 0f;
    float pitchSpeed = 0f;

    const float shipSpeed = 160f;
    const float mouseSpeed = 50.0f;
    
    const float rollSpeedAccel = 140f;
    const float rollSpeedDeaccel = 200f;
    const float rollSpeedMax = 100f;
    
    const float pitchSpeedAccel = 400f;
    const float pitchSpeedMax = 50f;

    const float maxSpeedLimiter = .25f;

    const float boostSpeed = 160f;
    const float boostWarmupTime = 1;
    const float boostWarmdownTime = 1;
    
    const float rollingEffectFromTurnAccel = 20f;
    const float rollingEffectFromTurnDeaccel = 60f;
    const float rollingEffectFromTurnMax = 50.0f;

    const float maxDistanceFromOrigin = 1000.0f;

    float fireRate = .6f;
    float timeSinceFired = 10;

    float mouseX;
    float mouseY;
    float vertical;
    float horizontal;
    float rollingEffectFromTurn = 0f;
    float boostEffect = 0;

    bool active = true;

    GameObject projectilePrefab;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        projectilePrefab = Resources.Load("Prefabs/Projectile") as GameObject;

        rotation = transform.rotation;
        Cursor.lockState = CursorLockMode.Locked;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        // Get inputs        
        if (active) {
            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");

            if (Input.GetButton("Fire1") && timeSinceFired > fireRate) {
                Instantiate(projectilePrefab, transform.position + transform.forward * 5, transform.rotation);
                timeSinceFired = 0;
            }
            timeSinceFired += Time.deltaTime;

            if (Input.GetButton("Fire2")) {
                boostEffect += Time.deltaTime / boostWarmupTime;
                if (boostEffect > 1) {
                    boostEffect = 1;
                }
            } else {
                boostEffect -= Time.deltaTime / boostWarmdownTime;
                if (boostEffect < 0) {
                    boostEffect = 0;
                }
            }
        } else {
            mouseX = 0;
            mouseY = 0;
            horizontal = 0;
            vertical = 0;
        }
        
        // set thruster sound volume
        audioSource.volume = 0.2f + (boostEffect * 0.8f);

        Color thrusterColor = new Color(0, boostEffect, 0, boostEffect);
        thrusterMaterial.SetColor("_EmissionColor", thrusterColor);
        thrusterMaterial.SetColor("_Color", thrusterColor);

        // can't turn while boost is active
        if (boostEffect > 0) {
            mouseX = mouseX * (1 - boostEffect);
            mouseY = mouseY * (1 - boostEffect);
        }

        // Handle yaw acceleration
        if (mouseX < 0 && rollingEffectFromTurn > -rollingEffectFromTurnMax) {
            rollingEffectFromTurn += mouseX * rollingEffectFromTurnAccel * Time.deltaTime;
            if (rollingEffectFromTurn > rollingEffectFromTurnDeaccel * Time.deltaTime)
                rollingEffectFromTurn -= rollingEffectFromTurnDeaccel * Time.deltaTime;
            
        } else if (mouseX > 0 && rollingEffectFromTurn < rollingEffectFromTurnMax) {
            rollingEffectFromTurn += mouseX * rollingEffectFromTurnAccel * Time.deltaTime;
            if (rollingEffectFromTurn < -rollingEffectFromTurnDeaccel * Time.deltaTime)
                rollingEffectFromTurn += rollingEffectFromTurnDeaccel * Time.deltaTime;
        } else {
            if (rollingEffectFromTurn > rollingEffectFromTurnDeaccel * Time.deltaTime)
                rollingEffectFromTurn -= rollingEffectFromTurnDeaccel * Time.deltaTime;
            else if (rollingEffectFromTurn < -rollingEffectFromTurnDeaccel * Time.deltaTime)
                rollingEffectFromTurn += rollingEffectFromTurnDeaccel * Time.deltaTime;
            else
                rollingEffectFromTurn = 0;
        }

        // Handle rolling acceleration
        if (horizontal > 0 && rollSpeed > -rollSpeedMax) {
            rollSpeed -= rollSpeedAccel * Time.deltaTime;
        } else if (horizontal < 0 && rollSpeed < rollSpeedMax) {
            rollSpeed += rollSpeedAccel * Time.deltaTime;
        } else {
            if (rollSpeed > rollSpeedDeaccel * Time.deltaTime)
                rollSpeed -= rollSpeedDeaccel * Time.deltaTime;
            else if (rollSpeed < -rollSpeedDeaccel * Time.deltaTime)
                rollSpeed += rollSpeedDeaccel * Time.deltaTime;
            else
                rollSpeed = 0;
        }
        // Handle pitch acceleration
        if (vertical > 0 && pitchSpeed > -pitchSpeedMax) {
            pitchSpeed -= pitchSpeedAccel * Time.deltaTime;
        } else if (vertical < 0 && pitchSpeed < pitchSpeedMax) {
            pitchSpeed += pitchSpeedAccel * Time.deltaTime;
        } else {
            if (pitchSpeed > pitchSpeedAccel * Time.deltaTime)
                pitchSpeed -= pitchSpeedAccel * Time.deltaTime;
            else if (pitchSpeed < -pitchSpeedAccel * Time.deltaTime)
                pitchSpeed += pitchSpeedAccel * Time.deltaTime;
            else
                pitchSpeed = 0;
        }

        //
        // Apply movements
        //
        rotation *= Quaternion.Euler(-mouseY * mouseSpeed * Time.deltaTime, 0, 0); // pitch
        rotation *= Quaternion.Euler(0, mouseX * mouseSpeed * Time.deltaTime, 0); // yaw
        rotation *= Quaternion.Euler(0, 0, rollSpeed * Time.deltaTime); // roll

        transform.rotation = rotation;
        transform.RotateAround(transform.position, transform.forward, -rollingEffectFromTurn); // roll caused by yaw (mouseX turning)

        float rollStrength = -Vector3.Dot(transform.up, Vector3.Cross(transform.forward, Vector3.up)); // used for slowing forward motion when turning
        speed = (shipSpeed + boostEffect * boostSpeed) * (1 - (Mathf.Abs(rollStrength) * maxSpeedLimiter));

        rb.velocity = transform.forward * speed; // forward movement

        // display warnring if player is nearing bounds of playable area
        if (transform.position.magnitude > maxDistanceFromOrigin * 0.8) {
            warningPanel.SetActive(true);
        } else {
            warningPanel.SetActive(false);
        }
        if (transform.position.magnitude > maxDistanceFromOrigin) {
            transform.position = Vector3.ClampMagnitude(transform.position, maxDistanceFromOrigin);
        }
    }

    void SetActive(bool b) {
        active = b;
    }
}
