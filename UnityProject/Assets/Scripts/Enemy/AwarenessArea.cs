using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AwarenessArea : MonoBehaviour {

    public List<MonoBehaviour> Group;
    private List<IAware> _group;

    void Awake()
    {
        _group = new List<IAware>();
        // By default set awareness to none
        for (int i = 0; i < Group.Count; i++)
        {
            IAware obj = Group[i] as IAware;
            if (obj == null)
            {
                Debug.Log("AwarenessArea found object which does not implement IAware: "
                    + Group[i]);
                continue;
            }
            _group.Add(obj);
            obj.SetAwareness(false);
        }
    }

    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        for (int i = 0; i < Group.Count; i++)
        {
            _group[i].EnterAwareness(col.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        for(int i = 0; i < Group.Count; i++)
        {
            _group[i].LeaveAwareness(col.gameObject);
        }
    }
}
