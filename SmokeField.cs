using System.Collections.Generic;
using UnityEngine;

public class SmokeField : MonoBehaviour
{
    public GameObject ParticlePrefab;
    public Vector2 Dimensions;
    public float Interval = 0.5f;

    struct ParticleInstance {
        public SpriteRenderer sprite;
        public float targetAlpha;
        public float pulseOffset;
    }

    List<ParticleInstance> _particles;

    void Awake()
    {
        _particles = new List<ParticleInstance>();
        for (var x = -Dimensions.x/2f; x < Dimensions.x/2f; x += Interval) {
            for (var y = -Dimensions.y/2f; y < Dimensions.y/2f; y += Interval) {
                var pos = new Vector3(
                    x + Random.value*Interval*0.5f,
                    y + Random.value*Interval*0.5f,
                    0
                );
                var particle = Instantiate(ParticlePrefab, transform.position + pos, Quaternion.identity) as GameObject;
                _particles.Add(new ParticleInstance {
                    sprite = particle.GetComponentInChildren<SpriteRenderer>(),
                    targetAlpha = 1f,
                    pulseOffset = Random.value * 2 * Mathf.PI
                });
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position, Dimensions.AsVector3());
    }

    void FixedUpdate()
    {
        for (int i = 0; i < _particles.Count; ++i) {
            var p = _particles[i];
            if ((p.sprite.transform.position - transform.position).sqrMagnitude < 3f) {
                p.targetAlpha = 0f;
            } else {
                p.targetAlpha = 1f;
            }
            var lag = p.targetAlpha < 0.5f ? 0.5f : 0.02f;
            var a = p.sprite.color.a;
            a += (p.targetAlpha - a) * lag;
            p.sprite.color = new Color(0,0,0,a);
            p.sprite.transform.localScale = Vector3.one*(1 + 0.2f*Mathf.Cos(p.pulseOffset + Time.time));
        }
    }
}
