using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.AI;
//using static UnityEngine.Experimental.GraphView.GraphView;

public enum State{
    Idle,
    Patrol,
    GoToPlayer,
    ReturningToPost,
    AttackPlayer
};
public enum PatrolType
{
    Idle,
    Patrol

};
public class EnemyBehaviour
{
    protected GameObject npc;
    public Transform player;
    protected float visionRadius;
    protected float visionRange;
    protected float attackRange;
    protected State state = State.Idle;
    protected NavMeshAgent navMeshAgent;
    protected Vector3 patrolSpot;
    public EnemyBehaviour(GameObject _npc,Transform playerTransform, float coneVision, float visionRange,float atkrange)
    {
        this.npc = _npc;
        this.player = playerTransform;
        this.visionRadius = coneVision;
        this.visionRange = visionRange;
        this.attackRange = atkrange;
        this.navMeshAgent = this.npc.GetComponent<NavMeshAgent>();
        this.patrolSpot = this.npc.transform.position;
    }

    public State CanSeePlayer()
    {
        Vector3 distance = this.player.position-this.npc.transform.position;
   
        if (distance.magnitude <= this.visionRange) 
        {
            float cosAngle = Vector3.Dot(distance.normalized, this.npc.transform.forward);
            float angle = Mathf.Acos(cosAngle) * Mathf.Rad2Deg;
            if (angle <= visionRadius || distance.magnitude < 5f)
            {
                return State.GoToPlayer;
            }
        }
        return State.Idle;
    }
    public State InRange()
    {
        Vector3 distance = this.player.position - this.npc.transform.position;

        if (distance.magnitude <= this.attackRange)
        {
            this.navMeshAgent.isStopped = true;
            return State.AttackPlayer;
        }
        else
        {
            this.navMeshAgent.isStopped = false;
            return State.GoToPlayer;
        }
    }

    public void BackToSpot()
    {
        this.navMeshAgent.isStopped = false;
        this.navMeshAgent.SetDestination(this.patrolSpot);
    }

    public void Stop()
    {
        if (this.navMeshAgent.isActiveAndEnabled)
        {
            this.navMeshAgent.isStopped = true;
        }
    }
    public void GotoPlace(Transform place)
    {
        if (this.navMeshAgent.isActiveAndEnabled)
        {
            this.navMeshAgent.SetDestination(place.position);
        }
    }
    public void GotoPlayer()
    {
        if (this.navMeshAgent.isActiveAndEnabled)
        {
            this.navMeshAgent.SetDestination(this.player.position);
        }
       
    }


    public void LookAtPlayer()
    {
        Vector3 direction = this.player.position - this.npc.transform.position;

        direction.y = 0;
        
        this.npc.transform.rotation = Quaternion.LookRotation(direction);
        
    }

}

