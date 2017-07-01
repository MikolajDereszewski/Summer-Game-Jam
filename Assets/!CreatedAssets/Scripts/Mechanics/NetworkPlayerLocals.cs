using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayerLocals : MonoBehaviour {

    [SerializeField]
    private PhotonView _localView;

	private void Start()
    {
        gameObject.SetActive(_localView.isMine);
	}
}
