﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace DoorScript
{
	[RequireComponent(typeof(AudioSource))]


	public class Door : MonoBehaviour
	{
		public bool isOpposite = false;
		public bool open;
		public float smooth = 1.0f;
		float DoorOpenAngle = 90.0f;
		float DoorCloseAngle = 0.0f;
		public AudioSource asource;
		public AudioClip openDoor, closeDoor;

		void Start()
		{
			asource = GetComponent<AudioSource>();
		}

		void Update()
		{
			if (isOpposite)
			{
				if (open)
				{
					var target = Quaternion.Euler(0, DoorOpenAngle, 0);
					transform.localRotation = Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * 5 * smooth);
				}
				else
				{
					var target1 = Quaternion.Euler(0, DoorCloseAngle, 0);
					transform.localRotation = Quaternion.Slerp(transform.localRotation, target1, Time.deltaTime * 5 * smooth);
				}
			}
			else
			{
				if (open)
				{
					var target = Quaternion.Euler(0, -DoorOpenAngle, 0);
					transform.localRotation = Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * 5 * smooth);
				}
				else
				{
					var target1 = Quaternion.Euler(0, -DoorCloseAngle, 0);
					transform.localRotation = Quaternion.Slerp(transform.localRotation, target1, Time.deltaTime * 5 * smooth);
				}
			}

		}

		public void OpenDoor()
		{
			open = !open;
			asource.clip = open ? openDoor : closeDoor;
			asource.Play();
		}
	}
}