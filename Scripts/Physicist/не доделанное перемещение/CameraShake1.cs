using UnityEngine;

public class CameraShake1 : MonoBehaviour
{
    [Header("Shake")]
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private AnimationCurve _perlinNoiseAmplitudeCurve;
    [SerializeField, Min(0)] private float _perlinNoiseTimeScale;

    [Header("Recoil")]
    [SerializeField, Min(0)] private float _tension;
    [SerializeField, Min(0)] private float _damping;

    private Vector3 _shakeAngle = new Vector3();

    private Vector3 _recoilAngle = new Vector3();
    private Vector3 _recoilVelocity = new Vector3();

    private float _time;
    private float _amplitude;
    private float _duration;
    private float _shakeTimer;

    private void Update()
    {
        UpdateRecoil();
        UpdateShake();

        _cameraTransform.localEulerAngles = _shakeAngle + _recoilAngle;
    }

    public void MakeRecoil(float impulse)
    {
        _recoilVelocity += -Vector3.right * Random.Range(impulse * 0.5f, impulse) + Vector3.up * Random.Range(-impulse, impulse) / 4f;
    }

    public void MakeShake(float amplitude, float duration)
    {
        _amplitude = amplitude;
        _duration = Mathf.Max(0.5f, duration);  //  выберается максимальное значение 
        _shakeTimer = 1;
    }

    private void UpdateRecoil()
    {
        _recoilAngle += Time.deltaTime * _recoilVelocity;
        _recoilVelocity += _recoilAngle * Time.deltaTime * -_tension;
        _recoilVelocity = Vector3.Lerp(_recoilVelocity, Vector3.zero, _damping * Time.deltaTime);
    }

    private void UpdateShake()
    {
        if (_shakeTimer > 0)
            _shakeTimer -= Time.deltaTime / _duration;

        _time = Time.time * _perlinNoiseTimeScale;
        _shakeAngle.x = Mathf.PerlinNoise(_time, 0);
        _shakeAngle.y = Mathf.PerlinNoise(0, _time);
        _shakeAngle.z = Mathf.PerlinNoise(_time, _time);

        _shakeAngle *= _amplitude;
        _shakeAngle *= _perlinNoiseAmplitudeCurve.Evaluate(Mathf.Clamp01(1 - _shakeTimer)); // Clamp01 нужен чтобы значение не ушло в минус, и минусуем от 1 потому-что кривая начинается с 1
    }
}