using UnityEngine;
using System.Collections;

public class HittableSubpart<CoreType> : HittablePart where CoreType : MonoBehaviour {

    protected CoreType core;

	void Awake()
    {
        core = GetComponentInParent<CoreType>();
    }
}
