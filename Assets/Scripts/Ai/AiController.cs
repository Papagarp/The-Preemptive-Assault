using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum aiState
{
    MELEE,
    RANGED,
    SEARCH,
    PATROL
}

public class AiController : MonoBehaviour
{
    #region Var

    aiState currentAIState;

    //nav mesh agent
    NavMeshAgent nav;

    //animation
    Animator aiAnimatorComponent;

    //Coroutine
    Coroutine swingCoroutine;
    Coroutine deathCo;

    //Material
    public Material deathEffect;
    
    //scripts
    FireBall fireBallScript;
    JesseAudioManager jesseAudioManager;

    [Header("Assign GameObjects")]
    public GameObject anchorPoint;
    public GameObject fireBall;
    public GameObject mesh;
    
    [Header("Is this a patrolling Ai")]
    public bool patrollingAI;

    [Header("Assign Patrol Points")]
    public GameObject[] patrolPoints;
    int currentPoint = 0;

    //starting point
    Vector3 startAiPoint;
    Quaternion startAiRotation;

    //player
    GameObject player;
    CharController playerScript;
    public bool foundPlayer;
    public bool foundPlayerCheck;
    public float distanceToPlayer;
    public float distanceToPoint;
    public float distanceToLastKnown;
    public float searchTime = 3.0f;
    public Vector3 lastKnownPosition;

    //speed
    float patrollingMovementSpeed = 3.0f;
    float firingMovementSpeed = 0.0f;
    float staggerMovementSpeed = 0.1f;
    float attackingMovementSpeed = 8.0f;

    [Header("other")]
    public bool stagger;
    public float stunRange = 10.0f;

    public float meleeRange = 30.0f;
    public float reloadTime = 3.0f;
    public float staggerTime = 3.0f;
    public float stunnedTime = 0.0f;
    public float stepTimer;
    float stepTimerCount;
    public AudioClip walking;

    bool isAttacking = false;

    float maxHp = 10;
    float currentHp = 10;

    Vector3 lastPosition;

    #endregion

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<CharController>();

        aiAnimatorComponent = GetComponentInChildren<Animator>();
        
        fireBallScript = GetComponentInChildren<FireBall>();

