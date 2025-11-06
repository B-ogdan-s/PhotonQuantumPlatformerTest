using UnityEngine;

public abstract class UI_Panel : MonoBehaviour
{
    public virtual void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }
}
