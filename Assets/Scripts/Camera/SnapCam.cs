using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SnapCam : MonoBehaviour
{
	Camera snapCam;
	[SerializeField] Camera mainCam;	
	int resWidth = 1920;
	int resHeight = 1080;
	new string name;
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
			AudioManager.instance.TryPlay3DEffect(0, transform.parent.transform.position); //Camera Snap
			
			Texture2D snapshot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
			snapCam.Render();
			RenderTexture.active = snapCam.targetTexture;
			snapshot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
			byte[] bytes = snapshot.EncodeToPNG();
			string fileName = SnapShotName();

			#if UNITY_STANDALONE
			if(!System.IO.Directory.Exists(Application.persistentDataPath + "/SnapShots")) 
			{
				System.IO.Directory.CreateDirectory(Application.persistentDataPath + "/SnapShots");
			}
			#endif
			#if UNITY_EDITOR
			if (!System.IO.Directory.Exists(Application.dataPath + "/SnapShots"))
			{
			 	System.IO.Directory.CreateDirectory(Application.dataPath + "/SnapShots");
			}
			#endif
			System.IO.File.WriteAllBytes(fileName, bytes);
			
			if(AnalyticsSender.instance.firstPhotoTaken && AnalyticsSender.instance.firstFlashcardDone)
			{
				if(AnalyticsSender.instance.GotGameMechanics == false)
				{
					AnalyticsSender.instance.AddAnalytics("SnapCam - Photo Taken", "Got base game mechanics", "True");
					AnalyticsSender.instance.GotGameMechanics = true;
				}
			}
			AnalyticsSender.instance.AddAnalytics("SnapCam - Photo Taken", "Time between photos", (Time.time - AnalyticsSender.instance.lastPhotoTaken).ToString());
			AnalyticsSender.instance.lastPhotoTaken = Time.time;
			
			FlashcardManager.instance.CreateFlashcard(name, fileName);
			FlashcardManager.instance.pictureScriptHolder.Enable(name, fileName);
			snapCam.gameObject.SetActive(false);
		}
	}
	public void TakeSnapShot(string n) 
	{		
		name = n;
		snapCam.gameObject.SetActive(true);
		snapCam.fieldOfView = mainCam.fieldOfView;
		snapCam.transform.rotation = mainCam.transform.rotation;
	}
	string SnapShotName() 
	{
		string photoName;
		#if UNITY_STANDALONE
		photoName =  string.Format("{0}/SnapShots/{1} {2}x{3} {4}.png", 
		Application.persistentDataPath,
		name,
		resWidth, resHeight, 
		System.DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss"));
		#endif
		#if UNITY_EDITOR
		photoName = string.Format("{0}/SnapShots/{1} {2}x{3} {4}.png",
		Application.dataPath,
		name,
		resWidth, resHeight,
		System.DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss"));
		#endif
		return photoName;
	}
}