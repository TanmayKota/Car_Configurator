using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RimSelector : MonoBehaviour
{
    public GameObject wheelscontainer;
    public GameObject originalwheels1;
    public GameObject originalwheels2;

    public void activatewheel(GameObject wheel)
    {
        if (originalwheels1 != null && originalwheels1.activeSelf) originalwheels1.SetActive(false);
        if (originalwheels2 != null && originalwheels2.activeSelf) originalwheels2.SetActive(false);

        foreach (Transform child in wheelscontainer.transform)
        {
            child.gameObject.SetActive(false);
        }
        wheel.SetActive(true);
    }

    public void deactivatewheel()
    {
        foreach (Transform child in wheelscontainer.transform)
        {
            child.gameObject.SetActive(false);
        }
        if (originalwheels1 != null) originalwheels1.SetActive(true);
        if (originalwheels2 != null) originalwheels2.SetActive(true);
    }
    
}
