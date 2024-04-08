using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BaseUIManager<EnumIdentifyingUIVisibleElement> : MonoBehaviour  
{
    public abstract class BaseVisibleUIElement : MonoBehaviour
    {
        public abstract EnumIdentifyingUIVisibleElement ElementIdentifier { get; }
    }

    protected List<BaseUIManager<EnumIdentifyingUIVisibleElement>.BaseVisibleUIElement> UIVisibleElementsList = new List<BaseUIManager<EnumIdentifyingUIVisibleElement>.BaseVisibleUIElement>();

    protected virtual void Awake()
    {
        GetAndStoreAllVisibleUIElements(transform);
    }

    protected virtual void OnEnable()
    {
        SubscribeEvents();
    }

    protected virtual void OnDisable()
    {
        UnsubscribeEvents();
    }

    protected void GetAndStoreAllVisibleUIElements(Transform root)
    {
        foreach (Transform child in root.GetComponent<Transform>())
        {
            if (child.gameObject.GetComponent<BaseVisibleUIElement>() != null)
            {
                BaseVisibleUIElement newElement = child.gameObject.GetComponent<BaseVisibleUIElement>();
                UIVisibleElementsList.Add(newElement);
            }
            GetAndStoreAllVisibleUIElements(child);
        }
    }

    protected void ShowUIElementsFromParameterAndHideAllOther(List<EnumIdentifyingUIVisibleElement> elementsToShow)
    {
        List<EnumIdentifyingUIVisibleElement> elemntsToHide = new List<EnumIdentifyingUIVisibleElement>();

        elemntsToHide.CopyListFromParameterElementsToThisList(Enum.GetValues(typeof(EnumIdentifyingUIVisibleElement)).Cast<EnumIdentifyingUIVisibleElement>().ToList());
        elemntsToHide.RemoveFromThisListElementsFromParameterListByTheirValue(elementsToShow);

        foreach (EnumIdentifyingUIVisibleElement elementToShow in elementsToShow)
        {
            foreach (BaseUIManager<EnumIdentifyingUIVisibleElement>.BaseVisibleUIElement element in UIVisibleElementsList)
            {
                if (EqualityComparer< EnumIdentifyingUIVisibleElement >.Default.Equals(elementToShow, element.ElementIdentifier)) element.gameObject.SetActive(true);
            }
        }

        foreach (EnumIdentifyingUIVisibleElement elementToHide in elemntsToHide)
        {
            foreach (BaseUIManager<EnumIdentifyingUIVisibleElement>.BaseVisibleUIElement element in UIVisibleElementsList)
            {
                if (EqualityComparer<EnumIdentifyingUIVisibleElement>.Default.Equals(elementToHide, element.ElementIdentifier)) element.gameObject.SetActive(false);
            }
        }
    }

    protected abstract void SubscribeEvents();
    protected abstract void UnsubscribeEvents();
}
