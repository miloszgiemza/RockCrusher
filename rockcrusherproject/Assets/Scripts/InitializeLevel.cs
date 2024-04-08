using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeLevel : MonoBehaviour
{
    [SerializeField] private GameObject gameWorldParent;
    private GameObject backgroundsParent;

    private float backgroundsGameworldZPos = 3;

    private void GenerateBackground()
    {
        backgroundsParent = new GameObject("Backgrounds");
        backgroundsParent.transform.parent = gameWorldParent.transform;
        backgroundsParent.transform.position = new Vector3(0f, 0f, backgroundsGameworldZPos);

        GameObject backgroundMiddle = new GameObject("Backgorund", typeof(SpriteRenderer));
        backgroundMiddle.transform.parent = backgroundsParent.transform;
        backgroundMiddle.transform.localPosition = new Vector3(0f, 0f, 0f);
        backgroundMiddle.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.LevelToLoad.LevelBackground;

        GameObject backgroundLeft = new GameObject("Backgorund", typeof(SpriteRenderer));
        backgroundLeft.transform.parent = backgroundsParent.transform;
        backgroundLeft.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.LevelToLoad.LevelBackground;
        backgroundLeft.transform.localPosition = new Vector3(backgroundMiddle.transform.position.x - backgroundMiddle.GetComponent<SpriteRenderer>().size.x, 0f, 0f);

        GameObject backgroundRight = new GameObject("Backgorund", typeof(SpriteRenderer));
        backgroundRight.transform.parent = backgroundsParent.transform;
        backgroundRight.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.LevelToLoad.LevelBackground;
        backgroundRight.transform.localPosition = new Vector3(backgroundMiddle.transform.position.x + backgroundMiddle.GetComponent<SpriteRenderer>().size.x, 0f, 0f);
    }
}
