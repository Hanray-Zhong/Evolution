using UnityEngine;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class SoftObject : MonoBehaviour
{
	public float Distance = 0.05f;
	public float Frequency = 10f;
	public float DampingRation;
	public float Mass = 1f;
	public float AngularDrag = 0.05f;
	public float LinearDrag;
	public List<mJoint> Joints = new List<mJoint>();
	public Action OnInitializeCompleted;

	private Transform thisTransform;
	private Renderer thisRenderer;
	private Rigidbody2D thisRigidbody;
	private MeshFilter meshFilter;
	private Mesh sharedMesh;
	private List<Vector3> jointsStartLocalPositions = new List<Vector3>();
	private Vector3[] startVertices;
	private int jointCountSqrt;
	private float jointSize;
	private bool canUpdate;
	private bool isCached;

	private const int JointCount = 4;
	private const float MinimalMass = 0.05f;

	[Serializable]
	public class mJoint
	{
		public CircleCollider2D Collider;
		public SpringJoint2D Joint;
		public GameObject GameObject;
		public Rigidbody2D Rigidbody2D;
		public Transform Transform;
	}

	void Awake()
	{
		Initialize();
	}

	private void CacheObjects()
	{
		thisTransform = transform;
		thisRenderer = GetComponent<Renderer>();
		thisRigidbody = GetComponent<Rigidbody2D>();
		meshFilter = GetComponent<MeshFilter>();
		sharedMesh = meshFilter.sharedMesh;
		startVertices = meshFilter.sharedMesh.vertices;
		isCached = true;
	}

	private void Initialize()
	{
		if (!isCached)
		{
			CacheObjects();
		}

		canUpdate = false;
		DestroyJoints();
		jointsStartLocalPositions.Clear();

		var size = thisRenderer.bounds.size;
		jointCountSqrt = (int)Mathf.Sqrt(JointCount);
		jointSize = ((size.x > size.y) ? size.x : size.y) / jointCountSqrt / 2f;
		var massOfJoint = Mass / JointCount;
		var mass = (massOfJoint > MinimalMass) ?  massOfJoint : MinimalMass;
		for (int i = 0; i < JointCount; i++)
		{	
			var gJoint = new GameObject("Joint" + (i + 1));
			gJoint.transform.parent = thisTransform;
			gJoint.transform.localPosition = Vector3.zero;

			var joint = gJoint.AddComponent<SpringJoint2D>();
			var jointRigidBody = joint.GetComponent<Rigidbody2D>();
			jointRigidBody.mass = mass;
			jointRigidBody.drag = LinearDrag;
			jointRigidBody.angularDrag = AngularDrag;
#if UNITY_5_3_OR_NEWER || UNITY_5_2 || UNITY_5_1
			jointRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
#else
			jointRigidBody.fixedAngle = true;
#endif
#if UNITY_5_3_OR_NEWER
			joint.autoConfigureConnectedAnchor = false;
			joint.autoConfigureDistance = false;
#endif
			joint.connectedBody = thisRigidbody;
			joint.distance = Distance;
			joint.frequency = Frequency;
			joint.dampingRatio = DampingRation;
			
			var circleCollider = gJoint.AddComponent<CircleCollider2D>();
			circleCollider.radius = jointSize;

			var mJoint = new mJoint
			{
				Collider = circleCollider,
				Joint = joint,
				GameObject = gJoint,
				Rigidbody2D = jointRigidBody,
				Transform = gJoint.transform
			};
			Joints.Add(mJoint);

			var x = (i % jointCountSqrt) * 2 - (jointCountSqrt - 1);
			var y = (i / jointCountSqrt) * 2 - (jointCountSqrt - 1);
			joint.connectedAnchor = new Vector2(x * jointSize / 2, y * jointSize / 2) * Mathf.Sqrt(2f);
			jointsStartLocalPositions.Add(joint.connectedAnchor);
		}
		
		for (int i = 0; i < Joints.Count - 1; i++)
		{
			for (int j = i + 1; j < Joints.Count; j++)
			{
				if (Joints[i] != null && Joints[j] != null)
				{
					Physics2D.IgnoreCollision(Joints[i].Collider, Joints[j].Collider);
				}
			}
		}
		canUpdate = true;

		if (OnInitializeCompleted != null)
		{
			OnInitializeCompleted();
		}
	}

	void LateUpdate()
	{
		if (canUpdate)
		{
			var vertices = sharedMesh.vertices;
			for (int i = 0; i < vertices.Length; i++)
			{
				for (int j = 0; j < Joints.Count; j++)
				{
					var jointDistance = Vector2.Distance(Joints[j].Transform.localPosition + (Vector3)Joints[j].Joint.connectedAnchor * jointCountSqrt, vertices[i]);
					if (jointDistance <= jointSize * 2.0f)
					{
						var newVertex = startVertices[i] + Joints[j].Transform.localPosition - jointsStartLocalPositions[j];
						vertices[i] = newVertex;
					}
				}
			}
			meshFilter.sharedMesh.vertices = vertices;
		}
	}

	private void DestroyJoints()
	{
		foreach (var joint in Joints)
		{
			if (joint != null)
			{
				DestroyImmediate(joint.GameObject);
			}
		}
		Joints.Clear();
	}

	public void ForceUpdate()
	{
		DestroyJoints();
		Initialize();
	}

	public void UpdateParams()
	{
		foreach (var joint in Joints)
		{
			joint.Rigidbody2D.mass = Mass;
			joint.Rigidbody2D.angularDrag = AngularDrag;
			joint.Rigidbody2D.drag = LinearDrag;
#if UNITY_5_3_OR_NEWER || UNITY_5_2 || UNITY_5_1
			joint.Rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
#else
			joint.Rigidbody2D.fixedAngle = true;
#endif
			joint.Joint.connectedBody = thisRigidbody;
			joint.Joint.distance = Distance;
			joint.Joint.frequency = Frequency;
			joint.Joint.dampingRatio = DampingRation;
			
			var circleCollider = (CircleCollider2D)joint.Collider;
			circleCollider.radius = jointSize;
		}
	}
}