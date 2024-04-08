using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpsPointsPooler : MonoBehaviour
{
    [SerializeField] private GameObject popUpPoints;
    private Vector2 idlePosition = new Vector2(-8486f, 0f);

    private int poolerSize = 15;
    private Stack<PopUpPoints> popUpsPool = new Stack<PopUpPoints>();

    private void Awake()
    {
        CreatePopUpsPooler(poolerSize);
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void CreatePopUpAndAddItToAvaliablePooler()
    {
        GameObject newPopUp = Instantiate(popUpPoints, transform);
        PopUpPoints newPopUpPoints = newPopUp.GetComponent<PopUpPoints>();
        newPopUpPoints.SetRectTransform(idlePosition);
        popUpsPool.Push(newPopUpPoints);
    }

    private void CreatePopUpsPooler(int size)
    {
        for(int i = 0; i < size; i++)
        {
            CreatePopUpAndAddItToAvaliablePooler();
        }
    } 
    
    public PopUpPoints ProvidePopUpPoints()
    {
        if(popUpsPool.Count <= 0)
        {
            CreatePopUpAndAddItToAvaliablePooler();
        }
        PopUpPoints popUpToReturn = popUpsPool.Pop();
        return popUpToReturn;
    }

    private void ReturnPopUpToAvaliablePool(PopUpPoints popUpToReturn)
    {
        popUpsPool.Push(popUpToReturn);
        popUpToReturn.SetRectTransform(idlePosition);
    }

    private void SubscribeEvents()
    {
        GameEvents.OnHidePopUuPoints += ReturnPopUpToAvaliablePool;
    }

    private void UnsubscribeEvents()
    {
        GameEvents.OnHidePopUuPoints += ReturnPopUpToAvaliablePool;
    }
}
