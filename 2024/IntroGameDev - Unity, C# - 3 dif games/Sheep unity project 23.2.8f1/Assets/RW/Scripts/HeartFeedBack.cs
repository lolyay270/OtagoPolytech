/// <remarks>
/// Author: Jenna Boyes
/// Date Created: 20th March 2024
/// Bugs: None known at this time
/// </remarks>

/// <summary>
/// The HeartFeedBack class controls:
/// - animate the heart
/// - despawn heart after animated
/// </summary>

using DG.Tweening;
using UnityEngine;

public class HeartFeedBack : MonoBehaviour
{
    private float timeAnimated = 6f;

    //method run on spawning to animate then despawn heart
    private void Start()
    {
        Sequence movementSequence = DOTween.Sequence();

        movementSequence.Append(transform.DOMoveY(timeAnimated, 2)) //move upwards
            .Join(transform.DORotate(new Vector3(0, 180, 0), timeAnimated / 3, RotateMode.LocalAxisAdd)) //rotate horizonally
            .Join(transform.DOScale(0, timeAnimated / 2)) //shrink to 0
            .AppendCallback(() => Destroy(gameObject)); //despawn after animated
    }
}
