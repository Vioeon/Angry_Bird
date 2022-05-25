using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    public Image cursorGaugeImage;
    public GameObject mainCam;
    public GameObject player;
    private float gaugeTimer = 0.0f;
    private float maxGauge = 5.0f;
    private bool isMouseHold = false;
    private Vector3 distance2Player;

    private bool isFired = false;
    private float zeroSpeed = 0.1f;
    private Vector3 initPlayerPosition;

    public bool Firestate = false;


    // Start is called before the first frame update
    void Start()
    {
        distance2Player = player.transform.position - this.transform.position;
        initPlayerPosition = player.transform.position;

        
    }

    // Update is called once per frame
    void Update()
    {
        player.transform.rotation = mainCam.transform.rotation;

        if (!isFired)
        {
            if (Input.GetMouseButtonDown(0))
                isMouseHold = true;
        }


        if (isMouseHold && !Firestate)
        {
            gaugeTimer += Time.deltaTime;
            if (gaugeTimer >= maxGauge)
            {
                isMouseHold = false;
                gaugeTimer = 0.0f;
            }

            if (Input.GetMouseButtonUp(0))
            {
                StartCoroutine(Fire());

            }
        }

        cursorGaugeImage.fillAmount = gaugeTimer / maxGauge;
        

        if (isFired && player.gameObject.GetComponent<Rigidbody>().velocity.magnitude <= zeroSpeed)
            StartCoroutine(Restart());


        if (player.gameObject.transform.position.y <= -5)
            StartCoroutine(Restart());
    }

    IEnumerator TurnOnFire()
    {
        yield return new WaitForSeconds(0.1f);
        isFired = true;
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(1.0f);

        player.gameObject.GetComponent<Rigidbody>().useGravity = false;
        player.transform.position = initPlayerPosition;
        player.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        isFired = false;
        Firestate = false;

        GameObject.Find("Chicken").GetComponent<ChickenCtrl>().GenerateTarget();
        
    }

    IEnumerator Fire()
    {

        Firestate = true;
        player.gameObject.GetComponent<Rigidbody>().useGravity = true;
        player.gameObject.GetComponent<Rigidbody>().AddForce(mainCam.transform.TransformDirection(Vector3.forward) * gaugeTimer * 200.0f);
        isMouseHold = false;
        gaugeTimer = 0.0f;

        ChickenCtrl chickenctrl = GameObject.Find("Chicken").GetComponent<ChickenCtrl>();
        chickenctrl.score -= 1;

        yield return new WaitForSeconds(3.0f);
        player.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        StartCoroutine(TurnOnFire());
    }

    private void LateUpdate()
    {
        this.transform.position = player.transform.position - mainCam.transform.TransformDirection(distance2Player);
    }
}
