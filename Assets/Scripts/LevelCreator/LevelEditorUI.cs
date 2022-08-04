using TanksEngine.LevelCreator;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TanksEngine.UI
{
    public class LevelEditorUI : MonoBehaviour
    {
        public Text DebugText;
        public InputField mapName;
        public InputField mapX;
        public InputField mapY;
        public LevelEditor levelEditor;
        public Transform Pallete;
        public GameObject PalleteButtonPrefab;
        public Dropdown dropdownLvlTypes;
        public GameObject SelectedMark;
        private string txt;
        public PlayerInput Input { get; private set; }
        private void Awake()
        {
            Input = new PlayerInput();
            Input.Enable();
            SetLevelType();
        }
        private void UpdatePallete()
        {
            foreach (Transform child in Pallete)
            {
                GameObject.Destroy(child.gameObject);
            }
            int n = 0;
            int id = levelEditor.currentLvlId;
            foreach (var item in levelEditor.levelDesignVendor.Levels[id].LevelDesignData.Blocks)
            {
                var obj = GameObject.Instantiate(PalleteButtonPrefab, Pallete);
                obj.transform.GetChild(1).GetComponent<Text>().text = item.name;
                int temp = n;
                obj.GetComponent<Button>().onClick.AddListener(() => SelectBlockFromPallete(temp));
                n++;
            }
        }
        private void UpdateDropdownLvlTypes()
        {
            dropdownLvlTypes.options.Clear();
            foreach (string s in Enum.GetNames(typeof(LevelDesignTypes)))
            {
                Dropdown.OptionData element=new();
                element.text = s;
                dropdownLvlTypes.options.Add(element);
            }
        }
        public void UpdateEditorUI()
        {
            UpdatePallete();
            UpdateDropdownLvlTypes();
        }
        public void SetLevelType()
        {
            LevelDesignTypes val = (LevelDesignTypes)Enum.ToObject(typeof(LevelDesignTypes), dropdownLvlTypes.value);
            levelEditor.SetLevelType(val);
        }
        public void Update()
        {
            txt = "";
            txt += $"\nMap size X = {levelEditor.MapSizeX}";
            txt += $"\nMap size Y = {levelEditor.MapSizeY}";
            txt += $"\nSelected pallete block = {levelEditor.selectedBlockType}";
            txt += $"\nMouse pos = {Input.Player.MousePosition.ReadValue<Vector2>()}";
            txt += $"\nLvl type = {levelEditor.currentLvlId}";
            DebugText.text = txt;


            CheckClick();
            CheckHover();
        }
      
        private void CheckClick()
        {
            if (Input.Player.MouseClick.triggered)
            {
                Debug.Log("triggerd");
                Ray ray = Camera.main.ScreenPointToRay(Input.Player.MousePosition.ReadValue<Vector2>());
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("hit"+ hit.transform.name);
                    if (hit.transform.gameObject.GetComponent<LevelBlock>())
                    {
                        var comp = hit.transform.gameObject.GetComponent<LevelBlock>();
                        //Debug.Log($"{comp.x} : {comp.y}");
                        SelectBlockFromLevel(comp.x, comp.y);
                    }
                }
            }
        }
        bool selected = false;
        private void CheckHover()
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.Player.MousePosition.ReadValue<Vector2>());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.GetComponent<LevelBlock>())
                {
                    //Debug.Log("hover" + hit.transform.name);
                    SelectedMark.transform.position = hit.transform.position;
                    SelectedMark.SetActive(true);
                    selected = true;
                }
            }
            else
            {
                if (selected)
                {
                    selected = false;
                    SelectedMark.SetActive(false);
                }
            }
        }
        public void UpdateMapSize()
        {
            levelEditor.UpdateLevel(int.Parse(mapX.text), int.Parse(mapY.text));
        }
        public void SelectBlockFromPallete(int blockType)
        {
            Debug.Log("select " + blockType);
            levelEditor.SetBlockType(blockType);
        }
        public void SelectBlockFromLevel(int x, int y)
        {
            levelEditor.SetBlock(x, y);
        }
        public void OnClickSave()
        {
            levelEditor.SaveLevel(mapName.text);
        }
        public void OnClickLoad()
        {
            //levelEditor.LoadLevel(mapName.text);
            TextAsset txtLevelData = Resources.Load<TextAsset>($"Levels2/{mapName.text}");
            levelEditor.LoadLevelFromStorage(txtLevelData);
        }
    }

}
