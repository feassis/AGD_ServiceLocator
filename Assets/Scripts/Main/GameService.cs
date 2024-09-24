using UnityEngine;
using ServiceLocator.Utilities;
using ServiceLocator.Events;
using ServiceLocator.Map;
using ServiceLocator.Wave;
using ServiceLocator.Sound;
using ServiceLocator.Player;
using ServiceLocator.UI;
using Microsoft.Win32.SafeHandles;

namespace ServiceLocator.Main
{
    public class GameService : GenericMonoSingleton<GameService>
    {
        // Services:
        public EventService EventService { get; private set; }
        public MapService MapService { get; private set; }
        public WaveService WaveService { get; private set; }
        public SoundService SoundService { get; private set; }
        public PlayerService PlayerService { get; private set; }

        [SerializeField] private UIService uiService;
        public UIService UIService => uiService;


        // Scriptable Objects:
        [SerializeField] private MapScriptableObject mapScriptableObject;
        [SerializeField] private WaveScriptableObject waveScriptableObject;
        [SerializeField] private SoundScriptableObject soundScriptableObject;
        [SerializeField] private PlayerScriptableObject playerScriptableObject;

        // Scene Referneces:
        [SerializeField] private AudioSource SFXSource;
        [SerializeField] private AudioSource BGSource;

        private void Start()
        {
            CreatServices();
            InjectDependencies();
        }

        private void Update()
        {
            PlayerService.Update();
        }

        private void CreatServices()
        {
            EventService = new EventService();
            PlayerService = new PlayerService(playerScriptableObject);;
            MapService = new MapService(mapScriptableObject);
            WaveService = new WaveService(waveScriptableObject);
            SoundService = new SoundService(soundScriptableObject, SFXSource, BGSource);
        }

        private void InjectDependencies()
        {
            PlayerService.Init(UIService, MapService, SoundService);
            WaveService.Init(EventService, UIService, MapService, SoundService, PlayerService);
            MapService.Init(EventService);
            UIService.Init(EventService, WaveService);
        }
    }
}