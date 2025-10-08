using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/Custom Button")] // Optional: makes it show in "UI" menu
public class CustomButton : Button
{
    public bool IsButtonPressed()
    {
        //Debug.Log("IsPressed is " + IsPressed());
        return IsPressed(); // Protected method from Selectable
    }
}
