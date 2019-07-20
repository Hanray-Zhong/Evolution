using UnityEngine;

[ExecuteInEditMode()]
[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class SoftSprite : MonoBehaviour
{
	public Texture Sprite;
	public Vector2 Scale = Vector2.one;

	private MeshRenderer meshRenderer;
	private MeshFilter meshFilter;

	private const float PixelPerMeter = 100f;
	private readonly Color32 Color = new Color32(255, 255, 255, 255);

	void Awake()
	{
		meshFilter = GetComponent<MeshFilter>();
		meshRenderer = GetComponent<MeshRenderer>();

		CreateMesh();
	}

	private void CreateMesh()
	{
		var material = new Material(Shader.Find("Unlit/Transparent"));

		UpdateMesh();
		UpdateTexture(material);

		meshRenderer.sharedMaterial = material;
	}

	private void UpdateMesh()
	{
		if (Sprite == null) return;

		var mesh = new Mesh();
		var ratio = new Vector2((Sprite.width / PixelPerMeter) * Scale.x, (Sprite.height / PixelPerMeter) * Scale.y);
		mesh.vertices = new[]
		{
			new Vector3(-0.5f * ratio.x, -0.5f * ratio.y, 0),
			new Vector3( 0.5f * ratio.x, -0.5f * ratio.y, 0),
			new Vector3(-0.5f * ratio.x,  0.5f * ratio.y, 0),
			new Vector3( 0.5f * ratio.x,  0.5f * ratio.y, 0),
		};
		
		mesh.triangles = new[]
		{
			0, 3, 1,
			0, 2, 3
		};
		
		mesh.uv = new[]
		{
			new Vector2(0, 0),
			new Vector2(1, 0),
			new Vector2(0, 1),
			new Vector2(1, 1)
		};
		
		mesh.colors32 = new[]
		{
			Color,
			Color,
			Color,
			Color
		};
		
		meshFilter.sharedMesh = mesh;
	}

	private void UpdateTexture(Material material)
	{
		material.mainTexture = Sprite;
	}

	public void ForceUpdate()
	{
		if (gameObject.activeInHierarchy)
		{
			UpdateMesh();
			UpdateTexture(meshRenderer.sharedMaterial);
		}
	}
}