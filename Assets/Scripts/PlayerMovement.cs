using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;
    
    Animator m_Animator;
    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;
    AudioSource m_AudioSource;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody> ();
        m_AudioSource = GetComponent<AudioSource>();
    }

    // Reason of choice: To make sure the movement vector and rotation are set in time with OnAnimatorMove
    //  By default it is called exactly 50 times every second.
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis ("Horizontal");
        float vertical = Input.GetAxis ("Vertical");
        
        m_Movement.Set(horizontal * 2f, 0f, vertical * 2f);
        m_Movement.Normalize ();
        
        bool hasHorizontalInput = !Mathf.Approximately (horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately (vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool ("IsWalking", isWalking);
        
        if(isWalking)
        {
            if(!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play ();
            }
        }
        else
        {
            m_AudioSource.Stop ();
        }
        
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement,
            turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation (desiredForward);
    }
    
    // This method allows you to apply root motion as you want, which means that movement and rotation can be applied separately.
    // OnAnimatorMove is actually going to be called in time with physics, and not with rendering like your Update method.  
    void OnAnimatorMove ()
    {
        // The Animator’s deltaPosition is the change in position due to root motion that would have been applied to this frame.
        // m_Movement movement vector we want the character to move
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation (m_Rotation);
    }
}
