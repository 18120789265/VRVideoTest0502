using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float MoveSpeed = 0.5f;
    public float WaitTime = 5.0f;
    public Transform PlayerAim;

    private GameObject _player;
    private Transform _playerTransform;

    private bool _isWalk = false;
    void Awake() {
        _player = GameObject.FindGameObjectWithTag("Player") as GameObject;
        _playerTransform = _player.transform;
    }

	void Start () {
        StartCoroutine("PlayerMove");
	}
	
	void Update () {
        if (_isWalk)
        {
            if (Vector3.Distance(_playerTransform.localPosition, PlayerAim.localPosition) > 0.05f)
            {
                _playerTransform.localPosition += (PlayerAim.localPosition - _playerTransform.localPosition).normalized * MoveSpeed * Time.deltaTime;
            }
            else
            {
                _isWalk = false;
            }
        }
	}

    IEnumerator PlayerMove() {
        yield return new WaitForSecondsRealtime(WaitTime);
        _isWalk = true;
        yield return 0;
    }
}
