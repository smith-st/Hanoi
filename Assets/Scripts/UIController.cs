using UnityEngine;
using UnityEngine.UI;

public class UIController:MonoBehaviour {
    [SerializeField] private Text _txtCircleCount;
    [SerializeField] private Text _txtSteps;
    [SerializeField] private Text _txtStepsOnComplete;
    [SerializeField] private GameObject _setupCont;
    [SerializeField] private GameObject _progressCont;
    [SerializeField] private GameObject _completeCont;

    public void ShowSetup() {
        _setupCont.SetActive(true);
        _progressCont.SetActive(false);
        _completeCont.SetActive(false);
    }
    public void ShowProgress() {
        _setupCont.SetActive(false);
        _progressCont.SetActive(true);
        _completeCont.SetActive(false);
    }
    public void ShowComplete() {
        _setupCont.SetActive(false);
        _progressCont.SetActive(false);
        _completeCont.SetActive(true);
    }
    public void CountCircle(float count) {
        CountCircle((int)count);
    }
    public void CountCircle(int count) {
        _txtCircleCount.text = count.ToString();
    }
    public void CountSteps(int count) {
        _txtSteps.text = count.ToString();
    }
    public void TotalSteps(int count) {
        _txtStepsOnComplete.text = count.ToString();
    }
}
