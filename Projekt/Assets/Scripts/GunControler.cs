using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControler : MonoBehaviour
{

    private InputManager inputManager;
    public bool readytoshot;
    public int guntype;
    public GameObject Bullet;
    private float spread;

    private float nextFire;
    private float FireRate = 1f;
    // Start is called before the first frame update
    void Start()
    {
        inputManager = InputManager.Instance;
        readytoshot = false;
    }

    // Update is called once per frame
    void Update() {
        if (inputManager.PlayerSchooted())
            readytoshot = !readytoshot;

        if (readytoshot && Time.time > nextFire) {
            nextFire = Time.time + FireRate;
            Debug.Log("PEW");
            if (guntype == 3)
                for (int i = 7; i >= 0; i--)
                    Shoot();
            else
                Shoot();
        }
        else {
            if (inputManager.NextGun()) {
                changeGun(1);
            }
            if (inputManager.PrevGun()) {
                changeGun(-1);
            }
        }

    }

    void changeGun(int i) {
        if (guntype + i > 3) { guntype = 1; }
        else if (guntype - i > 0) { guntype = 3; }
        else guntype += i;            

        switch (guntype) {
            case 1:
                FireRate = 0.5f;
                spread = 0f;
                
                break;
            case 2:
                FireRate = 0.01f;
                spread = 2f;

                break;
            case 3:
                FireRate = 1f;
                spread = 5f;

                break;
        }
    }

    void Shoot() {
        Vector3 barrelPos = transform.GetChild(0).position;

        var randomNumberX = Random.Range(-spread, spread);
        var randomNumberY = Random.Range(-spread, spread);
        var randomNumberZ = Random.Range(-spread, spread);

        var bullet = Instantiate(Bullet, barrelPos, transform.parent.rotation);
        bullet.transform.Rotate(randomNumberX, randomNumberY, randomNumberZ);

        Destroy(bullet, 5);
    }
}
