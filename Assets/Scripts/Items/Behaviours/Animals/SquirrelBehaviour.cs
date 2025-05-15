using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmerDemo
{
    internal class SquirrelBehaviour : AnimalBehaviour
    {
        public float MovementSpeed = 1f;
        public float PauseTimeBetweenMoves = 5f;
        public Vector2Int _targetPosition;

        protected void Start()
        {
            StartCoroutine(MoveAround());
            AdjustAnimationSpeed(7);
        }
        protected IEnumerator MoveAround()
        {
            while (true)
            {
                StartIdleAnimation();
                yield return new WaitForSeconds(Random.Range(1, PauseTimeBetweenMoves));

                SetRandomTargetPosition();
                StartTravelingAnimation();
                yield return new WaitForSeconds(0.2f);

                while (NotYetArrivedAtTargetPosition())
                {
                    yield return new WaitForSeconds(0.01f);
                    MoveOneStepTowardsTargetPosition();
                }
            }
        }

        protected void StartIdleAnimation()
        {
            GetComponent<Animator>().SetBool("IsTraveling", false);
        }

        protected void StartTravelingAnimation()
        {
            GetComponent<Animator>().SetBool("IsTraveling", true);
        }

        public void SetRandomTargetPosition()
        {
            int attempts = 0;
            while (attempts < 10)
            {
                attempts++;
                int randomX = ItemInstance.BottomLeft.x + Random.Range(-1, 2);
                int randomY = ItemInstance.BottomLeft.y + Random.Range(-1, 2);
                Vector2Int proposedLocation = new Vector2Int(randomX, randomY);
                if (!GridManagerScript.Instance.IsOccupied(proposedLocation))
                {
                    _targetPosition = new Vector2Int(randomX, randomY);
                    break;
                }
            }
        }

        private bool NotYetArrivedAtTargetPosition()
        {
            return (Vector2)transform.position != _targetPosition;
        }

        private void MoveOneStepTowardsTargetPosition()
        {
            float movementProgress = Time.deltaTime * MovementSpeed;
            transform.position = Vector2.MoveTowards(transform.position, _targetPosition, movementProgress);
            int currentX = (int)Mathf.Round(transform.position.x);
            int currentY = (int)Mathf.Round(transform.position.y);
            ItemInstance.OccupiedTiles.Clear();
            ItemInstance.OccupiedTiles.Add(new Vector2Int(currentX, currentY));
        }
    }
}
