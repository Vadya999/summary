using Kuhpik;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityTools.Extentions;

public class WinScreen : UIScreen
{
    [SerializeField] private Button _lobbyButton;
    [SerializeField] private Button _retryButton;

    [SerializeField] private GameObject _root;

    [SerializeField] private List<StarComponent> _result;

    public UnityEvent RetryButtonClicked => _retryButton.onClick;
    public UnityEvent LobbyButtonClicked => _lobbyButton.onClick;

    public void ShowResult(int collectedStars)
    {
        _root.gameObject.SetActive(true);
        _result.ForEach(x => x.SetState(false));
        _result.Take(collectedStars).ForEach(x => x.SetState(true));
    }
}
