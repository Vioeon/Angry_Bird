using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RayClick : MonoBehaviour
{
    public Image CursorGaugeImage;
    private Vector3 ScreenCenter;
    private float GaugeTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        //ScreenCenter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2); // 화면의 중심점
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 1000;

        CursorGaugeImage.fillAmount = GaugeTimer / 3.0f;

        //Ray ray = Camera.main.ScreenPointToRay(ScreenCenter);

        if (Physics.Raycast(this.transform.position, forward, out hit))
        {
            if (hit.collider.tag == "resultButton")
            {
                GaugeTimer += Time.deltaTime;
                if (GaugeTimer >= 3.0f)
                {
                    SceneManager.LoadScene("SampleScene");
                }
            }
            else
                GaugeTimer = 0.0f;
        }
        else
            GaugeTimer = 0.0f;

        
    }
}
