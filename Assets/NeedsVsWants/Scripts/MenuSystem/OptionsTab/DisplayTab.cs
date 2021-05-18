using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

        IEnumerable<Resolution> _Resolutions;

        void Awake()
        {
            List<TMP_Dropdown.OptionData> optionDataList = new List<TMP_Dropdown.OptionData>();

            // Resolution
            _Resolutions = Screen.resolutions.Where(resolution => resolution.width >= 1280 && resolution.height >= 720).
                GroupBy(resolution => new Vector2(resolution.width, resolution.height)).Select(resolution => resolution.First());
            
            foreach(Resolution resolution in _Resolutions)
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