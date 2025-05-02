using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject rotatingplate;
    public GameObject maincamera;
    public GameObject outsetcamera;
    public GameObject CarMenu;
    public GameObject closerbutton;
    public GameObject closerbutton1;
    public GameObject closerbutton2;
    public GameObject closerbutton3;
    public GameObject openbutton;
    public GameObject carmenuholder;
    public GameObject CarHolder;
    public Vector3 maincameraposition;
    public Vector3 maincameraangle;
    public Vector3 outercameraposition;
    public Vector3 outercameraangle;
    public GameObject rimcolormenu;
    public GameObject rimcolormenu2;
    public GameObject wheelsmenu;
    public GameObject wheelsmenu2;
    public GameObject wheelcontainer;
    public GameObject changecolor;
    public GameObject carcolormenu;
    public GameObject firstpanel;
    public GameObject colorcontainer;
    public GameObject colorcontainer2;
    public GameObject colorcontainer3;
    public GameObject internalpanel;


    public float duration = 2.0f; // Duration of the move in seconds

    public void CarActivator(GameObject car)
    {
        Debug.Log("CarActivatorClick" + car.name);
        foreach (Transform child in CarHolder.transform)
        {
            child.gameObject.SetActive(false);
        }
        car.SetActive(true);
        //Vector3 targetPosition = new Vector3(-4.378f, 4.011f, 1.662f);
        StartCoroutine(MoveCameramaincamera(maincameraposition, maincameraangle));
        CarMenu.SetActive(false);
    }

    public void CarDeactivator()
    {
        Debug.Log("Car Deactivator function clicked!");
        firstpanel.SetActive(false);
        foreach (Transform child in CarHolder.transform)
        {
            child.gameObject.SetActive(false);
        }
        StartCoroutine(MoveCameraoutercamera(outercameraposition, outercameraangle));

    }

    public void ActivateInternalCamera(GameObject internalcamera)
    {
        Debug.Log("ACtivateInternalCamera function!");
        internalcamera.SetActive(true);
        rotatingplate.GetComponent<RotateOnRightClick>().enabled = false;
        firstpanel.SetActive(false);
        internalpanel.SetActive(true);
        maincamera.SetActive(false);
    }

