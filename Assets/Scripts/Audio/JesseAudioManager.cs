using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class JesseAudioManager : MonoBehaviour
{
	[Serializable]
	public class AudioClass
	{
		public string name;
		public AudioSource source;
	}
	
	public AudioClass[] sources;
    Coroutine delayedSound;

	public void PlaySound(string Name)
	{
		foreach(AudioClass c in sources)
		{
			if(c.name == Name)
			{
				c.source.Play();
			}
		}
	}

	public void PlaySound(string Name, AudioClip Clip)
	{
		foreach(AudioClass c in sources)
		{
			if(c.name == Name)
			{
				Debug.Log(c.name);
				c.source.clip = Clip;
				c.source.Play();
			}
		}
	}

    public void PlaySoundWithDelay(string Name, float delay)
    {
        if (delayedSound != null)
        {
            StopCoroutine(delayedSound);
            delayedSound = null;
        }
        delayedSound = StartCoroutine(DelayedSound(Name, delay));
    }

    IEnumerator DelayedSound(string Name, float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach (AudioClass c in sources)
        {
            if (c.name == Name)
            {
                c.source.Play();
            }
        }
    }

    public void StopSound(string Name)
	{
		foreach(AudioClass c in sources)
		{
			if(c.name == Name)
			{
				c.source.Stop();
			}
		}
	}

	public void PlayOneShot(Vector3 position, AudioClip clip, float blend)
	{
		GameObject obj = new GameObject();

		AudioSource source = obj.AddComponent<AudioSource>();
		source.spatialBlend = blend;
		source.clip = clip;
		source.playOnAwake = false;

		obj.transform.position = position;

		source.Play();

		Destroy(obj, clip.length);
	}
}