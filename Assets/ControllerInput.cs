using UnityEngine;
using System.Collections;

public class ControllerInput : MonoBehaviour {

    public float pickupRadius = 1f;
    Pickup pickedObject;

    SteamVR_TrackedObject inputDevice;
    SteamVR_Controller.Device controller
    {
        get
        {
            return SteamVR_Controller.Input((int)inputDevice.index);
        }
    }

    void Start()
    {
        inputDevice = GetComponentInChildren<SteamVR_TrackedObject>();
    }

    void Update()
    {
        if (controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_Grip))
        {
            Collider[] pickups = Physics.OverlapSphere(transform.position, pickupRadius);
            if (pickups != null)
            {
                foreach(Collider collider in pickups)
                {
                    Pickup pickup = collider.GetComponent<Pickup>();
                    if (pickup != null)
                    {
                        pickup.GetPickedUp(gameObject);
                        pickedObject = pickup;
                    }
                }
            }
        }

        if (controller.GetPressUp(Valve.VR.EVRButtonId.k_EButton_Grip))
        {
            if (pickedObject != null)
            {
                pickedObject.GetReleased(controller.velocity);
                pickedObject = null;
            }
        }

    }
}
