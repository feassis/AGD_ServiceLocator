using UnityEngine;
using UnityEngine.UI;
using ServiceLocator.Main;
using ServiceLocator.Events;

namespace ServiceLocator.UI
{
    public class MapButton : MonoBehaviour
    {
        [SerializeField] private int MapId;

        [SerializeField] private EventService eventService;
        [SerializeField] private Color ActiveColor;
        [SerializeField] private Color DeactiveColor;
        [SerializeField] private Image buttonBG;

        private void Start() => GetComponent<Button>().onClick.AddListener(OnMapButtonClicked);

        public void Init(EventService eventService)
        {
            this.eventService = eventService;
            buttonBG.color = IsUnlocked() ? ActiveColor : DeactiveColor;

        }

        // To Learn more about Events and Observer Pattern, check out the course list here: https://outscal.com/courses
        private void OnMapButtonClicked()
        {
            if(!IsUnlocked())
            {
                return;
            }

            eventService.OnMapSelected.InvokeEvent(MapId);
        }

        private bool HasWonPreviousMap() => PlayerPrefs.GetInt($"Map{MapId - 1}Won") == 1;

        private bool IsUnlocked() => HasWonPreviousMap() || MapId == 1;
    }
}