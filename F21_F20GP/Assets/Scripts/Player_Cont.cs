using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Cont : MonoBehaviour
{
    public float movSpeed, gravityModifier, jumpPower, runSpeed = 12f;
    public CharacterController charCon;
    private Vector3 movInput;
    public Transform camTrans;
    public float mouseSensiv;
    public bool invertX;
    public bool invertY;
    private bool canJump, canDoubleJump;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;
    public Animator anim;
    //public GameObject bullet;
    public Transform firepoint;

    public Gun activeGun;
    public List<Gun> allGuns = new List<Gun>();
    public int currentGun;
    // Start is called before the first frame update
    void Start()
    {
        currentGun--;
        SwitchGun();
    }

    // Update is called once per frame
    void Update()
    {
        //movInput.x = Input.GetAxis("Horizontal") * movSpeed * Time.deltaTime;
        //movInput.z = Input.GetAxis("Vertical") * movSpeed * Time.deltaTime;
        //store y velocity
        float ystore = movInput.y;
        Vector3 vertMove = transform.forward * Input.GetAxis("Vertical");
        Vector3 horiMove = transform.right * Input.GetAxis("Horizontal");
        movInput = horiMove + vertMove;
        movInput.Normalize();
        if(Input.GetKey(KeyCode.LeftControl))
        {
            movInput = movInput * runSpeed;
        }
        else
        {
            movInput = movInput * movSpeed;
        }
        
        movInput.y = ystore;    
        movInput.y += Physics.gravity.y * gravityModifier * Time.deltaTime;

        if(charCon.isGrounded) 
        {
            movInput.y = Physics.gravity.y * gravityModifier * Time.deltaTime;
        }

        canJump = Physics.OverlapSphere(groundCheckPoint.position, .25f, whatIsGround).Length > 0;
        
       
        //Jumping
        if(Input.GetKeyDown(KeyCode.Space) && canJump) 
        {
            movInput.y = jumpPower;

            canDoubleJump = true;
        }

        else if (canDoubleJump && Input.GetKeyDown(KeyCode.Space))
        {
            movInput.y = jumpPower;

            canDoubleJump = false;  
        }

        charCon.Move(movInput * Time.deltaTime);
        //cam rotation
        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensiv;

        if(invertX )
        {
            mouseInput.x = -mouseInput.x;
        }
        if(invertY ) 
        {
            mouseInput.y = -mouseInput.y;   
        }
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);
        camTrans.rotation = Quaternion.Euler(camTrans.rotation.eulerAngles + new Vector3(-mouseInput.y, 0f, 0f));
        //shooting
        //singleshots
        if(Input.GetMouseButtonDown(0) && activeGun.fireCounter <=0)
        {
            RaycastHit hit;
            if(Physics.Raycast(camTrans.position, camTrans.forward, out hit, 50f))
            {
                if (Vector3.Distance(camTrans.position, hit.point) > 2f)
                {
                    firepoint.LookAt(hit.point);
                }
            }

            else
            {
                firepoint.LookAt(camTrans.position + (camTrans.forward * 30f));

               
            }


            //Instantiate(bullet, firepoint.position, firepoint.rotation);
            FireShot();
        }

        //repeating shots
        if(Input.GetMouseButton(0) && activeGun.canAutoFire)
        {
            if(activeGun.fireCounter <= 0)
            {
                FireShot();
            }
        }

        if(Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
        {
            SwitchGun();
        }


        anim.SetFloat("moveSpeed", movInput.magnitude);
        anim.SetBool("onGround", canJump);
    }

    public void FireShot()
    {
        if(activeGun.currentAmmo > 0)
        {
            activeGun.currentAmmo--;
            Instantiate(activeGun.bullet, firepoint.position, firepoint.rotation);
            activeGun.fireCounter = activeGun.fireRate;
        }
        
    }

    public void SwitchGun()
    {
        activeGun.gameObject.SetActive(false);
        currentGun++;
        if(currentGun >= allGuns.Count) 
        {
            currentGun = 0;
        }


        activeGun = allGuns[currentGun];
        activeGun.gameObject.SetActive(true);

        firepoint.position = activeGun.firepoint.position;
    }
}
