using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private int fromTankId;
    public float bulletSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Init(int tankIndex){
        fromTankId = tankIndex;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + transform.forward * Time.deltaTime * bulletSpeed;
    }

    private void OnTriggerEnter(Collider other){
        if (other.tag == "Tank"){
            int hitId = other.GetComponent<TankManager>().tankInfo.id;
            if ( hitId == fromTankId) return;
            GameManager.Instance.Hit(fromTankId, hitId);
        }
        Destroy(gameObject);
    }
}
