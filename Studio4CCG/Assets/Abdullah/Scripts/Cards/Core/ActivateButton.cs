using UnityEngine;
using UnityEngine.Events;

public class ActivateButton : MonoBehaviour
{

    public UnityEvent buttonEffect= new UnityEvent();

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)&& hit.collider != null 
                && hit.collider.gameObject==gameObject)
            {
                TriggerButton();

            }


        }
    }
        void TriggerButton()
    {
        buttonEffect.Invoke();
        
    }
    public void TestButton()
    {
        Debug.Log("I'm a button :3");

    }
}
