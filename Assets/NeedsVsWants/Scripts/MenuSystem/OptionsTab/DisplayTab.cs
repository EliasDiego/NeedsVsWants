using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

namespace NeedsVsWants.MenuSystem
{
    public class DisplayTab : OptionsTab
    {
        [SerializeField]
        TMP_Dropdown _ResolutionDropdown;
        [SerializeField]
        TMP_Dropdown _DisplayModeDropdown;

        Resolution _Resolutions;

        protected override void Start()
        {
            base.Start();

            List<TMP_Dropdown.OptionData> optionDataList = new List<TMP_Dropdown.OptionData>();

            // Resolution
            foreach(Resolution resolution in Screen.resolutions)
                optionDataList.Add(new TMP_Dropdown.OptionData(resolution.width.ToString() + " x " + resolution.height.ToString()));
            
            _ResolutionDropdown.options = optionDataList;

            // Display Mode
            optionDataList = new List<TMP_Dropdown.OptionData>();
            
            foreach(string name in System.Enum.GetNames(typeof(FullScreenMode)))
                optionDataList.Add(new TMP_Dropdown.OptionData(name));

            _DisplayModeDropdown.options = optionDataList;
        }
    }
}