/*    public void ExternalCamera()
    {
        internalcamera.SetActive(false);
        rotatingplate.GetComponent<RotateOnRightClick>().enabled = true;
        maincamera.SetActive(false);
    }*/

    public void ReturnToWheels()
    {
        colorcontainer.SetActive(false);
        StartCoroutine(CollapseColorMenu(rimcolormenu.GetComponent<RectTransform>(), duration));
        //wheelcontainer.SetActive(true);
    }

    public void ReturnToFirstPanel()
    {
        Vector2 originalcolormenutransforms = new Vector2(0,0);

        //Debug.Log("ReturnToFirstPanel function triggered");
        if (carcolormenu.activeSelf || rimcolormenu2.activeSelf || wheelsmenu.activeSelf)
        {
            carcolormenu.SetActive(false);
            carcolormenu.GetComponent<RectTransform>().anchoredPosition = originalcolormenutransforms;
            colorcontainer2.SetActive(false);
            rimcolormenu2.SetActive(false);
            rimcolormenu2.GetComponent<RectTransform>().anchoredPosition = originalcolormenutransforms;
            colorcontainer3.SetActive(false);
            wheelsmenu.SetActive(false);
            wheelsmenu2.GetComponent<RectTransform>().anchoredPosition = originalcolormenutransforms;
            //wheelcontainer.SetActive(false);
            firstpanel.SetActive(true);
        }

    }
    public void CarColorMenuExpander(GameObject panel)
    {
        firstpanel.SetActive(false);
        panel.SetActive(true);
        StartCoroutine(ExpandMenuCarColor(panel, panel.GetComponent<RectTransform>(), duration));
    }

    public void CollapseMenuButton()
    {
        closerbutton.SetActive(false);
        carmenuholder.SetActive(false);
        StartCoroutine(CollapseMenuOuter(CarMenu.GetComponent<RectTransform>(), 1f));
    }

    public void ExpandMenuButton()
    {
        openbutton.SetActive(false);
        CarMenu.SetActive(true);
        StartCoroutine(ExpandMenuOuter(CarMenu.GetComponent<RectTransform>(), 1f, 500f));
    }

    public void ColorMenu()
    {
        wheelcontainer.SetActive(false);
        //changecolor.SetActive(false);
        StartCoroutine(CollapseMenuwheels(wheelsmenu.GetComponent<RectTransform>(), 1f));
    }

    IEnumerator CollapseColorMenu(RectTransform menu,  float duration)
    {
        yield return StartCoroutine(CollapseMenu(menu, duration));
        yield return StartCoroutine(ExpandMenu(wheelsmenu.GetComponent<RectTransform>(), duration, 400f));
        wheelcontainer.SetActive(true);
    }

    IEnumerator MoveCameraoutercamera(Vector3 outercameraposition, Vector3 outercameranagle)
    {
        outsetcamera.SetActive(true);
        maincamera.SetActive(false);
        yield return StartCoroutine(MoveCamera(outercameraposition, outercameranagle));
        CarMenu.SetActive(true);
    }

    IEnumerator MoveCameramaincamera(Vector3 maincameraposition, Vector3 maincameraangle)
    {
        yield return StartCoroutine(MoveCamera(maincameraposition, maincameraangle));
        outsetcamera.SetActive(false);
        firstpanel.SetActive(true);
        maincamera.SetActive(true);
    }

    IEnumerator ExpandMenuCarColor(GameObject panel, RectTransform menuRect, float duration)
    {
        yield return StartCoroutine(ExpandMenuvertically(menuRect, duration));
        if (panel.name == "CarColor") colorcontainer2.SetActive(true);
        else if (panel.name == "WheelSelection2") {
            panel.SetActive(false);
            wheelsmenu.SetActive(true);
        }

        else if (panel.name == "RimColor2") colorcontainer3.SetActive(true);
    }

    IEnumerator ExpandMenucolor(RectTransform menuRect, float duration, float targetWidth)
    {

        yield return StartCoroutine(ExpandMenu(menuRect, duration, targetWidth));
        colorcontainer.SetActive(true);


    }

    IEnumerator CollapseMenuwheels(RectTransform menuRect, float duration)
    {
        //closerbutton1.SetActive(false);
        yield return StartCoroutine(CollapseMenu(menuRect, duration));
        yield return StartCoroutine(ExpandMenucolor(rimcolormenu.GetComponent<RectTransform>(), 1f, 150f));
    }
    IEnumerator CollapseMenu(RectTransform menuRect, float duration)
    {
        //Debug.Log("Duration in collapsemenu: " + duration);
        // Set pivot to right so collapse starts from right side
        menuRect.pivot = new Vector2(1f, 1f);

        float elapsedTime = 0f;
        float startWidth = menuRect.sizeDelta.x;
        Vector2 startPos = menuRect.anchoredPosition;
        Vector2 targetSize = new Vector2(0f, menuRect.sizeDelta.y);
        Vector2 targetPos = startPos - new Vector2(startWidth, 0); // Move left

        while (elapsedTime < duration)
        {
            //Debug.Log("Elapsed time in Collapse menu: " +  elapsedTime);
            float t = elapsedTime / duration;
            menuRect.sizeDelta = Vector2.Lerp(new Vector2(startWidth, menuRect.sizeDelta.y), targetSize, t);
            menuRect.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        menuRect.sizeDelta = targetSize;
        menuRect.anchoredPosition = targetPos;
        closerbutton2.SetActive(true);
        rimcolormenu.SetActive(true);


    }
    IEnumerator CollapseMenuOuter(RectTransform menuRect, float duration)
    {
        yield return StartCoroutine(CollapseMenu(menuRect, duration));
        CarMenu.SetActive(false);
        openbutton.SetActive(true);
    }

    IEnumerator ExpandMenu(RectTransform menuRect, float duration, float targetWidth)
    {
        //Debug.Log("Duration in expand menu: " + duration);
        // Ensure pivot is set to the right for correct expansion
        menuRect.pivot = new Vector2(1f, 1f);

        float elapsedTime = 0f;
        Vector2 startSize = new Vector2(0f, menuRect.sizeDelta.y);
        Vector2 targetSize = new Vector2(targetWidth, menuRect.sizeDelta.y);

        Vector2 startPos = menuRect.anchoredPosition;
        Vector2 targetPos = startPos + new Vector2(targetWidth, 0); // Move right

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            //Debug.Log("Elapsed Time in expand menu: " + elapsedTime);
            menuRect.sizeDelta = Vector2.Lerp(startSize, targetSize, t);
            menuRect.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        menuRect.sizeDelta = targetSize;
        menuRect.anchoredPosition = targetPos;
    }

    IEnumerator ExpandMenuvertically(RectTransform menuRect, float duration)
    {
        // Set the pivot at the top center so expansion occurs downward.
        //menuRect.pivot = new Vector2(0.5f, 1f);

        float elapsedTime = 0f;

        // The width remains the same, while the height changes from 0 to 1500.
        Vector2 startSize = new Vector2(menuRect.sizeDelta.x, 0f);
        Vector2 targetSize = new Vector2(menuRect.sizeDelta.x, 1500f);

        // With the pivot at the top, the anchoredPosition remains at the top as the height grows downward.
        Vector2 startPos = menuRect.anchoredPosition;
        Vector2 targetPos = startPos + new Vector2(0, -1500f);

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            // Interpolate the size and anchored position.
            menuRect.sizeDelta = Vector2.Lerp(startSize, targetSize, t);
            menuRect.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Set final values to ensure the menu is fully expanded.
        menuRect.sizeDelta = targetSize;
        //menuRect.anchoredPosition = targetPos;
    }

    IEnumerator ExpandMenuOuter(RectTransform menuRect, float duration, float targetWidth)
    {

        yield return StartCoroutine(ExpandMenu(menuRect, duration, targetWidth));

        closerbutton.SetActive(true);
        carmenuholder.SetActive(true);
    }

    private IEnumerator MoveCamera(Vector3 maincameraposition, Vector3 maincameraangle)
    {
        //Vector3 targetPosition = new Vector3(4.378f, 4.011f, -0.8f);
        Vector3 targetPosition = maincameraposition;
        //Vector3 targetEulerAngles = new Vector3(22.6f, -81.9f, 0f);
        Vector3 targetEulerAngles = maincameraangle;
        Vector3 startPosition = outsetcamera.transform.position;
        // Get the starting rotation as a Quaternion
        Quaternion startRotation = outsetcamera.transform.rotation;
        // Build the target rotation Quaternion from Euler angles
        Quaternion targetRotation = Quaternion.Euler(targetEulerAngles);

        float elapsed = 0f;
        while (elapsed < duration)
        {
            // Calculate the interpolation fraction (0 to 1)
            float t = elapsed / duration;

            // Smoothly interpolate the position
            outsetcamera.transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            // Smoothly interpolate the rotation (using SLERP for smooth rotation)
            outsetcamera.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);

            elapsed += Time.deltaTime;
            yield return null;
        }
        // Ensure the final position and rotation are set exactly to target values
        outsetcamera.transform.position = targetPosition;
        outsetcamera.transform.rotation = targetRotation;
    }


}
