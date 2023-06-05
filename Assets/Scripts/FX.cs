using UnityEngine;

public class FX : MonoBehaviour
{
    [SerializeField] private ParticleSystem _spotExplosionFX;
    private ParticleSystem.MainModule _spotExplosionFXMainModule;
    public static FX Instance;

    private void Awake() =>
        Instance = this;

    private void Start()
    {
        _spotExplosionFXMainModule = _spotExplosionFX.main;
    }

    public void PlayGoalExplosionFX(Vector3 position, Color color)
    {
        _spotExplosionFXMainModule.startColor = new ParticleSystem.MinMaxGradient(color);
        _spotExplosionFX.transform.position = position;
        _spotExplosionFX.Play();
    }
}