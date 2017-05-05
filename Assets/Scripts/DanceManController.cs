using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceManController : MonoBehaviour {


    public AudioSource _bgmSource;
    public Transform DanceManAim;
    public float MoveSpeed = 0.5f;
    public float RotateSpeed = 0.5f;
    public float WaitTime = 5.0f;
    //Voice
    public AudioClip Voice01;
    public AudioClip Voice02;
    public AudioClip Voice03;
    public AudioClip Voice04;
    public AudioClip Bgm01;
    public AudioClip Bgm02;
    public AudioClip Bgm03;

    private Animator _animator;
    private GameObject _player;
    private Transform _danceManTransform;
    private AudioSource _audioSource;

    private bool _isWalk = false;
    private bool _isRotate = false;
    private float _bmgVolume = 0.15f;

    void Awake() {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _player = GameObject.FindGameObjectWithTag("Player") as GameObject;
        _danceManTransform = transform;
    }
	void Start () {
        StartCoroutine("MoveMan");
        PlayAudio(Voice01);
        PlayBGM(Bgm01, _bmgVolume);
	}
	
	void Update () {
        if (_isWalk) {
            if (Vector3.Distance(_danceManTransform.localPosition, DanceManAim.localPosition) > 0.05f)
            {
                _animator.SetBool("Walk", true);
                _danceManTransform.localPosition += (DanceManAim.localPosition - _danceManTransform.localPosition).normalized * MoveSpeed * Time.deltaTime;
            }
            else {
                OnManIdle();
            }
        }
        if (_isRotate) {
            if (_danceManTransform.localEulerAngles.y < 180.0f)
            {
                _danceManTransform.Rotate(new Vector3(0, RotateSpeed, 0));
            }
            else {
                _isRotate = false;
                PlayAudio(Voice02);
                StartCoroutine("Dance01");
            }
        }
	}

    IEnumerator MoveMan() {
        yield return new WaitForSecondsRealtime(WaitTime);
        _isWalk = true;
        yield return 0;
    }

    IEnumerator Dance01() {
        yield return new WaitForSecondsRealtime(16.0f);
        PlayBGM(Bgm02, _bmgVolume);
        _animator.SetBool("Dance01", true);
        yield return 0;
    }
    IEnumerator Dance02()
    {
        yield return new WaitForSecondsRealtime(15.0f);
        PlayBGM(Bgm03, _bmgVolume);
        _animator.SetBool("Dance02", true);
        yield return 0;
    }
    IEnumerator EndVoice()
    {
        Debug.Log("结束语");
        yield return new WaitForSecondsRealtime(33.0f);
        _bgmSource.volume = _bmgVolume;
        _audioSource.Stop();
        yield return 0;
    }

    public void OnManIdle()
    {
        _isWalk = false;
        _animator.SetBool("Walk", false);
        _isRotate = true;
    }

    public void PlayAudio(AudioClip clip) {
        _audioSource.volume = 1.0f;
        _audioSource.clip = clip;
        _audioSource.Play();
    }
    public void PlayBGM(AudioClip clip,float volume)
    {
        _bgmSource.volume = volume;
        _bgmSource.clip = clip;
        _bgmSource.Play();
    }

    private int _dance01Size = 0;//Dance01的播放次数，三遍停止
    private bool _endVoice = false;

    public void OnDanceOver(int index) {
        if (index == 1) {
            if (_dance01Size == 2)
            {
                _bgmSource.Stop();
                _animator.SetBool("Dance01", false);
                PlayAudio(Voice03);
                StartCoroutine("Dance02");
            }
            else {
                _dance01Size++;
            }
        }
        if (index == 2 && !_endVoice)
        {
            //_endVoice = true;
            //_bgmSource.volume = 0.05f;
            //PlayAudio(Voice04);
            //StartCoroutine("EndVoice");
        }
    }
}
