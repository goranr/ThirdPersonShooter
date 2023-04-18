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

	public Transform debugAimSphere;
	public LayerMask aimColliderMask;

	private void Awake()
	{
		thirdPersonInputs = GetComponent<ThirdPersonShooterInputs>();
		thirdPersonController = GetComponent<ThirdPersonController>();
	}

	private void Update()
	{
		Vector3 mouseWorldPosition = Vector3.zero;
		Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
		Ray ray = Camera.main.ScreenPointToRay(screenCenter);

		if (Physics.Raycast(ray, out RaycastHit hit, 999f, aimColliderMask))
		{
			debugAimSphere.position = hit.point;
			mouseWorldPosition = hit.point;
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
		}
		else
		{
			aimCamera.gameObject.SetActive(false);
			thirdPersonController.SetSensitivity(normalSensitivity);
		}

		
	}
}
