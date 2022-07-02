using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankDetected : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Tank")
        {
            Debug.LogError("찾았습니다!!");
        }
    }
}
