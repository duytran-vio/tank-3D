using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private bool drawGizmos;
    [SerializeField] private float spawnRadius = 5f;
    [SerializeField] private LayerMask obstacleMask;

    public void SetSpawnRadius(float newRadius)
    {
        spawnRadius = newRadius;
    }

    public Vector3 GetSpawnPosition(float objectRadius)
    {
        Vector2 _randPos2D;
        Vector3 _randPos3D;
        float elapsedCycle = 0;
        do
        {
            _randPos2D = UnityEngine.Random.insideUnitCircle;
            _randPos3D = new Vector3(transform.position.x + _randPos2D.x, transform.position.y, transform.position.z + _randPos2D.y);
            elapsedCycle++;
        } while (elapsedCycle < 40 && Physics.OverlapCapsule(_randPos3D, _randPos3D + Vector3.up, objectRadius, obstacleMask).Length != 0);

        return _randPos3D;
    }

    private void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, spawnRadius);
        }
    }
}
