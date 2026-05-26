using UnityEngine;
using System.Collections;
using TMPro;

public class Gun : MonoBehaviour
{
    [Header("Gun Stats")]
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 1f; 

    [Header("Ammo System")]
    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;
    private float nextTimeToFire = 1f;

    [Header("Charge settings")]
    public float chargedBlastDamage = 50f;  // Heavy damage!
    public float chargeDuration = 1.5f;    // Time required to hold down right-click
    private float currentChargeTime = 0f;
    private bool isCharging = false;

    [Header("References")]
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public TextMeshProUGUI ammoText;
    public Animator animator;

    [Header("Sound Effects")]
    public AudioSource audioSource;
    public AudioClip shootSound;    
    public AudioClip reloadSound;
    public AudioClip chargeLoopSound;

    [Header("Beam")]
    public LineRenderer tracerEffect;
    public Transform shootPoint;
    public LineRenderer chargedbeam;
    public ParticleSystem chargeup;
        
    void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoUI();
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isReloading) return;

        if (Time.timeScale == 0) return;

        if (currentAmmo <= 0 && !isReloading)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo && !isReloading)
        {
            StartCoroutine(Reload());
            return;
        }


        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }

            HandleChargedInput();
        
    }

    IEnumerator Reload()
    {
        isReloading = true;
        if (ammoText != null) ammoText.text = "RELOADING...";
        if (animator != null)
        {
            animator.SetTrigger("Reload");
        }
        if (audioSource != null && reloadSound != null)
        {
            audioSource.PlayOneShot(reloadSound);
        }

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
        UpdateAmmoUI();
    }

    void Shoot()
    {
        if (currentAmmo <= 0) return;
        if (audioSource != null && shootSound != null)
        {
            
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(shootSound);
        }
        if (animator != null)
        {
            animator.SetTrigger("Fire");
        }

        if (muzzleFlash != null)
            muzzleFlash.Play();

        currentAmmo--;
        UpdateAmmoUI();

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();

            StartCoroutine(SpawnBeam(hit.point));

            if (target != null)
                target.TakeDamage(damage);

            BossTarget target2 = hit.transform.GetComponent<BossTarget>();
            if (target2 != null)
                target2.TakeDamage(damage);

            if (impactEffect != null)
            {
                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2f);
            }

            

        }
        else
        {
            Vector3 farPoint = fpsCam.transform.position + (fpsCam.transform.forward * range);
            StartCoroutine(SpawnBeam(farPoint));
        }
    }

    IEnumerator SpawnBeam(Vector3 hitPoint)
    {
        tracerEffect.gameObject.SetActive(true);

        tracerEffect.SetPosition(0, shootPoint.position);

        tracerEffect.SetPosition(1, hitPoint);

        yield return new WaitForSeconds(0.05f);

        tracerEffect.gameObject.SetActive(false);

    }

    IEnumerator SpawnChargedBeam(Vector3 hitPoint)
    {
        chargedbeam.gameObject.SetActive(true);

        chargedbeam.SetPosition(0, shootPoint.position);

        chargedbeam.SetPosition(1, hitPoint);

        yield return new WaitForSeconds(0.05f);

        chargedbeam.gameObject.SetActive(false);

    }

    void HandleChargedInput()
    {
        if (Input.GetButtonDown("Fire2") && Time.time >= nextTimeToFire && currentAmmo >= 2)
        {
            isCharging = true;
            currentChargeTime = 0f;

            if (animator != null) animator.SetBool("IsCharging", true);
            if (audioSource != null && chargeLoopSound != null) audioSource.PlayOneShot(chargeLoopSound);

            if (chargeup != null)
            {
                // TURN IT BACK ON HERE
                chargeup.gameObject.SetActive(true);

                var emission = chargeup.emission;
                emission.enabled = true;
                chargeup.Play();
            }

            Debug.Log("Charging blast...");
        }
        // 2. Continuous Holding & Release Checks (Using ELSE IF to prevent same-frame overlap)
        else if (isCharging)
        {
            // Advance timer calculation as long as button is actively held down
            if (Input.GetButton("Fire2"))
            {
                currentChargeTime += Time.deltaTime;

                if (currentChargeTime >= chargeDuration)
                {
                    FireChargedBlast();
                    isCharging = false;
                }
            }
            // If they are no longer holding it, it's an immediate release!
            else
            {
                CancelCharge();
            }
        }

        // 3. Fail-safe: Double check particle system states match logic states
        if (!isCharging && chargeup != null && chargeup.isPlaying)
        {
            var emission = chargeup.emission;
            emission.enabled = false;
            chargeup.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            chargeup.Clear();
        }
    }

    void FireChargedBlast()
    {
        nextTimeToFire = Time.time + 1f / fireRate; // Apply normal fire cooldown

        if (animator != null)
        {
            animator.SetBool("IsCharging", false);
            animator.SetTrigger("FireCharged"); // Trigger special firing animation if you have one
        }

        if (muzzleFlash != null)
            muzzleFlash.Play();

        // Deduct ammo chunk cost
        currentAmmo = Mathf.Max(0, currentAmmo - 2);
        UpdateAmmoUI();

        // Audio trigger
        if (audioSource != null && shootSound != null)
        {
            audioSource.pitch = 0.6f; // Lowers pitch to sound heavy and powerful!
            audioSource.PlayOneShot(shootSound);
        }

        // Raycasting for damage calculation
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            // Use specialized heavy damage instead of baseline stat
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
                target.TakeDamage(chargedBlastDamage);

            BossTarget target2 = hit.transform.GetComponent<BossTarget>();
            if (target2 != null)
                target2.TakeDamage(chargedBlastDamage);

            StartCoroutine(SpawnChargedBeam(hit.point));

            if (impactEffect != null)
            {
                // Spawn an impact visual effect at hit location
                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                impactGO.transform.localScale = Vector3.one * 2.5f; // Scales impact explosion up to look massive
                Destroy(impactGO, 2f);
            }
        }
        else
        {
            Vector3 farPoint = fpsCam.transform.position + (fpsCam.transform.forward * range);
            StartCoroutine(SpawnChargedBeam(farPoint));
        }

        chargeup.Stop();
        Debug.Log("Charged Blast Fired!");
    }


    void CancelCharge()
    {
        if (!isCharging) return;

        isCharging = false;
        currentChargeTime = 0f;
        if (animator != null) animator.SetBool("IsCharging", false);
        if (tracerEffect != null) tracerEffect.gameObject.SetActive(false);
        audioSource.Stop();
        // --- BRUTE FORCE PARTICLE SHUTDOWN ---
        if (chargeup != null)
        {
            // 1. Force the emission module completely off
            var emission = chargeup.emission;
            emission.enabled = false;

            // 2. Kill all active visual particles instantly
            chargeup.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            chargeup.Clear();

            chargeup.gameObject.SetActive(false);
        }
    }

    void UpdateAmmoUI()
    {
        if (ammoText != null)
            ammoText.text = "AMMO " + currentAmmo + "/" + maxAmmo;
    }
}

