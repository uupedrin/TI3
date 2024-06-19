using System;

[Serializable]
public class AnalyticsData
{
	public float time; //Tempo decorrido de jogo
	public string sender; //Quem Enviou
	public string track; //Evento que se quer rastrear
	public string value; //Valor que está 
	public AnalyticsData(float time, string sender, string track, string value)
	{
		this.time = time;
		this.sender = sender;
		this.track = track;
		this.value = value;
	}
}

[Serializable]
public class AnalyticsFile
{
	public AnalyticsData[] data;
}