using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    private GameObject[] characterList;
    public static  int index; // index of current model

    private void Start()
    {
        //index = PlayerPrefs.GetInt("CharacterSelected");
        characterList = new GameObject[transform.childCount];

        // Fill array with characters
        for (int i = 0; i < transform.childCount; i++)
            characterList[i] = transform.GetChild(i).gameObject;

        // Disable characters
        foreach(GameObject go in characterList)
            go.SetActive(false);

        // Enable the first character
        if (characterList[index])
            characterList[index].SetActive(true);
    }

    public void ChangeCharacter(bool isLeft)
    {
        // Disable current model
        characterList[index].SetActive(false);

        // Move left
        if (isLeft)
        {
            index--;
            if (index < 0)
                index = characterList.Length - 1;   // end of the list
        }

        // Move right
        if(!isLeft)
        {
            index++;
            if (index == characterList.Length)
                index = 0;
        }

        // Enable the new model
        characterList[index].SetActive(true);
    }

    
    public void LoadLevel()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
