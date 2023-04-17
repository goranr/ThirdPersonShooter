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

	private void Awake()
	{
		thirdPersonInputs = GetComponent<ThirdPersonShooterInputs>();
		thirdPersonController = GetComponent<ThirdPersonController>();
	}

	private void Update()
	{
		if (thirdPersonInputs.aim)
		{
			aimCamera.gameObject.SetActive(true);
			thirdPersonController.SetSensitivity(aimSensitivity);

		}
		else
		{
			aimCamera.gameObject.SetActive(false);
			thirdPersonController.SetSensitivity(normalSensitivity);
		}
	}
}
