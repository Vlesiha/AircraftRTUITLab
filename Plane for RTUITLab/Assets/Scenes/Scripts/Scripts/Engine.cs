using UnityEngine;

public class Engine : MonoBehaviour
{
	[Range(0, 1)]
	public float throttle = 1.0f;
	public float breakSpeed = 1.0f;

	[Tooltip("How much power the engine puts out.")]
	public float thrust;

	private Rigidbody rigid;

	private void Awake()
	{
		rigid = GetComponentInParent<Rigidbody>();
	}
    private void Update()
    {
        if (Input.GetKey(KeyCode.B))
        {
			rigid.AddRelativeForce(Vector3.back * breakSpeed, ForceMode.VelocityChange);
		}
    }
}
