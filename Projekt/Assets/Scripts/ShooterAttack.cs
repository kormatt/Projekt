using UnityEngine;
using System.Collections;

public class ShooterAttack : MonoBehaviour {


    private Quaternion bulletRotation;
    private GameObject playerObject;
    private Vector3 playerPosition;
    private Transform playerTransform;
    private Transform rifleExitTransform;
    private Transform shooterTransform;
    private Transform targetForShooterTransform;

    public float countdownToShot = 0f;
    public float shotInterval = 0.3f;
    public float shotIntervalRandomization = 0.5f;

    public GameObject shooterBulletPrefab; //nie zd¹¿y³e sprawdziæ czy mo¿e byæ private

    void Start() {
        shotInterval += Random.Range(-shotIntervalRandomization, shotIntervalRandomization);
        playerObject = GameObject.FindWithTag("Player");
        playerTransform = playerObject.transform;
        shooterTransform = transform;

        rifleExitTransform = shooterTransform.Find("Rifle_exit").transform;
        targetForShooterTransform = playerTransform.transform;

    }

    private bool Targeting(float range) {
        Transform shooterEyesTransform = shooterTransform.Find("Shooter_Eyes").transform;
        Ray shooterSightRay = new Ray(shooterEyesTransform.position, shooterTransform.forward);
        RaycastHit hitInfo;

        playerPosition = new Vector3(
            targetForShooterTransform.position.x,
            targetForShooterTransform.position.y,
            targetForShooterTransform.position.z
            );

        if (Physics.Raycast(shooterSightRay, out hitInfo, range)) {
            GameObject hitObject = hitInfo.collider.gameObject;

            if (hitObject.name.Equals(playerObject.name)) {
                return true;
            }
        }
        return false;
    }

    public Quaternion GetBulletRotation() {

        Vector3 riflePosition = rifleExitTransform.position + rifleExitTransform.forward;
        bulletRotation = Quaternion.LookRotation(playerPosition - riflePosition);

        return bulletRotation;
    }

    public float GetVisionRange() {
        ShooterController shooterController = gameObject.GetComponent<ShooterController>();
        return shooterController.GetVisionRange();
    }

    public void Shot() {
        Health playerHealth = playerObject.GetComponent<Health>();
        if (!playerHealth.IsDead()) {
            if (countdownToShot < shotInterval) {
                countdownToShot += Time.deltaTime;
            }

            if (countdownToShot >= shotInterval && Targeting(GetVisionRange())) {
                countdownToShot = 0;
                Vector3 riflePosition = rifleExitTransform.position + rifleExitTransform.forward;

                Instantiate(shooterBulletPrefab, riflePosition, GetBulletRotation());
            }
        }
    }
}