using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 5f;
    public float range = 1000f;
    public float bulletSpeed = 50f;
    public float impactForce = 30f;
    public float shootCooldown = 0.5f;

    private bool canShoot = true;

    [SerializeField] private Animator animator;
    public Camera cam;
    public ParticleSystem muzzleFlash;
    public GameObject gunCamera;
    public AudioSource gunSound;
    public GameObject bullet;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        gunSound = GetComponentInParent<AudioSource>();
    }

    private void Update()
    {
        gunSound.pitch = 2.0f;
        if (Input.GetButton("Fire1") && canShoot)
        {
            Shoot();
            StartCoroutine(ShootCooldown(shootCooldown));
        }

        if (Input.GetButton("Fire1"))
        {
            animator.SetBool("isShooting", true);
            gunCamera.SetActive(true);

            if (!gunSound.isPlaying)
            {
                gunSound.Play();
            }
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            animator.SetBool("isShooting", false);
            gunCamera.SetActive(false);
            muzzleFlash.Stop();

            if (gunSound.isPlaying)
            {
                gunSound.Stop();
            }
        }
    }

    private IEnumerator ShootCooldown(float duration)
    {
        canShoot = false;
        yield return new WaitForSeconds(duration);
        canShoot = true;
    }

    public void Shoot()
    {
        muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            Debug.Log($"You hit {hit.transform.name}");

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
        }

        FireBullet();
    }

    private void FireBullet()
    {
        // Instantiate the bullet
        GameObject firedBullet = Instantiate(bullet, cam.transform.position + cam.transform.forward, Quaternion.LookRotation(cam.transform.forward));

        Rigidbody bulletRigidbody = firedBullet.GetComponent<Rigidbody>();
        if (bulletRigidbody != null)
        {
            // Apply a forward force to the bullet
            bulletRigidbody.AddForce(cam.transform.forward * bulletSpeed, ForceMode.Impulse);
        }

        // Destroy the bullet after 2 seconds
        Destroy(firedBullet, 3f);
    }
}
