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
    public class GameService : MonoBehaviour
    {
        // Services:
        private EventService eventService;
        private MapService mapService;
        private WaveService waveService;
        private SoundService soundService;
        private PlayerService playerService;
        
        [SerializeField] private CorotineService corotineService;

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
            playerService.Update();
        }

        private void CreatServices()
        {
            eventService = new EventService();
            playerService = new PlayerService(playerScriptableObject);;
            mapService = new MapService(mapScriptableObject);
            waveService = new WaveService(waveScriptableObject);
            soundService = new SoundService(soundScriptableObject, SFXSource, BGSource);
        }

        private void InjectDependencies()
        {
            playerService.Init(UIService, mapService, soundService);
            waveService.Init(eventService, UIService, mapService, soundService, playerService, corotineService);
            mapService.Init(eventService);
            UIService.Init(eventService, waveService, playerService);
        }
    }
}