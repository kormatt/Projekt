using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GunControler : MonoBehaviour {

    private InputManager inputManager;
    private bool readytoshot;

    public GameObject Bullet;    
    private float nextFire;

    [SerializeField]
    private int shoots = 0;
    [SerializeField]
    private float spread = 0;
    [SerializeField]
    private float FireRate = 1f;

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
                for (int i = shoots; i >= 0; i--)
                    Shoot();
        }

    }

    void Shoot() {
        Vector3 barrelPos = transform.GetChild(0).position;

        var randomNumberX = Random.Range(-spread, spread);
        var randomNumberY = Random.Range(-spread, spread);
        var randomNumberZ = Random.Range(-spread, spread);

        var bullet = Instantiate(Bullet, barrelPos, transform.parent.rotation);
        bullet.transform.Rotate(randomNumberX, randomNumberY, randomNumberZ);

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
        if(spread>=0.7f)
        spread -= 0.7f;

    }
}
