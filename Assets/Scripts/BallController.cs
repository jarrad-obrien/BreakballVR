using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

	Rigidbody rb;
	AudioSource bounce;

	public float speed;
	public float size;

	public GameObject leftBorder;
	public GameObject rightBorder;
	public GameObject topBorder;
	public GameObject paddle;
	public GameObject brick;
	public GameObject background;
	

	private float speedX;
	private float speedY;
	private float xComponent = 0.5f;
	private float yComponent = 0.5f;
	
	void Awake()
	{
		rb = GetComponent<Rigidbody>();
		bounce = GetComponent<AudioSource>();
	}

	// Use this for initialization
	void Start () {
		speedX = speed;
		speedY = speed;
		rb.transform.localPosition = new Vector3(rb.transform.localPosition.x + (speedX * xComponent), rb.transform.localPosition.y + (speedY * yComponent), 0f);

		this.transform.localScale = Vector3.one * size;

	}
	
	// Update is called once per frame
	void Update () {
		rb.transform.localPosition = new Vector3(rb.transform.localPosition.x + (speedX * xComponent), rb.transform.localPosition.y + (speedY * yComponent), 0f);
	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject == leftBorder|| col.gameObject == rightBorder)
		{
			speedX *= -1;
		}

		else if(col.gameObject == topBorder || col.gameObject == paddle)
		{
			speedY *= -1;
		}

		else if(col.gameObject != background)
		{

			float[] direction = CalculateDirection(col);
			
			if(direction[0] == 1f)
			{
				Debug.Log("Flip X direction");
				speedX *= -1;
			}
			else if (direction[1] == 1f)
			{
				Debug.Log("Flip Y direction");
				speedY *= -1;
			}
			else
			{
				Debug.Log("Hit a corner");
				speedX = direction[0] * speed * -1;
				speedY = direction[1] * speed * -1;
			}
		}

		if (col.gameObject.tag == "Destructible")
		{
			GameObject.Destroy(col.gameObject);
		}

		bounce.Play();
	}

	/* 
	 * Calculates the direction the ball should go when it collides with another object.
	 * It gets the contact point, which will be somewhere on the ball, and makes the ball
	 * go in the opposite direction. 
	 */ 
	float[] CalculateDirection(Collision col)
	{
		float xLength = col.contacts[0].point.x - this.transform.position.x;
		float yLength = col.contacts[0].point.y - this.transform.position.y;

		if(Mathf.Abs(xLength) < 0.1f)
		{
			xLength = 0;
		}
		else if(Mathf.Abs(xLength) > 0.9f)
		{
			xLength = 1;
		}

		if (Mathf.Abs(yLength) < 0.1f)
		{
			yLength = 0;
		}
		else if (Mathf.Abs(yLength) > 0.9f)
		{
			yLength = 1;
		}

		float angle = Mathf.Atan(yLength / xLength);

		float xComponent = Mathf.Cos(angle);
		float yComponent = Mathf.Sin(angle);

		return new float[] {xComponent, yComponent};
	}

	public void ResetBall()
	{
		speedX = speed;
		speedY = speed;
		rb.transform.localPosition = new Vector3(0f, 8f, 0f);
	}
}
 