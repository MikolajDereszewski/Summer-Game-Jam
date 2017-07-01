using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public bool Started { get { return _started; } }
    public float GameSpeed { get { return _gameSpeed; } }

    [SerializeField]
    private MeteorScript _meteorPrefab = null;
    [SerializeField]
    private AnimationCurve _gameSpeedCurve = new AnimationCurve();

    private bool _started;
    private bool _dead;

    private float _gameSpeed;
    private float _time;

    private void Awake()
    {
        _gameSpeed = 1;
        _time = 0;
        _started = false;
        _dead = false;
        StartCoroutine(CheckForTwoPlayers());
    }

    private void Update()
    {
        _time += Time.deltaTime;
        _gameSpeed = _gameSpeedCurve.Evaluate(_time);
    }

    private IEnumerator CheckForTwoPlayers()
    {
        while(!(PhotonNetwork.playerList.Length == 2))
        {
            _started = false;
            Debug.Log(PhotonNetwork.playerList.Length);
            yield return null;
        }
        Debug.Log(PhotonNetwork.playerList.Length);
        _started = true;
        StartCoroutine(CreateMeteors());
    }

    private IEnumerator CreateMeteors()
    {
        WaitForSeconds delay = new WaitForSeconds(1f);
        while(!_dead)
        {
            yield return delay;
            InstantiateMeteor();
            delay = new WaitForSeconds(Random.Range(0.6f, 1.2f) / _gameSpeed);
        }
    }

    private void InstantiateMeteor()
    {
        var meteor = Instantiate(_meteorPrefab, RandomSpawnPosition(), Quaternion.identity);
        meteor.Init(this);
    }

    private Vector3 RandomSpawnPosition()
    {
        return new Vector3(Random.Range(-30, 30), Random.Range(-30, 30), 200);
    }
}
