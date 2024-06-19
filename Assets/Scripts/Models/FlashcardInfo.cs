using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class FlashcardInfo
{
	public string name = "";
	public string picturePath = "";
	public int dificultyScore = 0;
	public string pictureShader = "";
	public DateTime lastReview = new DateTime();
	public DateTime nextReview = new DateTime();
}
