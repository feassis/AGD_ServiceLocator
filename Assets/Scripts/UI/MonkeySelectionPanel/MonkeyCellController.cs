using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ServiceLocator.Player;
using ServiceLocator.Main;

namespace ServiceLocator.UI
{
    public class MonkeyCellController
    {
        private MonkeyCellView monkeyCellView;
        private MonkeyCellScriptableObject monkeyCellSO;
        private PlayerService playerService;
        private UIService uiService;

        public bool IsUnloked;

        public MonkeyCellController(Transform cellContainer, MonkeyCellView monkeyCellPrefab,
            MonkeyCellScriptableObject monkeyCellScriptableObject, UIService uiService, PlayerService playerService)
        {
            this.monkeyCellSO = monkeyCellScriptableObject;
            monkeyCellView = Object.Instantiate(monkeyCellPrefab, cellContainer);
            monkeyCellView.SetController(this);
            this.uiService = uiService;

            this.playerService = playerService;
            SetupMonkeyImage(monkeyCellScriptableObject, uiService);
        }

        private void SetupMonkeyImage(MonkeyCellScriptableObject monkeyCellScriptableObject, UIService uiService)
        {
            IsUnloked = this.playerService.IsMonkeyUnlocked(monkeyCellScriptableObject.Type);

            monkeyCellView.ConfigureCellUI(monkeyCellSO.Sprite, monkeyCellSO.Name, monkeyCellSO.Cost, uiService, IsUnloked, monkeyCellSO.UnlockCost);
        }

        public void TryToUnlock()
        {
            playerService.TryToUnlockMonkey(monkeyCellSO.Type);
            SetupMonkeyImage(monkeyCellSO, uiService);
        }

        public void MonkeyDraggedAt(Vector3 dragPosition)
        {
            playerService.ValidateSpawnPosition(monkeyCellSO.Cost, dragPosition);
        }

        public void MonkeyDroppedAt(Vector3 dropPosition)
        {
            playerService.TrySpawningMonkey(monkeyCellSO.Type, monkeyCellSO.Cost, dropPosition);
        }
    }
}