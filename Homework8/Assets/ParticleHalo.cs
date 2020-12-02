using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalInfo 
{
	public float radius = 0f;
    public float angle = 0f;
    public float time = 0f;

	public ParticalInfo(float radius, float angle, float time)
	{
		this.radius = radius;
		this.angle = angle;
		this.time = time;
	}
}

public class ParticleHalo : MonoBehaviour
{
    private ParticleSystem particleSys; //粒子系统
	private ParticleSystem.Particle[] particleArr; //粒子数组 
	private ParticalInfo[] particles; //粒子信息数组
	private float[] radius; 
	private float[] collect_radius;
	private int tier = 10;
	private int time = 0;

	public Gradient colorGradient; //粒子颜色
	public int particleNum = 10000; //粒子数量
	public float size = 0.1f; //粒子大小
	public float minRadius = 7.0f; //外圈的最小半径
	public float maxRadius = 10.0f; //外圈的最大半径
	public float collect_MinRadius = 1.0f; //内圈的最小半径
	public float collect_MaxRadius = 4.0f; //内圈的最大半径

	public bool clockwise = true; //顺时针/逆时针
	public float speed = 2f; //速度
	public float pingPong = 0.02f; //游离范围
	public bool isCollected = true; //内圈/外圈

	void Start () 
	{
		particleArr = new ParticleSystem.Particle[particleNum];
		particles = new ParticalInfo[particleNum];
		radius = new float[particleNum];
		collect_radius = new float[particleNum];
		particleSys = this.GetComponent<ParticleSystem>();

		particleSys.startSpeed = 0;            
		particleSys.startSize = size; 
		particleSys.loop = false;
		particleSys.maxParticles = particleNum;      
		particleSys.Emit(particleNum); 
		particleSys.GetParticles(particleArr);
		RandomlySpread();
	}
	
	void Update ()
	{
		for(int i = 0; i < particleNum; i++) {
			if(clockwise){
				particles [i].angle -= (i % tier + 1) * (speed / particles [i].radius / tier);
			}
			else{
				particles [i].angle += (i % tier + 1) * (speed / particles [i].radius / tier);
			}
			particles [i].angle = (360f + particles [i].angle) % 360f;
			float theta = particles [i].angle / 180 * Mathf.PI;
			if(isCollected == true){
				if(particles [i].radius > collect_radius [i]){
					particles [i].radius -= 15f * (collect_radius [i] / collect_radius [i]) * Time.deltaTime;  
				} 
			 	else{
					particles [i].radius = collect_radius [i];
				} 
			} 
			else {
				if(particles [i].radius < radius [i]){
					particles [i].radius += 15f * (collect_radius [i] / collect_radius [i]) * Time.deltaTime;  
				} 
				else{
					particles [i].radius += Mathf.PingPong (particles [i].time / minRadius / maxRadius, pingPong) - pingPong / 2f;
				} 
			}
			particleArr [i].position = new Vector3 (particles [i].radius * Mathf.Cos (theta), 0f, particles [i].radius * Mathf.Sin (theta));
		}
		changeColor ();
		particleSys.SetParticles(particleArr, particleArr.Length);
	}

	void OnGUI()
	{
		if(Input.GetMouseButtonDown(0)){
			time++;
			if(time%2 == 0){
				isCollected = !isCollected;
			}
		}
	}

	void RandomlySpread()
	{
		for(int i = 0; i < particleNum; ++i){  
			float midRadius = (maxRadius + minRadius) / 2;
			float minRate = UnityEngine.Random.Range(1.0f, midRadius / minRadius);
			float maxRate = UnityEngine.Random.Range(midRadius / maxRadius, 1.0f);
			float _radius = UnityEngine.Random.Range(minRadius * minRate, maxRadius * maxRate);
			radius[i] = _radius;
			float collect_MidRadius = (collect_MaxRadius + collect_MinRadius) / 2;
			float collect_outRate = Random.Range (1f, collect_MidRadius / collect_MinRadius);
			float collect_inRate = Random.Range (collect_MaxRadius / collect_MidRadius, 1f);
			float _collect_radius = Random.Range (collect_MinRadius * collect_outRate, collect_MaxRadius * collect_inRate);
			collect_radius[i] = _collect_radius;
			
			float angle = UnityEngine.Random.Range(0.0f, 360.0f);
			float theta = angle / 180 * Mathf.PI;
			
			float time = UnityEngine.Random.Range(0.0f, 360.0f);
			if(isCollected == false){
				particles [i] = new ParticalInfo (_radius, angle, time);
			} 
			else{
				particles [i] = new ParticalInfo (_collect_radius, angle, time);
			} 
			particleArr[i].position = new Vector3(particles[i].radius * Mathf.Cos(theta), 0f, particles[i].radius * Mathf.Sin(theta));
		}
		particleSys.SetParticles(particleArr, particleArr.Length);
	}
	
	void changeColor()
	{
		float colorValue;
		for(int i = 0; i < particleNum; i++){
			colorValue = (Time.realtimeSinceStartup - Mathf.Floor(Time.realtimeSinceStartup));
			colorValue += particles[i].angle/360;
			while (colorValue > 1) colorValue--;
			particleArr[i].startColor = colorGradient.Evaluate(colorValue);
		}
	}

}
