using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankManager : MonoBehaviour
{
    public TankInfo tankInfo;
    private TankMovement tankMovement;
    private GameObject bulletPrefabs;
    private Transform firePoint;
    private Slider  healthBar;
    // Start is called before the first frame update
    void Awake()
    {
        tankMovement = GetComponent<TankMovement>(); 
        bulletPrefabs = Resources.Load<GameObject>("Prefabs/Shell");
        firePoint = transform.Find("TankRenderers/TankTurret/FirePoint");
        healthBar = transform.Find("Canvas/HealthBar").GetComponent<Slider>();
    }

    public void Init(int id){
        tankInfo = new TankInfo(id);
        tankInfo.position = transform.position;
        healthBar.maxValue = tankInfo.HP;
        healthBar.value = tankInfo.HP;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tankMovement.HandleMovementToPosition(tankInfo.position);
        tankMovement.HandleTurretAngle(tankInfo.turretAngle);
    }

    public void SetPosition(Vector3 position){
        tankInfo.position = position;
    }

    public void SetTurretAngle(float angle){
        tankInfo.turretAngle = angle;
    }

    public void Fire(float angle){
        SetTurretAngle(angle);
        // StartCoroutine(OnReleaseBullet(tankInfo.turretAngle));
        GameObject bullet = Instantiate(bulletPrefabs, firePoint.position, tankMovement.GetTurretRotation());
        bullet.GetComponent<BulletManager>().Init(tankInfo.id);
    }

    public IEnumerator OnReleaseBullet(float targetAngle){
        float angle = 0; 
        do {
            angle = Quaternion.Angle(tankMovement.GetTurretRotation(), Quaternion.Euler(0, targetAngle, 0));
            if (angle >= 0.5f) 
                yield return null;
            else 
                break;
        }while (true);
        
        GameObject bullet = Instantiate(bulletPrefabs, firePoint.position, tankMovement.GetTurretRotation());
        bullet.GetComponent<BulletManager>().Init(tankInfo.id);
    }

    public void SetHP(int newHP){
        tankInfo.HP = newHP;
        healthBar.value = newHP;
    }

    public void Die(){
        Destroy(gameObject);
    }

}
