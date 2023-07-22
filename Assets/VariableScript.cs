using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableScript : MonoBehaviour
{
    public static bool blueWeapon = false;
    public static bool greenWeapon = false;
    public static bool redWeapon = false;
    public static GameObject currWeapon = null;

    public static void setAllFalse()
    {
        blueWeapon = false;
        greenWeapon = false;
        redWeapon = false;
    }
     
}
