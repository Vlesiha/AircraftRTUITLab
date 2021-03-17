using UnityEngine;

/// <summary>
/// Shoots a rigidbody forwards with an inital velocity.
/// </summary>
public class Shooter : MonoBehaviour
{
	[Tooltip("Launch speed in m/s")]
	public float launchSpeed = 0.0f;

	[Tooltip("Launches forwards on start. When false, use \"Launch\" function to launch.")]
	public bool launched = false;

	private Rigidbody rigid;

	private void Awake()
	{
		rigid = GetComponent<Rigidbody>();
	}
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
			Launch();
			launched = true;
        }
    }

    [ContextMenu("Launch!")]
	public void Launch()
	{
		if (rigid != null)
		{
			rigid.AddRelativeForce(Vector3.forward * launchSpeed, ForceMode.VelocityChange);
		}
	}

}