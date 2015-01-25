using UnityEngine;
using System.Collections;

public class GUIScripts : MonoBehaviour
{

    private bool valid;
    private StaminaBar staminaScript;
    private HeroMovement playerScript;

    void Awake()
    {
        if(GameObject.Find("Stamina Bar") == null || GameObject.Find("Hero") == null)
        {
            Debug.Log("Cannot find GameObjects!");
            valid = false;
            return;
        }
        staminaScript = GameObject.Find("Stamina Bar").GetComponent<StaminaBar>();
        playerScript = GameObject.Find("Hero").GetComponent<HeroMovement>();

        if(staminaScript == null || playerScript == null)
        {
            Debug.Log("Cannot find scripts!");
            valid = false;
            return;
        }

        valid = true;
    }

    void OnGUI()
    {
        if (!valid) return;
        float health = playerScript.GetCurrHealth();
        int stamina = (int) (staminaScript.length * 100);
        Rect healthBox = new Rect(15, 15, 100, 30);
        Rect staminaBox = new Rect(15, 50, 100, 30);
        GUI.Box(healthBox, "Health: " + health);
        GUI.Box(staminaBox, "Stamina: " + stamina);
    }
}
