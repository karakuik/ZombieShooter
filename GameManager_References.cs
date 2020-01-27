using UnityEngine;

public class GameManager_References : MonoBehaviour
{
    public string playerTag;
    public static string _playerTag;

    public string enemyTag;
    public static string _enemyTag;

    public static GameObject _player;

    private void OnEnable()
    {
        if(playerTag=="")
        {
            Debug.LogWarning("Please type in the name of the player tag in the GameManagerReferences " +
                "slot in the inspector");
        }
        if (enemyTag=="")
        {
            Debug.LogWarning("Please type in the name of the enemy tag in the GameManagerReferences " +
               "slot in the inspector");
        }

        _playerTag = playerTag;
        _enemyTag = enemyTag;

        _player = GameObject.FindGameObjectWithTag(_playerTag);
    }
}


