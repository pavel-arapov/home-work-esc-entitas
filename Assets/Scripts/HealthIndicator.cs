using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthIndicator : MonoBehaviour
{
    TextMesh textMesh;
    float displayedHealth;
    private Character character;

    void Start()
    {
        textMesh = GetComponent<TextMesh>();
        character = GetComponentInParent<Character>();
        displayedHealth = character.GetHealth() - 1.0f;
    }

    void Update()
    {
        float value = character.GetHealth();
        if (!Mathf.Approximately(displayedHealth, value)) { // !=
            displayedHealth = value;
            textMesh.text = $"{value}";
        }
    }
}