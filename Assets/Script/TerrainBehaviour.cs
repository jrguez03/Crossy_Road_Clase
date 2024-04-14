using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TerrainBehaviour : MonoBehaviour
{
    public Swipe_Controller t_SwipeController;
    public Input_Terrain t_InputTerrain;
    public PlayerBehaviour t_PlayerBehaviour;

    public float t_Offset = 100f;
    public float t_Duration = 0.25f;

    [SerializeField] GameObject t_Terrain;
    [SerializeField] GameObject t_Player;
    [SerializeField] TextMeshProUGUI t_StepsText;

    public int t_Steps = 0;
    private int t_Record = 0;

    public bool t_CanMove = true;

    public void Awake()
    {
        t_Terrain = this.gameObject;
    }

    public void Start()
    {
        t_Steps = PlayerPrefs.GetInt("Score", 0);
        t_Record = PlayerPrefs.GetInt("Record", 0);

        UpdateStepText();
    }

    public void Update()
    {
        PlayerPrefs.SetInt("Steps", t_Steps);
        PlayerPrefs.Save();

        if (t_Steps > t_Record)
        {
            t_Record = t_Steps;
            PlayerPrefs.SetInt("Record", t_Record);
            PlayerPrefs.Save();
        }

        UpdateStepText();
    }

    public void OnEnable()
    {
        t_SwipeController.OnSeMueve += MoveTarget;
    }

    public void OnDisable()
    {
        t_SwipeController.OnSeMueve -= MoveTarget;
    }

    public void MoveTarget(Vector3 t_Direction)
    {
        RaycastHit t_HitInfo = PlayerBehaviour.p_LastRay;

        Vector3 t_DirectionNormalized = t_Direction.normalized;

        if (t_PlayerBehaviour.p_CanMove && t_CanMove)
        {
            if (Physics.Raycast(t_Player.transform.position + new Vector3(0, 1f, 0), t_Direction, out t_HitInfo, 1f))
            {
                Debug.Log("Hit Something, Restricting Movement");
                if (t_HitInfo.collider.tag != "ProceduralTerrain")
                {
                    if (t_DirectionNormalized.z != 0)
                    {
                        t_DirectionNormalized.z = 0;
                    }
                }              

                Debug.DrawRay(transform.position + new Vector3(0, 1f, 0), transform.forward * t_HitInfo.distance, Color.red);
            }

            if (t_DirectionNormalized.z >= 0 && t_PlayerBehaviour.p_StepsBack == 0)
            {
                LeanTween.move(t_Terrain, t_Terrain.transform.position + new Vector3(0, 0, -t_DirectionNormalized.z), t_Duration).setEase(LeanTweenType.easeOutQuad).setOnComplete(() =>
                {
                    if(t_DirectionNormalized.z >= -3 && Mathf.Abs(t_DirectionNormalized.x) < Mathf.Abs(t_DirectionNormalized.z))
                    {
                        t_Steps += 1;
                    }
                });
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            t_PlayerBehaviour.p_CanMove = true;
        }
    }

    private void UpdateStepText()
    {
        t_StepsText.text = "Score: " + t_Steps.ToString() + "/Record: " + t_Record.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            t_CanMove = false;
        }
    }
}
