using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboUp : TileStateChangerBase
{
    [SerializeField]
    private int _comboCount = 3;

    [SerializeField]
    private ParticleSystem _effectParticle;
    [SerializeField]
    private Animator _effectAnim;

    public override void ChangeTiles(string owner)
    {
        if (owner == "beaver")
        {
            // ��� �޺� ��
        }
        else if (owner == "human")
        {
            // �߶� �޺� ��
        }

        if (_effectParticle)
            _effectParticle.Play();

        if (_effectAnim)
            _effectAnim.SetTrigger("effectOn");
    }
}
