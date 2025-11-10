using Quantum;
using UnityEngine;

public class AnimationView : QuantumEntityViewComponent<BaseCharacterViewContext>
{
	private Animator _animator;

	public override void OnInitialize()
	{
		_animator = GetComponentInChildren<Animator>();
	}

	public override void OnUpdateView()
	{
		var kcc = PredictedFrame.Get<CharacterController3D>(EntityRef);
		var push = PredictedFrame.Get<PlayerPush>(EntityRef);

		_animator.SetFloat("Speed", kcc.Velocity.XOZ.Magnitude.AsFloat);
		_animator.SetBool("IsGround", kcc.Grounded);


		if (push.IsPush)
			_animator.SetTrigger("IsPush");
	}
}
