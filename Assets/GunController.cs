using System;
using UnityEngine;

namespace AssemblyCSharp.Assets
{
    public class GunController : MonoBehaviour
    {
        public ParticleSystem muzzleFlash;
        public AudioSource laserShot;

        public void Start()
        {
        }
        public void Update()
        {
            if (Input.GetAxis("Mouse Y") < 0)
            {
                rotateX(0.5f);
            }
            if (Input.GetAxis("Mouse Y") > 0)
            {
                rotateX(-0.5f);
            }
            if (Input.GetAxis("Mouse X") > 0)
            {
                rotateY(-0.5f);
            }
            if (Input.GetAxis("Mouse X") < 0)
            {
                rotateY(0.5f);
            }
            if (Input.GetMouseButton(0))
            {
                muzzleFlash.Play();
                laserShot.Play(0);
            }
        }

        private void rotateX(float value)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x + value, transform.eulerAngles.y, transform.eulerAngles.z);
        }

        private void rotateY(float value)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + value, transform.eulerAngles.z);
        }
    }
}
