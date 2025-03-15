using UnityEngine;
using UnityEngine.UIElements;

public class GameUIHandler : MonoBehaviour
{
    
    private Label healthLabel;
    private VisualElement healthBarMask;
    
    public Titanic titanic;
    public UIDocument UIDoc;

    void Start()
    {
        healthLabel = UIDoc.rootVisualElement.Q<Label>("HealthLabel");
        healthBarMask = UIDoc.rootVisualElement.Q<VisualElement>("HealthBarMask");
    
        HealthUpdated();
    }

    void Update()
    {
        HealthUpdated();
    }

    void HealthUpdated()
    {
        healthLabel.text = $"{Mathf.CeilToInt(titanic.currentHealth)}/{Mathf.CeilToInt(titanic.maxHealth)}";
        float healthRatio = (float) titanic.currentHealth / titanic.maxHealth;
        float healthPercent = Mathf.Lerp(0, 100, healthRatio);
        healthBarMask.style.width = Length.Percent(healthPercent);
    }
}
