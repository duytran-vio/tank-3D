using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankInfo
{
    public int id;
    public Vector3 position;
    public float turretAngle;
    public int HP;
    public int damage;

    public TankInfo (int newId){
        id = newId;
        position = Vector3.zero;
        turretAngle = 0;
        HP = Config.TankHP;
        damage = Config.TankDamage;
    }
}
