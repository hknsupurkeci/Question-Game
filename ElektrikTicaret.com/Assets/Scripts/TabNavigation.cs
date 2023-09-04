using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TabNavigation : MonoBehaviour
{
    public TMP_InputField inputField1;
    public TMP_InputField inputField2;
    public TMP_InputField inputField3;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inputField1.isFocused)
            {
                inputField2.Select();
                inputField2.ActivateInputField();
            }
            else if (inputField2.isFocused)
            {
                inputField3.Select();
                inputField3.ActivateInputField();
            }
            else if (inputField3.isFocused)
            {
                inputField1.Select();
                inputField1.ActivateInputField();
            }
        }
    }
}
