using UnityEngine;

namespace FarmerDemo
{
    public class ItemBehaviour : MonoBehaviour
    {
        public ItemInstance ItemInstance;
        protected virtual void Awake()
        {
            if (GetComponent<Animator>() != null)
                GetComponent<Animator>().speed = 0.3f;
        }

        protected virtual void LateUpdate()
        {
            // Multiply by -100 to convert Y to int and get a good range
            int sortingOrder = Mathf.RoundToInt(transform.position.y * -100);
            GetComponent<SpriteRenderer>().sortingOrder = sortingOrder;
            foreach (SpriteRenderer childSpriteRenderer in transform.GetComponentsInChildren<SpriteRenderer>())
                childSpriteRenderer.sortingOrder = sortingOrder;
        }

        protected void AdjustAnimationSpeed(float speedMultiplier)
        {
            GetComponent<Animator>().speed *= speedMultiplier;
        }
    }
}