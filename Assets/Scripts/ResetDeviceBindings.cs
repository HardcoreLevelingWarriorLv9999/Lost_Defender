using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResetDeviceBindings : MonoBehaviour
{
    [SerializeField] private InputActionAsset _inputActionAsset;

    public void ResetAllBindings()
    {
        foreach (InputActionMap map in _inputActionAsset.actionMaps)
        {
            map.RemoveAllBindingOverrides();
        }
    }
}
