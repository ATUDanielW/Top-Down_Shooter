using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    [SerializeField]private GameObject _bulletPrefab;
    [SerializeField]private float _bulletSpeed;

    private bool _fireContinuously;
    private bool _fireSingle;
    private float _lastFireTime;

    [SerializeField]private Transform _gunOffset;

    [SerializeField] private float _timeBetweenShoots;


    // Update is called once per frame
    void Update()
    {       //check fire
        if (_fireContinuously || _fireSingle)
        {
            //this is to calculate how much time has passed since the last bullet (subtract last fire time from game time)
            float timeSinceLastFire = Time.time - _lastFireTime;

            //check if the time is greater or equal to the desired time between the shots
            if(timeSinceLastFire >= _timeBetweenShoots) //if it is then we can fire the bullet
            {
                FireBullet();

                _lastFireTime = Time.time;
                //every time press fire it will set it to true and once delay is finished it will set it back to false
                _fireSingle = false;
            }
            
        }
    }
    private void FireBullet()
    {
        GameObject bullet = Instantiate(_bulletPrefab, _gunOffset.position, transform.rotation);
        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();

        rigidbody.linearVelocity = _bulletSpeed * transform.up;
    }

    private void OnAttack(InputValue inputValue)
    {
        _fireContinuously = inputValue.isPressed;

        //check if button is pressed if so set it to true
        if(inputValue.isPressed)
        {
            _fireSingle = true;
        }
    }
}
