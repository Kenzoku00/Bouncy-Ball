using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SwitchButton : MonoBehaviour
{
    public Button refreshButton;       
    public float rotationSpeed = 2f;   
    public float buttonDelay = 0.5f; 

    public void StartAnimation()
    {
        refreshButton.interactable = false;
        
        transform.DORotate(new Vector3(0, 0, -360), rotationSpeed, RotateMode.FastBeyond360)
                 .SetEase(Ease.InOutCubic)
                 .OnComplete(() => 
                 {
                     
                     DOVirtual.DelayedCall(buttonDelay, () => 
                     {
                         refreshButton.interactable = true;
                     });
                 });
    }

    public void StopAnimation()
    {
        transform.DOKill();
    }
}
