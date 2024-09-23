using ServiceLocator.Player;
using ServiceLocator.Sound;
using ServiceLocator.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameService : GenericMonoSingleton<GameService>
{
    public PlayerService PlayerService { get; private set; }
    [SerializeField] private PlayerScriptableObject playerScriptableObject;

    public SoundService SoundService { get; private set; }
    [SerializeField] private SoundScriptableObject soundScriptableObject;
    [SerializeField] private AudioSource audioEffects;
    [SerializeField] private AudioSource backgroundMusic;

    protected void Start()
    {
        PlayerService = new PlayerService(playerScriptableObject);

        SoundService = new SoundService(soundScriptableObject, audioEffects, backgroundMusic);
    }

    private void Update()
    {
        PlayerService.Update();
    }
}
