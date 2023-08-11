using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections;

public class SoundManager : MonoBehaviour
{
	public Sound[] sounds;
	public Sound[] effects;
	public bool alone = true;

    public static SoundManager instance;

    void Awake()
    {	
    	if(instance != null){
            Debug.LogWarning("More than one instace of SoundManager found!");
            return;
        }
        instance = this;
        PlayRandomLoop(alone);
    }

    public void Play (string name, bool clear,bool loop){
    	if(clear == true){
	    	foreach (Sound x in sounds){
	        	Destroy(x.source);
	        }
    	}
    	Sound s = Array.Find(sounds, sound => sound.name == name);
    	s.source = gameObject.AddComponent<AudioSource>();
        s.source.clip = s.clip;
        s.source.volume = s.volume;
        s.source.pitch = s.pitch;
        s.source.loop = loop;
    	s.source.Play();
    }

    public void PlayEffect (string name, bool clear,bool loop){
    	if(clear == true){
	    	foreach (Sound x in sounds){
	        	Destroy(x.source);
	        }
    	}
    	Sound s = Array.Find(effects, sound => sound.name == name);
    	s.source = gameObject.AddComponent<AudioSource>();
        s.source.clip = s.clip;
        s.source.volume = s.volume;
        s.source.pitch = s.pitch;
        s.source.loop = loop;
    	s.source.Play();
    	StartCoroutine(EndEffect(s));
    }

    public void PlayRandom (bool clear,bool loop){
    	if(clear == true){
	    	foreach (Sound x in sounds){
	        	Destroy(x.source);
	        }
    	}
    	Sound s = sounds[UnityEngine.Random.Range(0, sounds.Length)];
    	s.source = gameObject.AddComponent<AudioSource>();
        s.source.clip = s.clip;
        s.source.volume = s.volume;
        s.source.pitch = s.pitch;
        s.source.loop = loop;
    	s.source.Play();
    }

    public void PlayRandomLoop (bool clear){
    	if(clear == true){
	    	foreach (Sound x in sounds){
	        	Destroy(x.source);
	        }
    	}
    	AudioSource loopAudioSource = gameObject.AddComponent<AudioSource>();
    	StartCoroutine(RandomLoop(loopAudioSource));
    }

    IEnumerator RandomLoop(AudioSource loopSource){
        Sound s = sounds[UnityEngine.Random.Range(0, sounds.Length)];
    	s.source = loopSource;
        s.source.clip = s.clip;
        s.source.volume = s.volume;
        s.source.pitch = s.pitch;
        s.source.loop = false;
    	s.source.Play();
        yield return new WaitForSeconds(s.source.clip.length);
        StartCoroutine(RandomLoop(loopSource));
    }

    IEnumerator EndEffect(Sound sound){
        yield return new WaitForSeconds(sound.source.clip.length);
        Destroy(sound.source);
    }
}