using UnityEngine;

public abstract class BaseEffect: MonoBehaviour

{
     public Transform parent, child;

    //the server ask both player if they have a response.
    public abstract bool RequestActivation();

    //queue card to be activated, Then ask again RequestActivation();
    // if both said no & no.
    //ResolvedEffect()
    public abstract void ActivateEffect();

    // cards will be resolved 
    // case it was destroied this effect won't activate
    // because it was removed from the queue
    public abstract void ResolvedEffect();



}
