using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour {

	[SerializeField] private float fillAmount;
	[SerializeField] private Image content;
	[SerializeField] private Text stamina;

	int val = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Handle();
	}

	private void Handle(){
		int.TryParse(stamina.text, out val);
		content.fillAmount = FillerMap(val,0,100);
	}

	private float FillerMap(float value, float min, float max){
		return (value-min)/(max-min);
	}
}
