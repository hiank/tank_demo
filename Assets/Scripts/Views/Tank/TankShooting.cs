using UnityEngine;
using UnityEngine.UI;

namespace Moba.Views
{
    public class TankShooting : MonoBehaviour
    {
        public ulong m_Uid = 1;              // Used to identify the different players.
        public Rigidbody m_Shell;                   // Prefab of the shell.
        public Transform m_FireTransform;           // A child of the tank where the shells are spawned.
        //public Slider m_AimSlider;                  // A child of the tank that displays the current launch force.
        public AudioSource m_ShootingAudio;         // Reference to the audio source used to play the shooting audio. NB: different to the movement audio source.
        public AudioClip m_ChargingClip;            // Audio that plays when each shot is charging up.
        public AudioClip m_FireClip;                // Audio that plays when each shot is fired.
        //public float m_MinLaunchForce = 15f;        // The force given to the shell if the fire button is not held.
        //public float m_MaxLaunchForce = 30f;        // The force given to the shell if the fire button is held for the max charge time.
        //public float m_MaxChargeTime = 0.75f;       // How long the shell can charge for before it is fired at max force.


        //point 为点击的位置
        public void DoFire(WarPb.Shoot fire)
        {
            Vector3 point = new Vector3(float.Parse(fire.Point.X), m_FireTransform.position.y, float.Parse(fire.Point.Z));
            Vector3 forward = point - m_FireTransform.position;
            Quaternion rotation = Quaternion.FromToRotation(m_FireTransform.forward, forward);

            Rigidbody shellInstance = Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation * rotation) as Rigidbody;
            shellInstance.velocity = 40 * forward.normalized;

            m_ShootingAudio.clip = m_FireClip;
            m_ShootingAudio.Play();
        }


        //private void Fire()
        //{
        //    // Set the fired flag so only Fire is only called once.
        //    //m_Fired = true;

        //    // Create an instance of the shell and store a reference to it's rigidbody.
        //    Rigidbody shellInstance =
        //        Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

        //    // Set the shell's velocity to the launch force in the fire position's forward direction.
        //    //shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward;

        //    // Change the clip to the firing clip and play it.
        //    m_ShootingAudio.clip = m_FireClip;
        //    m_ShootingAudio.Play();

        //    // Reset the launch force.  This is a precaution in case of missing button events.
        //    //m_CurrentLaunchForce = m_MinLaunchForce;
        //}
    }
}
