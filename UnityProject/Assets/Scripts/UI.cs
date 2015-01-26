using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour {

	public RectTransform rectTransform;
	
	void Awake() {
		rectTransform = this.GetComponent<RectTransform>();
	}
	
	void Update() {
		rectTransform.sizeDelta = new Vector3(300, 70, 2);
		rectTransform.position = new Vector3(25 + rectTransform.rect.width / 2,
		                                     Screen.height - rectTransform.rect.height / 2,
		                                     0);
	}
}