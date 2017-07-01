using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public bool Started { get { return _started; } }
    public float GameSpeed { get { return _gameSpeed; } }

    [SerializeField]
    private MeteorScript _meteorPrefab = null;
    [SerializeField]
    private UfoScript _ufoPrefab = null;
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
        StartCoroutine(CreateObstacles());
    }

    private IEnumerator CreateObstacles()
    {
        Vector2 delayClamp = (PlayerPrefs.GetInt("LOCAL_PLAYER_TYPE") == 0) ? new Vector2(0.6f, 1.2f) : new Vector2(2f, 4f);
        WaitForSeconds delay = new WaitForSeconds(1f);
        while(!_dead)
        {
            yield return delay;
            if (PlayerPrefs.GetInt("LOCAL_PLAYER_TYPE") == 0)
                InstantiateMeteor();
            else
                InstantiateUfo();
            delay = new WaitForSeconds(Random.Range(delayClamp.x, delayClamp.y) / _gameSpeed);
        }
    }

    private void InstantiateMeteor()
    {
        var meteor = Instantiate(_meteorPrefab, RandomSpawnPosition(400), Quaternion.identity);
        meteor.Init(this);
    }

    private void InstantiateUfo()
    {
        var ufo = Instantiate(_ufoPrefab, RandomSpawnPosition(-300), _ufoPrefab.transform.rotation);
        ufo.Init(this);
    }

    private Vector3 RandomSpawnPosition(float z)
    {
        return new Vector3(Random.Range(-30, 30), Random.Range(-30, 30), z);
    }
}
