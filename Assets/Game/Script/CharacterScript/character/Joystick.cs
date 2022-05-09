using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    Vector3 firstPressPos, secondPressPos, currentSwipe, SwipeCurrent;
    public float Senseofspeed=55f,ClampMax=45f;
    private float NothingFieldX = 100f, NothingFieldY = 100f;
    [SerializeField]
    private GameObject Joystickİmage, JoystickPoint;
    private bool isTouch, isDontTouch,isboxTouch,isboxDontTuch;
    private CharacterComponents characterComponents;
    public CharacterComponents CharacterComponents { get { return (characterComponents == null) ? characterComponents = GetComponentInParent<CharacterComponents>() : characterComponents; } }
    private Rigidbody rb;
    public Rigidbody Rb { get { return (rb == null) ? rb = GetComponent<Rigidbody>() : rb; } }
    private void OnEnable()
    {
        EventManager.getJoystick += GetJoystick;
    }
    private void OnDisable()
    {
        EventManager.getJoystick -= GetJoystick;
    }
    private Joystick GetJoystick()
    {
        return GetComponent<Joystick>();
    }
    private void Update()
    {
        JoyStick();
    }
    public void EnabledJoy()
    {
        JoystickİmageControl(false);
        CharacterComponents.onAnimationPlay.Invoke("Dancing");
        this.enabled = false;

    }
    public void JoyStick()
    {
        Rb.isKinematic = false;
        if (Input.GetMouseButtonDown(0))
        {
            firstPressPos = Input.mousePosition;
            JoystickİmageControl(true);
            Joystickİmage.transform.position = firstPressPos;
            if (!isTouch)
            {
                isDontTouch = false;
                isTouch = true;
                CharacterComponents.onAnimationPlay.Invoke("Move");
            }
        }
        if (Input.GetMouseButton(0) == true)
        {
             secondPressPos = Input.mousePosition;
            currentSwipe = secondPressPos - firstPressPos;
            SwipeCurrent = currentSwipe;
            currentSwipe = currentSwipe.normalized;
            JoystickMoveControl();

            if (SwipeCurrent.x > 35 || SwipeCurrent.x < -35 || SwipeCurrent.y < -35 || SwipeCurrent.y > 35)
            {
                Rb.velocity = Senseofspeed * 8f * new Vector3(currentSwipe.x,0f, currentSwipe.y) * Time.deltaTime;
                transform.LookAt(transform.position + new Vector3(currentSwipe.x, 0, currentSwipe.y));
                if (!isboxTouch)
                {
                    isboxTouch = true;
                    isboxDontTuch = false;
                    CharacterComponents.onAnimationPlay.Invoke("Move");
                }
            }
            else
            {
                if (!isboxDontTuch)
                {
                    isboxDontTuch = true;
                    isboxTouch = false;
                    CharacterComponents.onAnimationPlay.Invoke("Idle");
                }
            }
        }
        if (Input.GetMouseButton(0) == false)
        {
            Rb.isKinematic = true;
            currentSwipe.x = 0;
            JoystickİmageControl(false);
            if (!isDontTouch)
            {        
                isDontTouch = true;
                isTouch = false;
                CharacterComponents.onAnimationPlay.Invoke("Idle");

            }
        }
    }
    private void JoystickİmageControl(bool isTrue)
    {
        Joystickİmage.SetActive(isTrue);
        JoystickPoint.transform.localPosition = Vector3.Lerp(JoystickPoint.transform.localPosition, Vector3.zero, 1f);
    }
    private void JoystickMoveControl()
    {
        NothingFieldX = currentSwipe.x*ClampMax; 
        NothingFieldY = currentSwipe.y* ClampMax;
        if (NothingFieldX<0)
            NothingFieldX *= -1;     
        if (NothingFieldY<0)
            NothingFieldY *= -1;   
       JoystickPoint.transform.position=
  new Vector3(Mathf.Clamp(secondPressPos.x, firstPressPos.x - NothingFieldX, firstPressPos.x + NothingFieldX),
            Mathf.Clamp(secondPressPos.y, firstPressPos.y - NothingFieldY, firstPressPos.y + NothingFieldY), 0);
      
    }
}
