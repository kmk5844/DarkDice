using UnityEngine;
using System.Collections;

public class NetworkChecker : MonoBehaviour
{
    private const float CheckInterval = 10f; // ��Ʈ��ũ üũ ���� (��)
    public GameObject CheckNetwork;

    private void Start()
    {
        StartCoroutine(NetworkCheckCoroutine());
    }

    private IEnumerator NetworkCheckCoroutine()
    {
        while (true)
        {
            // ��Ʈ��ũ üũ ������ ���⿡ �����մϴ�.
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
