using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Moba.Events;

namespace Moba.Logic
{
    public class PlaneManager : MonoBehaviour
    {
        public Meet m_Meet;

        [SerializeField]
        private float m_StartDelay = 3f;
        [SerializeField]
        private float m_EndDelay = 3f;


        private WaitForSeconds m_StartWait;
        private WaitForSeconds m_EndWait;


        public Transform Look { set; get; }

        private void Start()
        {
            m_StartWait = new WaitForSeconds(m_StartDelay);
            m_EndWait = new WaitForSeconds(m_EndDelay);

            StartCoroutine(GameLoop());
        }

        public void DoMatch(WarPb.War_Match data)
        {
            m_Meet.DoMatch(data);
            //ulong uid = 2;
            //foreach (var tm in m_Meet.m_Tanks)
            //{
            //    if (uid == tm.m_Uid)
            //    {

            //        Look = tm.m_Instance.transform;
            //        break;
            //    }
            //}
            TankManager tm = m_Meet.GetTM(1);
            if (null != tm)
                Look = tm.m_Instance.transform;
        }

        private IEnumerator GameLoop()
        {
            yield return StartCoroutine(RoundStarting());
            yield return StartCoroutine(RoundPlaying());
            yield return StartCoroutine(RoundEnding());
        }


        private IEnumerator RoundStarting()
        {
            yield return m_StartWait;

        }


        private IEnumerator RoundPlaying()
        {
            yield return null;
        }


        private IEnumerator RoundEnding()
        {
            yield return m_EndWait;
        }


        private bool OneTankLeft()
        {
            int numTanksLeft = 0;

            //for (int i = 0; i < m_Tanks.Length; i++)
            //{
            //    if (m_Tanks[i].m_Instance.activeSelf)
            //        numTanksLeft++;
            //}

            return numTanksLeft <= 1;
        }

        private void ResetAllTanks()
        {
            //for (int i = 0; i < m_Tanks.Length; i++)
            //{
            //    m_Tanks[i].Reset();
            //}
        }


        private void EnableTankControl()
        {
            //for (int i = 0; i < m_Tanks.Length; i++)
            //{
            //    m_Tanks[i].EnableControl();
            //}
        }


        private void DisableTankControl()
        {
            //for (int i = 0; i < m_Tanks.Length; i++)
            //{
            //    m_Tanks[i].DisableControl();
            //}
        }
    }
}
