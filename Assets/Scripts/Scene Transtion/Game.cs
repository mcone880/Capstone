using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Game : Singleton<Game>
{
	[SerializeField] SceneLoader sceneLoader;
	public GameData gameData;

	private void Start()
	{
		InitScene();SceneManager.activeSceneChanged += OnSceneWasLoaded;
	}

	void InitScene()
    {
		SceneDescriptor sceneDescriptor = FindObjectOfType<SceneDescriptor>();
		//if(sceneDescriptor != null) Instantiate(sceneDescriptor.player, sceneDescriptor.playerSpawn.position, sceneDescriptor.playerSpawn.rotation);
    }

	private void Update()
	{
	}

	public void OnLoadScene(string sceneName)
    {
		sceneLoader.Load(sceneName);
    }

	public void OnPlayerDead()
    {
		
    }

	void OnSceneWasLoaded(Scene current, Scene next)
    {
		InitScene();
    }
}
