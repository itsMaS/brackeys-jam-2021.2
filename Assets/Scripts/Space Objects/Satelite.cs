using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satelite : SpaceObject
{
    [SerializeField] float baseBeepInterval;

    private void Start()
    {
        
    }
    IEnumerator Beeping()
    {
        while(true)
        {
            yield return new WaitForSeconds(baseBeepInterval);
        }
    }
}
