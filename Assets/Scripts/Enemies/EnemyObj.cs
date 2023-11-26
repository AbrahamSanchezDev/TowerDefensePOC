using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace WorldsDev
{
    public class EnemyObj : MonoBehaviour
    {
        public Face faces;
        private GameObject SmileBody;
        public SlimeAnimationState currentState;

        private Animator animator;
        private NavMeshAgent agent;
        public Vector3[] waypoints;
        public int damType;

        private int m_CurrentWaypointIndex;

        private bool move;
        private Material faceMaterial;
        private Vector3 enemyPos;

        public enum WalkType
        {
            ToBase,
            ToEnemy
        }

        private WalkType walkType;

        protected virtual void Awake()
        {
            agent = gameObject.AddComponent<NavMeshAgent>();
            agent.speed = 1;
            agent.angularSpeed = 120;
            agent.acceleration = 20;
            agent.stoppingDistance = 0.5f;

            agent.radius = 0.29f;
            agent.height = 0.5f;
            agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
            agent.avoidancePriority = 50;

            agent.autoRepath = true;

            

            if (faces)
            {
                SmileBody = GetComponentInChildren<SkinnedMeshRenderer>().gameObject;
                faceMaterial = SmileBody.GetComponent<Renderer>().materials[1];
            }


            animator = GetComponent<Animator>();
            if (animator == null)
            {
                animator = GetComponentInChildren<Animator>();
                animator.gameObject.AddComponent<AnimationListener>();
            }

            var specialSetups = gameObject.GetComponentsInChildren<ISpecialSetup>();
            for (int i = 0; i < specialSetups.Length; i++)
            {
                specialSetups[i].DoSetup();
            }

            walkType = WalkType.ToBase;
        }

        public void WalkToNextDestination()
        {
            currentState = SlimeAnimationState.Walk;
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
            agent.SetDestination(waypoints[m_CurrentWaypointIndex]);
            if (faces)
                SetFace(faces.WalkFace);
        }

        public void CancelGoNextDestination()
        {
            CancelInvoke(nameof(WalkToNextDestination));
        }

        private void SetFace(Texture tex)
        {
            if (faceMaterial)
                faceMaterial.SetTexture("_MainTex", tex);
        }

        private void Update()
        {
            switch (currentState)
            {
                case SlimeAnimationState.Idle:

                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) return;
                    StopAgent();
                    if (faces)
                        SetFace(faces.Idleface);
                    break;

                case SlimeAnimationState.Walk:

                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk")) return;

                    agent.isStopped = false;
                    agent.updateRotation = true;

                    if (walkType == WalkType.ToEnemy)
                    {
                        agent.SetDestination(enemyPos);
                        // Debug.Log("WalkToOrg");
                        if (faces)
                            SetFace(faces.WalkFace);
                        // agent reaches the destination
                        if (agent.remainingDistance < agent.stoppingDistance)
                        {
                            walkType = WalkType.ToBase;

                            //facing to camera
                            transform.rotation = Quaternion.identity;

                            currentState = SlimeAnimationState.Idle;
                        }
                    }
                    //Patroll
                    else
                    {
                        if (waypoints[0] == null) return;

                        agent.SetDestination(waypoints[m_CurrentWaypointIndex]);

                        // agent reaches the destination
                        if (agent.remainingDistance < agent.stoppingDistance)
                        {
                            currentState = SlimeAnimationState.Idle;

                            //wait 2s before go to next destionation
                            Invoke(nameof(WalkToNextDestination), 2f);
                        }
                    }

                    // set Speed parameter synchronized with agent root motion moverment
                    animator.SetFloat("Speed", agent.velocity.magnitude);


                    break;

                case SlimeAnimationState.Jump:

                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jump")) return;

                    StopAgent();
                    if (faces)
                        SetFace(faces.jumpFace);
                    animator.SetTrigger("Jump");

                    //Debug.Log("Jumping");
                    break;

                case SlimeAnimationState.Attack:

                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) return;
                    StopAgent();
                    if (faces)
                        SetFace(faces.attackFace);
                    animator.SetTrigger("Attack");

                    // Debug.Log("Attacking");

                    break;
                case SlimeAnimationState.Damage:

                    // Do nothing when animtion is playing
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("Damage0")
                        || animator.GetCurrentAnimatorStateInfo(0).IsName("Damage1")
                        || animator.GetCurrentAnimatorStateInfo(0).IsName("Damage2")) return;

                    StopAgent();
                    animator.SetTrigger("Damage");
                    animator.SetInteger("DamageType", damType);
                    if (faces)
                        SetFace(faces.damageFace);

                    //Debug.Log("Take Damage");
                    break;
            }
        }


        private void StopAgent()
        {
            agent.isStopped = true;
            animator.SetFloat("Speed", 0);
            agent.updateRotation = false;
        }

        // Animation Event
        public void AlertObservers(string message)
        {
            if (message.Equals("AnimationDamageEnded"))
            {
                // When Animation ended check distance between current position and first position 
                //if it > 1 AI will back to first position 

                var distanceOrg = Vector3.Distance(transform.position, enemyPos);
                if (distanceOrg > 1f)
                {
                    walkType = WalkType.ToEnemy;
                    currentState = SlimeAnimationState.Walk;
                }
                else
                {
                    currentState = SlimeAnimationState.Idle;
                }

                //Debug.Log("DamageAnimationEnded");
            }

            if (message.Equals("AnimationAttackEnded")) currentState = SlimeAnimationState.Idle;

            if (message.Equals("AnimationJumpEnded")) currentState = SlimeAnimationState.Idle;
        }

        private void OnAnimatorMove()
        {
            // apply root motion to AI
            var position = animator.rootPosition;
            position.y = agent.nextPosition.y;
            transform.position = position;
            agent.nextPosition = transform.position;
        }
    }
}