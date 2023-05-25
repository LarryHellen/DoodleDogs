using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TempSetDefaultJSONValues : MonoBehaviour
{
    private JsonDataManipulation jsonDataManipulation = new JsonDataManipulation();

    public void SetJSONToDefaultValues()
    {
        jsonDataManipulation.LoadByJSON();

        jsonDataManipulation.LoadDefaultValuesFromPlayerDataRefactored();

        jsonDataManipulation.SaveByJSON();
    }
}
