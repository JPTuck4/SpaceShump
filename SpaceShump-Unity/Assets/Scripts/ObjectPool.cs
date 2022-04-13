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
        for (int i = 0; i < poolStartSize; i++)
        {
            GameObject projectileGO = Instantiate(projectilePrefab); //create prefab instance
            projectiles.Enqueue(projectileGO); //add to queue
            projectileGO.SetActive(false);//hide projectile
        }//end for(int i = 0; i < poolStartSize; i++)
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetObject()
    {
        //If there are objects in the pool
        if (projectiles.Count > 0)
        {
            GameObject gObject = projectiles.Dequeue(); //remove from queue
            gObject.SetActive(true); //enable
            return gObject; //return object
        }
        else
        {
            Debug.LogWarning("Out of objects, reloading...");
            return null; //return null
        }//end if(projecitles.Count > 0)
    }//end GetProjectile()

    public void ReturnObject(GameObject gObject)
    {
        projectiles.Enqueue(gObject); //add back to queue
        gObject.SetActive(false); //disable
    }//end ReturnProjectile()
}
