using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using StarterAssets;

public class ThirdPersonShooterController : MonoBehaviour
{
    public CinemachineVirtualCamera aimCamera;
	public float aimSensitivity = 0.5f;
	public float normalSensitivity = 1.0f;

	private ThirdPersonController thirdPersonController;
	private ThirdPersonShooterInputs thirdPersonInputs;
	private Animator anim;

	public Transform aimSphere;
	public LayerMask aimColliderMask;

	public Transform bulletSpawnPoint;
	public Transform bulletPrefab;
	public Transform explosion;
	public Transform badExplosion;

	private void Awake()
	{
		thirdPersonInputs = GetComponent<ThirdPersonShooterInputs>();
		thirdPersonController = GetComponent<ThirdPersonController>();
		anim = GetComponent<Animator>();
	}

	private void Update()
	{
		Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
		Vector3 mouseWorldPosition = Vector3.zero;
		Transform hitObject = null;

		Ray ray = Camera.main.ScreenPointToRay(screenCenter);
		if (Physics.Raycast(ray, out RaycastHit hit, 999f, aimColliderMask))
		{
			aimSphere.position = hit.point;
			mouseWorldPosition = hit.point;
			hitObject = hit.transform;
		}

		if (thirdPersonInputs.aim)
		{
			aimCamera.gameObject.SetActive(true);
			thirdPersonController.SetSensitivity(aimSensitivity);

			anim.SetLayerWeight(1, Mathf.Lerp(anim.GetLayerWeight(1), 1, Time.deltaTime * 10));
			thirdPersonController.SetOnMoveRotation(false);

			Vector3 worldAimTarget = mouseWorldPosition;
			worldAimTarget.y = transform.position.y;
			Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

			transform.forward = Vector3.Lerp(transform.forward, aimDirection,
				Time.deltaTime * 20f);


		}
		else
		{
			aimCamera.gameObject.SetActive(false);
			thirdPersonController.SetSensitivity(normalSensitivity);

			anim.SetLayerWeight(1, Mathf.Lerp(anim.GetLayerWeight(1), 0, Time.deltaTime * 10));
			thirdPersonController.SetOnMoveRotation(true);

		}

		if (thirdPersonInputs.shoot)
		{
			//Codigo para disparar con Raycast
			/*if(hitObject != null)
			{
				if(hitObject.GetComponent<ShootableObject>() != null)
				{
					Debug.Log("Shot shootable object");
					Instantiate(explosion, mouseWorldPosition,
						Quaternion.identity);
				}
				else
				{
					Debug.Log("Shot other object");
					Instantiate(badExplosion, mouseWorldPosition,
						Quaternion.identity);
				}
			}*/
			//Codigo para disparar con Prefabs
			Vector3 aimDirection = (mouseWorldPosition - bulletSpawnPoint.position).normalized;
			Transform newBullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
			newBullet.LookAt(mouseWorldPosition);

			thirdPersonInputs.shoot = false;
		}

	}
}