        nav = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        //setting the guard's station point
        if (!patrollingAI)
        {
            startAiPoint = gameObject.transform.position;
            startAiRotation = gameObject.transform.rotation;
        }
        jesseAudioManager = FindObjectOfType<JesseAudioManager>();
        stepTimerCount = stepTimer;
    }

    private void Update()
    {
        if (currentHp <= 0)
        {
            Death();
        }
        else
        {
            #region state switching

            switch (currentAIState)
            {
                case (aiState.MELEE):
                    anchorPoint.SetActive(false);
                    nav.isStopped = false;
                    nav.SetDestination(player.transform.position);
                    aiAnimatorComponent.SetBool("Moving", true);
                    nav.speed = attackingMovementSpeed;
                    break;

                case (aiState.RANGED):
                    anchorPoint.SetActive(true);
                    aiAnimatorComponent.SetBool("Moving", false);
                    nav.isStopped = true;
                    nav.speed = firingMovementSpeed;
                    break;

                case (aiState.SEARCH):
                    nav.speed = patrollingMovementSpeed;
                    nav.isStopped = false;
                    break;

                case (aiState.PATROL):
                    nav.speed = patrollingMovementSpeed;
                    nav.isStopped = false;
                    break;
            }

            #endregion

            #region Stun Function

            if (distanceToPlayer < stunRange && playerScript.stunned)
            {
                nav.speed = 0.0f;
            }

            #endregion

            #region Stagger Function

            if (stagger)
            {
                nav.speed = staggerMovementSpeed;

                staggerTime -= Time.deltaTime;

                if (staggerTime <= 0)
                {
                    stagger = false;
                }
            }

            #endregion

            #region Movement Animation

            if (isAttacking)
            {
                nav.isStopped = true;
            }
            else
            {
                nav.isStopped = false;

                if (lastPosition != gameObject.transform.position)
                {
                    //animation
                    aiAnimatorComponent.SetBool("Moving", nav.remainingDistance > 1 ? true : false);

                    if (stepTimerCount > 0)
                    {
                        stepTimerCount -= Time.deltaTime;
                    }
                    else
                    {
                        stepTimerCount = stepTimer;
                        jesseAudioManager.PlaySound("Cultist Moving");
                    }
                }

                lastPosition = gameObject.transform.position;
            }

            #endregion

            #region Patrolling & Search Function & reseting AI position

            if (!foundPlayer)
            {
                if (!fireBallScript.fired)
                {
                    fireBallScript.ReturnFireBall();
                    fireBall.SetActive(false);
                }

                if (foundPlayerCheck)
                {
                    currentAIState = aiState.SEARCH;
                }
                else
                {
                    currentAIState = aiState.PATROL;
                }

                //if the player was lost then look at the last spot the player was seen
                if (currentAIState == aiState.SEARCH)
                {
                    nav.SetDestination(lastKnownPosition);

                    distanceToLastKnown = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
                    new Vector3(lastKnownPosition.x, 0, lastKnownPosition.z));

                    //if at the last known spot of the player then search for a few seconds
                    if (distanceToLastKnown < 0.1f)
                    {
                        searchTime -= Time.deltaTime;

                        //if cannot find player then return to patrol
                        if (searchTime <= 0)
                        {
                            currentAIState = aiState.PATROL;
                            foundPlayerCheck = false;
                        }
                    }
                }

                //if the player hasn't been seen then patrol/guard an area
                if (currentAIState == aiState.PATROL)
                {
                    //is this Ai meant to patrol??
                    if (patrollingAI)
                    {
                        nav.SetDestination(patrolPoints[currentPoint].transform.position);

                        distanceToPoint = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
                        new Vector3(patrolPoints[currentPoint].transform.position.x, 0, patrolPoints[currentPoint].transform.position.z));

                        //move between patrol points
                        if (currentPoint == (patrolPoints.Length - 1) && distanceToPoint <= 0.1f)
                        {
                            currentPoint = 0;
                            nav.SetDestination(patrolPoints[currentPoint].transform.position);
                        }
                        else if (distanceToPoint <= 0.1f)
                        {
                            currentPoint++;
                            nav.SetDestination(patrolPoints[currentPoint].transform.position);
                        }
                    }
                    //if not a patrolling ai then its a guarding ai
                    else
                    {
                        nav.SetDestination(startAiPoint);

                        float distanceToStart = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
                            new Vector3(startAiPoint.x, 0, startAiPoint.z));

                        if (distanceToStart < 1.0f)
                        {
                            gameObject.transform.rotation = startAiRotation;
                        }
                    }
                }
            }

            #endregion

            #region Attacking the player

            if (foundPlayer)
            {
                gameObject.transform.eulerAngles = new Vector3(0, Quaternion.LookRotation(player.transform.position - gameObject.transform.position, Vector3.up).eulerAngles.y, 0);

                foundPlayerCheck = true;
                searchTime = 3.0f;

                lastKnownPosition = player.transform.position;

                distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

                //if the player is within melee range, hit the player
                if (distanceToPlayer <= meleeRange)
                {
                    currentAIState = aiState.MELEE;

                    if (distanceToPlayer < 2.0f)
                    {
                        MeleeAttack();
                    }
                }
                //if the player is outside melee range, fireball the player
                else if (distanceToPlayer >= meleeRange)
                {
                    currentAIState = aiState.RANGED;

                    //if the fireball hasnt been fired then fire
                    if (!fireBallScript.fired)
                    {
                        reloadTime -= Time.deltaTime;
                        if (reloadTime < 0)
                        {
                            fireBall.SetActive(true);
                            fireBallScript.fired = true;
                            jesseAudioManager.PlaySound("Shoot");
                        }
                        else
                        {
                            fireBall.SetActive(false);
                        }
                    }
                }
            }

            #endregion
        }
    }

    public void TakeDmg(float damage)
    {
        currentHp = currentHp - damage;
    }

    void MeleeAttack()
    {
        isAttacking = true;

        if (swingCoroutine == null)
        {
            swingCoroutine = StartCoroutine(AttackCombo());
        }
    }

    IEnumerator AttackCombo()
    {
        aiAnimatorComponent.SetTrigger("Claw1");

        float clipLength = -1;
        float clip2Length = -1;
        foreach (AnimationClip c in aiAnimatorComponent.runtimeAnimatorController.animationClips)
        {
            if (c.name == "Claw1")
            {
                clipLength = c.length;
            }
            if (c.name == "Claw2")
            {
                clip2Length = c.length;
            }
        }

        //wait for first animation to end
        yield return new WaitForSeconds(clipLength);

        //TODO:Claw 2

        isAttacking = false;

        if(distanceToPlayer < 1.0f)
        {
            print("claw 2");
        }
        else
        {
            swingCoroutine = null;
        }
    }

    void Death()
    {
        if (deathCo == null)
        {
            deathCo = StartCoroutine(DeathAnim());
        }
    }

    IEnumerator DeathAnim()
    {
        float timeOfEffect = 1;

        mesh.GetComponent<SkinnedMeshRenderer>().material = deathEffect;

        yield return new WaitForSeconds(timeOfEffect);

        Destroy(gameObject);
    }
}