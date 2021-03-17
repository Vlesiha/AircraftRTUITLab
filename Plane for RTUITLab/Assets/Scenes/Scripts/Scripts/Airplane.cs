using UnityEngine;
using System;

public class Airplane : MonoBehaviour
{
	public CtrlSurface elevator;
	public CtrlSurface aileronLeft;
	public CtrlSurface aileronRight;
	public CtrlSurface rudder;
	public Engine engine;

	public Rigidbody Rigidbody { get; internal set; }

	private bool yawDefined = false;

	private void Awake()
	{
		Rigidbody = GetComponent<Rigidbody>();
	}

	private void Start()
	{
		if (elevator == null)
			Debug.LogWarning(name + ": Airplane missing elevator!");
		if (aileronLeft == null)
			Debug.LogWarning(name + ": Airplane missing left aileron!");
		if (aileronRight == null)
			Debug.LogWarning(name + ": Airplane missing right aileron!");
		if (rudder == null)
			Debug.LogWarning(name + ": Airplane missing rudder!");
		if (engine == null)
			Debug.LogWarning(name + ": Airplane missing engine!");

		try
		{
			Input.GetAxis("Yaw");
			yawDefined = true;
		}
		catch (ArgumentException e)
		{
			Debug.LogWarning(e);
			Debug.LogWarning(name + ": \"Yaw\" axis not defined in Input Manager. Rudder will not work correctly!");
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (elevator != null)
		{
			elevator.targetDeflection = -Input.GetAxis("Vertical");
		}
		if (aileronLeft != null)
		{
			aileronLeft.targetDeflection = -Input.GetAxis("Horizontal");
		}
		if (aileronRight != null)
		{
			aileronRight.targetDeflection = Input.GetAxis("Horizontal");
		}
		if (rudder != null && yawDefined)
		{
			rudder.targetDeflection = Input.GetAxis("Yaw");
		}
	}

	private float CalculatePitchG()
	{
		// Angular velocity is in radians per second.
		Vector3 localVelocity = transform.InverseTransformDirection(Rigidbody.velocity);
		Vector3 localAngularVel = transform.InverseTransformDirection(Rigidbody.angularVelocity);

		// Local pitch velocity (X) is positive when pitching down.

		// Radius of turn = velocity / angular velocity
		float radius = (Mathf.Approximately(localAngularVel.x, 0.0f)) ? float.MaxValue : localVelocity.z / localAngularVel.x;

		// The radius of the turn will be negative when in a pitching down turn.

		// Force is mass * radius * angular velocity^2
		float verticalForce = (Mathf.Approximately(radius, 0.0f)) ? 0.0f : (localVelocity.z * localVelocity.z) / radius;

		// Express in G (Always relative to Earth G)
		float verticalG = verticalForce / -9.81f;

		// Add the planet's gravity in. When the up is facing directly up, then the full
		// force of gravity will be felt in the vertical.
		verticalG += transform.up.y * (Physics.gravity.y / -9.81f);

		return verticalG;
	}

	private void OnGUI()
	{
		const float msToKnots = 1.94384f;
		GUI.Label(new Rect(10, 260, 300, 20), string.Format("Speed: {0:0.0} knots", Rigidbody.velocity.magnitude * msToKnots));
		GUI.Label(new Rect(10, 280, 300, 20), string.Format("G Load: {0:0.0} G", CalculatePitchG()));
	}
}
