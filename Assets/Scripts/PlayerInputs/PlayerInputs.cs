using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldsDev
{
    public class PlayerInputs : MonoBehaviour
    {
        protected void Awake()
        {
            Setup();
        }

        protected void Setup()
        {
        }

        private RaycastHit hit;

        private bool Clicked()
        {
            if (Input.GetMouseButtonDown(0))
            {
                return true;
            }
            return false;
        }
        protected void Update()
        {
            if (Clicked())
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit,10000,GameControl.ClickMask))
                {
                    var selectable = hit.transform.gameObject.GetComponent<ISelectable>();
                    selectable?.OnSelected();
                }
            }
           
        }
    }
}