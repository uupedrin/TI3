using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

[RequireComponent(typeof(Camera))]
public class SnapCam : MonoBehaviour
{
	Camera snapCam;
	[SerializeField]
	Camera mainCam;

	int resWidth = 1920;
	int resHeight = 1080;
	void Awake()
	{
		snapCam = GetComponent<Camera>();
		if(snapCam.targetTexture == null) 
		{
			snapCam.targetTexture = new RenderTexture(resWidth, resHeight, 24);
		}
		else 
		{
			resWidth = snapCam.targetTexture.width;
			resHeight = snapCam.targetTexture.height;
		}
		snapCam.gameObject.SetActive(false);
	}
	void LateUpdate()
	{
		if(snapCam.gameObject.activeInHierarchy) 
		{
			AudioManager.instance.TryPlay2DEffect(0); //Camera Snap
			
			Texture2D snapshot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
			snapCam.Render();
			RenderTexture.active = snapCam.targetTexture;
			snapshot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
			byte[] bytes = snapshot.EncodeToPNG();
			string fileName = SnapShotName();

			/*if(!System.IO.Directory.Exists(Application.persistentDataPath + "/SnapShots")) 
			{
				System.IO.Directory.CreateDirectory(Application.persistentDataPath + "/SnapShots");
			}  (PARA A BUILDAR, EXCLUA O IF DE BAIXO!!! E TIRE O /* DESSA)  */

			if (!System.IO.Directory.Exists(Application.dataPath + "/SnapShots"))
			{
				System.IO.Directory.CreateDirectory(Application.dataPath + "/SnapShots");
			}
			System.IO.File.WriteAllBytes(fileName, bytes);
			snapCam.gameObject.SetActive(false);
		}
	}
	public void TakeSnapShot() 
	{
		snapCam.gameObject.SetActive(true);
		snapCam.fieldOfView = mainCam.fieldOfView;
		snapCam.transform.rotation = mainCam.transform.rotation;
	}
	string SnapShotName() 
	{
		return string.Format("{0}/SnapShots/snap {1}x{2} {3}.png", 
		//Application.persistentDataPath, (PARA A BUILDAR, EXCLUA A LINHA DE BAIXO!!! E TIRE O // DESSA)
		Application.dataPath,
		resWidth, resHeight, 
		System.DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss"));
	}
}