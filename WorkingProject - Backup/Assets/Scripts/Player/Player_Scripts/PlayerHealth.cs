using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public Combat CombatComponent;
    [SerializeField] private Image HealthBar;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HealthBar.fillAmount = CombatComponent.currentHealth/100;
    }
}
