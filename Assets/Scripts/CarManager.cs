using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    public GameObject internalcamera;
    public Vector3 internalcameraoriginalposition;
    public Vector3 internalcameraoriginalangle;
    public GameObject maincamera;
    public GameObject internalpanel;
    public GameObject scenemanager;
    public GameObject firstpanel;
    public GameObject seatselection;
    public GameObject seatcolor;
    public GameObject seatcolor2;
    public GameObject internalcamerafront;
    public GameObject internalcamera45degrees;
    public GameObject internalcamerasteeringwheel;
    public GameObject steeringmenu;
    public GameObject dashboardmenu;
    public GameObject windshield;


    public void SeatSelectionbutton(GameObject camera)
    {
        internalcamera.transform.position = camera.transform.position;
        internalcamera.transform.eulerAngles = camera.transform.eulerAngles;
        internalpanel.SetActive(false);
        windshield.SetActive(false);
        seatselection.SetActive(true);
        internalcamera.GetComponent<Camera>().fieldOfView = 100f;
    }

    public void SeatSelectionColor(GameObject camera)
    {
        internalcamera.transform.position = camera.transform.position;
        internalcamera.transform.eulerAngles = camera.transform.eulerAngles;
        internalpanel.SetActive(false);
        windshield.SetActive(false);
        seatcolor.SetActive(true);
        internalcamera.GetComponent<Camera>().fieldOfView = 100f;
    }

    public void CameraChanger()
    {
        if (internalcamera.transform.position == internalcamerafront.transform.position)
        {
            internalcamera.transform.position = internalcamera45degrees.transform.position;
            internalcamera.transform.eulerAngles = internalcamera45degrees.transform.eulerAngles;
        }
        else
        {
            internalcamera.transform.position = internalcamerafront.transform.position;
            internalcamera.transform.eulerAngles = internalcamerafront.transform.eulerAngles;
        }
    }

    public void ReturnButton(GameObject menu)
    {
        if(internalcamera.transform.localPosition != internalcameraoriginalposition)
        {
            internalcamera.transform.localPosition = internalcameraoriginalposition;
            internalcamera.transform.localEulerAngles = internalcameraoriginalangle;
            internalcamera.GetComponent<Camera>().fieldOfView = 40f;
        }

        if (!windshield.activeSelf) windshield.SetActive(true);   
        menu.SetActive(false);
        internalpanel.SetActive(true);
    }

    public void SeatColor()
    {
        seatcolor2.SetActive(true);
        seatselection.SetActive(false);
    }

    public void ReturnToSeatSelection()
    {
        seatcolor2.SetActive(false);
        seatselection.SetActive(true); 
    }

    public void SteeringWheelMenu()
    {
        internalpanel.SetActive(false);
        steeringmenu.SetActive(true);
        internalcamera.transform.position = internalcamerasteeringwheel.transform.position;
        internalcamera.GetComponent<Camera>().fieldOfView = 55f;
        internalcamera.transform.eulerAngles = internalcamerasteeringwheel.transform.eulerAngles;
    }

    public void DashboardMenu()
    {
        internalpanel.SetActive(false);
        dashboardmenu.SetActive(true);
    }

    public void ChangeViewButton(GameObject cameraat45dgerees)
    {
        if (internalcamera.transform.localPosition != internalcamerafront.transform.localPosition)
        {
            internalcamera.transform.localPosition = internalcamerafront.transform.localPosition;
            internalcamera.transform.localEulerAngles = internalcamerafront.transform.localEulerAngles;
            internalcamera.GetComponent<Camera>().fieldOfView = 100f;
        }
        else
        {
            internalcamera.transform.localPosition = cameraat45dgerees.transform.localPosition;
            internalcamera.transform.localEulerAngles = cameraat45dgerees.transform.localEulerAngles;
            internalcamera.GetComponent<Camera>().fieldOfView = 90f;
        }
    }

    public void InternalToMainCamera()
    {
        internalcamera.SetActive(false);
        internalpanel.SetActive(false);
        maincamera.SetActive(true);
        firstpanel.SetActive(true);

    }

    public void InternalToOuterCamera()
    {
        internalcamera.SetActive(false);
        internalpanel.SetActive(false);
        maincamera.SetActive(true);
        firstpanel.SetActive(true);
        Manager manager = scenemanager.GetComponent<Manager>();
        manager.CarDeactivator();
    }
}
