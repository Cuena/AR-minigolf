using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TrackingEventHandler : MonoBehaviour
{

    public VehicleMover vehicleMover;

    private void Awake()
    {
        var trackableBehaviour = GetComponent<TrackableBehaviour>();
        trackableBehaviour.RegisterOnTrackableStatusChanged(OnTrackingEventChanged);
    }

    private void OnTrackingEventChanged(TrackableBehaviour.StatusChangeResult obj)
    {
        if (obj.NewStatus != TrackableBehaviour.Status.NO_POSE)
        {
            vehicleMover.ResetPosition();
        }
    }
}
