namespace Quantum {
  using Photon.Deterministic;
  using UnityEngine;

  /// <summary>
  /// A Unity script that creates empty input for any Quantum game.
  /// </summary>
  public class QuantumDebugInput : MonoBehaviour {

    private void OnEnable() {
      QuantumCallback.Subscribe(this, (CallbackPollInput callback) => PollInput(callback));
    }

    /// <summary>
    /// Set an empty input when polled by the simulation.
    /// </summary>
    /// <param name="callback"></param>
    public void PollInput(CallbackPollInput callback) 
    {
      Quantum.Input i = new Quantum.Input();

      float x = UnityEngine.Input.GetAxis("Horizontal");
      float y = UnityEngine.Input.GetAxis("Vertical");

      i.Direction = new FPVector2(x.ToFP(), y.ToFP());
      i.Jump = UnityEngine.Input.GetButton("Jump");

      var cam = Camera.main.transform;

      Vector3 forward = cam.forward;
      Vector3 right = cam.right;
      forward.y = 0;
      right.y = 0; 
      forward.Normalize();
      right.Normalize();

      i.CameraForward = forward.ToFPVector3();
      i.CameraRight = right.ToFPVector3();

      callback.SetInput(i, DeterministicInputFlags.Repeatable);
    }
  }
}
