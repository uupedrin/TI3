using UnityEngine;

[RequireComponent(typeof(Categories))]
public class Outliner : MonoBehaviour
{
	static Material outlineMat;
	bool outlined = false;
	bool activate = false;
	
	void Start()
	{
		outlineMat = Resources.Load<Material>("Materials/OutlineMaterial");
	}
	
	void LateUpdate()
	{
		if(activate)
		{
			if(!outlined) AddOutline();
		}
		else
		{
			if(outlined) RemoveOutline();
		}
	}
	
	public void EnableOutline()
	{
		activate = true;
	}
	public void DisableOutline()
	{
		activate = false;
	}
	
	void AddOutline()
	{
		Renderer objectRenderer = GetComponent<Renderer>();
		Material[] objectMaterials = new Material[objectRenderer.materials.Length + 1];
		for (int i = 0; i < objectMaterials.Length - 1; i++)
		{
			objectMaterials[i] = objectRenderer.materials[i];
		}
		objectMaterials[objectMaterials.Length-1] = outlineMat;
		objectRenderer.materials = objectMaterials;
		outlined = true;
	}
	void RemoveOutline()
	{
		Renderer objectRenderer = GetComponent<Renderer>();
		Material[] objectMaterials = new Material[objectRenderer.materials.Length - 1];
		for (int i = 0; i < objectMaterials.Length; i++)
		{
			objectMaterials[i] = objectRenderer.materials[i];
		}
		objectRenderer.materials = objectMaterials;
		outlined = false;
	}
}
