using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    Player_Cont moveScript;
    public float dashSpeed;
    public float dashTime;
    // Start is called before the first frame update
    void Start()
    {
        moveScript = GetComponent<Player_Cont>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift)) 
        {
            StartCoroutine(Dodge());

        }
        IEnumerator Dodge()
        {

            float startTime = Time.time;  
            while(Time.time < startTime + dashTime)
            {
                if (Input.GetKey(KeyCode.A))
                {
                    transform.Translate(Vector3.left * dashSpeed * Time.deltaTime);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    transform.Translate(Vector3.right * dashSpeed * Time.deltaTime);
                }

                else 
                {
                    transform.Translate(Vector3.forward * dashSpeed * Time.deltaTime);
                }

                yield return null;
            } 
                
           
        }
    }
}
