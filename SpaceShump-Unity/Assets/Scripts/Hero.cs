/**** 
 * Created by: Your Name
 * Date Created: March 16, 2022
 * 
 * Last Edited by: NA
 * Last Edited: March 16, 2022
 * 
 * Description: Hero ship controller
****/

/** Using Namespaces **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase] //forces selection of parent object
public class Hero : MonoBehaviour
{
    /*** VARIABLES ***/

    #region PlayerShip Singleton
    static public Hero SHIP; //refence GameManager

    //Check to make sure only one gm of the GameManager is in the scene
    void CheckSHIPIsInScene()
    {

        //Check if instnace is null
        if (SHIP == null)
        {
            SHIP = this; //set SHIP to this game object
        }
        else //else if SHIP is not null send an error
        {
            Debug.LogError("Hero.Awake() - Attempeeted to assign second Hero.SHIP");
        }
    }//end CheckGameManagerIsInScene()
    #endregion

    GameManager gm; //reference to game manager

    ObjectPool pool; //reference to ObjectPool

    [Header("Ship Movement")]
    public float speed = 10;
    public float rollMult = -45;
    public float pitchMult = 30;

    [Space(10)]

    [Header("Projectile Settings")]
    public float projectileSpeed = 40;
    public AudioClip projectSound; //sound clip of projectile
    private AudioSource audioSource; //the audio source of the game object

    [Space(10)]

    private GameObject lastTriggerGo; //reference to the last triggering game object

    [Header("Shield Settings")]
    [SerializeField] //show in inspector
    private float _shieldLevel = 1; //level for shields
    public int maxShield = 4; //maximum shield level

    //method that acts as a field (property), if the property falls below zero the game object is desotryed
    public float shieldLevel
    {
        get { return (_shieldLevel); }
        set
        {
            _shieldLevel = Mathf.Min(value, maxShield); //Min returns the smallest of the values, therby making max sheilds 4

            //if the sheild is going to be set to less than zero
            if (value < 0)
            {
                Destroy(this.gameObject);
                Debug.Log(gm.name);
                gm.LostLife();

            }

        }
    }

    /*** MEHTODS ***/

    //Awake is called when the game loads (before Start).  Awake only once during the lifetime of the script instance.
    void Awake()
    {
        CheckSHIPIsInScene(); //check for Hero SHIP
    }//end Awake()

    //Start is called once before the update
    private void Start()
    {
        gm = GameManager.GM; //find the game manager
        pool = ObjectPool.POOL;
        audioSource = GetComponent<AudioSource>();
    }//end Start()

    // Update is called once per frame (page 551)
    void Update()
    {
        // Pull in information from the Input class
        float xAxis = Input.GetAxis("Horizontal"); // b
        float yAxis = Input.GetAxis("Vertical"); // b
                                                 // Change transform.position based on the axes
        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;
        // Rotate the ship to make it feel more dynamic // c
        transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult, 0);

        // Allow the ship to fire
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireProjectile();
            //If there is an audio source
            if (audioSource != null)
            {
                audioSource.PlayOneShot(projectSound); //play projectileSound
            }
        }
    }//end Update()

    void FireProjectile()
    { // b
        GameObject projGO = pool.GetObject();
        if(projGO != null)
        {
            projGO.transform.position = transform.position;
            Rigidbody rigidB = projGO.GetComponent<Rigidbody>();
            rigidB.velocity = Vector3.up * projectileSpeed;
        }
    }


    //Taking Damage
    private void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;
        //print("Triggered: "+go.name); // b
        // Make sure it's not the same triggering go as last time
        if (go == lastTriggerGo)
        { // c
            return;
        }
        lastTriggerGo = go; // d
        if (go.tag == "Enemy")
        { // If the shield was triggered by an enemy
            shieldLevel--; // Decrease the level of the shield by 1
            Destroy(go); // … and Destroy the enemy // e
        }
        else
        {
            print("Triggered by non-Enemy: " + go.name); // f
        }


    }//end OnTriggerEnter()

    public void AddToScore(int value)
    {
        gm.UpdateScore(value);
    }

}
