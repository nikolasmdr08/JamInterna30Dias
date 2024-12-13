using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    [Range(0,360)]
    public float viewAngles;

    public LayerMask TargetMask;
    public LayerMask ObstacleMask;

    public Assasin_IA Assasin_code;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    void Start()
    {
        StartCoroutine("FindTargetswithDelay",0f);
        Assasin_code = this.GetComponent<Assasin_IA>();
    }

    IEnumerator FindTargetswithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }
    void FindVisibleTargets ()
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius , TargetMask);
        
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle (transform.forward,dirToTarget) < viewAngles / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position,dirToTarget,distanceToTarget,ObstacleMask))
                {
                    visibleTargets.Add(target);

                    print("TE VEO");

                    if (Assasin_code.huntingMode && !Assasin_code.VictimTarged )
                    {
                        Assasin_code.Victim = targetsInViewRadius[i].transform;
                        Assasin_code.VictimTarged = true;
                        Assasin_code.agent.isStopped = true;
                    }
                    
                }
            }
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees,bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
