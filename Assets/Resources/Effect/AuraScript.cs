using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AuraScript : MonoBehaviour {
	//ゲームが始まった際に動画を再生させる
	public MovieTexture movie;

	void Start(){
		movie.Play();

	}
}
