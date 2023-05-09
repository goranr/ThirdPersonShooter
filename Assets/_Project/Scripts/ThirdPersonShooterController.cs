using System.Collections;
using System.Collections.Generic;
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

	public Transform debugAimSphere;
	public LayerMask aimColliderMask;

	public Transform explosion;
	public Transform bulletPrefab;
	public Transform bulletSpawnPoint;

	private void Awake()
	{
		thirdPersonInputs = GetComponent<ThirdPersonShooterInputs>();
		thirdPersonController = GetComponent<ThirdPersonController>();
		anim = GetComponent<Animator>();
	}

	private void Update()
	{
		Vector3 mouseWorldPosition = Vector3.zero;
		Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
		Transform hitTransform = null;
		
		Ray ray = Camera.main.ScreenPointToRay(screenCenter);

		if (Physics.Raycast(ray, out RaycastHit hit, 999f, aimColliderMask))
		{
			//debugAimSphere.position = hit.point;
			mouseWorldPosition = hit.point;
			hitTransform = hit.transform;
		}


		if (thirdPersonInputs.aim)
		{
			aimCamera.gameObject.SetActive(true);
			thirdPersonController.SetSensitivity(aimSensitivity);

			Vector3 worldAimTarget = mouseWorldPosition;
			worldAimTarget.y = transform.position.y;
			Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

			transform.forward = Vector3.Lerp(transform.forward, aimDirection,
				Time.deltaTime * 20f);

			anim.SetLayerWeight(1, Mathf.Lerp(anim.GetLayerWeight(1), 
				1f, Time.deltaTime * 10f));
		}
		else
		{
			aimCamera.gameObject.SetActive(false);
			thirdPersonController.SetSensitivity(normalSensitivity);
			anim.SetLayerWeight(1, Mathf.Lerp(anim.GetLayerWeight(1),
				0f, Time.deltaTime * 10f));
		}

		if (thirdPersonInputs.shoot)
		{
			//Disparo con Raycast
			/*if(hitTransform != null)
			{
				Instantiate(explosion, mouseWorldPosition, Quaternion.identity);
			}*/
			Vector3 aimDir = (mouseWorldPosition - bulletSpawnPoint.position).normalized;
			Instantiate(bulletPrefab, bulletSpawnPoint.position, 
				Quaternion.LookRotation(aimDir, Vector3.up));

			thirdPersonInputs.shoot = false;
		}
	}
}
