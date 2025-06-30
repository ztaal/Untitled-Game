using UnityEngine;

public class PassiveItem : MonoBehaviour
{
    protected PlayerStats player;
    public PassiveItemScriptableObject passiveItemData;

    protected virtual void ApplyModifier()
    {
        /** Apply the boost value to the appropriate stat in the child classes. */
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindAnyObjectByType<PlayerStats>();
        ApplyModifier();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
