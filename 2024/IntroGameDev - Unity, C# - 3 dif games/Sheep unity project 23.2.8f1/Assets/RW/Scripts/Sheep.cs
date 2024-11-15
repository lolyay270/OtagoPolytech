/// <remarks>
/// Author: Jenna Boyes
/// Date Created: 20th March 2024
/// Bugs: None known at this time
/// </remarks>

/// <summary>
/// The Sheep class controls:
/// - sheep constant movement
/// - sheep eating hay
/// - sheep falling off map
/// </summary>

using UnityEngine;
using UnityEngine.Events;

public class Sheep : MonoBehaviour
{
    #region local variables
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float dropDestroyTime;
    private bool dropped;
    [SerializeField] private GameObject feedBackHeart;
    private bool hit;
    #endregion

    #region event setup
    public class SheepEvent : UnityEvent<Sheep> { }

    public SheepEvent OnEatHay = new(); 
    public SheepEvent OnDrop = new();
    #endregion

    #region methods
    //sheep move towards hay machine
    private void Update()
    {
        transform.Translate(0, 0, -forwardSpeed * Time.deltaTime);
    }

    //trigger collision with edge of map and with haybale
    private void OnTriggerEnter(Collider other)
    {
        //remove sheep and hay bale when hit with hay
        if (other.tag == "DestroyHay" && !hit)
        {
            hit = true; //stop triggering twice
            OnEatHay?.Invoke(this);         // adding ? so it looks for listen first, if none doesnt make call
            Instantiate(feedBackHeart, new Vector3(transform.position.x, 2, transform.position.z), transform.rotation); //spawn heart above sheep
            SFXManager.Instance.SheepHit();
            Destroy(gameObject);
            Destroy(other.gameObject); //despawn both hay and sheep
            GameManager.Instance.SaveSheep();
        }

        //if hit edge of map
        else if (other.tag == "DropSheep" && !dropped)
        {
            dropped = true; //stop triggering twice
            GetComponent<Rigidbody>().useGravity = true; //make it physically fall
            OnDrop?.Invoke(this);
            SFXManager.Instance.SheepDrop();
            GameManager.Instance.DroppedSheep();
            Destroy(gameObject, dropDestroyTime); //despawn after time
        }
    }
    #endregion
}
