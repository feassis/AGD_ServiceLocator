using ServiceLocator.Events;
using ServiceLocator.Map;
using ServiceLocator.Player;
using ServiceLocator.Sound;
using ServiceLocator.UI;
using ServiceLocator.Utilities;
using ServiceLocator.Wave;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameService : GenericMonoSingleton<GameService>
{
    [Header("Player Service")]
    [SerializeField] private PlayerScriptableObject playerScriptableObject;
    public PlayerService PlayerService { get; private set; }

    [Header("Sound Service")]
    [SerializeField] private SoundScriptableObject soundScriptableObject;
    [SerializeField] private AudioSource audioEffects;
    [SerializeField] private AudioSource backgroundMusic;
    public SoundService SoundService { get; private set; }

    [Header("UI Service")]
    [SerializeField] private UIService uIService;
    public UIService UIService => uIService;

    [Header("Map Service")]
    [SerializeField] private MapScriptableObject mapScriptableObject;
    public MapService MapService { get; private set; }

    public EventService EventService { get; private set; }

    [Header("Wave Service")]
    [SerializeField] private WaveScriptableObject waveScriptableObject;
    public WaveService WaveService { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        EventService = new EventService();
    }

    protected void Start()
    {
        PlayerService = new PlayerService(playerScriptableObject);

        SoundService = new SoundService(soundScriptableObject, audioEffects, backgroundMusic);

        MapService = new MapService(mapScriptableObject);

        WaveService = new WaveService(waveScriptableObject);
    }

    private void Update()
    {
        PlayerService.Update();
    }
}
