using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ProjectGraphics
{
    public class InsectEffectContoller : MonoBehaviour
    {
        [SerializeField] SpriteRenderer fire;
        [SerializeField] SpriteRenderer thunder;
        [SerializeField] TrailRenderer trail;

        private void Awake()
        {
            fire.enabled = thunder.enabled = trail.enabled = false;    
        }

        public void FireEffect(bool on)
        {
            fire.enabled = on;
        }

        public void ThunderEffect(bool on)
        {
            thunder.enabled = on;
        }

        public void TrailEffect(bool on)
        {
            if (on) trail.enabled = true;
            else
            {
                trail.Clear();
                trail.enabled = false;
            }
        }
    }
}