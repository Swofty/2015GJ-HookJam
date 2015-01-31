using UnityEngine;
using System.Collections;

public interface IAware {
    void SetAwareness(bool aware);
    void EnterAwareness(GameObject go);
    void LeaveAwareness(GameObject go);
}
