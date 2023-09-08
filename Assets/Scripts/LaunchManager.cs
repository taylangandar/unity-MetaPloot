using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;


public class LaunchManager : MonoBehaviourPunCallbacks
{
    [Header("InputField")]
    public InputField _nameInputField;
    public InputField _odaIsmıInputField;
    public InputField _maxOyuncuInputField;
    [Header("LoginPanel")]
    public GameObject _loginPanel;
    public GameObject[] _loginPanelGameObjects;
    [Header("GameSettingsPanel")]
    public GameObject _GameSettingsPanel;
    public GameObject[] _gameSettingsPanelGameObjects;
    [Header("CreateJoinRoomPanel")]
    public GameObject _createJoinRoomPanel;
    public GameObject[] _createJoinRoomPanelGameObjects;
    [Header("RandomRoomPanel")]
    public GameObject _joinRandomRoomPanel;
    public GameObject[] _joinRandomRoomPanelGameObjects;
    public Text randomRoom;

    void Start()
    {
        ActiveLoginPanel();
        PhotonNetwork.AutomaticallySyncScene = true;
    }




    #region PhotonCallBack
    public override void OnConnectedToMaster()
    {
        Debug.Log("Photon'a Bağlandı");
        Debug.Log(PhotonNetwork.NickName+" İsimli Oyuncu Photon'a Bağlandı");
        ActiveGameSettingsPanel();
    }

    public override void OnConnected()
    {
        Debug.Log("İnternete Bağlandı");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.NickName + " Adlı Kişi Şu Odaya Bağlandı " + PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.LoadLevel("Scenes/GameScene/Scene/GameScene");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Oda Bulunamadı.");
        Debug.Log("Yeni Oda Kuruluyor...");
        CreateAndJoinRoom();
    }

    #endregion

    #region Unity Methods
    public void ActiveLoginPanel()
    {
        _loginPanel.SetActive(true);
        foreach (GameObject gameObject in _loginPanelGameObjects)
        {
            gameObject.SetActive(true);
        }
    }

    public void ActiveJoinRandomRoomPanel()
    {
        _GameSettingsPanel.SetActive(false);
        foreach(GameObject gameObject in _gameSettingsPanelGameObjects)
        {
            gameObject.SetActive(false);
        }
        _joinRandomRoomPanel.SetActive(false);
        foreach (GameObject gameObject in _joinRandomRoomPanelGameObjects)
        {
            gameObject.SetActive(true);
        }

        PhotonNetwork.JoinRandomRoom();
        randomRoom.text = "Bir Oda Aranıyor";
    }

    public void ActiveGameSettingsPanel()
    {
        _loginPanel.SetActive(false);
        foreach (GameObject gameObject in _loginPanelGameObjects)
        {
            gameObject.SetActive(false);
        }
        _GameSettingsPanel.SetActive(true);
        foreach (GameObject gameObject in _gameSettingsPanelGameObjects)
        {
            gameObject.SetActive(true);
        }
    }

    public void ActiveCreateJoinRoomPanel()
    {
        _GameSettingsPanel.SetActive(false);
        foreach(GameObject gameObject in _gameSettingsPanelGameObjects)
        {
            gameObject.SetActive(false);
        }

        _createJoinRoomPanel.SetActive(true);
        foreach (GameObject gameObject in _createJoinRoomPanelGameObjects)
        {
            gameObject.SetActive(true);
        }
    }

    public void OnGiris()
    {
        Debug.Log("Buton Çalıştı");
        PhotonNetwork.NickName = _nameInputField.text;
        PhotonNetwork.ConnectUsingSettings();
    }

    public void OdaOlusturButtonClicked()
    {
        string roomName = _odaIsmıInputField.text;
        RoomOptions _roomOptions = new RoomOptions();
        _roomOptions.MaxPlayers = (byte)int.Parse(_maxOyuncuInputField.text);
        PhotonNetwork.CreateRoom(roomName, _roomOptions);
    }

    public void CreateAndJoinRoom()
    {
        string randomIsim = "Room " + Random.Range(10, 100);
        RoomOptions _roomOptions1 = new RoomOptions();
        _roomOptions1.IsOpen = true;
        _roomOptions1.IsVisible = true;
        PhotonNetwork.CreateRoom(randomIsim, _roomOptions1);
    }
    #endregion

}
