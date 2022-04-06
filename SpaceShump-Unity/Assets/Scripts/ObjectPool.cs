/**** 
 * Created by: JP Tucker
 * Date Created: March 16, 2022
 * 
 * Last Edited by: NA
 * Last Edited: March 16, 2022
 * 
 * Description: creates pool of objects
****/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    static public ObjectPool POOL;

    #region POOL Singleton
    void CheckPoolIsInScene()
    {
        if (POOL == null)
            POOL = this;
        else
            Debug.LogError("POOL.Awake() - Attempted to assign a second ObjectPool.POOl");
    }
    #endregion

    private Queue<GameObject> projectiles = new Queue<GameObject>(); //queue to hold projectiles

    [Header("Pool Settings")]
    public GameObject projectilePrefab;
    public int poolStartSize = 5;

    private void Awake()
    {
        CheckPoolIsInScene();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
