using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject _gameCharacterPrefab;
    public GameManager gameManager;
    [Header("Game UI Buttons")]
    public GameObject _TabMenüPanel;
    public Text _RoomPlayerCountInfo;
    public GameObject _menüPanel;
    public GameObject _LogOutButton;
    public GameObject[] _loginPanelGameObjects;

    void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            float x = Random.Range(-4f, 4f);
            float z = Random.Range(-4f, 4f);
            PhotonNetwork.Instantiate(_gameCharacterPrefab.name, new Vector3(x, 0.1f, z), Quaternion.identity);
            _RoomPlayerCountInfo.text = "Oyuncu Sayısı: " + PhotonNetwork.CurrentRoom.PlayerCount + "\nMaksimum Oyuncu Sayısı: " + PhotonNetwork.CurrentRoom.MaxPlayers;
        }
    }
    #region Method
    private void Update()
    {
        KeyDownEscape();
        KeyTab();
    }

    public void LogOutButton()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("LobbyScene");
    }

    public void KeyDownEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_menüPanel.activeSelf == true)
            {
                _menüPanel.SetActive(false);
            }
            else
            {
                _menüPanel.SetActive(true);
            }
        }
    }

    public void KeyTab()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _TabMenüPanel.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            _TabMenüPanel.SetActive(false);
        }
    }
    #endregion

    #region Photon
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        _RoomPlayerCountInfo.text = "Oyuncu Sayısı: " + PhotonNetwork.CurrentRoom.PlayerCount + "\nMaksimum Oyuncu Sayısı: " + PhotonNetwork.CurrentRoom.MaxPlayers;
    }

    public override void OnPlayerLeftRoom (Player otherPlayer)
    {
        _RoomPlayerCountInfo.text = "Oyuncu Sayısı: " + PhotonNetwork.CurrentRoom.PlayerCount + "\nMaksimum Oyuncu Sayısı: " + PhotonNetwork.CurrentRoom.MaxPlayers;
    }
    #endregion
}

