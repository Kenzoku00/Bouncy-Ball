using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Teleport : MonoBehaviour
{
    public int tpId;
    private GameObject ball;
    private float dissolveValue = 0f ;
    private Renderer renderer;

    private void Start()
    {
        ball = GameObject.FindGameObjectWithTag("unit");
        Debug.Log(ball.name);
        renderer = ball.GetComponent<Renderer>();
        ////Teleport(() =>
        //{
            
        //});
    }

    public void Teleports(Action callback)
    {
        StartCoroutine(TeleportCoroutine(callback));
    }

    private IEnumerator TeleportCoroutine(Action callback)
    {
        GameObject[] tp = GameObject.FindGameObjectsWithTag("tp");

        

        foreach (GameObject tp2 in tp)
        {
            Teleport teleportComponent = tp2.GetComponent<Teleport>();
            if (teleportComponent == null)
            {
                Debug.LogError("No Teleport component found on " + tp2.name);
                continue;
            }

            if (tp2 != this.gameObject && tpId == teleportComponent.tpId)
            {
                yield return DissolveController(0, 1, 0.5f);
                ball.transform.position = new Vector3(tp2.transform.position.x, tp2.transform.position.y + 1, tp2.transform.position.z);
                Debug.Log("Unit has been teleported.");
                yield return DissolveController(1, 0, 0.5f);
                yield return null;
                callback?.Invoke();
            }
        }
    }


    private IEnumerator DissolveController(int startValue, int endValue, float duration){
        float elapsedTime= 0f;

        while (elapsedTime < duration){

        elapsedTime += Time.deltaTime;
        dissolveValue = Mathf.Lerp(startValue, endValue, elapsedTime / duration);

        if (renderer != null)
        {
            renderer.material.SetFloat("_disolve", dissolveValue);
        }
        Debug.Log (dissolveValue);
        yield return null; }
    }

}
