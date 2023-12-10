using UnityEngine;
using System.Collections;

public class NetworkChecker : MonoBehaviour
{
    private const float CheckInterval = 10f; // 네트워크 체크 간격 (초)
    public GameObject CheckNetwork;

    private void Start()
    {
        StartCoroutine(NetworkCheckCoroutine());
    }

    private IEnumerator NetworkCheckCoroutine()
    {
        while (true)
        {
            // 네트워크 체크 로직을 여기에 구현합니다.
            bool isNetworkAvailable = (Application.internetReachability != NetworkReachability.NotReachable);

            if (isNetworkAvailable)
            {
                CheckNetwork.SetActive(false);
            }
            else
            {
                CheckNetwork.SetActive(true);
            }

            yield return new WaitForSeconds(CheckInterval);
        }
    }

    public void NetworkExitButton()
    {
        Application.Quit();
    }
}
