using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VrButton : MonoBehaviour {

    public string collideTag = "GameController";
    public Material hoverMat;
    public Material baseMat;
    public string buttonText = "Button Text";

    Renderer _rend;
    Text _buttonText;

    private void Start()
    {
        _rend = GetComponent<Renderer>();
        _buttonText = GetComponentInChildren<Text>();
        _rend.material = baseMat;

        if (_buttonText != null) _buttonText.text = buttonText;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == collideTag)
        {
            _rend.material = hoverMat;

            var ctl = collision.gameObject.GetComponent<VrController>();
            if (ctl)
            {
                ctl.activeButton = this;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == collideTag)
        {
            _rend.material = baseMat;

            var ctl = collision.gameObject.GetComponent<VrController>();
            if (ctl)
            {
                ctl.activeButton = null;
            }
        }
    }

    public virtual void Click(string param)
    {
        Debug.Log("Clicked button: " + buttonText + ". Param: " + param);
    }
}
