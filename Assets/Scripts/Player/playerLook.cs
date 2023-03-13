using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerLook : MonoBehaviour
{
    [Header("Camera movement")]
    public bool lockMouse = false;
    public float sensibility = 2f;
    public float lerpSpeed;
    public Vector3 starterRotation = new Vector3(4, -90, 0);
    [Space(7)]
    public float xMinimum;
    public float xMaximum;
    public float yMinimum;
    public float yMaximum;
    float mouseX, mouseY;

    [Header("Bobbing")]
    public float bobbingSpeed = 14f;
    public float bobbingAmount = 0.05f;
    public playerMove playerMove;
    float defaultPosY = 0;
    float timer = 0;

    [Header("Shaking")]
    // Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	public Transform camTransform;
	
	// How long the object should shake for.
	public static float shakeDuration = 0f;
	
	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;
	
	Vector3 originalPos;
	
	void Awake()
	{
		if (camTransform == null)
		{
			camTransform = GetComponent(typeof(Transform)) as Transform;
		}

        if(lockMouse)
        {
            Cursor.visible = false;
	    	Cursor.lockState = CursorLockMode.Locked;
        }

        defaultPosY = transform.position.y;
	}
	
	void OnEnable()
	{
		originalPos = camTransform.localPosition;
	}

	void Update()
	{
        // Camera shake

		if (shakeDuration > 0)
		{
			camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
			
			shakeDuration -= Time.deltaTime * decreaseFactor;
		}
		else
		{
			shakeDuration = 0f;
			camTransform.localPosition = originalPos;

            // Headbob
            timer += Time.deltaTime * bobbingSpeed;
            transform.localPosition = new Vector3(transform.localPosition.x, (defaultPosY + playerMove.transform.position.y / 2) + Mathf.Sin(timer) * bobbingAmount * (playerMove.canJump ? 1 : 0), transform.localPosition.z);
        }
        
        // Mouse

        mouseX += Input.GetAxis("Mouse X") * sensibility;
		mouseY -= Input.GetAxis("Mouse Y") * sensibility;

        // Debug.Log("X: " + mouseX + " Y: " + mouseY);

        if(mouseX < xMinimum)
        {
            mouseX = xMinimum;
        }
        else if(mouseX > xMaximum)
        {
            mouseX = xMaximum;
        }

        if(mouseY < yMinimum)
        {
            mouseY = yMinimum;
        }
        else if(mouseY > yMaximum)
        {
            mouseY = yMaximum;
        }

		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(starterRotation.x + mouseY, starterRotation.y + mouseX, starterRotation.z + 0)), lerpSpeed);
    }
}