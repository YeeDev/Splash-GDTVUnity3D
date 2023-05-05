using UnityEngine;

namespace Splash.Core
{
    public class Tracker : MonoBehaviour
    {
        Transform player;

        private void Awake() => player = GameObject.FindGameObjectWithTag("Player").transform;

        private void LateUpdate()
        {
            Vector3 trackPosition = player.position;
            trackPosition.y = transform.position.y;
            transform.position = trackPosition;
        }
    }
}