using UnityEngine;
using System.Collections;
using TMPro;

public class Gun : MonoBehaviour
{
<<<<<<< HEAD
=======
    [Header("Gun Stats")]
>>>>>>> 198360400bcfdcda90d9bb689c30ddc9f0338add
    public float damage = 10f;
    public float range = 100f;

    [Header("Ammo System")]
    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;

    [Header("References")]
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public TextMeshProUGUI ammoText;
    public Animator animator;

    void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoUI();
    }

    void Update()
    {
<<<<<<< HEAD
        if (isReloading) return;

=======
        if (isReloading)
            return;

        // Auto-reload if empty
>>>>>>> 198360400bcfdcda90d9bb689c30ddc9f0338add
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

<<<<<<< HEAD
=======
        // Manual reload
>>>>>>> 198360400bcfdcda90d9bb689c30ddc9f0338add
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        if (ammoText != null)
            ammoText.text = "RELOADING...";

        if (animator != null)
            animator.SetTrigger("Reload");
<<<<<<< HEAD
        else
            Debug.LogWarning("No Animator assigned to the Gun script!");
=======
        }
>>>>>>> 198360400bcfdcda90d9bb689c30ddc9f0338add

        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
        UpdateAmmoUI();
    }

    void Shoot()
    {
        if (muzzleFlash != null)
            muzzleFlash.Play();

        currentAmmo--;
        UpdateAmmoUI();

        RaycastHit hit;
        // Shoot from camera center forward
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log("Hit: " + hit.transform.name);

<<<<<<< HEAD
=======
            // Handle damage
>>>>>>> 198360400bcfdcda90d9bb689c30ddc9f0338add
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
                target.TakeDamage(damage);

<<<<<<< HEAD
=======
            // Handle impact particles
>>>>>>> 198360400bcfdcda90d9bb689c30ddc9f0338add
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
<<<<<<< HEAD
    }
}
=======
        }
    }
}
>>>>>>> 198360400bcfdcda90d9bb689c30ddc9f0338add
