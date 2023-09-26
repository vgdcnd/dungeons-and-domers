  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;

    // Movement variables
    [SerializeField] private float speed;   
    private Vector2 moveDirection;
    private bool canMove = true;
        //Force variables
        [SerializeField] private float dampForce;
        [SerializeField] private float forceScale;
        private Vector2 addedForce;

    //Sword swing variables
    [SerializeField] private float attackDelay;
    [SerializeField] private int damage;
    private bool hitAgain = true;
    private float lastAttack = -Mathf.Infinity;
 
    //Damage Variables
    [SerializeField] private float iFrameCount;   
    private bool canGetHit = true;
    private float health = 100;
    private float max_health;
    //Projectile Variables
    private Transform firePoint;
    [SerializeField] GameObject bullet;
    private Vector2 lastMove = new Vector2 (0,-1); //start the player facing down

    //new 
    public Vector3 lastPosition;
    [SerializeField] Image healthBar;


    void Start(){
        firePoint = gameObject.transform.GetChild(0); //might just make it serialized and drag it in, wanted to try another method tho
        max_health = health;
        /*
            Screen.SetResolution (1920, 1080, false);
    QualitySettings.vSyncCount = 0;
    Application.targetFrameRate = 60;
        */
    }

    void Update()
    {
        GetInputs();

    }

    void FixedUpdate()
    {
        if (canMove)
        {
            Move();
            Animate();
        }
    }

    void GetInputs(){

        float moveX = Input.GetAxisRaw("Horizontal");
        float   moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;

        if(Input.GetButtonDown("Swing")) Swing();
        else if(Input.GetButtonDown("Shoot")) Shoot();
    }
    void Move(){

        
        Vector2 movePlayer = new Vector2(moveDirection.x * speed, moveDirection.y * speed);

         movePlayer +=addedForce * forceScale; //Force Management
        addedForce /= dampForce;
        if (Mathf.Abs(addedForce.x) <= 0.1f && Mathf.Abs(addedForce.y) <= 0.1f) addedForce = Vector2.zero;

        rb.velocity = movePlayer;
    }

    void Swing(){
        if (Time.time - lastAttack <= attackDelay) return; // checking if enough time has based so that the pplayer can swing again

 
        DisableMove();
        lastAttack = Time.time; 
        animator.Play("Attack_Tree"); 

        // 1) make sure attack doesnt call the on trigger enter multiple times for a single swing ? 

    }

    void Shoot(){
        if (Time.time - lastAttack <= attackDelay) return;
        lastAttack = Time.time; 

        DisableMove();
        animator.Play("Shoot_Tree"); //animator will adjust firepoint to make sure bullet fired at right place
        //currently animator calls the create bulet function
        //is easy to just invoke create bullet or remove it and just put code here, used the animator to make sure bullet was spawned on right time and stuff. .


//        Invoke("CreateBullet", .1f);//small delay to make sure firepoint had time to be moved
    

    }

   public void CreateBullet(){
        GameObject spawnedBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
        spawnedBullet.GetComponent<ProjectileScript>().SetUpProjectile(7f, lastMove);

    }
   
   public void EnableMove(){ //called by the animator at the end of sword swing and hurt animations
        canMove = true ; 
        hitAgain = true;
    }

    void DisableMove(){ //stops player from moving and resets velocity to be 0, could as be made to be called by animator but idk 
        canMove = false;
        rb.velocity = new Vector2(0,0);
    }

    void Animate(){
          animator.SetFloat("moveX", moveDirection.x);
            animator.SetFloat("moveY", moveDirection.y);
            animator.SetFloat("speed", moveDirection.sqrMagnitude);
            
             if ( Mathf.Abs(moveDirection.x) == 1|| Mathf.Abs(moveDirection.y) == 1)
                 {
                    lastMove = new Vector2 (moveDirection.x, moveDirection.y);
            animator.SetFloat("lastMoveX", lastMove.x);
            animator.SetFloat("lastMoveY", lastMove.y);

             }
              //would want to be able to use animator.Play() eventually but still working on it 
    }

    void OnTriggerEnter2D(Collider2D hit){ 

        if (hitAgain && hit.gameObject.tag == "Enemy"){
            Debug.Log("touch enemey");
            hit.gameObject.GetComponent<enemyScript>().TakeDamage(damage);
            hitAgain = false; //supposed to make sure single swing wont hit multiple times, kinda iffy still

        }

    }

    public void TakeDamage(float damage, Vector2 directionHitFrom){
        if (!canGetHit) return;
         //visual sprite flicker
        animator.Play("Player_Hurt");

        //KnockBack
        addedForce += ((Vector2)transform.position - directionHitFrom).normalized  ;

        //Damage Management
        health -= damage;
        if (healthBar) healthBar.fillAmount = health / max_health;
        if (health <= 0) Die();
       // Debug.Log("Player Health: " +  health.ToString());

        //Invicibility frame management
         canGetHit = false;
        Invoke("DealWithIFrames", iFrameCount); //for iFrameCount, player will have cangetHit be false meaning they wont be able to take dmg for shot tamount of time 
    }

    public void DealWithIFrames(){
    
        canGetHit = true;
    }

 public void Die(){
        Debug.Log("Dead");
 }

    
    public void SpawnPlayer()
    {

        Debug.Log("Spawned");
         // here
         
        if (lastPosition != null)
        {
            transform.position = lastPosition;
            Debug.Log(lastPosition);
        }
    }
    


}