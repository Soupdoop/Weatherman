﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormGlass : Device {

	public ParticleSystem part;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void set(Day d) {
		set (d.Pressure);
	}

	public void set(float f) {
		ParticleSystem.MainModule main = part.main;
		main.startLifetime = ((11.6f - 1.17f) / 10) * (105 - f) + 1.17f;
		main.maxParticles = 100 * (int)(105 - f);
	}
}
