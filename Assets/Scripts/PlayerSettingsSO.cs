using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/PlayerSettings")]
public class PlayerSettingsSO : ScriptableObject
{
    public float speed;
    public float jumpHeight;
}
