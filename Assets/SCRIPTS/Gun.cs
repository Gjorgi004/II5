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
            if (target != null)
                target.TakeDamage(damage);

            if (impactEffect != null)
            {
                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2f);
            }
        }
    }

    void UpdateAmmoUI()
    {
        if (ammoText != null)
            ammoText.text = "AMMO " + currentAmmo + "/" + maxAmmo;
    }
}

