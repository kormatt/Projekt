using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GunControler : MonoBehaviour {

    private InputManager inputManager;
    private bool readytoshot;

    public GameObject Bullet;    
    private float nextFire;

    private int shoots = 0; 
    private float spread = 0;
    private float firstspread = 2f;  
    private float FireRate = 1f;
    private int bulletDamage = 1;
    //Singleton pattern 
    private static GunControler _instance;
    public static GunControler Instance {
        get {
            return _instance;
        }
    }

    private void Awake() {
        //checking if that instance exist, if yes destroy this object, singleton pattern 
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        }
        else {
            _instance = this;
        }
    }

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
                for (int i = shoots; i >= 0; i--) {
                if (i == 2 && spread > firstspread) {
                    float tempSpread = spread;
                    spread = firstspread;
                    Shoot();
                    spread = tempSpread;
                    i--;
                }
                    Shoot();
            }
                    
        }

    }

    void Shoot() {
        Vector3 barrelPos = transform.GetChild(0).position;

        var randomNumberX = Random.Range(-spread, spread);
        var randomNumberY = Random.Range(-spread, spread);
        var randomNumberZ = Random.Range(-spread, spread);

        var bullet = Instantiate(Bullet, barrelPos, transform.parent.rotation);
        bullet.transform.Rotate(randomNumberX, randomNumberY, randomNumberZ);
        bullet.GetComponentInChildren<BulletControler>().damage = bulletDamage;
        Destroy(bullet, 2);
    }

    public void PowerMoreBullets(int i) {
        shoots += i;
        for(int j = i;j>=0;j--)
            FireRate = FireRate *1.2f ;
        spread += 1f;
        Debug.Log(FireRate);
    }
    public void PowerFasterFireRate(int i) {
        if (FireRate >= 0.03f) {
            FireRate -= 0.03f * i;
            spread += 1f;
            Debug.Log(FireRate);
        }
    }
    public void PowerBetterAccu(int i) {
        if(spread>=0.51f)
        spread -= 0.5f;

    }

    public void SetWeapon(int i) {
        switch (i) {
            case 1:
                FireRate = 1.5f;
                shoots = 4;
                spread = 8f;
                bulletDamage = 2;
                break;
            case 2:
                FireRate = 0.7f;
                shoots = 0;
                spread = 2f;
                bulletDamage = 1;
                break;
            case 3:
                FireRate = 2f;
                shoots = 0;
                spread = 0f;
                bulletDamage = 5;
                break;
            default:
                break;
        }
    }
}
