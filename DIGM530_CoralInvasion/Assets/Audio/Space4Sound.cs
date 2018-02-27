using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space4Sound : MonoBehaviour {

	public AudioClip SoundBurst;
	private AudioSource source;
	private float vollowRange = .1f;
	private float volHighRange = 2.0f;


	// Use this for initialization
	void Awake () {
		source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Space))
		{
			float vol = Random.Range (vollowRange, volHighRange);
			source.PlayOneShot(SoundBurst,1F);
		}

	}
}
