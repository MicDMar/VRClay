using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewtonVR;

// TODO: Use NVR interation system
//[RequireComponent(typeof(NVRHand))]
public class VacuumHand : MonoBehaviour
{
	public float VacuumDistance = 5.0f;
	public float VacuumRadius = 0.5f;

	public float MaxVelocityChange = 10.0f;
	private const float VelocityMagic = 600.0f;


	public NVRButtons VacuumButton = NVRButtons.Trigger;

	private NVRHand hand;
	private NVRPlayer player;

	private ParticleSystem particles;

	private void Awake()
	{
		hand = this.GetComponent<NVRHand>();
		particles = this.GetComponent<ParticleSystem>();
		particles.Stop();
	}

	void Start()
	{
		player = hand.Player;
	}
	
	void LateUpdate()
	{
		var enabled = (hand != null && hand.Inputs[VacuumButton].SingleAxis > 0.01f);
		var emission = particles.emission;

		if (enabled)
		{
			emission.enabled = true;
			
			var ray = new Ray(transform.position, transform.forward);

			var hits = Physics.SphereCastAll(ray, VacuumRadius, VacuumDistance);
			
			foreach (var hit in hits)
			{
				var body = hit.transform.gameObject.GetComponent<Rigidbody>();

				if (body)
				{
					var positionDelta = transform.position - body.transform.position;
					var newVelocity = positionDelta * VelocityMagic * Time.deltaTime;

					if (float.IsNaN(newVelocity.x) == false)
					{
						body.velocity = Vector3.MoveTowards(body.velocity, newVelocity, MaxVelocityChange);
					}
				}
			}

			var endpoint = transform.position + (transform.forward * VacuumDistance);
		}
		else
		{
			emission.enabled = false;
		}
		

	}
}
