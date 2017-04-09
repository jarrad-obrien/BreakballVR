using UnityEngine;
using System.Collections;

public class PaddleController : MonoBehaviour {

	SteamVR_TrackedObject trackedObj;
	SteamVR_Controller.Device device;

	public GameObject laserPrefab;
	public GameObject paddle;
	public GameObject ball;

	private GameObject laser;
	private Transform laserTransform;
	private Vector3 hitPoint;
	private RaycastHit hit;
	private PaddleMovement paddleMovementInstance;

	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	// Use this for initialization
	void Start () {
		device = SteamVR_Controller.Input((int)trackedObj.index);

		paddleMovementInstance = paddle.GetComponent<PaddleMovement>();

		laser = Instantiate(laserPrefab);

		laserTransform = laser.transform;

	}
	
	// Update is called once per frame
	void Update () {
		if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100))
		{
			hitPoint = hit.point;
			ShowLaser(hit);
			paddleMovementInstance.movePaddle(hitPoint);
		}
		else
		{
			laser.SetActive(false);
		}

		if (device.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
		{
			ball.GetComponent<BallController>().ResetBall();
		}
	}

	private void ShowLaser(RaycastHit hit)
	{
		laser.SetActive(true);

		laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hitPoint, .5f);

		laserTransform.LookAt(hitPoint);

		laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y, hit.distance);


	}
}
