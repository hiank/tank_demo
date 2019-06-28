using System;
using UnityEngine;

namespace Moba.Logic
{
    [Serializable]
    public class TankManager
    {
        public Color m_PlayerColor;
        public Transform m_SpawnPoint;
        [HideInInspector] public ulong m_Uid;
        [HideInInspector] public string m_ColoredPlayerText;
        [HideInInspector] public GameObject m_Instance;
        [HideInInspector] public int m_Wins;


        private Views.TankMovement m_Movement;
        private Views.TankShooting m_Shooting;
        private GameObject m_CanvasGameObject;

        private Vector3 moveForward;
        public Vector3 MoveForward
        {
            set
            {
                m_Movement.DoTurn(value);
                moveForward = value;
            }
        }         //NOTE: 移动forward

        public WarPb.Shoot FireInfo { set; get; }       //NOTE: 射击信息
        //public Vector3 FirePoint { set; get; }      //NOTE: 射击位置，下一次Do 的时候处理

        public void Setup()
        {
            m_Movement = m_Instance.GetComponent<Views.TankMovement>();
            m_Shooting = m_Instance.GetComponent<Views.TankShooting>();
            m_CanvasGameObject = m_Instance.GetComponentInChildren<Canvas>().gameObject;

            m_Movement.m_Uid = m_Uid;
            m_Shooting.m_Uid = m_Uid;

            moveForward = Vector3.zero;
            FireInfo = null;


            m_ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(m_PlayerColor) + ">PLAYER " + m_Uid + "</color>";

            MeshRenderer[] renderers = m_Instance.GetComponentsInChildren<MeshRenderer>();

            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].material.color = m_PlayerColor;
            }

        }

        public void Do(float dt)
        {
            if (!moveForward.Equals(Vector3.zero))
            {
                m_Movement.DoMove(dt, moveForward);
            }
            if (FireInfo != null)
            {
                m_Shooting.DoFire(FireInfo);
                FireInfo = null;
            }
        }


        public void DisableControl()
        {
            m_Movement.enabled = false;
            m_Shooting.enabled = false;

            m_CanvasGameObject.SetActive(false);
        }


        public void EnableControl()
        {
            m_Movement.enabled = true;
            m_Shooting.enabled = true;

            m_CanvasGameObject.SetActive(true);
        }


        public void Reset()
        {
            m_Instance.transform.position = m_SpawnPoint.position;
            m_Instance.transform.rotation = m_SpawnPoint.rotation;

            m_Instance.SetActive(false);
            m_Instance.SetActive(true);
        }
    }

}
