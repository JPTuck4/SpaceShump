/**** 
 * Created by: JP Tucker
 * Date Created: March 16, 2022
 * 
 * Last Edited by: NA
 * Last Edited: March 16, 2022
 * 
 * Description: projectile
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private BoundsCheck bndCheck;
    void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
    }
    void Update()
    {
        if (bndCheck.offUp)
        { // a
            Destroy(gameObject);
        }

    }
}
