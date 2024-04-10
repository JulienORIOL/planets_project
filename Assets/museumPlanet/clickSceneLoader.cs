using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class clickSceneLoader : MonoBehaviour
{
	public void LoadLevel()
	{
		SceneManager.LoadScene("raceScene");
	}
}

