using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class MusicSystem : ComponentSystem
{

	
	private float clipTimeElapsed = 0.0f;
	private float clipLength = 4.8f; // 8 beats @ 100bpm.

	public struct Data{
		public AudioClipContainer acc;
		public AudioSource audioSource;
	}
	
	

	protected override void OnUpdate(){

		if(clipTimeElapsed > clipLength){
			clipTimeElapsed = 0;
		}
		else
		{
			clipTimeElapsed += Time.deltaTime;
		}


		foreach (var entity in GetEntities<Data>())
		{

			if(!entity.audioSource.isPlaying || entity.audioSource.time > clipTimeElapsed){
				entity.audioSource.time = clipTimeElapsed;
				entity.audioSource.Stop();
				entity.audioSource.Play();
			}
		}

	}


}
