using UnityEngine;



public class WaveAnimation : MonoBehaviour
{
    [Header("Breath Animation")]
    [SerializeField] float _amplitude = 0.2f; // the height of the animation
    [SerializeField] float _frequency = 2f; // the speed of the animation

    [SerializeField] bool _poscorrect;

    private Vector3 _startPosition;

    void Start()
    {
        _startPosition = transform.localPosition;
    }

    void Update()
    {
        float y = _amplitude * Mathf.Sin(Time.time * _frequency);
        if (_poscorrect)
        {
            y = -y;
        }
        transform.localPosition = _startPosition + new Vector3(0, y, 0);
    }


}

