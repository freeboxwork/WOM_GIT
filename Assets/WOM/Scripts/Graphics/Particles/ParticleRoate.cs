using UnityEngine;

namespace ProjectGraphics
{
    public class ParticleRoate : MonoBehaviour
    {
        //��ƼŬ ȣ��� �� �޼ҵ� ������ �ּ���.
        public void SetParticleRotateDirection(float zRot)
        {
            transform.rotation = Quaternion.Euler(0, 0, zRot);
        }

        private void OnDisable()
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
