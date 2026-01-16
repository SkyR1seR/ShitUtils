using UnityEngine;
using UnityEngine.UI;

namespace ShitUtils
{
    [RequireComponent(typeof(RectTransform))]
    public class UIRefresher : MonoBehaviour
    {
        private void OnEnable()
        {
            Refresh();
        }

        public void Refresh()
        {
            DelayExtension.FrameDelay(Rebuild, 1);
        }

        private void Rebuild()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
        }
    }
}