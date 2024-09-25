using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Transactions;

namespace ServiceLocator.UI
{
    public class MonkeyCellView : MonoBehaviour
    {
        private MonkeyCellController controller;

        [SerializeField] private MonkeyImageHandler monkeyImageHandler;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI costText;
        [SerializeField] private string lockedTest = "Locked";

        public void SetController(MonkeyCellController controllerToSet) => controller = controllerToSet;

        public void ConfigureCellUI(Sprite spriteToSet, string nameToSet, int costToSet, UIService uiSevice, bool isUnlocked, int unlockCost)
        {
            monkeyImageHandler.ConfigureImageHandler(spriteToSet, controller, uiSevice);

            if(isUnlocked)
            {
                nameText.SetText(nameToSet);
                costText.SetText(costToSet.ToString());
            }
            else
            {
                nameText.SetText(lockedTest);
                costText.SetText(unlockCost.ToString());
            }
        }

    }
}