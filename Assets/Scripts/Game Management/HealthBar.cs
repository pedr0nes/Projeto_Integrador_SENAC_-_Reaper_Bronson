using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Health Bar is a Unity MonoBehaviour derived class
// It manages the values of a UI Slider used to represent the remaining life of game bosses

public class HealthBar : MonoBehaviour
{
	//Variable Declaration
	public Slider slider;
	public Gradient gradient;
	public Image fill;

    #region Health Bar Methods

	//Sets slider's maximun value to be the given int parameter and resets current value to the maximum. Also gives the slider a full health color.
    public void SetMaxHealth(int health)
	{
		slider.maxValue = health;
		slider.value = health;

		fill.color = gradient.Evaluate(1f);
	}

	//Sets slider's current value to be the given int parameter. Also changes slider color to represent the current health value properly.
	public void SetHealth(int health)
	{
		slider.value = health;

		fill.color = gradient.Evaluate(slider.normalizedValue);
	}
    #endregion
}
