using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace MyCell.GUI
{
    public enum Panels
    {
        MainMenu,
        Play,
        // Options,
        // Archive,
        // Exit
    }

    [System.Serializable]
    public class MenuPanels
    {
        public GameObject MainMenu;
        public GameObject Play;
        // public GameObject Options;
        // public GameObject Archive;
    }

    [System.Serializable]
    public class MenuGUI
    {
        public Text GameScore;
        public Text VehicleName;
        public Text VehiclePrice;

        public Slider VehicleSpeed;
        public Slider VehicleBraking;
        public Slider VehicleNitro;

        public Slider sensitivity;

        public Toggle audio;
        public Toggle music;
        public Toggle vibrateToggle;
        public Toggle ButtonMode, AccelMode;

        public Image wheelColor, smokeColor;
        public Image loadingBar;

        public GameObject loading;
        public GameObject customizeVehicle;
        public GameObject buyNewVehicle;
    }

    public class MainMenu : MonoBehaviour
    {
        private Panels _activePanel = Panels.MainMenu;
        private bool _startingGame = false;
        private AsyncOperation _sceneLoadingOperation = null;
        private int _currentLevelNumber = 0;
        private float _menuLoadTime = 0.0f;

        public string SceneID;
        public MenuPanels MenuPanels;
        public Animator FadeBackGround;
        public MenuGUI menuGUI;

        // GamePanels/////////////////////////////////////////////////////////////////////////////////////////////////////
        // public void CurrentPanel(int current)
        // {

        //     _activePanel = (Panels)current;

        //     switch (_activePanel)
        //     {

        //         case Panels.MainMenu:
        //             MenuPanels.MainMenu.SetActive(true);
        //             MenuPanels.SelectVehicle.SetActive(false);
        //             MenuPanels.SelectLevel.SetActive(false);
        //             if (menuGUI.wheelColor) menuGUI.wheelColor.gameObject.SetActive(true);

        //             break;
        //         case Panels.SelectVehicle:
        //             MenuPanels.MainMenu.gameObject.SetActive(false);
        //             MenuPanels.SelectVehicle.SetActive(true);
        //             MenuPanels.SelectLevel.SetActive(false);
        //             break;
        //         case Panels.SelectLevel:
        //             MenuPanels.MainMenu.SetActive(false);
        //             MenuPanels.SelectVehicle.SetActive(false);
        //             MenuPanels.SelectLevel.SetActive(true);
        //             break;
        //         case Panels.Settings:
        //             MenuPanels.MainMenu.SetActive(false);
        //             MenuPanels.SelectVehicle.SetActive(false);
        //             MenuPanels.SelectLevel.SetActive(false);
        //             break;
        //     }
        // }

        private void Awake()
        {
            AudioListener.pause = false;
            Time.timeScale = 1.0f;
        }

        private void Update()
        {
            if (_sceneLoadingOperation != null)
            {
                menuGUI.loadingBar.fillAmount = Mathf.MoveTowards(menuGUI.loadingBar.fillAmount, _sceneLoadingOperation.progress + 0.2f, Time.deltaTime * 0.5f);

                if (menuGUI.loadingBar.fillAmount > _sceneLoadingOperation.progress)
                    _sceneLoadingOperation.allowSceneActivation = true;
            }
        }

        public void StartGame()
        {
            if (_startingGame) return;
            FadeBackGround.SetBool("FadeOut", true);
            StartCoroutine(LoadLevelGame(1.5f));
            _startingGame = true;
        }

        IEnumerator LoadLevelGame(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            menuGUI.loading.SetActive(true);
            StartCoroutine(LoadLevelAsync());
        }

        IEnumerator LoadLevelAsync()
        {
            yield return new WaitForSeconds(0.4f);

            _sceneLoadingOperation = SceneManager.LoadSceneAsync(SceneID);
            _sceneLoadingOperation.allowSceneActivation = false;

            while (!_sceneLoadingOperation.isDone || _sceneLoadingOperation.progress < 0.9f)
            {
                _menuLoadTime += Time.deltaTime;

                yield return 0;
            }
        }
    }
}
