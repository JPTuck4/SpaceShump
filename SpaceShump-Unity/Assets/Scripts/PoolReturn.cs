/**** 
 * Created by: JP Tucker
 * Date Created: March 16, 2022
 * 
 * Last Edited by: NA
 * Last Edited: March 16, 2022
 * 
 * Description: returns objects to pool
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolReturn : MonoBehaviour
{
    private ObjectPool pool;
    // Start is called before the first frame update
    void Start()
    {
        pool = ObjectPool.POOL;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        //if the pool is not empty
        if (pool != null)
        {
            pool.ReturnObject(this.gameObject); //return to pool
        }//end if ( pool != null)
    }//end OnDisable()
}
