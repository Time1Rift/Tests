using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private MiniMap _miniMap;

    private void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            Player player = PhotonNetwork.Instantiate(_playerPrefab.name, transform.position, Quaternion.identity).GetComponent<Player>();
            _miniMap.Initialize(player.transform);
        }
    }
}