using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.UI;

public class SlideshowMaster : MonoBehaviour {

    [SerializeField]
    private Image spriteHolder;
    [SerializeField]
    private float spriteFadeDuration;
    [SerializeField]
    private Sprite[] slides;
    [SerializeField]
    private Transform[] shots;
    [SerializeField]
    private float cameraMoveDuration;
    [SerializeField]
    private GameObject cam;
    [SerializeField]
    private GameObject[] activatorScripts;
    [SerializeField]
    private PostProcessingProfile[] lerpProfiles;
    [SerializeField]
    private GameObject[] orblings;
    [SerializeField]
    private GameObject orb;
    
    
    private int stepCounter;

	void Start () 
	{
        spriteHolder.color = new Vector4(1, 1, 1, 0);

        StartCoroutine(LerpCamera(shots[6]));
	}
	
	void Update () 
	{
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextStep();
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            PreviousStep();
        }
    }

    private void NextStep()
    {
        ShowStep(stepCounter);
        stepCounter++;
    }

    private void PreviousStep()
    {
        stepCounter --;
        ShowStep(stepCounter - 1);
    }

    private void ShowStep(int step)
    {
        switch (step)
        {
            case 0:
                //Niklas Slide Elevator Pitch
                StartCoroutine(LerpCamera(shots[11]));
                StartCoroutine(FadeSpriteIn(5));
                break;
            case 1:
                //Miklas Slide Intentions
                StartCoroutine(FadeSpriteOutIn(7));
                break;
            case 2:
                //Rube Slide Environment
                StartCoroutine(LerpCamera(shots[12]));
                StartCoroutine(FadeSpriteOutIn(4));
                break;
            case 3:
                //Rube Slide Character
                StartCoroutine(FadeSpriteOutIn(3));
                break;
            case 4:
                //Iggy Statue 1
                StartCoroutine(FadeSpriteOut());
                StartCoroutine(LerpCamera(shots[0]));
                break;
            case 5:
                //Iggy Statue 2
                StartCoroutine(LerpCamera(shots[1]));
                break;
            case 6:
                //Iggy Statue 3
                StartCoroutine(LerpCamera(shots[2]));
                break;
            case 7:
                //Iggy Statue 4
                StartCoroutine(LerpCamera(shots[3]));
                break;
            case 8:
                //Iggy Statue 5
                StartCoroutine(LerpCamera(shots[4]));
                break;
            case 9:
                //Niklas Orb
                StartCoroutine(LerpCamera(shots[6]));
                break;
            case 10:
                //Niklas Orbling
                StartCoroutine(LerpCamera(shots[7]));
                break;
            case 11:
                //Niklas Rad
                StartCoroutine(LerpCamera(shots[8]));
                break;
            case 12:
                //Niklas Whale
                activatorScripts[0].GetComponent<ObjectActivatorTrigger>().Trigger(true);
                StartCoroutine(LerpCamera(shots[5]));
                break;
            case 13:
                //Raven Sound Text
                StartCoroutine(LerpCamera(shots[11]));
                StartCoroutine(FadeSpriteIn(1));
                break;
            case 14:
                //Raven Sound Graph
                StartCoroutine(FadeSpriteOutIn(2));
                break;
            case 15:
                //Raven Orbling Demo
                StartCoroutine(FadeSpriteOut());
                StartCoroutine(LerpCamera(shots[13]));
                break;
            case 16:
                //Raven Orbling Drop 1
                GameObject go1 = GameObject.Instantiate<GameObject>(orblings[0]);
                go1.transform.position = new Vector3(0, 60, 0);
                break;
            case 17:
                //Raven Orbling Drop 2
                GameObject go2 = GameObject.Instantiate<GameObject>(orblings[1]);
                go2.transform.position = new Vector3(0, 60, 0);
                break;
            case 18:
                //Raven Orbling Drop 3
                GameObject go3 = GameObject.Instantiate<GameObject>(orblings[2]);
                go3.transform.position = new Vector3(0, 60, 0);
                break;
            case 19:
                //Raven Orbling Drop 4
                GameObject go4 = GameObject.Instantiate<GameObject>(orblings[3]);
                go4.transform.position = new Vector3(0, 60, 0);
                break;
            case 20:
                //Raven Orbling Drop 5
                GameObject go5 = GameObject.Instantiate<GameObject>(orblings[4]);
                go5.transform.position = new Vector3(0, 60, 0);
                break;
            case 21:
                //Maurice Grid Shot
                SongTurnOff();
                StartCoroutine(LerpCamera(shots[10]));
                break;
            case 22:
                //Maurice Wall Shot
                StartCoroutine(LerpCamera(shots[9]));
                break;
            case 23:
                //Maurice Orb Shot
                StartCoroutine(LerpCamera(shots[6]));
                break;
            case 24:
                //Rube Roadmap Wall Shot
                StartCoroutine(LerpCamera(shots[11]));
                StartCoroutine(FadeSpriteIn(8));
                break;
            case 25:
                //End City Shot
                StartCoroutine(LerpCamera(shots[10]));
                StartCoroutine(FadeSpriteOutIn(6));
                break;
            case 26:

                break;
            case 27:

                break;
            case 28:

                break;
            case 29:

                break;
            case 30:

                break;
            case 31:

                break;
            case 32:

                break;
            case 33:

                break;
            case 34:

                break;
            default:
                Debug.Log("No more steps programmed");
                break;
        }
    }

    //private void LerpSlide(int slideIndex, int cameraIndex)
    //{
    //    slides[slideIndex].SetActive(true);

    //    StartCoroutine(LerpCamera(shots[cameraIndex]));

    //    StartCoroutine(DeactivateSlides(slides[slideIndex]));
    //}

    //private void TPSlide(int slideIndex, int cameraIndex)
    //{
    //    foreach (GameObject slide in slides)
    //    {
    //        slide.SetActive(false);
    //    }

    //    slides[slideIndex].SetActive(true);

    //    SetCamera(shots[cameraIndex]);
    //}

    //private IEnumerator DeactivateSlides(GameObject exception)
    //{
    //    yield return new WaitForSeconds(cameraMoveDuration);

    //    foreach (GameObject slide in slides)
    //    {
    //        if(slide != exception)
    //        {
    //            slide.SetActive(false);
    //        }
    //    }
    //}

    private IEnumerator FadeSpriteIn(int i)
    {
        spriteHolder.sprite = slides[i];
        spriteHolder.color = new Vector4(1, 1, 1, 0);

        float t = 0.0F;
        float lerpStart = Time.time;

        while (t <= 1F)
        {
            t = (Time.time - lerpStart) / spriteFadeDuration;

            spriteHolder.color = Vector4.Lerp(new Vector4(1, 1, 1, 0), new Vector4(1, 1, 1, 1), t);

            yield return null;
        }
    }

    private IEnumerator FadeSpriteOutIn(int i)
    {
        float t = 0.0F;
        float lerpStart = Time.time;

        while (t <= 1F)
        {
            t = (Time.time - lerpStart) / (spriteFadeDuration / 2);

            spriteHolder.color = Vector4.Lerp(new Vector4(1, 1, 1, 1), new Vector4(1, 1, 1, 0), t);

            yield return null;
        }

        spriteHolder.sprite = slides[i];

        t = 0.0F;
        lerpStart = Time.time;

        while (t <= 1F)
        {
            t = (Time.time - lerpStart) / (spriteFadeDuration / 2);

            spriteHolder.color = Vector4.Lerp(new Vector4(1, 1, 1, 0), new Vector4(1, 1, 1, 1), t);

            yield return null;
        }
    }

    private IEnumerator FadeSpriteOut()
    {
        float t = 0.0F;
        float lerpStart = Time.time;

        while (t <= 1F)
        {
            t = (Time.time - lerpStart) / spriteFadeDuration;

            spriteHolder.color = Vector4.Lerp(new Vector4(1, 1, 1, 1), new Vector4(1, 1, 1, 0), t);

            yield return null;
        }
    }

    private IEnumerator LerpCamera(Transform target)
    {
        Vector3 position = cam.transform.position;
        Quaternion rotation = cam.transform.rotation;
        Vector3 targetPosition = target.position;
        Quaternion targetRotation = target.rotation;

        float t = 0.0F;
        float lerpStart = Time.time;

        while(t <= 1F)
        {
            t = (Time.time - lerpStart) / cameraMoveDuration;

            cam.transform.position = Vector3.Lerp(position, targetPosition, t);
            cam.transform.rotation = Quaternion.Slerp(rotation, targetRotation, t);

            yield return null;
        }
    }

    private void SetCamera(Transform target)
    {
        cam.transform.position = target.position;
        cam.transform.rotation = target.rotation;
    }

    private void SongTurnOff()
    {
        //foreach(string s in TrackRegistry.percussionTracksIntro)
        //{
        //    AkSoundEngine.SetState(s, "off");
        //}
        //foreach (string s in TrackRegistry.bassTracksIntro)
        //{
        //    AkSoundEngine.SetState(s, "off");
        //}
        //foreach (string s in TrackRegistry.leadTracksIntro)
        //{
        //    AkSoundEngine.SetState(s, "off");
        //}
        //foreach (string s in TrackRegistry.kickTracksIntro)
        //{
        //    AkSoundEngine.SetState(s, "off");
        //}
        //foreach (string s in TrackRegistry.chordTracksIntro)
        //{
        //    AkSoundEngine.SetState(s, "off");
        //}

        //AkSoundEngine.SetState("I_kick02", "on");

        AkSoundEngine.PostEvent("Volume_low", orb);
    }
}
