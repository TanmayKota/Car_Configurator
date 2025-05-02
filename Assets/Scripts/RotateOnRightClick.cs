using UnityEngine;

public class RotateOnRightClick : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public GameObject empty;
    public float x;
    public float y;
    public float z;

    void Update()
    {
        // Check if the right mouse button is held down
        if (Input.GetMouseButton(1))
        {
            // Get horizontal mouse movement (delta)
            float mouseX = Input.GetAxis("Mouse X");
            float rotationAmount = -mouseX * rotationSpeed * Time.deltaTime;

            Vector3 pivot = new Vector3(x, y, z);
        /*    Instantiate(empty, pivot, Quaternion.identity);
            Debug.Log("Pivot: " + pivot);*/

            // Rotate around the computed pivot
            transform.RotateAround(pivot, Vector3.up, rotationAmount);
        }
    }
}
