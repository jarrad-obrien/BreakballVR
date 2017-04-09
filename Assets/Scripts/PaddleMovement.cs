using UnityEngine;
using System.Collections;

public class PaddleMovement : MonoBehaviour {

	public GameObject leftBorder;
	public GameObject rightBorder;

	private float leftBorderPosition;
	private float rightBorderPosition;

	void Awake()
	{

	}

	// Use this for initialization
	void Start () {
		leftBorderPosition = leftBorder.transform.position.x + (leftBorder.transform.localScale.x / 2) + (this.transform.localScale.x / 2);
		rightBorderPosition = rightBorder.transform.position.x - (rightBorder.transform.localScale.x / 2) - (this.transform.localScale.x / 2);
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void movePaddle(Vector3 hitPoint)
	{
		if (hitPoint.x < leftBorderPosition)
		{
			this.transform.position = new Vector3(leftBorderPosition , this.transform.position.y, this.transform.position.z);
		}
		else if (hitPoint.x > rightBorderPosition)
		{
			this.transform.position = new Vector3(rightBorderPosition, this.transform.position.y, this.transform.position.z);
		}
		else {
			this.transform.position = new Vector3(hitPoint.x, this.transform.position.y, this.transform.position.z);
		}
	}
}
