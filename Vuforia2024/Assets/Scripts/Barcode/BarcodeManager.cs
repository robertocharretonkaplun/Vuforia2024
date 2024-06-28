using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Vuforia;

public class BarcodeManager : MonoBehaviour
{
  public static BarcodeManager Instance;
  public GameObject GoToWebSiteBTN;
  public GameObject HistoryTitleElementPrefab;
  public GameObject HistoryConteiner;
  public GameObject HistoryWindow;
  public List<string> History;
  public List<GameObject> HistoryTitleElements;
  public bool wasDetected = false;
  public int lastWebSiteLinkIndex = 0;

  private void Awake()
  {
    if (Instance != null)
    {
      return;
    }
    else
    {
      Instance = this;
    }
  }

  public void AccessToQRWebSite()
  {
    if (History.Count > 0 && lastWebSiteLinkIndex >= 0 && lastWebSiteLinkIndex < History.Count)
    {
      Application.OpenURL(History[lastWebSiteLinkIndex]);
    }
    else
    {
      Debug.LogError("Índice fuera de rango o la lista de History está vacía.");
    }
  }

  public void AccessToQRWebSite(string _url)
  {
    Application.OpenURL(_url);
  }

  public string GetLastItemFromHistory()
  {
    return History[lastWebSiteLinkIndex];
  }

  public void GoToWebSite_Btn_Status()
  {
    if (!wasDetected)
    {
      GoToWebSiteBTN.transform.GetChild(0).GetComponent<Button>().interactable = false;
      GoToWebSiteBTN.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().color = Color.gray;
      GoToWebSiteBTN.transform.GetChild(1).GetComponent<Text>().text = "ANALIZANDO...";
    }
    else
    {
      GoToWebSiteBTN.transform.GetChild(0).GetComponent<Button>().interactable = true;
      GoToWebSiteBTN.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color32(245, 56, 85, 255);
      GoToWebSiteBTN.transform.GetChild(1).GetComponent<Text>().text = "ACCEDER";
    }
  }

  public void StoreHistoryItem(string _item)
  {
    if (!wasDetected)
    {
      // Agregamos al historial el elemento a guardar
      History.Add(_item);
      // Creamos un gameobject para el HistorytitleElement
      GameObject item = Instantiate(HistoryTitleElementPrefab);
      // Agregar al historial el texto del elemento
      item.transform.GetChild(0).GetComponent<TMP_Text>().text = _item; 
      // Almacenamos el GO en su lista
      HistoryTitleElements.Add(item);
      // Hacemos hijo de HistoryContenier al nuevo item
      item.transform.parent = HistoryConteiner.transform;
      item.transform.localPosition = new Vector3(0, 0, 0);
      // Activamos que el item fue detectado
      wasDetected = true;
      // Revisar si nuestro historial es mayor a 0
      if (History.Count == 1)
      {
        lastWebSiteLinkIndex = 0;
      }
      else
      {
        lastWebSiteLinkIndex++;
      }
    }
  }

  public void ActiveHistoryWindow()
  {
    HistoryWindow.SetActive(true);
  }
  public void DeactiveHistoryWindow()
  {
    HistoryWindow.SetActive(false);
  }
}
