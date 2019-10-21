using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace RainyWoods.UI
{
    public class MainMenuController : MonoBehaviour
    {
        public GameObject creditsScreen;
        public Material[] materials;

        private void Awake()
        {
            Time.timeScale = 1.0f; // Just to be save.
        }

        private void Start()
        {
            foreach(Material mat in materials)
            {
                mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                mat.SetInt("_ZWrite", 1);
                mat.DisableKeyword("_ALPHATEST_ON");
                mat.DisableKeyword("_ALPHABLEND_ON");
                mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                mat.renderQueue = -1;
            }

            Time.timeScale = 1.0f;
        }

        public void NewGame() => SceneManager.LoadScene("Tutorial Level", LoadSceneMode.Single);
        public void ExitGame() => Application.Quit(0);
        public void ToggleCredits() => creditsScreen?.SetActive(!creditsScreen.activeInHierarchy);
    }
}
