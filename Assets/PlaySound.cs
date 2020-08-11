using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] AnimationClip clip;

    bool isFadingOut;

    float clipLength;
    float pitch;

    // Start is called before the first frame update
    void Start()
    {
        clipLength = clip.length - .5f;
        pitch = Random.Range(.7f, 1.3f);
        AudioManager.instance.ChangePitch(pitch, "Text Fadein");
        AudioManager.instance.PlaySound("Text Fadein");
        StartCoroutine(WaitForSound());
        Debug.Log("Cliplength: " + clipLength);
    }

    private void Update()
    {
        if (isFadingOut)
        {
            AudioManager.instance.ChangePitch(pitch, "Text Fadeout");
            AudioManager.instance.PlaySound("Text Fadeout");
            isFadingOut = false;
        }
    }

    IEnumerator WaitForSound()
    {
        yield return new WaitForSecondsRealtime(clipLength);
        isFadingOut = true;
    }
}
