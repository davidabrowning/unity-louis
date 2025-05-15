using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

namespace FarmerDemo
{
    [System.Serializable]
    public class PlayerScript : MonoBehaviourSingletonBase<PlayerScript>
    {
        public List<ResourceAmount> ResourceInventory = new();
        public List<GameObject> ActiveElectricityProducers = new();
        public float MoveSpeed = 5f;
        public bool HasBasket = false;
        public bool HasPickaxe = false;
        public bool ElectricityIsOn { get { return ActiveElectricityProducers.Count > 0; } }
        public List<IElectricityStatusObserver> ElectricityStatusObservers = new();
        public GameObject BasketVisual;
        public GameObject BasketWithFewBerriesVisual;
        public GameObject BasketWithBerriesVisual;
        public GameObject PickaxeVisual;
        public AudioSource AudioSource;
        public AudioClip ShortSuccessSound;
        public AudioClip MiningSound;
        public AudioClip OuchSound;
        public AudioClip TravelingWhirringSound;

        private Rigidbody2D rb;
        private Vector2 movement;
        private SpriteRenderer _spriteRenderer;
        private bool _isMoving = false;
        private bool _wasMoving = false;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            BasketVisual.SetActive(false);
            BasketWithFewBerriesVisual.SetActive(false);
            BasketWithBerriesVisual.SetActive(false);

            Animator _animator = GetComponent<Animator>();
            if (_animator != null)
                _animator.speed = 0.2f;

            // Temporary settings for testing/development
            AddToInventory(ItemType.Twig, 100);
            AddToInventory(ItemType.Berry, 1000);
            AddToInventory(ItemType.Circuit, 100);
            AddToInventory(ItemType.Iron, 100);
            AddToInventory(ItemType.Stone, 100);
            AddToInventory(ItemType.Fish, 1000);
            AddToInventory(ItemType.Seed, 1000);
        }

        void Update()
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");

            movement = new Vector2(moveX, moveY);
            if (movement != Vector2.zero)
                _isMoving = true;
            else
                _isMoving = false;

            if (_isMoving && !_wasMoving)
            {
                AudioSource.Stop();
                AudioSource.PlayOneShot(TravelingWhirringSound, 0.3f);
            }
            
            if (!_isMoving && _wasMoving)
            {
                AudioSource.Stop();
                AudioSource.PlayOneShot(TravelingWhirringSound, 0.2f);
            }

            _wasMoving = _isMoving;

            if (HasBasket)
            {
                if (AmountInInventory(ItemType.Berry) < 5)
                {
                    BasketVisual.SetActive(true);
                    BasketWithFewBerriesVisual.SetActive(false);
                    BasketWithBerriesVisual.SetActive(false);
                }
                if (AmountInInventory(ItemType.Berry) > 5)
                {
                    BasketVisual.SetActive(false);
                    BasketWithFewBerriesVisual.SetActive(true);
                    BasketWithBerriesVisual.SetActive(false);
                }
                if (AmountInInventory(ItemType.Berry) > 10)
                {
                    BasketVisual.SetActive(false);
                    BasketWithFewBerriesVisual.SetActive(false);
                    BasketWithBerriesVisual.SetActive(true);
                }
            }

            PickaxeVisual.SetActive(HasPickaxe);
        }

        void FixedUpdate()
        {
            rb.MovePosition(rb.position + movement * MoveSpeed * Time.fixedDeltaTime);
        }

        protected void LateUpdate()
        {
            // Multiply by -100 to convert Y to int and get a good range
            int sortingOrder = Mathf.RoundToInt(transform.position.y * -100);
            _spriteRenderer.sortingOrder = sortingOrder;
            foreach (SpriteRenderer childSpriteRenderer in transform.GetComponentsInChildren<SpriteRenderer>())
                childSpriteRenderer.sortingOrder = sortingOrder;
        }

        public Vector2Int? LocationInt()
        {
            if (rb == null)
                return null;
            return new Vector2Int((int)Mathf.Round(rb.position.x), (int)Mathf.Round(rb.position.y));
        }

        public void SetHasBasket(bool hasBasket)
        {
            HasBasket = hasBasket;
        }

        public void SetHasPickaxe(bool hasPickaxe)
        {
            HasPickaxe = hasPickaxe;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<IManualHarvestable>() != null)
                PlayerScript.Instance.AddToInventory(collision.gameObject.GetComponent<IManualHarvestable>().ManualHarvest());
        }

        public void AddToInventory(ItemType type, int amount)
        {
            if (ResourceInventory.Where(r => r.ItemType == type).Any())
                ResourceInventory.Where(r => r.ItemType == type).First().Amount += amount;
            else
                ResourceInventory.Add(new ResourceAmount(type, amount));
        }

        public void AddToInventory(ResourceAmount resourceAmount)
        {
            AddToInventory(resourceAmount.ItemType, resourceAmount.Amount);
        }

        public void AddToInventory(List<ResourceAmount> resourceAmounts)
        {
            foreach (ResourceAmount resourceAmount in resourceAmounts)
            {
                AddToInventory(resourceAmount.ItemType, resourceAmount.Amount);
            }
        }

        public void RemoveFromInventory(ResourceAmount resourceAmount)
        {
            AddToInventory(resourceAmount.ItemType, resourceAmount.Amount * -1);
        }

        public void RemoveFromInventory(List<ResourceAmount> resourceAmounts)
        {
            foreach (ResourceAmount resourceAmount in resourceAmounts)
            {
                RemoveFromInventory(resourceAmount);
            }
        }

        public int AmountInInventory(ItemType type)
        {
            return ResourceInventory.Where(r => r.ItemType == type).Select(r => r.Amount).Sum();
        }

        public bool HasInInventory(ResourceAmount resourceAmount)
        {
            if (resourceAmount == null) 
                return false;
            return ResourceInventory.Where(r => r.ItemType == resourceAmount.ItemType).Select(r => r.Amount).Sum() >= resourceAmount.Amount;
        }

        public bool HasInInventory(List<ResourceAmount> resourceAmounts)
        {
            foreach (ResourceAmount resourceAmount in resourceAmounts)
                if (!HasInInventory(resourceAmount))
                    return false;
            return true;
        }

        public string GetInventoryAsString()
        {
            string output = "";
            foreach (ResourceAmount resourceAmount in ResourceInventory)
                output += resourceAmount.ToString();
            return output;
        }

        public void AddActivePowerProducer(GameObject activePowerProducer)
        {
            bool previousPowerStatus = ElectricityIsOn;
            ActiveElectricityProducers.Add(activePowerProducer);
            if (previousPowerStatus !=  ElectricityIsOn)
                foreach(IElectricityStatusObserver powerStatusObserver in ElectricityStatusObservers)
                    powerStatusObserver.HandlePowerStatusUpdate(ElectricityIsOn);
        }

        public void RemoveActivePowerProducer(GameObject powerProducer)
        {
            bool previousPowerStatus = ElectricityIsOn;
            ActiveElectricityProducers.Remove(powerProducer);
            if (previousPowerStatus != ElectricityIsOn)
                foreach (IElectricityStatusObserver powerStatusObserver in ElectricityStatusObservers)
                    powerStatusObserver.HandlePowerStatusUpdate(ElectricityIsOn);
        }

        public void PlayShortSuccessSound()
        {
            AudioSource.PlayOneShot(ShortSuccessSound);
        }

        public void PlayMiningSound()
        {
            AudioSource.PlayOneShot(MiningSound);
        }

        public void PlayOuchSound()
        {
            AudioSource.PlayOneShot(OuchSound);
        }

    }
}