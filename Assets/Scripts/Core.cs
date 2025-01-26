using UnityEngine;
using System.Collections;

// Basic demonstration of a music system that uses PlayScheduled to preload and sample-accurately
// stitch two AudioClips in an alternating fashion.  The code assumes that the music pieces are
// each 16 bars (4 beats / bar) at a tempo of 140 beats per minute.
// To make it stitch arbitrary clips just replace the line
//   nextEventTime += (60.0 / bpm) * numBeatsPerSegment
// by
//   nextEventTime += clips[flip].length;

[RequireComponent(typeof(AudioSource))]
public class Core : MonoBehaviour
{
    public float bpm = 150.0f;
    public int numBeatsPerSegment = 16;
    public AudioClip[] clips = new AudioClip[2];

    private double nextEventTime;
    private int flip = 0;
    private AudioSource[] audioSources = new AudioSource[2];
    public static bool running = false;
    public Transform cube;
    public int counter;
    public bool second;
    public static ITickHandler tickHandler;

    void Start()
    {
        for (int i = 0; i < 2; i++)
        {
            GameObject child = new GameObject("Player");
            child.transform.parent = gameObject.transform;
            audioSources[i] = child.AddComponent<AudioSource>();
        }

        nextEventTime = AudioSettings.dspTime + 2.0f;
        running = true;
        if (second)nextEventTime += (60.0f / bpm * numBeatsPerSegment) / 2;
    }

    void Update()
    {
        if (!running)
        {
            return;
        }

        double time = AudioSettings.dspTime;

        if (time + 1.0f > nextEventTime)
        {
            // We are now approx. 1 second before the time at which the sound should play,
            // so we will schedule it now in order for the system to have enough time
            // to prepare the playback at the specified time. This may involve opening
            // buffering a streamed file and should therefore take any worst-case delay into account.
            //audioSources[flip].clip = clips[flip];
            //audioSources[flip].PlayScheduled(nextEventTime);

            //cube.transform.position = new Vector3 (Random.Range (-3.0F, 3.0F), Random.Range (-3.0F, 3.0F));

            // Place the next event 16 beats from here at a rate of 140 beats per minute
            nextEventTime += 60.0f / bpm * numBeatsPerSegment;
            tickHandler.Tick ();

            // Flip between two audio sources so that the loading process of one does not interfere with the one that's playing out
            flip = 1 - flip;
            //counter++;
        }
    }
}

public interface ITickHandler {
    public void Tick ();
}